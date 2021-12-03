using System.Diagnostics;
using UnityEngine;

namespace TerraformingDtoCalculator
{
    public class TerraformingDtoCalculator
    {
        private readonly FromServerDto _fromServerDto;
        
        public TerraformingDtoCalculator()
        {
            _fromServerDto = new FromServerDto();
        }

        public long InitialFillDto()
        {
            _fromServerDto.ChankDtos.Clear();
            
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            for (var i = 0; i < TerraformingDtoCalculatorConstants.ChunksMaxCount; i++)
            {
                var chunkDto = new ChankDTO();

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
                    chunkDto.VertList.Add(Vector3.zero);
                }
                
                _fromServerDto.ChankDtos.Add(chunkDto);
            }
            
            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }
    }
}