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
    private Queue<string> stringData = new Queue<string>();   //��ũ���ͺ� �迭�� ����

    public TextMeshProUGUI text;        // ��� �ؽ�Ʈ
    public Image blackImage;            //���� ���
    public float typingSpeed = 0.05f;   // Ÿ���� �ӵ�
    private bool isTyping;              // Ÿ���� üũ
    public float fadeOutSpeed = 0.5f;

    private void Start()
    {
        //ó�� �����Ҷ� ���� ��� �������ϱ�
        EvnetManager.Instance.AddListener(EVENT_TYPE.BlackScreen, this);
        // -------------------------------------
        Color initialColor = blackImage.color;
        initialColor.a = 1f; // �ʱ� ���� �� ����
        blackImage.color = initialColor;
        blackImage.gameObject.SetActive(true);
        text.gameObject.SetActive(true );
        // -------------------------------------
        EvnetManager.Instance.PostListener(EVENT_TYPE.BlackScreen, this, data, 2);
    }

    public void OnEvnet(EVENT_TYPE type, Component sender, object param1 = null, object param2 = null)
    {
        //�� 1�� ��ũ���ͺ� ������
        // �� 2�� � fade�������(1, 2)�� ����
        switch (type)
        {
            case EVENT_TYPE.BlackScreen:
                if (param1 is DialogueStringData data && param2 is int intiger)
                {
                    SetupString(data, intiger); // ����ȯ ���� �� �޼��� ȣ��
                }
                break;
        }
    }

    private void SetupString(DialogueStringData data, int intiger)
    {
        if (data.dialgueString.Length > 0)
        {
            //��ũ���ͺ� �迭�� ������ ����
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
        while (stringData.Count > 0) // ť�� ������� ������ �ݺ�
        {
            if (!isTyping)
            {
                string data = stringData.Dequeue(); // ť���� ������ ����
                yield return StartCoroutine(OnTyping(data)); // Ÿ���� �Ϸ� ���
                yield return new WaitUntil(() => Input.GetMouseButtonDown(0)); // Ŭ�� ���
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

        while (imageColor.a > 0f) // ������ 0�� ������ ������
        {
            imageColor.a -= Time.deltaTime * fadeOutSpeed;
            blackImage.color = imageColor;
            yield return null;
        }

        blackImage.gameObject.SetActive(false);
    }
    private IEnumerator FadeInImage()
    {
        blackImage.gameObject.SetActive(true); // �ٽ� Ȱ��ȭ
        Color imageColor = blackImage.color;

        while (imageColor.a < 1f) // ������ 1�� ������ ������
        {
            imageColor.a += Time.deltaTime * fadeOutSpeed;
            blackImage.color = imageColor;
            yield return null;
        }
        text.gameObject.SetActive(true);
    }
}
