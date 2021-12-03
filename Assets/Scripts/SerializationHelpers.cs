using System;
using System.Collections.Generic;

namespace TerraformingDtoCalculator
{
    public static class SerializationHelpers
    {
        public static void Resize<T>(this List<T> list, int size, Func<T> defaultValue)
        {
            if (list.Count > size)
            {
                var diff = list.Count - size;
                for(int i = 0; i < diff; i++)
                    list.RemoveAt(0);
            }
            else if (list.Count < size)
            {
                var diff = size - list.Count;
                for (int i = 0; i < diff; i++)
                {
                    list.Add(defaultValue());
                }
            }
        }
    }
}