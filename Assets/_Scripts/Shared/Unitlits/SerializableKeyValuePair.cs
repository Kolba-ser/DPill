using System;
using UnityEngine.Assertions;

[Serializable]
public struct SerializableKeyValuePair<TKey, TValue>
{
    public TKey Key;
    public TValue Value;

    public SerializableKeyValuePair(TKey key, TValue value)
    {
        Assert.IsTrue(!typeof(TKey).IsSerializable, $"Imposible to serialize {nameof(TKey)}");
        Assert.IsTrue(!typeof(TValue).IsSerializable, $"Imposible to serialize {nameof(TValue)}");
        Key = key;
        Value = value;
    }
}