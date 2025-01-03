using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NameText : MonoBehaviour
{
    private TextMeshProUGUI textMeshPro;

    private void Awake()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
    }

    public void SetName(string name)
    {
        textMeshPro.text = name;
    }

    public void SetColor(Color color)
    {
        textMeshPro.color = color;
    }
}
