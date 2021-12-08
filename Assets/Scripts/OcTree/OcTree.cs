using System.Collections.Generic;

namespace TerraformingDtoCalculator.OcTree
{
    public class OcTree
    {
        private readonly Octangle _octangle;
        private readonly OcTree[] _childs = new OcTree[8];
        
        private IOcTreeNode _treeNode;

        public OcTree(Octangle octangle)
        {
            _octangle = octangle;
        }

        public void Initialization(int powerOfTwo)
        {
            if (powerOfTwo == 0)
            {
                _octangle.CorrectPosition();
                return;
            }

            Subdivide();
            var previousPowerOfTwo = powerOfTwo - 1;

            for (var i = 0; i < _childs.Length; i++)
            {
                _childs[i].Initialization(previousPowerOfTwo);
            }
        }

        public bool InsertNode(IOcTreeNode node)
        {
            if (_octangle.Width != 1)
            {
                return TryInsertToSubTree(node);
            }

            if (!_octangle.Contains(node))
            {
                return false;
            }

            _treeNode = node;
            return true;
        }
        
        private void Subdivide()
        {
            var x = _octangle.X;
            var y = _octangle.Y;
            var z = _octangle.Z;
            
            var width = _octangle.Width;
            var height = _octangle.Height;
            var depth = _octangle.Depth;

            var newRectUpLeftFront = new Octangle(OctangleType.LeftUpFront, x - width / 4, y + height / 4, z + depth / 4, width / 2, height / 2, depth / 2);
            var newRectUpRightFront = new Octangle(OctangleType.RightUpFront,x + width / 4, y + height / 4, z + depth / 4, width / 2, height / 2, depth / 2);
            var newRectDownLeftFront = new Octangle(OctangleType.LeftDownFront,x - width / 4, y - height / 4, z + depth / 4, width / 2, height / 2, depth / 2);
            var newRectDownRightFront = new Octangle(OctangleType.RightDownFront,x + width / 4, y - height / 4, z + depth / 4, width / 2, height / 2, depth / 2);
            var newRectUpLeftBack = new Octangle(OctangleType.LeftUpBack,x - width / 4, y + height / 4, z - depth / 4, width / 2, height / 2, depth / 2);
            var newRectUpRightBack = new Octangle(OctangleType.RightUpBack,x + width / 4, y + height / 4, z - depth / 4, width / 2, height / 2, depth / 2);
            var newRectDownLeftBack = new Octangle(OctangleType.LeftDownBack,x - width / 4, y - height / 4, z - depth / 4, width / 2, height / 2, depth / 2);
            var newRectDownRightBack = new Octangle(OctangleType.RightDownBack,x + width / 4, y - height / 4, z - depth / 4, width / 2, height / 2, depth / 2);

            _childs[0] = new OcTree(newRectUpLeftFront);
            _childs[1] = new OcTree(newRectUpRightFront);
            _childs[2] = new OcTree(newRectDownLeftFront);
            _childs[3] = new OcTree(newRectDownRightFront);
            _childs[4] = new OcTree(newRectUpLeftBack);
            _childs[5] = new OcTree(newRectUpRightBack);
            _childs[6] = new OcTree(newRectDownLeftBack);
            _childs[7] = new OcTree(newRectDownRightBack);
        }
        
        private bool TryInsertToSubTree(IOcTreeNode node)
        {
            var i = 0;
            while (!_childs[i].InsertNode(node))
            {
                i++;
                if (i == _childs.Length)
                {
                    return false;
                }
            }

            return true;
        }
    }
}