using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine.UI;

public class UI_Start : UI_Base
{
    //�ε��� �޼����� ����� �ؽ�Ʈ
    Text _loadText; 
    private void Awake()
    {
        _loadText = FindChild<Text>("Text");
        //������ �ε� �� �� ���� AssetLoaded�� �����
        EventManager.AssetOnLoaded += AssetLoaded;
    }
    void AssetLoaded(string message, bool isAllLoaded)
    {
        _loadText.text = message;
        if (isAllLoaded)
        {
            Managers.Game.CurrentScene = GameManager.Scene.Game;
        }
    }
}
