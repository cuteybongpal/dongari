using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class ResourceManager
{
    Dictionary<string, UnityEngine.Object> _resources = new Dictionary<string, UnityEngine.Object>();

    public T Load<T>(string key) where T : UnityEngine.Object
    {
        if (_resources.ContainsKey(key))
        {
            return _resources[key] as T;
        }
        return null;
    }
    public GameObject Instantiate(string key)
    {
        return GameObject.Instantiate(Load<GameObject>(key));
    }
    //Addressable에 등록한 오브젝트를 제네릭 형식으로 비동기 방식으로 불러온다.
    public void LoadAsync<T>(string key, Action<bool> callback = null) where T : UnityEngine.Object
    {
        //_resouces의 키값이 존재 한다면 그것을 가져온다.
        if (_resources.ContainsKey(key))
        {
            //로딩이 성공 여부 성공하면 true를 매개변수로 넣어준다.
            callback.Invoke(true);
            return;
        }
        else
        {
            AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(key);
            handle.Completed += (result) =>
            {
                if (result.Status == AsyncOperationStatus.Succeeded)
                {

                    _resources.Add(key, (UnityEngine.Object)handle.Result);
                    callback.Invoke(true);
                }
                else
                { 
                    callback.Invoke(false);
                }
            };
        }
    }
    public void LoadAllAsync<T>(string label, Action callback = null, Action<string> log = null) where T : UnityEngine.Object
    {
        var handle = Addressables.LoadResourceLocationsAsync(label, typeof(T));

        handle.Completed += (loadedAsset) =>
        {
            if (loadedAsset.Status == AsyncOperationStatus.Succeeded)
            {
                bool[] LoadedArray = new bool[loadedAsset.Result.Count];
                for (int i = 0; i < LoadedArray.Length; i++)
                {
                    int a = i;
                    LoadAsync<T>(loadedAsset.Result[a].PrimaryKey, (bool isload) =>
                    {
                        LoadedArray[a] = isload;
                        bool isLoadAll = true;
                        for (int j = 0; j < LoadedArray.Length; j++)
                            isLoadAll &= LoadedArray[j];
                        if (isLoadAll)
                        {
                            callback.Invoke();
                        }
                        // 로딩이 될 때마다 로딩이 되었다고 써준다.
                        log?.Invoke(loadedAsset.Result[a].PrimaryKey+" is Loaded");
                    });
                }
            }
            else
            {
                Debug.Log("AsyncLoadingFailed");
            }
        };
    }
}
