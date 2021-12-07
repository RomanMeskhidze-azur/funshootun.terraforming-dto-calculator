namespace TerraformingDtoCalculator.QuadTree
{
    public class Cube
    {
        // Координаты центра квадрата
        public int X { get; }
        public int Y { get; }
        public int Z { get; }
        public int HalfWidth { get; }
        public int HalfHeight { get; }
        public int HalfDepth { get; }
        

        public Cube(int x, int y, int z, int width, int height, int depth)
        {
            X = x;
            Y = y;
            Z = z;
            
            HalfWidth = width / 2;
            HalfHeight = height / 2;
            HalfDepth = depth / 2;
        }

        public bool Contains(ICubeTreeNode node)
        {
            return node.X <= X + HalfWidth &&
                   node.X >= X - HalfWidth &&
                   node.Y <= Y + HalfHeight &&
                   node.Y >= Y - HalfHeight &&
                   node.Z <= Z + HalfDepth &&
                   node.Z >= Z - HalfDepth;
        }
    }
}