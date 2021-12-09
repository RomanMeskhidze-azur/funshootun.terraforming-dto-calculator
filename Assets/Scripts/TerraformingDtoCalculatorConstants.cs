using UnityEngine;

namespace TerraformingDtoCalculator
{
    public class TerraformingDtoCalculatorConstants : MonoBehaviour
    {
        [SerializeField] private int _chunksMaxCount = 1331;
        [SerializeField] private int _verticesMaxCount = 1331;
        [SerializeField] private int _indicesMaxCount = 5000;
        [SerializeField] private int _vertListMaxCount = 1331;
        [SerializeField] private int _quadTreeCapacity = 4;
        [SerializeField] private int _generateDiffChunksCount;

        public static int ChunksMaxCount;
        public static int VerticesMaxCount;
        public static int IndicesMaxCount;
        public static int VertListMaxCount;
        public static int QuadTreeCapacity;
        public static int GenerateDiffChunksCount;

        private void Awake()
        {
            ChunksMaxCount = _chunksMaxCount;
            VerticesMaxCount = _verticesMaxCount;
            IndicesMaxCount = _indicesMaxCount;
            VertListMaxCount = _vertListMaxCount;
            QuadTreeCapacity = _quadTreeCapacity;
            GenerateDiffChunksCount = _generateDiffChunksCount;
        }
    }
}