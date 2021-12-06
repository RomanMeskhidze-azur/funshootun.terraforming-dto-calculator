using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Common.Utils.Serialization;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace TerraformingDtoCalculator
{
    public class TerraformingDtoCalculator
    {
        private readonly HashSet<int> _fromInitialChunkIds;
        private FromServerDto _initialFromServerDto;
        private FromServerDto _differentFromServerDto;
        
        private readonly ISerializer _writer = new StrictBitsPacker(new byte[8]);
        
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
                
                _initialFromServerDto.ChankDtos.Add(chunkDto);
            }
            
            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }

        public (long, long) SerDiff()
        {
            _writer.Clear();
            var ms = new MemoryStream();
            _writer.SetStream(ms);
            
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            
            _differentFromServerDto.SerDiff(_writer, _initialFromServerDto);
            
            stopwatch.Stop();
            return (stopwatch.ElapsedMilliseconds, ms.Position);
        }
    }
}