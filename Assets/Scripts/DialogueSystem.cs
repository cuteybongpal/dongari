using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct Dialogue
{
    public string name;

    [TextArea(1, 10)]
    public string[] sentences;
}


public class DialogueSystem : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;

    public Dialogue[] dialogues;

    int currentDialogueIndex = 0;

    float typingSpeed = 0.1f;
    bool isTyping = false;

    private void Start()
    {
        StartCoroutine(DialogueCoroutine());
    }

    IEnumerator DialogueCoroutine()
    {
        foreach (Dialogue currentDialogue in dialogues)
        {
            nameText.text = currentDialogue.name;
            foreach (string currentSentence in  currentDialogue.sentences)
            {
                StartCoroutine(TypingCoroutine(currentSentence));
                while (isTyping)
                {
                    yield return null;
                }
                while (!Input.GetMouseButtonDown(0))
                {
                    yield return null;
                }
            }
        }
    }

    IEnumerator TypingCoroutine(string text)
    {
        isTyping = true;
        dialogueText.text = "";
        foreach (char c in text)
        {
            yield return new WaitForSeconds(typingSpeed);
            dialogueText.text += c;
        }
        isTyping = false;
    }
}


