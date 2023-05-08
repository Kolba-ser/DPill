using UnityEngine;

namespace Shared
{
    public static class JsonExtensions
    {
        public static T ToDeserialized<T>(this string json) =>
            JsonUtility.FromJson<T>(json);

        public static string ToJson(this object obj) =>
            JsonUtility.ToJson(obj);
    }
}