namespace TerraformingDtoCalculator.OcTree
{
    public class Octangle
    {
        public OctangleType Type { get; }
        public int X { get; }
        public int Y { get; }
        public int Z { get; }
        public int Width { get; }
        public int Height { get; }
        public int Depth { get; }
        

        public Octangle(OctangleType type, int x, int y, int z, int width, int height, int depth)
        {
            Type = type;
            
            X = x;
            Y = y;
            Z = z;
            
            Width = width;
            Height = height;
            Depth = depth;
        }

        public bool Contains(IOcTreeNode node)
        {
            return node.X + 1 == X &&
                   node.Y + 1 == Y &&
                   node.Z + 1 == Z;
        }
    }
}