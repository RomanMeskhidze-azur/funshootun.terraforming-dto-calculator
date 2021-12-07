using System.Collections.Generic;

namespace TerraformingDtoCalculator.QuadTree
{
    public class CubeTree
    {
        private readonly Cube _cube;
        private readonly int _capacity;
        private readonly List<ICubeTreeNode> _nodes;

        private CubeTree _upLeftFrontTree;
        private CubeTree _upRightFrontTree;
        private CubeTree _downLeftFrontTree;
        private CubeTree _downRightFrontTree;
        private CubeTree _upLeftBackTree;
        private CubeTree _upRightBackTree;
        private CubeTree _downLeftBackTree;
        private CubeTree _downRightBackTree;

        private bool _isSubdivided;

        public CubeTree(Cube cube, int capacity)
        {
            _cube = cube;
            _capacity = capacity;
            _nodes = new List<ICubeTreeNode>(capacity);
        }

        public bool InsertNode(ICubeTreeNode node)
        {
            if (!_cube.Contains(node))
            {
                return false;
            }
            
            if (_nodes.Count < _capacity)
            {
                _nodes.Add(node);
                return true;
            }

            if (!_isSubdivided)
            {
                Subdivide();
            }

            return TryInsertToSubTree(node);
        }

        private void Subdivide()
        {
            var x = _cube.X;
            var y = _cube.Y;
            var z = _cube.Z;
            var width = _cube.HalfWidth;
            var height = _cube.HalfHeight;
            var depth = _cube.HalfDepth;

            var newRectUpLeftFront = new Cube(x - width, y + height, z + depth, width, height, depth);
            var newRectUpRightFront = new Cube(x + width, y + height, z + depth, width, height, depth);
            var newRectDownLeftFront = new Cube(x - width, y - height, z + depth, width, height, depth);
            var newRectDownRightFront = new Cube(x + width, y - height, z + depth, width, height, depth);
            var newRectUpLeftBack = new Cube(x - width, y + height, z - depth, width, height, depth);
            var newRectUpRightBack = new Cube(x + width, y + height, z - depth, width, height, depth);
            var newRectDownLeftBack = new Cube(x - width, y - height, z - depth, width, height, depth);
            var newRectDownRightBack = new Cube(x + width, y - height, z - depth, width, height, depth);

            _upLeftFrontTree = new CubeTree(newRectUpLeftFront, TerraformingDtoCalculatorConstants.QuadTreeCapacity);
            _upRightFrontTree = new CubeTree(newRectUpRightFront, TerraformingDtoCalculatorConstants.QuadTreeCapacity);
            _downLeftFrontTree = new CubeTree(newRectDownLeftFront, TerraformingDtoCalculatorConstants.QuadTreeCapacity);
            _downRightFrontTree = new CubeTree(newRectDownRightFront, TerraformingDtoCalculatorConstants.QuadTreeCapacity);
            _upLeftBackTree = new CubeTree(newRectUpLeftBack, TerraformingDtoCalculatorConstants.QuadTreeCapacity);
            _upRightBackTree = new CubeTree(newRectUpRightBack, TerraformingDtoCalculatorConstants.QuadTreeCapacity);
            _downLeftBackTree = new CubeTree(newRectDownLeftBack, TerraformingDtoCalculatorConstants.QuadTreeCapacity);
            _downRightBackTree = new CubeTree(newRectDownRightBack, TerraformingDtoCalculatorConstants.QuadTreeCapacity);

            _isSubdivided = true;
        }

        private bool TryInsertToSubTree(ICubeTreeNode node)
        {
            if (_upLeftFrontTree.InsertNode(node))
            {
                return true;
            }

            if (_upRightFrontTree.InsertNode(node))
            {
                return true;
            }

            if (_downLeftFrontTree.InsertNode(node))
            {
                return true;
            }

            if (_downRightFrontTree.InsertNode(node))
            {
                return true;
            }
            
            if (_upLeftBackTree.InsertNode(node))
            {
                return true;
            }

            if (_upRightBackTree.InsertNode(node))
            {
                return true;
            }

            if (_downLeftBackTree.InsertNode(node))
            {
                return true;
            }

            if (_downRightBackTree.InsertNode(node))
            {
                return true;
            }

            return false;
        }
    }
}