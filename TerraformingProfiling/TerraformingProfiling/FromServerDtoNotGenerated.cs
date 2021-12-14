using System.Collections.Generic;
using Common.Utils.Serialization;
using TerraformingDtoCalculator.OcTree;

namespace TerraformingDtoCalculator
{
    public partial class FromServerDto
    {
        public void NotGeneratedSerDiff(ISerializer packer, OcTree<ChunkDTO> ocTree)
        {
            packer.WriteBool(ocTree.IsBusy);
            if (!ocTree.IsBusy)
            {
                return;
            }

            packer.WriteInt(ocTree.CountInsertedNodes, 13);
            var masks = new List<int>();
           // Profiler.BeginSample("Serialize_OcTree");
            SerializeOcTree(packer, ocTree, masks);
          //  Profiler.EndSample();
        }

        private void SerializeOcTree(ISerializer packer, OcTree<ChunkDTO> ocTree, List<int> masks)
        {
            var childs = ocTree.Childs;
            if (childs[0] == null)
            {
                foreach (var mask in masks)
                {
                    var biteMask = 1 << mask;
                    packer.WriteInt(biteMask, 8);
                }
                ocTree.NewTreeNode.SerDiff(packer, ocTree.OldTreeNode);
                masks.RemoveAt(masks.Count - 1);
                return;
            }
            
            for (var i = 0; i < childs.Length; i++)
            {
                var child = childs[i];
                
                if (!child.IsBusy)
                {
                    continue;
                }

                masks.Add(i);
                SerializeOcTree(packer, child, masks);
            }

            if (masks.Count > 0)
            {
                masks.RemoveAt(masks.Count - 1);
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

            var newChunks = new List<ChunkDTO>(20);
            var countChunks = packer.ReadInt(13);
            
            //Profiler.BeginSample("Deserialize_OcTree");
            for (var i = 0; i < countChunks; i++)
            {
                DeserializeOcTree(packer, ocTree, other, newChunks);
            }
          //  Profiler.EndSample();

          //  Profiler.BeginSample("ProcessDeserialized_ocTree");
            ProcessDeserializedChunks(other, newChunks);
          //  Profiler.EndSample();
        }

        private void DeserializeOcTree(ISerializer packer, OcTree<ChunkDTO> ocTree, FromServerDto other, List<ChunkDTO> newChunks)
        {
            var childs = ocTree.Childs;
            if (childs[0] == null)
            {
                var octPosition = ocTree.Position;
                var chunkX = octPosition.X - 1;
                var chunkY = octPosition.Y - 1;
                var chunkZ = octPosition.Z - 1;
            //    Profiler.BeginSample("FindInDeserOcTree");
                var prevChunk = other.ChankDtos.Find(x => x.X == chunkX && x.Y == chunkY && x.Z == chunkZ);
             //   Profiler.EndSample();

                var newChunk = new ChunkDTO
                {
                    X = chunkX,
                    Y = chunkY,
                    Z = chunkZ
                };
             //   Profiler.BeginSample("DeserDiff");
                newChunk.DeserDiff(packer, prevChunk);
             //   Profiler.EndSample();
                newChunks.Add(newChunk);
                
                return;
            }

            var mask = packer.ReadInt(8);
          //  Profiler.BeginSample("Deserialize_GetIndexByMask");
            var index = GetIndexByMask(mask);
         //   Profiler.EndSample();
            DeserializeOcTree(packer, ocTree.Childs[index], other, newChunks);
        }

        private int GetIndexByMask(int mask)
        {
            if (mask == 1)
            {
                return 0;
            }

            var i = 0;
            while (mask != 1)
            {
                mask = mask / 2;
                i++;
            }

            return i;
        }

        private void ProcessDeserializedChunks(FromServerDto other, List<ChunkDTO> newChunks)
        {
            this.ChankDtos = other.ChankDtos;
            
            for (var i = 0; i < newChunks.Count; i++)
            {
                var chunk = newChunks[i];

                var oldChunk = other.ChankDtos.FindIndex(x => x.X == chunk.X && x.Y == chunk.Y && x.Z == chunk.Z);
                this.ChankDtos[oldChunk] = chunk;
            }
        }
    }
}