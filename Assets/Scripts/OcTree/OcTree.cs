using UnityEngine;

namespace TerraformingDtoCalculator.OcTree
{
    public class OcTree<T> where T : IOcTreeNode
    {
        private readonly Octangle<T> _octangle;
        private readonly OcTree<T>[] _childs = new OcTree<T>[8];

        public Vector3Int Position => new Vector3Int(_octangle.X, _octangle.Y, _octangle.Z);
        public int CountInsertedNodes { get; private set; }
        
        private T _newTreeNode;
        private T _oldTreeNode;
        
        public bool IsBusy { get; private set; }

        public OcTree<T>[] Childs => _childs;
        public T NewTreeNode => _newTreeNode;
        public T OldTreeNode => _oldTreeNode;

        public OcTree(Octangle<T> octangle)
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

        public bool InsertNode(T newNode, T oldNode)
        {
            if (_octangle.Type == OctangleType.Root)
            {
                CountInsertedNodes++;
            }
            
            if (_octangle.Width != 1)
            {
                var result = TryInsertToSubTree(newNode, oldNode);
                if (!IsBusy)
                {
                    IsBusy = result;
                }
                return result;
            }

            if (!_octangle.Contains(newNode))
            {
                return false;
            }

            _newTreeNode = newNode;
            _oldTreeNode = oldNode;
            IsBusy = true;
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

            var newRectUpLeftFront = new Octangle<T>(OctangleType.LeftUpFront, x - width / 4, y + height / 4, z + depth / 4, width / 2, height / 2, depth / 2);
            var newRectUpRightFront = new Octangle<T>(OctangleType.RightUpFront,x + width / 4, y + height / 4, z + depth / 4, width / 2, height / 2, depth / 2);
            var newRectDownLeftFront = new Octangle<T>(OctangleType.LeftDownFront,x - width / 4, y - height / 4, z + depth / 4, width / 2, height / 2, depth / 2);
            var newRectDownRightFront = new Octangle<T>(OctangleType.RightDownFront,x + width / 4, y - height / 4, z + depth / 4, width / 2, height / 2, depth / 2);
            var newRectUpLeftBack = new Octangle<T>(OctangleType.LeftUpBack,x - width / 4, y + height / 4, z - depth / 4, width / 2, height / 2, depth / 2);
            var newRectUpRightBack = new Octangle<T>(OctangleType.RightUpBack,x + width / 4, y + height / 4, z - depth / 4, width / 2, height / 2, depth / 2);
            var newRectDownLeftBack = new Octangle<T>(OctangleType.LeftDownBack,x - width / 4, y - height / 4, z - depth / 4, width / 2, height / 2, depth / 2);
            var newRectDownRightBack = new Octangle<T>(OctangleType.RightDownBack,x + width / 4, y - height / 4, z - depth / 4, width / 2, height / 2, depth / 2);

            _childs[0] = new OcTree<T>(newRectUpLeftFront);
            _childs[1] = new OcTree<T>(newRectUpRightFront);
            _childs[2] = new OcTree<T>(newRectDownLeftFront);
            _childs[3] = new OcTree<T>(newRectDownRightFront);
            _childs[4] = new OcTree<T>(newRectUpLeftBack);
            _childs[5] = new OcTree<T>(newRectUpRightBack);
            _childs[6] = new OcTree<T>(newRectDownLeftBack);
            _childs[7] = new OcTree<T>(newRectDownRightBack);
        }
        
        private bool TryInsertToSubTree(T newNode, T oldNode)
        {
            for (var i = 0; i < _childs.Length; i++)
            {
                if (!_childs[i].InsertNode(newNode, oldNode))
                {
                    continue;
                }

                return true;
            }

            return false;
        }
    }
}