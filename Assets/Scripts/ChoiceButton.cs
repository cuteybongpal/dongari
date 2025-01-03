using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChoiceButton : MonoBehaviour
{
    [SerializeField]
    private Image choiceButtonImage;
    [SerializeField]
    private TextMeshProUGUI choiceText;

    [SerializeField]
    private Color hoverImgaeColor;
    [SerializeField]
    private Color hoverTextColor;

    private Color defaultImageColor;
    private Color defaultTextColor;

    public bool IsChoice { get; private set; }

    private void Start()
    {
        defaultImageColor = choiceButtonImage.color;
        defaultTextColor = choiceText.color;
        EventTrigger eventTrigger = GetComponent<EventTrigger>();
        EventTrigger.Entry pointerEnter = new EventTrigger.Entry { eventID = EventTriggerType.PointerEnter };
        EventTrigger.Entry pointerExit = new EventTrigger.Entry { eventID = EventTriggerType.PointerExit };
        EventTrigger.Entry pointerDown = new EventTrigger.Entry { eventID = EventTriggerType.PointerDown };

        pointerEnter.callback.AddListener((data) => { SetHoverColor(); });
        pointerExit.callback.AddListener((data) => { SetDefaultColor(); });

        pointerDown.callback.AddListener((data) => { IsChoice = true; });

        eventTrigger.triggers.Add(pointerEnter);
        eventTrigger.triggers.Add(pointerExit);
        eventTrigger.triggers.Add(pointerDown);

        Hide();
    }

    private void SetDefaultColor()
    {
        choiceButtonImage.color = defaultImageColor;
        choiceText.color = defaultTextColor;
    }

    private void SetHoverColor()
    {
        choiceButtonImage.color = hoverImgaeColor;
        choiceText.color = hoverTextColor;
    }

    public void Show(string choice)
    {
        IsChoice = false;
        choiceText.text = choice;
        SetDefaultColor();
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        IsChoice = false;
        gameObject.SetActive(false);
    }
}
