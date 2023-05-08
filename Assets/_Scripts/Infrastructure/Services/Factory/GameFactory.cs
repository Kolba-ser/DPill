using Cysharp.Threading.Tasks;
using System.Collections.Generic;

namespace Infrastructure.Services.Factory
{
    public class GameFactory : IGameFactory
    {
        private SubFactoriesContainer<UnityEngine.Object> factoriesContainer;

        public GameFactory()
        {
            factoriesContainer = new SubFactoriesContainer<UnityEngine.Object>();
        }

        public T CreateAsset<T>(string assetName, string pathPrefix = "", string pathPostfix = "") where T : UnityEngine.Object =>
            factoriesContainer.Get(pathPrefix + assetName + pathPostfix).CreateAsset() as T;

        public async UniTask Payload()
        {
            int count = factoriesContainer.Factories.Count;
            UniTask[] tasks = new UniTask[count];
            int index = 0;

            foreach (var pair in factoriesContainer.Factories)
            {
                tasks[index] = pair.Value.Payload();
                index++;
            }

            await UniTask.WhenAll(tasks);
        }

        public void RegisterSubFactory<T>(IAssetFactory<T> factory) where T : UnityEngine.Object
        {
            factoriesContainer.Add(factory);
        }

        public void Release()
        {
            foreach (var pair in factoriesContainer.Factories)
                pair.Value.Release();
        }

        private class SubFactoriesContainer<T> where T : UnityEngine.Object
        {
            private Dictionary<string, IAssetFactory<T>> factories;

            public IReadOnlyDictionary<string, IAssetFactory<T>> Factories => factories;

            public SubFactoriesContainer()
            {
                factories = new Dictionary<string, IAssetFactory<T>>();
            }

            public void Add(IAssetFactory<T> factory)
            {
                if (!factories.ContainsKey(factory.AssetName))
                    factories.Add(factory.AssetName, factory);
            }

            public void Remove(IAssetFactory<T> factory)
            {
                if (factories.ContainsKey(factory.AssetName))
                    factories.Remove(factory.AssetName);
            }

            public void Remove(string assetName)
            {
                if (factories.ContainsKey(assetName))
                    factories.Remove(assetName);
            }

            public IAssetFactory<T> Get(string assetName)
            {
                if (factories.ContainsKey(assetName))
                    return factories[assetName];

                return null;
            }
        }
    }
}