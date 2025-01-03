using System.Collections;
using TMPro;
using UnityEngine;

public class DialogText : MonoBehaviour
{
    bool isTypingSkip;
    private TextMeshProUGUI textMeshPro;

    private void Awake()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isTypingSkip = true;
        }
    }

    public void SetText(string text)
    {
        textMeshPro.text = text;
    }

    public IEnumerator Typing(string sentence, float typingDelay)
    {
        textMeshPro.text = "";
        foreach (char c in sentence)
        {
            isTypingSkip = false;
            yield return new WaitForSeconds(typingDelay);
            if (isTypingSkip)
            {
                textMeshPro.text = sentence;
                break;
            }
            textMeshPro.text += c;
        }
    }

}
