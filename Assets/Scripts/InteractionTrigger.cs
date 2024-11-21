using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionTrigger : MonoBehaviour
{
    public GameObject targetObject;
    
    private Renderer targetRenderer;
    private Material targetMaterial;
    public Color highlightColor = Color.red;
    private Color originColor;
    public float interactionDistance;
    private bool isInRange = false;
    /*public ChatManager chatManager;*/
    public GameObject chatPanel;

    private void Start()
    {
        targetRenderer = targetObject.GetComponent<Renderer>();
        if (targetRenderer != null)
        {
            targetMaterial = targetRenderer.material;
            targetMaterial.EnableKeyword("_EMISSION");
            originColor = targetMaterial.GetColor("_EmissionColor");    
        }
    }

    private void Update()
    {
        if (isInRange && Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }

    // 충돌이 발생했을 때 호출됩니다.
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")) // 플레이어와 충돌할 때
        {
            isInRange = true;
            if (targetMaterial != null)
            {
                targetMaterial.SetColor("_EmissionColor", highlightColor);
                DynamicGI.SetEmissive(targetRenderer, highlightColor);
            }
        }
    }

    // 충돌이 끝났을 때 호출됩니다.
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")) // 플레이어와의 충돌이 끝날 때
        {
            isInRange = false;
            if (targetMaterial != null)
            {
                targetMaterial.SetColor("_EmissionColor", originColor);
                DynamicGI.SetEmissive(targetRenderer, originColor);
            }
        }
    }

    private void Interact()
    {
        /*chatManager.Action(targetObject);*/
        chatPanel.SetActive(true);
        Debug.Log("Interaction On!");
    }
}
