using System;
using System.Collections.Generic;

public static class CollectionExtensions
{
    public static bool InBounds<T>(this int index, ICollection<T> collection)
    {
        return collection != null
                && collection.Count > 0
                && index >= 0
                && index < collection.Count;
    }

    public static T GetRandom<T>(this T[] collection)
    {
        if(collection == null || collection.Length == 0)
            return default(T);

        int randomIndex = UnityEngine.Random.Range(0, collection.Length);

        return collection[randomIndex];
    }
}