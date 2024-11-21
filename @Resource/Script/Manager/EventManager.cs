using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager
{
    //resource매니저에서 에셋이 로딩 될 때 마다 실행됨 메세지와 전부 다 로딩이 되었는지 여부가 체크됨
    public static Action<string, bool> AssetOnLoaded;

    public static void AssetOnLoad(string message, bool isAllLoaded)
    {
        AssetOnLoaded?.Invoke(message, isAllLoaded);
    }
}
