using System;

namespace TerraformingProfiling
{
    class Program
    {
        static void Main(string[] args)
        {
            var terraformingDtoCalculator = new TerraformingDtoCalculator.TerraformingDtoCalculator();
            
            var fillInitializeTime = terraformingDtoCalculator.FillInitialFromServerDto();
            
            terraformingDtoCalculator.GenerateDifferentChunkIds();

            var fillDifferentTime = terraformingDtoCalculator.FillDifferentFromServerDto();

            var serDiffTime = terraformingDtoCalculator.SerDeserDiff();

            var notGeneratedSerDiffTime = terraformingDtoCalculator.NotGeneratedSerDeserDiff();

            Console.WriteLine($"Result: Fill init dto {fillInitializeTime}ms.; Fill different dto {fillDifferentTime}ms.\nSerDiff {serDiffTime.Item1}ms. {serDiffTime.Item2} bytes\nDeserDiff {serDiffTime.Item3}ms.\nNotGeneratedSerDiff {notGeneratedSerDiffTime.Item1}ms. {notGeneratedSerDiffTime.Item2} bytes\nNotGeneratedDeserDiff {notGeneratedSerDiffTime.Item3}ms.");
        }
    }
}