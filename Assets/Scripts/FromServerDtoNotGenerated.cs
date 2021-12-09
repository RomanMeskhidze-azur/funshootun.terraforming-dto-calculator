using System.Collections.Generic;
using Common.Utils.Serialization;
using TerraformingDtoCalculator.OcTree;
using UnityEngine;

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

            Debug.Log($"sered chunks count {ocTree.CountInsertedNodes}");
            packer.WriteInt(ocTree.CountInsertedNodes, 13);
            var masks = new List<int>();
            SerializeOcTree(packer, ocTree, masks);
            Debug.Log($"masks count {masks.Count}");
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

            var newChunks = new List<ChunkDTO>();
            var countChunks = packer.ReadInt(13);
            Debug.Log($"desered count {countChunks}");
            for (var i = 0; i < countChunks; i++)
            {
                DeserializeOcTree(packer, ocTree, other, newChunks);
            }
            Debug.Log($"new chunks count {newChunks.Count}");

            ProcessDeserializedChunks(other, newChunks);
        }

        private void DeserializeOcTree(ISerializer packer, OcTree<ChunkDTO> ocTree, FromServerDto other, List<ChunkDTO> newChunks)
        {
            var childs = ocTree.Childs;
            if (childs[0] == null)
            {
                var octPosition = ocTree.Position;
                var chunkX = octPosition.x - 1;
                var chunkY = octPosition.y - 1;
                var chunkZ = octPosition.z - 1;
                var prevChunk = other.ChankDtos.Find(x => x.X == chunkX && x.Y == chunkY && x.Z == chunkZ);

                var newChunk = new ChunkDTO
                {
                    X = chunkX,
                    Y = chunkY,
                    Z = chunkZ
                };
                newChunk.DeserDiff(packer, prevChunk);
                newChunks.Add(newChunk);
                
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

        private void ProcessDeserializedChunks(FromServerDto other, List<ChunkDTO> newChunks)
        {
            for (var i = 0; i < other.ChankDtosCount; i++)
            {
                var chunkDto = other.ChankDtos[i];

                var newChunk = newChunks.Find(x => x.X == chunkDto.X && x.Y == chunkDto.Y && x.Z == chunkDto.Z);
                if (newChunk != null)
                {
                    this.ChankDtos[i] = newChunk;
                    continue;
                }

                this.ChankDtos[i] = chunkDto;
            }
        }
    }
}