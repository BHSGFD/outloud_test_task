using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AddressableAssetProvider : IAddressableAssetProvider
{
    private readonly Dictionary<string, AsyncOperationHandle> _cache = new();

    public async UniTask<IList<T>> LoadAssetGroup<T>(string label, Action<T> callback) where T : class
    {
        if (_cache.TryGetValue(label, out var cashedHandle))
        {
            await new WaitUntil(() => cashedHandle.IsDone);
            return cashedHandle.Result as IList<T>;
        }

        var handle = Addressables.LoadAssetsAsync(label, callback);
        _cache.Add(label, handle);
        await handle.Task;

        return handle.Result;
    }

    public async UniTask<T> LoadAssets<T>(AssetReference assetReference) where T : class
    {
        if (_cache.TryGetValue(assetReference.AssetGUID, out var cashedHandle))
        {
            await new WaitUntil(() => cashedHandle.IsDone);
            return cashedHandle.Result as T;
        }

        var handle = Addressables.LoadAssetAsync<T>(assetReference);
        _cache.Add(assetReference.AssetGUID, handle);
        await handle.Task;

        return handle.Result;
    }

    public async UniTask<T> LoadGameObjectAsset<T>(AssetReference assetReference) where T : MonoBehaviour
    {
        if (_cache.TryGetValue(assetReference.AssetGUID, out var cashedHandle))
        {
            await new WaitUntil(() => cashedHandle.IsDone);
            return (cashedHandle.Result as GameObject)?.GetComponent<T>();
        }

        var handle = Addressables.LoadAssetAsync<GameObject>(assetReference);
        _cache.Add(assetReference.AssetGUID, handle);
        await handle.Task;

        return handle.Result?.GetComponent<T>();
    }

    public bool IsInitialized => true;
}