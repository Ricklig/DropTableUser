using System;
using System.Collections.Generic;

static class IListExtensions
{
    static Random rg = new Random();
    public static void Shuffle<T>(this IList<T> list)
    {
        for (int i = list.Count; i > 1; i--)
        {
            int k = rg.Next(i);
            T temp = list[k];
            list[k] = list[i - 1];
            list[i - 1] = temp;
        }
    }
}