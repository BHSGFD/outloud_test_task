using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

public interface IAddressableAssetProvider : IService
{
    public UniTask<IList<T>> LoadAssetGroup<T>(string label, Action<T> callback = null) where T : class;
    public UniTask<T> LoadAssets<T>(AssetReference assetReference) where T : class;
    public UniTask<T> LoadGameObjectAsset<T>(AssetReference assetReference) where T : MonoBehaviour;
}