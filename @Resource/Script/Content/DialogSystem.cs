using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogSystem : MonoBehaviour
{
    //현재 시나리오
    Scenes _currentSenario;
    //대화한 카운트
    int _currentConversationIndex = 0;
    public void Init()
    {
        setCurrnetSenario();
    }
    void setCurrnetSenario()
    {
        //지금 시나리오를 현재의 시나리오로 바꿔준다.
        _currentSenario = Managers.Data.GameData.CurrentSenario;
        _currentConversationIndex = 0;
    }
    //현재 대화
    public Line GetLine()
    {
        if (_currentConversationIndex >= _currentSenario.Lines.Count)
        {
            Debug.Log("끝");
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
        //다음 시나리오로 넘어감
        Managers.Data.GameData.CurrentSenarioNum++;
    }
    public string SelectChoice(int index)
    {
        //선택한 버튼의 호감도 가중치에 따라 캐릭터의 표정 이미지가 바뀌는데 Addressable의 키값을 반환해준다..
        Debug.Log(index);
        //현재 대화중의 클릭한 버튼의 호감도 가중치를 가져온다.
        int l = _currentSenario.Lines[_currentConversationIndex - 1].probability[index];
        Debug.Log(l);
        //가져온 가중치에 따라 코드 흐름을 다르게 한다.
        //양수 : 좋음
        //음수 : 나쁨
        //0 : 그냥 그런 상태
        switch (l)
        {
            case 0:
                Debug.Log("보통");
                return "Char0_default.sprite";
            case 1:
                Debug.Log("좋음");
                return "Char2_good.sprite";
            case -1:
                Debug.Log("나쁨");
                return "Char3_bad.sprite";
            default:
                return "";
        }
    }
}
