namespace TerraformingDtoCalculator.QuadTree
{
    public class Cube
    {
        // Координаты центра куба
        public int X { get; }
        public int Y { get; }
        public int Z { get; }
        public int Width { get; }
        public int Height { get; }
        public int Depth { get; }
        

        public Cube(int x, int y, int z, int width, int height, int depth)
        {
            X = x;
            Y = y;
            Z = z;
            
            Width = width;
            Height = height;
            Depth = depth;
        }

        public bool Contains(ICubeTreeNode node)
        {
            return node.X <= X + Width / 2 &&
                   node.X >= X - Width / 2 &&
                   node.Y <= Y + Height / 2 &&
                   node.Y >= Y - Height / 2 &&
                   node.Z <= Z + Depth / 2 &&
                   node.Z >= Z - Depth / 2;
        }
    }
}