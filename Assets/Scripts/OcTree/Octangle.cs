namespace TerraformingDtoCalculator.OcTree
{
    public class Octangle<T> where T : IOcTreeNode
    {
        public OctangleType Type { get; }
        public int X { get; private set; }
        public int Y { get; private set; }
        public int Z { get; private set; }
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

        public bool Contains(T node)
        {
            return node.X + 1 == X &&
                   node.Y + 1 == Y &&
                   node.Z + 1 == Z;
        }

        public void CorrectPosition()
        {
            switch (Type)
            {
                case OctangleType.LeftDownFront:
                    Z++;
                    return;
                case OctangleType.RightDownFront:
                    Z++;
                    X++;
                    return;
                case OctangleType.LeftUpFront:
                    Z++;
                    Y++;
                    return;
                case OctangleType.RightUpFront:
                    Z++;
                    X++;
                    Y++;
                    return;
                case OctangleType.LeftDownBack:
                    return;
                case OctangleType.RightDownBack:
                    X++;
                    return;
                case OctangleType.LeftUpBack:
                    Y++;
                    return;
                case OctangleType.RightUpBack:
                    X++;
                    Y++;
                    return;
            }
        }
    }
}