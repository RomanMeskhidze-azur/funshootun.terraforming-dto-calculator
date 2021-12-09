using Common.Utils.Serialization;
using TerraformingDtoCalculator.OcTree;

namespace TerraformingDtoCalculator
{
    public partial class FromServerDto
    {
        public void NotGeneratedSerDiff(ISerializer packer, OcTree<ChunkDTO> ocTree)
        {
            if (!ocTree.IsBusy)
            {
                packer.WriteBool(false);
                return;
            }
            
            SerializeOcTree(packer, ocTree);
        }

        private void SerializeOcTree(ISerializer packer, OcTree<ChunkDTO> ocTree)
        {
            var childs = ocTree.Childs;
            if (childs[0] == null)
            {
                if (!ocTree.IsBusy)
                {
                    return;
                }
                
                ocTree.NewTreeNode.SerDiff(packer, ocTree.OldTreeNode);
                return;
            }
            
            for (var i = 0; i < childs.Length; i++)
            {
                var child = childs[i];
                
                if (!child.IsBusy)
                {
                    continue;
                }

                var mask = 1 << i;
                packer.WriteInt(mask, 8);
                SerializeOcTree(packer, child);
            }
        }
        
        public void NotGeneratedDeserDiff(ISerializer packer, FromServerDto other, OcTree<ChunkDTO> ocTree)
        {
            var isBusy = packer.ReadBool();
            if (!isBusy)
            {
                for (int i = 0; i < ChankDtosCount; i++)
                {
                    other[new ChankDtosIndexer(i)] = this[new ChankDtosIndexer(i)];
                }
                return;
            }
        }
    }
}