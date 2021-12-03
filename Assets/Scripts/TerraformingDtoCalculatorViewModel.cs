using System;
using UnityEngine;
using UnityEngine.UI;

namespace TerraformingDtoCalculator
{
    public class TerraformingDtoCalculatorViewModel : MonoBehaviour
    {
        [SerializeField] private Text _chunksCountText;
        [SerializeField] private Text _verticesCountText;
        [SerializeField] private Text _indicesCountText;
        [SerializeField] private Text _vertListCountText;
        [SerializeField] private Text _generateDiffChunksPercentsText;

        [SerializeField] private Button _runTestButton;

        [SerializeField] private Text _resultText;

        private TerraformingDtoCalculator _terraformingDtoCalculator;

        private void Start()
        {
            _chunksCountText.text = $"Chucks count: {TerraformingDtoCalculatorConstants.ChunksMaxCount}";
            _verticesCountText.text = $"Vertices per chunk count: {TerraformingDtoCalculatorConstants.VerticesMaxCount}";
            _indicesCountText.text = $"Indices per chunk count: {TerraformingDtoCalculatorConstants.IndicesMaxCount}";
            _vertListCountText.text = $"VertList per chunk count: {TerraformingDtoCalculatorConstants.VertListMaxCount}";
            _generateDiffChunksPercentsText.text = $"Generate diff chunk percents: {(int) (TerraformingDtoCalculatorConstants.GenerateDiffChunksPercents * 100)}";
            _resultText.text = "Result: ";
            
            _runTestButton.onClick.AddListener(RunTests);

            _terraformingDtoCalculator = new TerraformingDtoCalculator();
        }

        private void OnDestroy()
        {
            _runTestButton.onClick.RemoveListener(RunTests);
        }

        private void RunTests()
        {
            var initializeTime = _terraformingDtoCalculator.InitialFillDto();

            _resultText.text = $"Result: InitDto {initializeTime}ms.";
        }
    }
}