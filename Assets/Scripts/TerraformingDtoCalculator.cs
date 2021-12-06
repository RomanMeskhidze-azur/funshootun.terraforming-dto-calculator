﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Common.Utils.Serialization;
using Random = UnityEngine.Random;

namespace TerraformingDtoCalculator
{
    public class TerraformingDtoCalculator
    {
        private const int OutputBufferSize = 2048000;
        
        private readonly HashSet<int> _fromInitialChunkIds;
        private FromServerDto _initialFromServerDto;
        private FromServerDto _differentFromServerDto;
        
        private byte[] _genOutputBuf;

        public TerraformingDtoCalculator()
        {
            _fromInitialChunkIds = new HashSet<int>();
        }

        public long FillInitialFromServerDto()
        {
            _initialFromServerDto = new FromServerDto();
            
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            for (var i = 0; i < TerraformingDtoCalculatorConstants.ChunksMaxCount; i++)
            {
                var chunkDto = new ChunkDTO();

                for (var j = 0; j < TerraformingDtoCalculatorConstants.VerticesMaxCount; j++)
                {
                    chunkDto.Vertices.Add(0);
                }

                for (var j = 0; j < TerraformingDtoCalculatorConstants.IndicesMaxCount; j++)
                {
                    chunkDto.Indices.Add(0);
                }

                for (var j = 0; j < TerraformingDtoCalculatorConstants.VertListMaxCount; j++)
                {
                    chunkDto.VertListX.Add(0f);
                }
                
                for (var j = 0; j < TerraformingDtoCalculatorConstants.VertListMaxCount; j++)
                {
                    chunkDto.VertListY.Add(0f);
                }
                
                for (var j = 0; j < TerraformingDtoCalculatorConstants.VertListMaxCount; j++)
                {
                    chunkDto.VertListZ.Add(0f);
                }

                chunkDto.Generation = 1;
                
                _initialFromServerDto.ChankDtos.Add(chunkDto);
            }
            
            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }

        public void GenerateDifferentChunkIds()
        {
            _fromInitialChunkIds.Clear();

            var iterationsCount = (int) (TerraformingDtoCalculatorConstants.ChunksMaxCount * TerraformingDtoCalculatorConstants.GenerateDiffChunksPercents);
            for (var i = 0; i < iterationsCount; i++)
            {
                var chunkIndex = Random.Range(0, TerraformingDtoCalculatorConstants.ChunksMaxCount + 1);
                _fromInitialChunkIds.Add(chunkIndex);
            }
        }

        public long FillDifferentFromServerDto()
        {
            _differentFromServerDto = new FromServerDto();
            
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            for (var i = 0; i < TerraformingDtoCalculatorConstants.ChunksMaxCount; i++)
            {
                if (!_fromInitialChunkIds.Contains(i))
                {
                    _differentFromServerDto.ChankDtos.Add(_initialFromServerDto.ChankDtos[i]);
                    continue;
                }
                
                var chunkDto = new ChunkDTO();

                for (var j = 0; j < TerraformingDtoCalculatorConstants.VerticesMaxCount; j++)
                {
                    chunkDto.Vertices.Add(1);
                }

                for (var j = 0; j < TerraformingDtoCalculatorConstants.IndicesMaxCount; j++)
                {
                    chunkDto.Indices.Add(1);
                }

                for (var j = 0; j < TerraformingDtoCalculatorConstants.VertListMaxCount; j++)
                {
                    chunkDto.VertListX.Add(1f);
                }
                
                for (var j = 0; j < TerraformingDtoCalculatorConstants.VertListMaxCount; j++)
                {
                    chunkDto.VertListY.Add(1f);
                }
                
                for (var j = 0; j < TerraformingDtoCalculatorConstants.VertListMaxCount; j++)
                {
                    chunkDto.VertListZ.Add(1f);
                }
                
                chunkDto.Generation = 2;
                
                _differentFromServerDto.ChankDtos.Add(chunkDto);
            }
            
            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }

        public (long, long, long) SerDeserDiff()
        {
            var clientSerPacker = new StrictBitsPacker(new byte[8]);
            _genOutputBuf = new byte[OutputBufferSize];
            var ms = new MemoryStream(_genOutputBuf, 0, _genOutputBuf.Length, true, true);
            clientSerPacker.SetStream(ms);
            
            var serStopwatch = new Stopwatch();
            serStopwatch.Start();
            
            _differentFromServerDto.SerDiff(clientSerPacker, _initialFromServerDto);
            
            serStopwatch.Stop();
            
            clientSerPacker.Flush();
            var generatedArray = new ArraySegment<byte>(_genOutputBuf, 0, (int) ms.Position);
            
            var clientDeserPacker = new StrictBitsPacker(new byte[8]);
            var stream = new MemoryStream(generatedArray.Array, generatedArray.Offset, generatedArray.Count);
            clientDeserPacker.SetStream(stream);

            var deserStopWatch = new Stopwatch();
            deserStopWatch.Start();

            var deseredDto = new FromServerDto();
            deseredDto.DeserDiff(clientDeserPacker, _initialFromServerDto);
            
            deserStopWatch.Stop();

            return (serStopwatch.ElapsedMilliseconds, ms.Position, deserStopWatch.ElapsedMilliseconds);
        }
    }
}