using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UI_Game : UI_Base
{
    //��� �̹���
    Image _background;
    //ĳ����
    Image _character;
    //ĳ������ ��簡 �� �ؽ�Ʈ
    Text _line;
    //ĳ������ �̸��� ��
    Text _characterText;
    //�÷��̾ ������ ��ư�� ������ ��ư���� �� 3���� ����
    Button[] Choices = new Button[3];
    void Start()
    {
        _background = FindChild<Image>("Image_BackGround");
        _character = FindChild<Image>("Image_Character");
        _line = FindChild<Text>("Text_Line");
        _characterText = FindChild<Text>("Text_Speaker");
        //��ư �ʱ�ȭ
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
        //����� �̹����� �����´�.
        Sprite backgroundSprite = Managers.Resource.Load<Sprite>(Managers.Dialog.GetBackGround());
        _background.sprite = backgroundSprite;
        //���� ĳ������ �̹����� �����´�.
        Sprite characterSprite = Managers.Resource.Load<Sprite>("Char0_default.sprite");
        _character.sprite = characterSprite;
        PrintLine();
    }
    //��縦 ������ ����Ѵ�
    async void PrintLine()
    {
        Line line = Managers.Dialog.GetLine();
        if (line.line == null)
            return;
        
        //ȭ�ڰ� ��� ĳ���� �϶�
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
        //ȭ�ڰ� �÷��̾� �϶�
        else if (line.Character == DataManager.Speaker.Me)
        {
            for (int i =0; i < line.line.Length; i++)
            {
                Choices[i].gameObject.SetActive(true);
                Choices[i].transform.GetComponentInChildren<Text>().text = line.line[i];
            }
        }
        //ȭ�ڰ� �����̼��϶���
        else
        {
            //todo - �����̼��� �� ��� ĳ�����϶��� �����ϰ� �ϵ� ���ڴ� ȸ������
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
            //�� ������ ����, Ŭ�� �ߴٸ�, �ѱ��� �� ����� �ƴ϶� ���δ� ����Ѵ�.
            await UniTask.Yield();
            time += Time.deltaTime;
            if (Input.GetMouseButtonUp(0))
            {
                _line.text = line;
                break;
            }
            //0.1�ʸ��� �ѱ��� �� ������ش�.
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
