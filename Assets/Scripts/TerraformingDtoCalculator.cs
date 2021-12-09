using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Common.Utils.Serialization;
using TerraformingDtoCalculator.OcTree;
using UnityEngine.Profiling;
using Random = UnityEngine.Random;
using Debug = UnityEngine.Debug;

namespace TerraformingDtoCalculator
{
    public class TerraformingDtoCalculator
    {
        private const int MaxX = 3;
        private const int MaxY = 3;
        private const int MaxZ = 3;
        
        private const int OutputBufferSize = 2560000;
        
        private readonly HashSet<int> _newChunkIds;
        private FromServerDto _initialFromServerDto;
        private FromServerDto _differentFromServerDto;

        private int _currentX;
        private int _currentY;
        private int _currentZ;

        private OcTree<ChunkDTO> _ocTree;

        public TerraformingDtoCalculator()
        {
            _newChunkIds = new HashSet<int>();
        }

        public long FillInitialFromServerDto()
        {
            _initialFromServerDto = new FromServerDto();
            
            _currentX = 0;
            _currentY = 0;
            _currentZ = 0;
            
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            for (var i = 0; i < TerraformingDtoCalculatorConstants.ChunksMaxCount; i++)
            {
                var chunkDto = new ChunkDTO();

                for (var j = 0; j < TerraformingDtoCalculatorConstants.VerticesMaxCount; j++)
                {
                    chunkDto.Vertices[j] = 0;
                }

                for (var j = 0; j < TerraformingDtoCalculatorConstants.IndicesMaxCount; j++)
                {
                    chunkDto.Indices.Add(0);
                }

                for (var j = 0; j < TerraformingDtoCalculatorConstants.VertListMaxCount; j++)
                {
                    chunkDto.VertListX.Add(0);
                }
                
                for (var j = 0; j < TerraformingDtoCalculatorConstants.VertListMaxCount; j++)
                {
                    chunkDto.VertListY.Add(0);
                }
                
                for (var j = 0; j < TerraformingDtoCalculatorConstants.VertListMaxCount; j++)
                {
                    chunkDto.VertListZ.Add(0);
                }

                chunkDto.Generation = 1;
                SetNextCoordinates(chunkDto);
                
                _initialFromServerDto.ChankDtos[i] = chunkDto;
            }
            
            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }

        public void GenerateDifferentChunkIds()
        {
            _newChunkIds.Clear();

            var iterationsCount = TerraformingDtoCalculatorConstants.GenerateDiffChunksCount;
            for (var i = 0; i < iterationsCount; i++)
            {
                var chunkIndex = Random.Range(0, TerraformingDtoCalculatorConstants.ChunksMaxCount + 1);
                _newChunkIds.Add(chunkIndex);
            }
        }

        public long FillDifferentFromServerDto()
        {
            _differentFromServerDto = new FromServerDto();
            
            var cube = new Octangle<ChunkDTO>(OctangleType.Root, 2, 2, 2, 4, 4, 4);
            _ocTree = new OcTree<ChunkDTO>(cube);
            _ocTree.Initialization(2);

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            for (var i = 0; i < TerraformingDtoCalculatorConstants.ChunksMaxCount; i++)
            {
                if (!_newChunkIds.Contains(i))
                {
                    _differentFromServerDto.ChankDtos[i] = _initialFromServerDto.ChankDtos[i];
                    continue;
                }

                var chunkDto = new ChunkDTO
                {
                    X = _initialFromServerDto.ChankDtos[i].X,
                    Y = _initialFromServerDto.ChankDtos[i].Y,
                    Z = _initialFromServerDto.ChankDtos[i].Z
                };
                
                for (var j = 0; j < TerraformingDtoCalculatorConstants.VerticesMaxCount; j++)
                {
                    chunkDto.Vertices[j] = 1;
                }

                for (var j = 0; j < TerraformingDtoCalculatorConstants.IndicesMaxCount; j++)
                {
                    chunkDto.Indices.Add(1);
                }

                for (var j = 0; j < TerraformingDtoCalculatorConstants.VertListMaxCount; j++)
                {
                    chunkDto.VertListX.Add(1);
                }
                
                for (var j = 0; j < TerraformingDtoCalculatorConstants.VertListMaxCount; j++)
                {
                    chunkDto.VertListY.Add(1);
                }
                
                for (var j = 0; j < TerraformingDtoCalculatorConstants.VertListMaxCount; j++)
                {
                    chunkDto.VertListZ.Add(1);
                }
                
                chunkDto.Generation = 2;
                
                _differentFromServerDto.ChankDtos[i] = chunkDto;
                _ocTree.InsertNode(chunkDto, _initialFromServerDto.ChankDtos[i]);
            }
            
            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }

        public (long, long, long) SerDeserDiff()
        {
            var clientSerPacker = new StrictBitsPacker(new byte[8]);
            var genOutputBuf = new byte[OutputBufferSize];
            var ms = new MemoryStream(genOutputBuf, 0, genOutputBuf.Length, true, true);
            clientSerPacker.SetStream(ms);
            
            var serStopwatch = new Stopwatch();
            serStopwatch.Start();
            
            Profiler.BeginSample("SerDiff");
            
            _differentFromServerDto.SerDiff(clientSerPacker, _initialFromServerDto);
            
            Profiler.EndSample();
            
            serStopwatch.Stop();

            clientSerPacker.Flush();
            var generatedArray = new ArraySegment<byte>(genOutputBuf, 0, (int) ms.Position);
            
            var clientDeserPacker = new StrictBitsPacker(new byte[8]);
            var stream = new MemoryStream(generatedArray.Array, generatedArray.Offset, generatedArray.Count);
            clientDeserPacker.SetStream(stream);

            var deserStopWatch = new Stopwatch();
            deserStopWatch.Start();

            Profiler.BeginSample("DeserDiff");
            
            var deseredDto = new FromServerDto();
            deseredDto.DeserDiff(clientDeserPacker, _initialFromServerDto);
            
            Profiler.EndSample();
            
            deserStopWatch.Stop();
            
            Debug.Log($"ser and deser equals {_differentFromServerDto.IsEqual(deseredDto)}");

            return (serStopwatch.ElapsedMilliseconds, ms.Position, deserStopWatch.ElapsedMilliseconds);
        }

        public (long, long, long) NotGeneratedSerDeserDiff()
        {
            var clientSerPacker = new StrictBitsPacker(new byte[8]);
            var genOutputBuf = new byte[OutputBufferSize];
            var ms = new MemoryStream(genOutputBuf, 0, genOutputBuf.Length, true, true);
            clientSerPacker.SetStream(ms);
            
            var serStopwatch = new Stopwatch();
            serStopwatch.Start();
            
            Profiler.BeginSample("NotGeneratedSerDiff");
            
            _differentFromServerDto.NotGeneratedSerDiff(clientSerPacker, _ocTree);
            
            Profiler.EndSample();
            
            serStopwatch.Stop();
            
            clientSerPacker.Flush();
            var generatedArray = new ArraySegment<byte>(genOutputBuf, 0, (int) ms.Position);
            
            var clientDeserPacker = new StrictBitsPacker(new byte[8]);
            var stream = new MemoryStream(generatedArray.Array, generatedArray.Offset, generatedArray.Count);
            clientDeserPacker.SetStream(stream);
            
            var deserStopWatch = new Stopwatch();
            deserStopWatch.Start();

            Profiler.BeginSample("NotGeneratedDeserDiff");
            
            var deseredDto = new FromServerDto();
            deseredDto.NotGeneratedDeserDiff(clientDeserPacker, _initialFromServerDto, _ocTree);
            
            Profiler.EndSample();
            
            deserStopWatch.Stop();
            
            Debug.Log($"not generated ser and deser equals {_differentFromServerDto.IsEqual(deseredDto)}");

            return (serStopwatch.ElapsedMilliseconds, ms.Position, deserStopWatch.ElapsedMilliseconds);
        }

        private void SetNextCoordinates(ChunkDTO chunk)
        {
            chunk.X = _currentX;
            chunk.Y = _currentY;
            chunk.Z = _currentZ;
            
            if (_currentZ != MaxZ)
            {
                _currentZ++;
                return;
            }

            _currentZ = 0;

            if (_currentY != MaxY)
            {
                _currentY++;
                return;
            }

            _currentY = 0;
            
            _currentX++;
        }
    }
}