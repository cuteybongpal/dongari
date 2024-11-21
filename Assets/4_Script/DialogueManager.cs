using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//선택지는 3개를 넘지 않는다
// 처음 시작시 절대 선택지 X
// 0번 인덱스는 주인공
// n번 부터 그냥 따까리들
public class DialogueManager : MonoBehaviour, ILinerEvnet
{
    public  static DialogueManager Instance { get { return instance; } }
    private static DialogueManager instance = null;
    public Speaker[] speaker;
    public MainSpeaker mainSpeaker;
    public DialogData[] data;

    public DialogData currentData;

    public float typingSpeed = 0.1f;
    private bool isTyping;
    private bool isChooise;
    private bool canChat = true;

    public GameObject nextBtn;
    public Button chooise1;
    public Button chooise2;
    public Button chooise3;

    //start문 초기화
    public TextMeshProUGUI text1;
    public TextMeshProUGUI text2;
    public TextMeshProUGUI text3;

    public Queue<DialogData> dataQueue = null;

    private void OnDisable()
    {
        EvnetManager.Instance.RemoveListener(EVENT_TYPE.StartDia);
        EvnetManager.Instance.RemoveListener(EVENT_TYPE.NextDia);
        EvnetManager.Instance.RemoveListener(EVENT_TYPE.StopDia);
    }
    private void Awake()
    {

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            DestroyImmediate(this);

        }
    }
    private void Start()
    {
            //text1 = chooise1.transform.Find("Text").GetComponent<TextMeshPro>();
            //text2 = chooise1.transform.Find("Text").GetComponent<TextMeshPro>();
            //text3 = chooise1.transform.Find("Text").GetComponent<TextMeshPro>();
        
        EvnetManager.Instance.AddListener(EVENT_TYPE.StartDia, this);
        EvnetManager.Instance.AddListener(EVENT_TYPE.NextDia, this);
        EvnetManager.Instance.AddListener(EVENT_TYPE.StopDia, this);

        text1 = chooise1.transform.Find("Text").GetComponent<TextMeshProUGUI>();
        text2 = chooise2.transform.Find("Text").GetComponent<TextMeshProUGUI>();
        text3 = chooise3.transform.Find("Text").GetComponent<TextMeshProUGUI>();

        nextBtn.SetActive(false);
        chooise1.gameObject.SetActive(false);
        chooise2.gameObject.SetActive(false);
        chooise3.gameObject.SetActive(false);

        text1.text = "";
        text2.text = "";
        text3.text = "";

    }

    private void Update()
    {
        if(!isTyping && Input.GetMouseButton(0) && canChat)
        {

            EvnetManager.Instance.PostListener(EVENT_TYPE.NextDia, this, null, currentData);
        }
    }
    public void OnEvnet(EVENT_TYPE type, Component sender, object param1 = null, object param2 = null)
    {
        Debug.Log(type + "현재 타입");
        switch (type)
        {
            case EVENT_TYPE.StartDia:
                StartDialogue();
                break;

            case EVENT_TYPE.NextDia:
                int paramValue = param1 != null && param1 is int ? (int)param1 : 0; // 씷어
                //DialogData dataValue = param2 is DialogData dialogData ? dialogData : default;
                NextDialogue(paramValue, currentData);
                break;
            case EVENT_TYPE.StopDia:
                StopDialogue(param1);
                break;

        }
    }

    private void StartDialogue()
    {
        Debug.Log("start");
        dataQueue = new Queue<DialogData>();
        foreach (DialogData d in data)
        {
            dataQueue.Enqueue(d);
        }
        if (dataQueue.Count > 0)
        {
            Debug.Log(dataQueue.Count);
            currentData = dataQueue.Dequeue(); // 현재 데이터 초기화
            EvnetManager.Instance.PostListener(EVENT_TYPE.NextDia, this, null, currentData);
        }
    }
    private void NextDialogue(int param1, DialogData param2)
    {
        nextBtn.SetActive(false);
        if (dataQueue.Count <= 0)
        {
            if (param2.speakerIndex > 0)
            {
                EndDialogue(param1, param2, speaker[param2.speakerIndex - 1].textDialogue);
            }
            else
            {
                EndDialogue(param1, param2, mainSpeaker.textDialogue);
            }
            EvnetManager.Instance.PostListener(EVENT_TYPE.StopDia, this);
        }
        if (isChooise) return;
     
        Debug.Log(param1);
        Debug.Log(param2);
        //캐릭터 초기화
        if (param2.speakerIndex > 0 && param2.multiData.Length <= 0)
        {
            Debug.Log("첫번째");
            if (param2.spriteRenderer != null)
                speaker[param2.speakerIndex - 1].spriteRenderer.sprite = param2.spriteRenderer;
        }
        //멀티 선택지 선택
        if (param2.multiData.Length > 0)
        {
            Debug.Log("두번째");
            isChooise = true;
            SetupButton(param2.multiData, param2);
            return;
        }
        //아무것도 아니면 그냥 대화 시작
        if (param2.speakerIndex > 0)
        {
            Debug.Log("첫번째 캐릭");
            StartCoroutine(OnTypingText(param1, param2, speaker[param2.speakerIndex - 1].textDialogue));
        }
        else
        {
            Debug.Log("두번째 캐릭");
            StartCoroutine(OnTypingText(param1, param2, mainSpeaker.textDialogue));
        }
        currentData = dataQueue.Dequeue();

    }


    private IEnumerator OnTypingText(int value, DialogData dia, TextMeshProUGUI gui)
    {
        //if (isTyping) yield break; // 중복 방지
        Debug.Log("실행이 되어라 하느니라");
        Debug.Log(value);
        isTyping = true;
        gui.text = "";
        foreach (char s in dia.dialogue[value].ToCharArray())
        {
            gui.text += s;
            yield return new WaitForSeconds(typingSpeed);
        }
        nextBtn.SetActive(true);
        isTyping = false;
    }

    private void SetupButton(MultipleData[] data, DialogData date)
    {
        //chooise1.onClick.RemoveAllListeners();
        //chooise2.onClick.RemoveAllListeners();
        //chooise3.onClick.RemoveAllListeners();

        if (data.Length > 0)
        {
            chooise1.gameObject.SetActive(true);
            chooise1.GetComponentInChildren<TextMeshProUGUI>().text = data[0].dialogue;
            chooise1.onClick.AddListener(() => OnChooiseSelect(data[0].dataNum, date));
        }
        if (data.Length > 1)
        {
            chooise2.gameObject.SetActive(true);
            chooise2.GetComponentInChildren<TextMeshProUGUI>().text = data[1].dialogue;
            chooise2.onClick.AddListener(() => OnChooiseSelect(data[1].dataNum, date));
        }
        if (data.Length > 2)
        {
            chooise3.gameObject.SetActive(true);
            chooise3.GetComponentInChildren<TextMeshProUGUI>().text = data[2].dialogue;
            chooise3.onClick.AddListener(() => OnChooiseSelect(data[2].dataNum, date));
        }
    }

    private void OnChooiseSelect(int dataNum, DialogData data)
    {
        isChooise = false;
        chooise1.onClick.RemoveAllListeners();
        chooise2.onClick.RemoveAllListeners();
        chooise3.onClick.RemoveAllListeners();

        chooise1.gameObject.SetActive(false);
        chooise2.gameObject.SetActive(false);
        chooise3.gameObject.SetActive(false);
        if (data.speakerIndex > 0)
        {
          
            if (data.spriteRenderer != null)
                speaker[data.speakerIndex - 1].spriteRenderer.sprite = data.spriteRenderer;
        }
        StartCoroutine(OnTypingText(dataNum, data, speaker[data.speakerIndex - 1].textDialogue));
        currentData = dataQueue.Dequeue();
    }

    private void EndDialogue(int value, DialogData dia, TextMeshProUGUI gui)
    {
       //마지막 데이터를 얻어와서 말을 한다 그리고 마지막에 하고 싶은 거 만들기
       StartCoroutine(OnTypingText(value, dia, gui));
        // 밑에 하고 싶은 거
    }

    private void StopDialogue(object param)
    {
        canChat = false;
    }
}

[System.Serializable]
public struct Speaker
{
    public GameObject textBox;
    public Image spriteRenderer;
    public Image imageDialog;
    public TextMeshProUGUI textName;
    public TextMeshProUGUI textDialogue;

}
[System.Serializable]

public struct MainSpeaker
{
    public GameObject textBox;
    public TextMeshProUGUI textName;
    public TextMeshProUGUI textDialogue;
    public GameObject objectArrow;

}
[System.Serializable]
public struct DialogData
{
    public int speakerIndex;
    public Sprite spriteRenderer;
    public string name;
    [TextArea(3, 5)]
    public string[] dialogue;
    public MultipleData[] multiData;
}
[System.Serializable]
public struct MultipleData
{
    public int dataNum;
    public string dialogue;
}
