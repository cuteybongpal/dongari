using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UI_Game : UI_Base
{
    //배경 이미지
    Image _background;
    //캐릭터
    Image _character;
    //캐릭터의 대사가 들어갈 텍스트
    Text _line;
    //캐릭터의 이름이 들어감
    Text _characterText;
    //플레이어가 선택할 버튼들 선택할 버튼들은 총 3개가 있음
    Button[] Choices = new Button[3];
    void Start()
    {
        _background = FindChild<Image>("Image_BackGround");
        _character = FindChild<Image>("Image_Character");
        _line = FindChild<Text>("Text_Line");
        _characterText = FindChild<Text>("Text_Speaker");
        //버튼 초기화
        for (int i =0; i < Choices.Length;i++)
        {
            Choices[i] = FindChild<Button>($"Button_Choice{i+1}");
            int a = i;
            Choices[i].onClick.AddListener(() =>
            {
                string key =  Managers.Dialog.SelectChoice(a);
                SetCharacterImage(key);
                for (int j =0; j < Choices.Length;j++)
                {
                    Choices[j].gameObject.SetActive(false);
                }
                PrintLine();
            });
            Choices[i].gameObject.SetActive(false);
        }
        _line.text = "";
        //배경의 이미지를 가져온다.
        Sprite backgroundSprite = Managers.Resource.Load<Sprite>(Managers.Dialog.GetBackGround());
        _background.sprite = backgroundSprite;
        //현재 캐릭터의 이미지를 가져온다.
        Sprite characterSprite = Managers.Resource.Load<Sprite>("Char0_default.sprite");
        _character.sprite = characterSprite;
        PrintLine();
    }
    //대사를 가져와 출력한다
    async void PrintLine()
    {
        Line line = Managers.Dialog.GetLine();
        if (line.line == null)
            return;
        
        //화자가 상대 캐릭터 일때
        if (line.Character == DataManager.Speaker.Other)
        {
            for (int i = 0; i < line.line.Length; i++)
            {
                
                if (line.line[i] == "" || line.line == null)
                    break;
                _line.text = "";
                await PrintLines(line.line[i]);
                if (i  != line.line.Length - 1)
                    await WaitUntilClick();
            }
        }
        //화자가 플레이어 일때
        else if (line.Character == DataManager.Speaker.Me)
        {
            for (int i =0; i < line.line.Length; i++)
            {
                Choices[i].gameObject.SetActive(true);
                Choices[i].transform.GetComponentInChildren<Text>().text = line.line[i];
            }
        }
        //화자가 나레이션일때임
        else
        {
            //todo - 나레이션일 때 상대 캐릭터일때와 동일하게 하되 글자는 회색으로
        }
        if (line.Character != DataManager.Speaker.Me)
        {
            await WaitUntilClick();
            SetCharacterImage("Char0_default.sprite");
            PrintLine();
        }
    }
    async UniTask PrintLines(string line)
    {
        float time = 0;
        int index = 0;
        while(true)
        {
            //한 프레임 쉬고, 클릭 했다면, 한글자 씩 출력이 아니라 전부다 출력한다.
            await UniTask.Yield();
            time += Time.deltaTime;
            if (Input.GetMouseButtonUp(0))
            {
                _line.text = line;
                break;
            }
            //0.1초마다 한글자 씩 출력해준다.
            if (time >= .1f)
            {
                _line.text += line[index];
                index++;
                time = 0; 
            }
            if (index >= line.Length)
                break;
        }
    }
    async UniTask WaitUntilClick()
    {
        while (true)
        {
            await UniTask.Yield();
            if (Input.GetMouseButtonUp(0))
                break;
        }
    }
    void SetCharacterImage(string key)
    {
        Sprite charImage = Managers.Resource.Load<Sprite>(key);
        _character.sprite = charImage;
    }
}
