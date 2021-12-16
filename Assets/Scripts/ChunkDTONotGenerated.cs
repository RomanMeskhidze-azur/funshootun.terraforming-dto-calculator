using System;
using Common.Utils.Serialization;
using TerraformingDtoCalculator.OcTree;
using UnityEngine;

namespace TerraformingDtoCalculator
{
    public partial class ChunkDTO : IOcTreeNode
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
        
        public virtual void NotGeneratedSerDiff(ISerializer packer, ChunkDTO data)
        {
            var other = data;
            packer.WriteByte(Generation, 8);

            byte temp = 0;
            var counter = 0;
            var conversionsBuffer = new byte[8];

            #region Vertices

            var bitsMaskCount = GetMaskBitsCount(VerticesCount);
            var masksCounter = 0;
            var masksArray = new byte[bitsMaskCount];

            var verticesValues = new byte[VerticesCount * 2];
            for (var i = 0; i < VerticesCount; i++)
            {
                var isNotEqual = this[new VerticesIndexer(i)] != other[new VerticesIndexer(i)];
                var isNotEqualByte = isNotEqual ? (byte) 1 : (byte) 0;
                temp = (byte)(temp | (isNotEqualByte << (byte) (i % 8)));
                if (isNotEqual)
                {
                    var byteArray = new ShortToByte
                    {
                        val = this[new VerticesIndexer(i)]
                    };
                    byteArray.ToByteArray(conversionsBuffer, out _);
                    verticesValues[counter++] = conversionsBuffer[0];
                    verticesValues[counter++] = conversionsBuffer[1];
                }
                
                if (i % 8 == 7)
                {
                    masksArray[masksCounter++] = temp;
                    temp = 0;
                }
            }
            
            if ((VerticesCount - 1) % 8 != 7)
            {
                masksArray[masksCounter] = temp;
            }
            
            packer.WriteBytes(masksArray, 0, masksCounter + 1);
            packer.WriteInt(counter, 12);
            packer.WriteBytes(verticesValues, 0, counter);

            #endregion
        }

        public virtual void NotGeneratedDeserSerDiff(ISerializer packer, ChunkDTO data)
        {
            var other = data;
            Generation = packer.ReadByte(8);

            var maskBitsCount = GetMaskBitsCount(TerraformingDtoCalculatorConstants.VerticesMaxCount);
            var mask = new byte[maskBitsCount];
            packer.ReadBytes(mask, 0, maskBitsCount);
            var dataCount = packer.ReadInt(12);
            var dataArray = new byte[dataCount];
            packer.ReadBytes(dataArray, 0, dataCount);

            var coutdesered = 0;
            var countcopied = 1;
            var dataCounter = 0;
            var dataBytes = new byte[2];

            for (var i = 0; i < VerticesCount; i++)
            {
                if (GetIsEqual(mask[GetByteIndex(i)], i))
                {
                    var byteArray = new ShortToByte();
                    dataBytes[0] = dataArray[dataCounter++];
                    dataBytes[1] = dataArray[dataCounter++];
                    
                    byteArray.FromByteArray(dataBytes);
                    this[new VerticesIndexer(i)] = byteArray.val;
                    coutdesered++;
                }
                else
                {
                    this[new VerticesIndexer(i)] = other[new VerticesIndexer(i)];
                    countcopied++;
                }
            }
        }

        private int GetMaskBitsCount(int count)
        {
            if (count % 8 == 0)
            {
                return count / 8;
            }

            return (count / 8) + 1;
        }

        private bool GetIsEqual(byte byteValue, int index)
        {
            var bitIndex = index % 8;
            return (byteValue & 1 << bitIndex) != 0;
        }

        private int GetByteIndex(int index)
        {
            return index / 8;
        }
    }
}