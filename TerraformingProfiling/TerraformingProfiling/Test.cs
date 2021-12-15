using System;
using System.Diagnostics;
using System.IO;
using Common.Utils.Serialization;

namespace TerraformingProfiling
{
    public class Test
    {
        public static void UnpackedTest()
        {
            var packer = new StrictBitsPacker(new byte[8]);
            int res = 0;
            var stream = new MemoryStream();
            packer.SetStream(stream);
            var timer = new Stopwatch();
            timer.Restart();
            var count = 1000000;
            for (int i = 0; i < count; i++)
            {
                var equation = (i % 2 == 0 ? (byte) 1 : (byte) 0);
                res += i;
                packer.WriteBool(equation > 0);
            }
                
            packer.Flush();
            stream.Seek(0, SeekOrigin.Begin);
            packer.SetStream(stream);
            for (int i = 0; i < count; i++)
            {
                var equation = packer.ReadBool();
                if (equation)
                {
                    res += i;
                }
            }
            
            Console.WriteLine($"Time: {timer.ElapsedMilliseconds} || {res}");
        }
        
        public static void PackedTest()
        {
            var packer = new StrictBitsPacker(new byte[8]);
            int res = 0;
            var stream = new MemoryStream();
            packer.SetStream(stream);
            var timer = new Stopwatch();
            timer.Restart();
            byte accum = 0;
            var count = 1000000;
            for (int i = 0; i < count; i++)
            {
                var equation = ((i % 2 == 0 ? (byte) 1 : (byte) 0));
                accum = (byte)(accum | (equation << (byte) (i % 8)));
                
                res += i;
                
                if (i % 8 == 7)
                {
                    packer.WriteByte(accum);
                    accum = 0;
                }
            }
            if ((count - 1) % 8 != 7)
            {
                packer.WriteByte(accum);
            }
                
            packer.Flush();
            stream.Seek(0, SeekOrigin.Begin);
            packer.SetStream(stream);
            for (int i = 0; i < count; i++)
            {
                if (i % 8 == 0)
                {
                    accum = packer.ReadByte();
                }
                var equation = (accum & ((i % 2 == 0 ? (byte) 1 : (byte) 0) << (byte) (i % 8)));
                if (equation > 0)
                {
                    res += i;
                }
            }
            
            Console.WriteLine($"Time: {timer.ElapsedMilliseconds} || {res}");
        }
    }
}