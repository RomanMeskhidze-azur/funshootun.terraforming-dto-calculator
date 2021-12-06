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
            _generateDiffChunksPercentsText.text = $"Generate diff chunk count: {(int) (TerraformingDtoCalculatorConstants.ChunksMaxCount * TerraformingDtoCalculatorConstants.GenerateDiffChunksPercents)}";
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
            var fillInitializeTime = _terraformingDtoCalculator.FillInitialFromServerDto();
            
            _terraformingDtoCalculator.GenerateDifferentChunkIds();

            var fillDifferentTime = _terraformingDtoCalculator.FillDifferentFromServerDto();

            var serDiffTime = _terraformingDtoCalculator.SerDeserDiff();

            _resultText.text = $"Result: Fill init dto {fillInitializeTime}ms.; Fill different dto {fillDifferentTime}ms.\nSerDiff {serDiffTime.Item1}ms. {serDiffTime.Item2} bytes\nDeserDiff {serDiffTime.Item3}ms.";
        }
    }
}