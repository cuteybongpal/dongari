using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager
{
    #region Game State and Scene Enums
    // ���� ���� (Play, Slow, Pause)
    public enum GameState
    {
        Play,
        Slow,
        Pause
    }

    // �� ���� (Lobby, Game)
    public enum Scene
    {
        Lobby,
        Game
    }
    #endregion

    #region Fields
    //���� �ó����� ������
    public Scenes CurrentSenario;
    // �� �ε� ������ Ȯ���ϴ� ����
    bool isChangingScene = false;

    // ���� �� ��ȣ
    int _currentSceneIndex;

    // ���� ����
    GameState _state = GameState.Play;

    // ���� �� ����
    Scene _scene = Scene.Lobby;
    #endregion

    #region Properties

    // ���� ���� (Play, Slow, Pause)
    public GameState State
    {
        get { return _state; }
        set
        {
            _state = value;
            switch (_state)
            {
                case GameState.Play:
                    Time.timeScale = 1;
                    Time.fixedDeltaTime = Time.deltaTime;
                    break;
                case GameState.Slow:
                    Time.timeScale = 0.5f;
                    Time.fixedDeltaTime = Time.deltaTime * 0.5f;
                    break;
                case GameState.Pause:
                    Time.timeScale = 0;
                    break;
            }
        }
    }
    // ���� ��
    public Scene CurrentScene
    {
        get { return _scene; }
        set
        {
            _scene = value;
            ChangeScene(_scene);
        }
    }
    #endregion

    #region Scene Change Logic
    
    // ���� �񵿱������� ����
    async void ChangeScene(Scene scene)
    {
        // �̹� ���� ���� ���̸� ��ٸ�
        while (isChangingScene)
            await WaitForSeconds(Time.deltaTime);

        // �� �񵿱� �ε�
        AsyncOperation op = SceneManager.LoadSceneAsync((int)scene, LoadSceneMode.Additive);
        op.completed += (a) =>
        {
            Debug.Log("�ε� �� ��ȣ : " + (int)scene);

            // ���� �� ��ε�
            AsyncOperation _op = SceneManager.UnloadSceneAsync(_currentSceneIndex);
            _op.completed += (a) =>
            {
                Debug.Log("��ε� �� ��ȣ : " + _currentSceneIndex);
                _currentSceneIndex = (int)scene;
            };

            // ��ε� ���� ��Ȳ �α�
            while (_op.isDone)
                Debug.Log(_op.progress + "%");
        };

        // �� �ε� ���� ��Ȳ �α�
        while (op.isDone)
            Debug.Log(op.progress + "%");
    }
    #endregion

    #region Utility Methods
    // �Ű�������ŭ �� ������ ���
    async UniTask WaitForSeconds(float t)
    {
        await UniTask.Delay(System.TimeSpan.FromSeconds(t));
    }
    #endregion
}