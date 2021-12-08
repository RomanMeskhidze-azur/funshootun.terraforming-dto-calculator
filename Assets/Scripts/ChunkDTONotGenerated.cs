using System;
using TerraformingDtoCalculator.OcTree;

namespace TerraformingDtoCalculator
{
    public partial class ChunkDTO : IOcTreeNode
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
    }
}