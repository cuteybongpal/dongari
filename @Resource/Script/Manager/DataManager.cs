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
    //��� �ε���(���)
    const int DialogueStartIndex = 1;
    //ȣ���� ����ġ �ε���(���)
    const int LikeabilityStartIndex = 4;
    //ȭ�� �÷��̾�, ��� ĳ����, �����̼�
    public enum Speaker
    {
        Me,
        Other,
        Narration
    }
    //���
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
        //�� �ٲ� �������� ����
        string[] rows = csvData.Split(new[] { "\n", "\r\n" }, StringSplitOptions.None);
        Scenes _scene = new Scenes();
        for (int i =0; i < rows.Length - 1; i++)
        {
            //ù����� 3��°�� ������ �̸� �׷��Ƿ� continue
            if (i == 0 || i == 2)
                continue;
            //����ǥ�� �������� ���ڿ��� ����
            string[] datas = rows[i].Split(",", StringSplitOptions.None);

            if (i == 1)
            {
                //ù��° �����ʹ� ���� �������
                _scene.BGMName = datas[0];
                _scene.BackGround = (BackGround)Enum.Parse(typeof(BackGround), datas[1]);
                continue;
            }
            //�ι�°���ʹ� ������ ������ 1. ĳ���� �̸�, 2 - 4. ��� 5 - 7. ȣ���� ����ġ ���� �����ϰ� �ڵ� «
            Speaker cha = (Speaker)Enum.Parse(typeof(Speaker), datas[0]);
            List<string> lines = new List<string>();
            //��� �־��ֱ�
            for (int j = 0; j < 3; j++)
            {
                if (datas[j + DialogueStartIndex] == null || datas[j + DialogueStartIndex] == "")
                    break;
                lines.Add(datas[j + DialogueStartIndex]);
            }
            Debug.Log(cha);
            //ȣ���� ����ġ �־���
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
    //ȭ��
    public Speaker Character;
    //���
    public string[] line;
    //����(��� x )
    //public AudioClip SoundEff;
    //ȣ���� ����ġ
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
    //���
    public BackGround BackGround;
    //�����
    public string BGMName;
    public List<Line> Lines = new List<Line>();
}
public class GameData
{
    //���� �ó�����
    public Scenes CurrentSenario;
    //���� �ó����� �ѹ�
    int _currentSenarioNum = 0;
    //�ε��� �����͵�
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
            //���� ������ CurrentSenario�� �ٲ��ش�.
            _currentSenarioNum = value;
            CurrentSenario = _loadedData.SenarioDatas[_currentSenarioNum];
        }
    }
}

public class LoadedData
{
    public List<Scenes> SenarioDatas = new List<Scenes>();
}