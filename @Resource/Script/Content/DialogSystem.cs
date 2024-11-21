using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogSystem : MonoBehaviour
{
    //���� �ó�����
    Scenes _currentSenario;
    //��ȭ�� ī��Ʈ
    int _currentConversationIndex = 0;
    public void Init()
    {
        setCurrnetSenario();
    }
    void setCurrnetSenario()
    {
        //���� �ó������� ������ �ó������� �ٲ��ش�.
        _currentSenario = Managers.Data.GameData.CurrentSenario;
        _currentConversationIndex = 0;
    }
    //���� ��ȭ
    public Line GetLine()
    {
        if (_currentConversationIndex >= _currentSenario.Lines.Count)
        {
            Debug.Log("��");
            return new Line();
        }
        else
        {
            Line lines = _currentSenario.Lines[_currentConversationIndex];
            _currentConversationIndex++;
            return lines;
        }
    }
    public string GetBackGround()
    {
        switch (_currentSenario.BackGround)
        {
            case DataManager.BackGround.ClassRoom:
                return "BG_ClassRoom.sprite";
            case DataManager.BackGround.Park:
                return "BG_Park.sprite";
            case DataManager.BackGround.Corrider:
                return "BG_Corridor.sprite";
            case DataManager.BackGround.Library:
                return "BG_Library.sprite";
            case DataManager.BackGround.Street:
                return "BG_Street.sprite";
            default:
                return "";
        }
    }
    public void NextSenario()
    {
        //���� �ó������� �Ѿ
        Managers.Data.GameData.CurrentSenarioNum++;
    }
    public string SelectChoice(int index)
    {
        //������ ��ư�� ȣ���� ����ġ�� ���� ĳ������ ǥ�� �̹����� �ٲ�µ� Addressable�� Ű���� ��ȯ���ش�..
        Debug.Log(index);
        //���� ��ȭ���� Ŭ���� ��ư�� ȣ���� ����ġ�� �����´�.
        int l = _currentSenario.Lines[_currentConversationIndex - 1].probability[index];
        Debug.Log(l);
        //������ ����ġ�� ���� �ڵ� �帧�� �ٸ��� �Ѵ�.
        //��� : ����
        //���� : ����
        //0 : �׳� �׷� ����
        switch (l)
        {
            case 0:
                Debug.Log("����");
                return "Char0_default.sprite";
            case 1:
                Debug.Log("����");
                return "Char2_good.sprite";
            case -1:
                Debug.Log("����");
                return "Char3_bad.sprite";
            default:
                return "";
        }
    }
}
