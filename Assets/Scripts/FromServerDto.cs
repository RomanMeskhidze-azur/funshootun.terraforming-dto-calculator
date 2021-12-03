using System.Collections.Generic;

namespace TerraformingDtoCalculator
{
    public class FromServerDto
    {
        public List<ChankDTO> ChankDtos = new List<ChankDTO>(TerraformingDtoCalculatorConstants.ChunksMaxCount);
    }
}