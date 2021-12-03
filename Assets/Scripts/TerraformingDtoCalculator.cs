using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace TerraformingDtoCalculator
{
    public class TerraformingDtoCalculator
    {
        private readonly HashSet<int> _differentChunkIds;
        private readonly FromServerDto _initialFromServerDto;
        
        public TerraformingDtoCalculator()
        {
            _initialFromServerDto = new FromServerDto();
            _differentChunkIds = new HashSet<int>();
        }

        public long InitialFillDto()
        {
            _initialFromServerDto.ChankDtos.Clear();
            
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
                
                _initialFromServerDto.ChankDtos.Add(chunkDto);
            }
            
            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }

        public void GenerateDifferentChankIds()
        {
            _differentChunkIds.Clear();
            
            
        }
    }
}