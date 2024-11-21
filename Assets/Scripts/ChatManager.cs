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
        talkData.Add(1, new string[] { "어.. 그래 안녕", "그게 학생의 의무.. 니까..?", "그.. 렇구나..", "어떠한 사람이 좋은 사람인데?" });
        talkData.Add(2, new string[] {"반가워!", "오전에는 왜 학교를 가야하는 걸까?", "그런 당연한 이유를 묻는게 아니야", "사람을 잘 만나야 행복한거랬어."});
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
