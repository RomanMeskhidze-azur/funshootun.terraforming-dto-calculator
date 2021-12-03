using UnityEngine;

namespace TerraformingDtoCalculator
{
    public class TerraformingDtoCalculatorConstants : MonoBehaviour
    {
        [SerializeField] private int _chunksMaxCount = 1331;
        [SerializeField] private int _verticesMaxCount = 1331;
        [SerializeField] private int _indicesMaxCount = 5000;
        [SerializeField] private int _vertListMaxCount = 1331;
        [SerializeField, Range(0, 1)] private float _generateDiffChunksPercents;

        public static int ChunksMaxCount;
        public static int VerticesMaxCount;
        public static int IndicesMaxCount;
        public static int VertListMaxCount;
        public static float GenerateDiffChunksPercents;

        private void Awake()
        {
            ChunksMaxCount = _chunksMaxCount;
            VerticesMaxCount = _verticesMaxCount;
            IndicesMaxCount = _indicesMaxCount;
            VertListMaxCount = _vertListMaxCount;
            GenerateDiffChunksPercents = _generateDiffChunksPercents;
        }
    }
}