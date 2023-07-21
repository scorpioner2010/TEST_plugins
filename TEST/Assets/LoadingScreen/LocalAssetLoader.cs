using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class LocalAssetLoader
{
    private GameObject _cachedObject;

    protected async Task<T> LoadInternal<T>(string assetId)
    {
        AsyncOperationHandle<GameObject> handle = Addressables.InstantiateAsync(assetId);
        _cachedObject = await handle.Task;
        
        if (_cachedObject.TryGetComponent(out T loadingScreen) == false)
        {
            Debug.LogError("LoadError");
        }

        return loadingScreen;
    }

    protected void UnloadInternal()
    {
        if (_cachedObject == null)
        {
            return;
        }
        
        _cachedObject.SetActive(false);
        Addressables.ReleaseInstance(_cachedObject);
        _cachedObject = null;
    }
}