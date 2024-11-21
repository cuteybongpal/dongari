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

    // �浹�� �߻����� �� ȣ��˴ϴ�.
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")) // �÷��̾�� �浹�� ��
        {
            isInRange = true;
            if (targetMaterial != null)
            {
                targetMaterial.SetColor("_EmissionColor", highlightColor);
                DynamicGI.SetEmissive(targetRenderer, highlightColor);
            }
        }
    }

    // �浹�� ������ �� ȣ��˴ϴ�.
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")) // �÷��̾���� �浹�� ���� ��
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
