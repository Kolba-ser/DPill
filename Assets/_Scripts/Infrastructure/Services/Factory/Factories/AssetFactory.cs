using Cysharp.Threading.Tasks;
using Infrastructure.Services.Providers.Assets;
using System;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Infrastructure.Services.Factory
{
    public class AssetFactory<T> : IAssetFactory<T> where T : UnityEngine.Object
    {
        private string assetPath;
        private T prefabAsset;
        private IAssetsProvider assetsProvider;
        private readonly bool needPayload;
        private readonly bool cacheAsset;

        public string AssetName { get; private set; }

        private AssetFactory(IAssetsProvider assetsProvider, bool needPayload, bool cacheAsset)
        {
            this.assetsProvider = assetsProvider;
            this.needPayload = needPayload;
            this.cacheAsset = cacheAsset;
        }

        public AssetFactory(string assetPath, IAssetsProvider assetsProvider, bool needPayload = true, bool cacheAsset = true) : this(assetsProvider, needPayload, cacheAsset)
        {
            this.assetPath = assetPath;

            AssetName = GetAssetName(assetPath);
            Debug.Log(AssetName);
        }

        private static string GetAssetName(string assetPath)
        {
            var builder = new StringBuilder();
            for (int i = assetPath.Length - 1; i >= 0; i--)
            {
                if (assetPath[i] == '/')
                    break;

                builder.Append(assetPath[i]);
            }

            string reversedName = builder.ToString();
            builder.Clear();

            for (int i = reversedName.Length - 1; i >= 0; i--)
                builder.Append(reversedName[i]);

            return builder.ToString();
        }

        public AssetFactory(T prefabAsset, IAssetsProvider assetsProvider, bool needPayload = true, bool cacheAsset = true) : this(assetsProvider, needPayload, cacheAsset)
        {
            this.prefabAsset = prefabAsset;
            AssetName = prefabAsset.name;
        }

        public virtual T CreateAsset()
        {
            if (prefabAsset == null)
                LoadAsset();

            T asset = GameObject.Instantiate<T>(prefabAsset);
            
            if (!cacheAsset)
                prefabAsset = null;

            return asset;
        }

        public async UniTask Payload()
        {
            if (needPayload)
                prefabAsset = await assetsProvider.LoadAsync<T>(assetPath);
        }

        protected void LoadAsset(string pathPrefix = "", string pathPostfix = "") =>
            prefabAsset = assetsProvider.Load<T>(pathPrefix + assetPath + pathPostfix);

        public void Release()
        {
            prefabAsset = null;
            assetsProvider = null;
            assetPath = String.Empty;
        }
    }
}