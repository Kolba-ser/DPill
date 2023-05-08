using Cysharp.Threading.Tasks;
using System;

namespace Infrastructure.Services.Factory
{
    public interface IAssetFactory<out T> where T : UnityEngine.Object
    {
        public string AssetName { get; }

        public T CreateAsset();

        public UniTask Payload();

        public void Release();
    }
}