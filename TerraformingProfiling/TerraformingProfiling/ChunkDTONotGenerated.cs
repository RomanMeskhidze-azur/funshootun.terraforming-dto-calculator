using System;
using System.Collections.Generic;
using Common.Utils.Serialization;
using TerraformingDtoCalculator.OcTree;

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
                    packer.WriteByte(temp);
                    temp = 0;
                }
            }
            
            if ((VerticesCount - 1) % 8 != 7)
            {
                packer.WriteByte(temp);
            }
            
            packer.WriteBytes(verticesValues, 0, counter);

            #endregion

            // temp = 0;
            // counter = 0;
            //
            // #region Indices
            //
            // packer.WriteInt(IndicesCount, 13);
            //
            // var indicesValues = new byte[IndicesCount * 2];
            // for (var i = 0; i < IndicesCount; i++)
            // {
            //     var isNotEqual = this[new IndicesIndexer(i)] != other[new IndicesIndexer(i)];
            //     var isNotEqualByte = isNotEqual ? (byte) 1 : (byte) 0;
            //     temp = (byte)(temp | (isNotEqualByte << (byte) (i % 8)));
            //     if (isNotEqual)
            //     {
            //         var byteArray = new IntToByte
            //         {
            //             val = this[new IndicesIndexer(i)]
            //         };
            //         byteArray.ToByteArray(conversionsBuffer, out _);
            //         
            //         indicesValues[counter++] = conversionsBuffer[0];
            //         indicesValues[counter++] = conversionsBuffer[1];
            //     }
            //     
            //     if (i % 8 == 7)
            //     {
            //         packer.WriteByte(temp);
            //         temp = 0;
            //     }
            // }
            //
            // if ((IndicesCount - 1) % 8 != 7)
            // {
            //     packer.WriteByte(temp);
            // }
            //
            // packer.WriteBytes(indicesValues, 0, counter);
            //
            // #endregion
            //
            // temp = 0;
            // counter = 0;
            //
            // #region VertListX
            //
            // packer.WriteInt(VertListXCount, 13);
            //
            // var vertListValues = new byte[VertListXCount];
            // for (var i = 0; i < VertListXCount; i++)
            // {
            //     var isNotEqual = this[new VertListXIndexer(i)] != other[new VertListXIndexer(i)];
            //     var isNotEqualByte = isNotEqual ? (byte) 1 : (byte) 0;
            //     temp = (byte)(temp | (isNotEqualByte << (byte) (i % 8)));
            //     if (isNotEqual)
            //     {
            //         vertListValues[counter++] = this[new VertListXIndexer(i)];
            //     }
            //     
            //     if (i % 8 == 7)
            //     {
            //         packer.WriteByte(temp);
            //         temp = 0;
            //     }
            // }
            //
            // if ((VertListXCount - 1) % 8 != 7)
            // {
            //     packer.WriteByte(temp);
            // }
            //
            // packer.WriteBytes(vertListValues, 0, counter);
            //
            // #endregion
            //
            // temp = 0;
            // counter = 0;
            //
            // #region VertListY
            //
            // packer.WriteInt(VertListYCount, 13);
            //
            // for (var i = 0; i < VertListYCount; i++)
            // {
            //     var isNotEqual = this[new VertListYIndexer(i)] != other[new VertListYIndexer(i)];
            //     var isNotEqualByte = isNotEqual ? (byte) 1 : (byte) 0;
            //     temp = (byte)(temp | (isNotEqualByte << (byte) (i % 8)));
            //     if (isNotEqual)
            //     {
            //         vertListValues[counter++] = this[new VertListYIndexer(i)];
            //     }
            //     
            //     if (i % 8 == 7)
            //     {
            //         packer.WriteByte(temp);
            //         temp = 0;
            //     }
            // }
            //
            // if ((VertListYCount - 1) % 8 != 7)
            // {
            //     packer.WriteByte(temp);
            // }
            //
            // packer.WriteBytes(vertListValues, 0, counter);
            //
            // #endregion
            //
            // temp = 0;
            // counter = 0;
            //
            // #region VertListZ
            //
            // packer.WriteInt(VertListZCount, 13);
            //
            // for (var i = 0; i < VertListZCount; i++)
            // {
            //     var isNotEqual = this[new VertListZIndexer(i)] != other[new VertListZIndexer(i)];
            //     var isNotEqualByte = isNotEqual ? (byte) 1 : (byte) 0;
            //     temp = (byte)(temp | (isNotEqualByte << (byte) (i % 8)));
            //     if (isNotEqual)
            //     {
            //         vertListValues[counter++] = this[new VertListZIndexer(i)];
            //     }
            //     
            //     if (i % 8 == 7)
            //     {
            //         packer.WriteByte(temp);
            //         temp = 0;
            //     }
            // }
            //
            // if ((VertListZCount - 1) % 8 != 7)
            // {
            //     packer.WriteByte(temp);
            // }
            //
            // packer.WriteBytes(vertListValues, 0, counter);
            //
            // #endregion
        }

        public virtual void NotGeneratedDeserSerDiff(ISerializer packer, ChunkDTO data)
        {
            var other = data;
            Generation = packer.ReadByte(8);

            var maskBitsCount = GetMaskBitsCount(TerraformingDtoCalculatorConstants.VerticesMaxCount);
            var mask = new byte[maskBitsCount];
            packer.ReadBytes(mask, 0, maskBitsCount);

            var coutdesered = 0;
            var countcopied = 1;

            for (var i = 0; i < VerticesCount; i++)
            {
                if (GetIsEqual(mask[GetByteIndex(i)], i))
                {
                    this[new VerticesIndexer(i)] = packer.ReadShort();
                    coutdesered++;
                }
                else
                {
                    this[new VerticesIndexer(i)] = other[new VerticesIndexer(i)];
                    countcopied++;
                }
            }
            
            Console.WriteLine($"desede {coutdesered} copied {countcopied}");
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