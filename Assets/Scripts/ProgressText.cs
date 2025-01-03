using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class ProgressText : MonoBehaviour
{
    private TextMeshProUGUI textMeshPro;

    private void Start()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
        StartCoroutine(FadeOut(0f));
    }

    public IEnumerator FadeOut(float fadeTime)
    {
        Color color = textMeshPro.color;

        for (float timer = 0f; timer < fadeTime; timer += Time.deltaTime)
        {
            color.a = 1f - timer / fadeTime;
            textMeshPro.color = color;
            yield return null;
        }

        color.a = 0f;
        textMeshPro.color = color;
    }

    public IEnumerator FadeIn(float fadeTime)
    {
        Color color = textMeshPro.color;

        for (float timer = 0f; timer < fadeTime; timer += Time.deltaTime)
        {
            color.a = timer / fadeTime;
            textMeshPro.color = color;
            yield return null;
        }

        color.a = 1f;
        textMeshPro.color = color;
    }
}
