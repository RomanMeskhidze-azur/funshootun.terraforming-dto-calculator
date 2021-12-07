using System;
using TerraformingDtoCalculator.QuadTree;

namespace TerraformingDtoCalculator
{
    public partial class ChunkDTO : ICubeTreeNode
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
    }
}