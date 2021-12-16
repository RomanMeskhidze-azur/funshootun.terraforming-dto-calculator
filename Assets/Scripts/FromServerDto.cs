using System.Collections.Generic;
using System.Linq;
using Common.Utils.Serialization;
using UnityEngine.Profiling;

namespace TerraformingDtoCalculator
{
    public partial class FromServerDto
    {
        public List<ChunkDTO> ChankDtos = new List<ChunkDTO>(TerraformingDtoCalculatorConstants.ChunksMaxCount);

        public FromServerDto()
        {
            ChankDtos.AddRange(Enumerable.Repeat(new ChunkDTO(), TerraformingDtoCalculatorConstants.ChunksMaxCount));
        }

        #region CodeGen

        public int ChankDtosCount
        {
            get => ChankDtos.Count;
            set => ChankDtos.Resize(value, () => new ChunkDTO());
        }

        public struct ChankDtosIndexer
        {
            public ChankDtosIndexer(int index)
            {
                Index = index;
            }

            public int Index;
        }

        public ChunkDTO this[ChankDtosIndexer index]
        {
            get => ChankDtos[index.Index];
            set => ChankDtos[index.Index] = value;
        }

        public void Ser(ISerializer packer)
        {
#if UNITY_EDITOR
            if (ChankDtosCount > 6500) throw new System.Exception("Max possible value for ChankDtosCount is 6500");
#endif

            packer.WriteInt(ChankDtosCount, 13);
            for (int i = 0; i < ChankDtosCount; i++)
            {
                this[new ChankDtosIndexer(i)].Ser(packer);
            }
        }

        public void Deser(ISerializer packer)
        {
            int ChankDtosCount;
            ChankDtosCount = packer.ReadInt(13);
            ChankDtos.Resize(ChankDtosCount, () => new ChunkDTO());
            for (int i = 0; i < ChankDtosCount; i++)
            {
                var codegenTemp45 = new ChunkDTO();
                codegenTemp45.Deser(packer);
                this[new ChankDtosIndexer(i)] = codegenTemp45;
            }
        }

        public bool IsEqual(FromServerDto other)
        {
            if (!this.ChankDtosCount.Equals(other.ChankDtosCount)) return false;
            for (int i = 0; i < ChankDtosCount; i++)
            {
                if (!this[new ChankDtosIndexer(i)].IsEqual(other[new ChankDtosIndexer(i)])) return false;
            }

            return true;
        }

        public void SerDiff(ISerializer packer, FromServerDto data)
        {
            packer.WriteBool(data != null);
            if (data == null)
            {
                Ser(packer);
                return;
            }

            var other = data;
            Profiler.BeginSample("IterateChunksCollection");
            for (int i = 0; i < ChankDtosCount; i++)
            {
                var isNotEqual = !this[new ChankDtosIndexer(i)].IsEqual(other[new ChankDtosIndexer(i)]);
                packer.WriteBool(isNotEqual);
                if (isNotEqual)
                {
                    Profiler.BeginSample("SerDiffConcreteChunk");
                    this[new ChankDtosIndexer(i)].SerDiff(packer, other[new ChankDtosIndexer(i)]);
                    Profiler.EndSample();
                }
            }
            Profiler.EndSample();
        }

        public void DeserDiff(ISerializer packer, FromServerDto other)
        {
            var isDiff = packer.ReadBool();
            if (!isDiff)
            {
                Deser(packer);
                return;
            }

            Profiler.BeginSample("IterateChunksCollection");
            for (int i = 0; i < ChankDtosCount; i++)
            {
                if (packer.ReadBool())
                {
                    Profiler.BeginSample("ChunkDeserDiff");
                    this[new ChankDtosIndexer(i)].DeserDiff(packer, other[new ChankDtosIndexer(i)]);
                    Profiler.EndSample();
                }
                else
                {
                    // ТУТ ДОЛЖНО БЫТЬ ЧАНКОХРАНИЛИЩЕ
                    Profiler.BeginSample("CopyLink");
                    this[new ChankDtosIndexer(i)] = other[new ChankDtosIndexer(i)];
                    Profiler.EndSample();
                }
            }
            Profiler.EndSample();
        }

        #endregion
    }
}