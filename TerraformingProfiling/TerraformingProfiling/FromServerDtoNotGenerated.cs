using System.Collections.Generic;
using Common.Utils.Serialization;
using TerraformingDtoCalculator.OcTree;

namespace TerraformingDtoCalculator
{
    public partial class FromServerDto
    {
        public struct ChunkIndexPair
        {
            public int Index;
            public ChunkDTO NewChunk;

            public ChunkIndexPair(int index, ChunkDTO newChunk)
            {
                Index = index;
                NewChunk = newChunk;
            }
        }
        
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
                ocTree.NewTreeNode.NotGeneratedSerDiff(packer, ocTree.OldTreeNode);
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

            var newChunks = new List<ChunkIndexPair>(125);
            var countChunks = packer.ReadInt(13);
            
            for (var i = 0; i < countChunks; i++)
            {
                DeserializeOcTree(packer, ocTree, other, newChunks);
            }

            ProcessDeserializedChunks(other, newChunks);
        }

        private void DeserializeOcTree(ISerializer packer, OcTree<ChunkDTO> ocTree, FromServerDto other, List<ChunkIndexPair> newChunks)
        {
            var childs = ocTree.Childs;
            if (childs[0] == null)
            {
                var octPosition = ocTree.Position;
                var chunkX = octPosition.X - 1;
                var chunkY = octPosition.Y - 1;
                var chunkZ = octPosition.Z - 1;
                var prevChunkIndex = other.ChankDtos.FindIndex(x => x.X == chunkX && x.Y == chunkY && x.Z == chunkZ);

                var newChunk = new ChunkDTO
                {
                    X = chunkX,
                    Y = chunkY,
                    Z = chunkZ
                };
                newChunk.NotGeneratedDeserSerDiff(packer, other.ChankDtos[prevChunkIndex]);
                newChunks.Add(new ChunkIndexPair(prevChunkIndex, newChunk));
                
                return;
            }

            var mask = packer.ReadInt(8);
            var index = GetIndexByMask(mask);
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

        private void ProcessDeserializedChunks(FromServerDto other, List<ChunkIndexPair> newChunks)
        {
            this.ChankDtos = other.ChankDtos;
            
            for (var i = 0; i < newChunks.Count; i++)
            {
                var chunk = newChunks[i];

                this.ChankDtos[chunk.Index] = chunk.NewChunk;
            }
        }
    }
}