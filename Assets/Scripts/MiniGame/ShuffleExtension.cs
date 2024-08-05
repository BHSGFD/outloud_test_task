using System.Collections.Generic;
using UnityEngine;

public static class ShuffleExtension
{
    public static void Shuffle<T>(this List<T> list)
    {
        System.Random random = new System.Random();
        
        for (int i = 0; i < Random.Range(list.Count, list.Count*10); i++)
        {
            var i1 = random.Next(list.Count);
            var i2 = random.Next(list.Count);

            (list[i1], list[i2]) = (list[i2], list[i1]);
        }
    }
}