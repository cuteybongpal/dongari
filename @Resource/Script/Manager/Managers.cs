using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.VersionControl;
using UnityEngine;

public class Managers : MonoBehaviour
{
    private static Managers instance = null;
    public static Managers Instance
    {
        get
        {
            return instance;
        }
    }

    ResourceManager _resources = new ResourceManager();
    DataManager _data = new DataManager();
    GameManager _game = new GameManager();
    DialogSystem _dialog;

    public static ResourceManager Resource { get { return Instance._resources; } }
    public static DataManager Data { get { return Instance._data; } }
    public static GameManager Game { get { return Instance._game; } }
    public static DialogSystem Dialog { get => Instance._dialog; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            _dialog = GetComponent<DialogSystem>();
            //Addressable�� �ִ� ��� ������Ʈ���� ���� �񵿱� �ε��Ѵ�.
            //���Ƿ� ���� ���̱� ������ ���߿� �˾Ƽ� �ٲٻ�
            Resource.LoadAllAsync<Sprite>("PreLoad_Sprite", () =>
            {
                EventManager.AssetOnLoad("Sprite AllLoaded!", false);
                Resource.LoadAllAsync<TextAsset>("PreLoad_data", () =>
                {
                    EventManager.AssetOnLoad("Data AllLoaded!", false);
                    Resource.LoadAllAsync<GameObject>("PreLoad_GameObject", () =>
                    {
                        EventManager.AssetOnLoad("Prefab AllLoaded!", true);
                        Data.Init();
                        Dialog.Init();
                    }, (string message) => EventManager.AssetOnLoad(message, false));

                }, (string message) => EventManager.AssetOnLoad(message, false));
                //���µ��� �ε� �ɶ� ���� �ε� �޼����� ����
            }, (string message) => EventManager.AssetOnLoad(message, false));
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
