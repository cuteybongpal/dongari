using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LikeabilityText : MonoBehaviour
{
    private TextMeshProUGUI textMeshPro;

    private void Awake()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
    }

    public void SetText(string text)
    {
        textMeshPro.text = text;
    }

    public void SetColor(Color color)
    {
        textMeshPro.color = color;
    }
}
