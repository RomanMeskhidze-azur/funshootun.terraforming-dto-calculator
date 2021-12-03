using System.Collections.Generic;
using UnityEngine;

namespace TerraformingDtoCalculator
{
    public class ChankDTO
    {
        public List<short> Vertices = new List<short>(TerraformingDtoCalculatorConstants.VerticesMaxCount);
        public List<int> Indices = new List<int>(TerraformingDtoCalculatorConstants.IndicesMaxCount);
        public List<Vector3> VertList = new List<Vector3>(TerraformingDtoCalculatorConstants.VertListMaxCount);
    }
}
