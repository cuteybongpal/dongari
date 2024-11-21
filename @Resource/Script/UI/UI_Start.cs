using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine.UI;

public class UI_Start : UI_Base
{
    //로딩한 메세지를 출력할 텍스트
    Text _loadText; 
    private void Awake()
    {
        _loadText = FindChild<Text>("Text");
        //에셋이 로딩 될 때 마다 AssetLoaded가 실행됨
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
