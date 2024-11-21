using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName ="DialogueData", menuName ="StringData", order = 1)]
public class DialogueStringData : ScriptableObject
{
    [TextArea(3,10)]
    public string[] dialgueString;
}
public class ScreenControal : MonoBehaviour, ILinerEvnet
{
    public DialogueStringData data;
    private Queue<string> stringData = new Queue<string>();   //스크립터블 배열의 값들

    public TextMeshProUGUI text;        // 가운데 텍스트
    public Image blackImage;            //검정 배경
    public float typingSpeed = 0.05f;   // 타이핑 속도
    private bool isTyping;              // 타이핑 체크
    public float fadeOutSpeed = 0.5f;

    private void Start()
    {
        //처음 시작할때 검정 배경 나오게하기
        EvnetManager.Instance.AddListener(EVENT_TYPE.BlackScreen, this);
        // -------------------------------------
        Color initialColor = blackImage.color;
        initialColor.a = 1f; // 초기 알파 값 설정
        blackImage.color = initialColor;
        blackImage.gameObject.SetActive(true);
        text.gameObject.SetActive(true );
        // -------------------------------------
        EvnetManager.Instance.PostListener(EVENT_TYPE.BlackScreen, this, data, 2);
    }

    public void OnEvnet(EVENT_TYPE type, Component sender, object param1 = null, object param2 = null)
    {
        //값 1은 스크립터블 데이터
        // 값 2는 어떤 fade사용인지(1, 2)로 결정
        switch (type)
        {
            case EVENT_TYPE.BlackScreen:
                if (param1 is DialogueStringData data && param2 is int intiger)
                {
                    SetupString(data, intiger); // 형변환 성공 시 메서드 호출
                }
                break;
        }
    }

    private void SetupString(DialogueStringData data, int intiger)
    {
        if (data.dialgueString.Length > 0)
        {
            //스크립터블 배열의 값들을 얻어옴
            foreach (string s in data.dialgueString)
            {
                stringData.Enqueue(s);
            }
            StartCoroutine(StartCenterChat(intiger));
        }
    }
    private IEnumerator StartCenterChat(int intiger)
    {
        if(intiger == 1)
        {
            StartCoroutine(FadeInImage());
            yield return StartCoroutine(FadeInImage());
        }
        while (stringData.Count > 0) // 큐가 비어있을 때까지 반복
        {
            if (!isTyping)
            {
                string data = stringData.Dequeue(); // 큐에서 데이터 추출
                yield return StartCoroutine(OnTyping(data)); // 타이핑 완료 대기
                yield return new WaitUntil(() => Input.GetMouseButtonDown(0)); // 클릭 대기
            }
        }
      if(intiger == 2)
        {
            StartCoroutine(FadeOutImage());
            yield return StartCoroutine(FadeOutImage());
        }

        EvnetManager.Instance.PostListener(EVENT_TYPE.StartDia, this);

    }
    private IEnumerator OnTyping(string data)
    {
        isTyping = true;
        text.text = "";
        foreach(char c in data.ToCharArray())
        {
            text.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }
        isTyping = false;
    }

    private IEnumerator FadeOutImage()
    {
        text.gameObject.SetActive(false);
        Color imageColor = blackImage.color;

        while (imageColor.a > 0f) // 투명도가 0에 도달할 때까지
        {
            imageColor.a -= Time.deltaTime * fadeOutSpeed;
            blackImage.color = imageColor;
            yield return null;
        }

        blackImage.gameObject.SetActive(false);
    }
    private IEnumerator FadeInImage()
    {
        blackImage.gameObject.SetActive(true); // 다시 활성화
        Color imageColor = blackImage.color;

        while (imageColor.a < 1f) // 투명도가 1에 도달할 때까지
        {
            imageColor.a += Time.deltaTime * fadeOutSpeed;
            blackImage.color = imageColor;
            yield return null;
        }
        text.gameObject.SetActive(true);
    }
}
