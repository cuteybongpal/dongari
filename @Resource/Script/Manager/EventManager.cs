using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager
{
    //resource�Ŵ������� ������ �ε� �� �� ���� ����� �޼����� ���� �� �ε��� �Ǿ����� ���ΰ� üũ��
    public static Action<string, bool> AssetOnLoaded;

    public static void AssetOnLoad(string message, bool isAllLoaded)
    {
        AssetOnLoaded?.Invoke(message, isAllLoaded);
    }
}
