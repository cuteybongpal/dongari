using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System;
using UnityEditor.AddressableAssets.HostingServices;
using System.Text;
using static DataManager;

public class DataManager
{
    //대사 인덱스(상수)
    const int DialogueStartIndex = 1;
    //호감도 가중치 인덱스(상수)
    const int LikeabilityStartIndex = 4;
    //화자 플레이어, 상대 캐릭터, 나레이션
    public enum Speaker
    {
        Me,
        Other,
        Narration
    }
    //배경
    public enum BackGround
    {
        ClassRoom,
        Corrider,
        Library,
        Park,
        Street
    }
    public LoadedData LoadData;
    public GameData GameData;
    public void Init()
    {
        LoadData = new LoadedData();
        GameData = new GameData(LoadData);
        ReadData(Encoding.GetEncoding("euc-kr").GetString(Managers.Resource.Load<TextAsset>("Scene1.data").bytes));
        GameData.CurrentSenarioNum = 0;
    }
    public void ReadData(string data)
    {
        string csvData = data;
        //줄 바꿈 기준으로 나눔
        string[] rows = csvData.Split(new[] { "\n", "\r\n" }, StringSplitOptions.None);
        Scenes _scene = new Scenes();
        for (int i =0; i < rows.Length - 1; i++)
        {
            //첫번재와 3번째는 데이터 이름 그러므로 continue
            if (i == 0 || i == 2)
                continue;
            //따옴표를 기준으로 문자열을 나눔
            string[] datas = rows[i].Split(",", StringSplitOptions.None);

            if (i == 1)
            {
                //첫번째 데이터는 배경과 배경음임
                _scene.BGMName = datas[0];
                _scene.BackGround = (BackGround)Enum.Parse(typeof(BackGround), datas[1]);
                continue;
            }
            //두번째부터는 데이터 순서는 1. 캐릭터 이름, 2 - 4. 대사 5 - 7. 호감도 가중치 이점 참고하고 코드 짬
            Speaker cha = (Speaker)Enum.Parse(typeof(Speaker), datas[0]);
            List<string> lines = new List<string>();
            //대사 넣어주기
            for (int j = 0; j < 3; j++)
            {
                if (datas[j + DialogueStartIndex] == null || datas[j + DialogueStartIndex] == "")
                    break;
                lines.Add(datas[j + DialogueStartIndex]);
            }
            Debug.Log(cha);
            //호감도 가중치 넣어줌
            if (cha == Speaker.Me)
            {
                int[] likesability = new int[3];
                for (int j =0; j < likesability.Length; j++)
                {
                    Debug.Log(datas[j + LikeabilityStartIndex]);
                    likesability[j] = int.Parse(datas[j + LikeabilityStartIndex]);
                }
                Line _line = new Line(cha, lines.ToArray(), likesability);
                _scene.Lines.Add(_line);
            }
            else
            {
                int[] likesability = new int[3] {0, 0, 0};
                Line _line = new Line(cha, lines.ToArray(), likesability);
                _scene.Lines.Add(_line);
            }
            LoadData.SenarioDatas.Add(_scene);
        }
    }
    
    
}
public struct Line
{
    //화자
    public Speaker Character;
    //대사
    public string[] line;
    //사운드(대사 x )
    //public AudioClip SoundEff;
    //호감도 가중치
    public int[] probability;
    public Line(Speaker _char, string[] _line, int[] _likeability)
    {
        Character = _char;
        line = _line;
        probability = _likeability;
    }
}
public class Scenes
{
    //배경
    public BackGround BackGround;
    //배경음
    public string BGMName;
    public List<Line> Lines = new List<Line>();
}
public class GameData
{
    //현재 시나리오
    public Scenes CurrentSenario;
    //현재 시나리오 넘버
    int _currentSenarioNum = 0;
    //로딩된 데이터들
    LoadedData _loadedData;
    public int Characterprobability;
    public GameData(LoadedData loadedData)
    {
        this._loadedData = loadedData;
    }
    public int CurrentSenarioNum
    {
        get => _currentSenarioNum;
        set
        {
            //변할 때마다 CurrentSenario를 바꿔준다.
            _currentSenarioNum = value;
            CurrentSenario = _loadedData.SenarioDatas[_currentSenarioNum];
        }
    }
}

public class LoadedData
{
    public List<Scenes> SenarioDatas = new List<Scenes>();
}