using UnityEngine;

namespace TerraformingDtoCalculator
{
    public class TerraformingDtoCalculatorConstants : MonoBehaviour
    {
        [SerializeField] private int _chunksMaxCount = 1331;
        [SerializeField] private int _verticesMaxCount = 1331;
        [SerializeField] private int _indicesMaxCount = 5000;
        [SerializeField] private int _vertListMaxCount = 1331;

        public static int ChunksMaxCount;
        public static int VerticesMaxCount;
        public static int IndicesMaxCount;
        public static int VertListMaxCount;

        private void Awake()
        {
            ChunksMaxCount = _chunksMaxCount;
            VerticesMaxCount = _verticesMaxCount;
            IndicesMaxCount = _indicesMaxCount;
            VertListMaxCount = _vertListMaxCount;
        }
    }
}