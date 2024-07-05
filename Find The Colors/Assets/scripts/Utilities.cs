using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public static class Utilities 
{
    private static System.Random random = new System.Random();
    public static T GetRandomElement<T>(this List<T> list)
    {
        if (list == null || list.Count == 0)
        {
            throw new ArgumentException("The list is null or empty.");
        }

        int randomIndex = random.Next(list.Count);
        return list[randomIndex];
    }
}
