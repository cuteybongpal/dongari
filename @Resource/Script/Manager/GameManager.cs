using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager
{
    #region Game State and Scene Enums
    // 게임 상태 (Play, Slow, Pause)
    public enum GameState
    {
        Play,
        Slow,
        Pause
    }

    // 씬 상태 (Lobby, Game)
    public enum Scene
    {
        Lobby,
        Game
    }
    #endregion

    #region Fields
    //현재 시나리오 데이터
    public Scenes CurrentSenario;
    // 씬 로딩 중인지 확인하는 변수
    bool isChangingScene = false;

    // 현재 씬 번호
    int _currentSceneIndex;

    // 게임 상태
    GameState _state = GameState.Play;

    // 현재 씬 상태
    Scene _scene = Scene.Lobby;
    #endregion

    #region Properties

    // 게임 상태 (Play, Slow, Pause)
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
    // 현재 씬
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
    
    // 씬을 비동기적으로 변경
    async void ChangeScene(Scene scene)
    {
        // 이미 씬이 변경 중이면 기다림
        while (isChangingScene)
            await WaitForSeconds(Time.deltaTime);

        // 씬 비동기 로드
        AsyncOperation op = SceneManager.LoadSceneAsync((int)scene, LoadSceneMode.Additive);
        op.completed += (a) =>
        {
            Debug.Log("로딩 씬 번호 : " + (int)scene);

            // 기존 씬 언로드
            AsyncOperation _op = SceneManager.UnloadSceneAsync(_currentSceneIndex);
            _op.completed += (a) =>
            {
                Debug.Log("언로딩 씬 번호 : " + _currentSceneIndex);
                _currentSceneIndex = (int)scene;
            };

            // 언로드 진행 상황 로그
            while (_op.isDone)
                Debug.Log(_op.progress + "%");
        };

        // 씬 로딩 진행 상황 로그
        while (op.isDone)
            Debug.Log(op.progress + "%");
    }
    #endregion

    #region Utility Methods
    // 매개변수만큼 초 단위로 대기
    async UniTask WaitForSeconds(float t)
    {
        await UniTask.Delay(System.TimeSpan.FromSeconds(t));
    }
    #endregion
}