using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScreenOverlay : MonoBehaviour
{
    private Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void SetAlpha(float alpha)
    {
        Color color = image.color;
        color.a = alpha;
        image.color = color;
    }

    public IEnumerator FadeIn(float fadeTime)
    {
        Color color = image.color;
        float _timer = 0f;
        while (_timer < fadeTime)
        {
            color.a = 1f - _timer / fadeTime;
            image.color = color;
            yield return null;
            _timer += Time.deltaTime;
        }
    }

    public IEnumerator FadeOut(float fadeTime)
    {
        Color color = image.color;
        float _timer = 0f;
        while (_timer < fadeTime)
        {
            color.a = _timer / fadeTime;
            image.color = color;
            yield return null;
            _timer += Time.deltaTime;
        }
    }
}
