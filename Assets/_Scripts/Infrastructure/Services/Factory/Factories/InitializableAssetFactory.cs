using Infrastructure.Services.Providers.Assets;
using System;

namespace Infrastructure.Services.Factory
{
    public class InitializableAssetFactory<T> : AssetFactory<T> where T : UnityEngine.Object
    {
        private readonly Action<T> initialize;

        public InitializableAssetFactory(string assetPath, IAssetsProvider assetsProvider, Action<T> initialize, bool needPayload = true, bool cacheAsset = true) : base(assetPath, assetsProvider, needPayload, cacheAsset)
        {
            this.initialize = initialize;
        }


        public InitializableAssetFactory(T prefabAsset, IAssetsProvider assetsProvider, Action<T> initialize, bool needPayload = true, bool cacheAsset = true) : base(prefabAsset, assetsProvider, needPayload, cacheAsset)
        {
            this.initialize = initialize;
        }

        public override T CreateAsset()
        {
            T asset = base.CreateAsset();
            initialize(asset);
            return asset;
        }
    }
}