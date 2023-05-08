using Cysharp.Threading.Tasks;
using Infrastructure.Services.Interfaces;

namespace Infrastructure.Services.Factory
{
    public interface IGameFactory : IService 
    {
        public T CreateAsset<T>(string AssetName, string pathPrefix = "", string pathPostfix = "") where T : UnityEngine.Object;

        public UniTask Payload();

        public void RegisterSubFactory<T>(IAssetFactory<T> factory) where T : UnityEngine.Object;

        public void Release();
    }
}