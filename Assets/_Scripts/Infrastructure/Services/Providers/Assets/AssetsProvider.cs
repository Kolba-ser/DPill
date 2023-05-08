using Cysharp.Threading.Tasks;
using Infrastructure.Services.Interfaces;
using UnityEngine;
using UnityEngine.Assertions;

namespace Infrastructure.Services.Providers.Assets
{
    public class AssetsProvider : IAssetsProvider
    {
        public T Load<T>(string path) where T : Object
        {
            var asset = Resources.Load<T>(path);
            CheckForNull<T>(path, asset);
            return asset;
        }

        public async UniTask<T> LoadAsync<T>(string path) where T : UnityEngine.Object
        {
            var asset = await Resources.LoadAsync<T>(path).ToUniTask();
            CheckForNull<T>(path, asset);
            return (asset as T);
        }

        private static void CheckForNull<T>(string path, Object asset) where T : Object => 
            Assert.IsNotNull(asset, $"Object type of {typeof(T)} not found by {path}");
    }

    public interface IAssetsProvider : IService
    {
        public UniTask<T> LoadAsync<T>(string path) where T : UnityEngine.Object;
        public T Load<T>(string path) where T : UnityEngine.Object;
    }
}