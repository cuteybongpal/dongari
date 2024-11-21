using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;
    public GameObject chatPannel;
    public GameObject thisObject;
    public ObjectData objectData;
    public Text talkText;
    public bool isAction;
    public int talkIndex2;

    private void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        InsertData();
        chatPannel.SetActive(false);
    }

    void InsertData()
    {
        talkData.Add(1, new string[] { "��.. �׷� �ȳ�", "�װ� �л��� �ǹ�.. �ϱ�..?", "��.. ������..", "��� ����� ���� ����ε�?" });
        talkData.Add(2, new string[] {"�ݰ���!", "�������� �� �б��� �����ϴ� �ɱ�?", "�׷� �翬�� ������ ���°� �ƴϾ�", "����� �� ������ �ູ�Ѱŷ���."});
    }

    public string ActionTalk(int id, int talkIndex)
    {
        if (talkIndex == talkData[id].Length)
        {
            return null;
        }
        else
        {
            return talkData[id][talkIndex];
        }
        
    }

    public void Action(GameObject getObject)
    {
        if (isAction)
        {
            isAction = false;
        }
        else
        {
            isAction = true;
            thisObject = getObject;
            ObjectData objectData = thisObject.GetComponent<ObjectData>();
            Talk(objectData.entityId);
        }

        chatPannel.SetActive(isAction);
    }

    void Talk(int id)
    {
        string talkData = ActionTalk(id, talkIndex2);
        if (talkData == null)
        {
            isAction = false;
            talkIndex2 = 0;
            return;
        }

        talkText.text = talkData;
        isAction = true;
        talkIndex2++;
    }
}
