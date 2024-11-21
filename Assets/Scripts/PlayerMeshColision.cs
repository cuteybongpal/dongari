using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeshColision : MonoBehaviour
{
    public float groundCheckDistance = 0.1f;
    private void Start()
    {
        CapsuleCollider collider = GetComponent<CapsuleCollider>();
        if (collider != null)
        {
            collider.center = new Vector3(0, -0.5f, 0);  // Collider�� �߽��� �� �� �κп� ����
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground")) // ����� �浹 ��
        {
            // ���� ���� ������ �÷��̾��� Y ��ġ�� ����
            Vector3 playerPosition = transform.position;
            playerPosition.y = other.bounds.max.y; // Ground ������Ʈ�� �ִ� Y������ ����
            transform.position = playerPosition;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Ground")) // ����� ��� ���� ���� ��
        {
            // ���������� �÷��̾ ���� ���� ���� (�ʿ��)
            Vector3 playerPosition = transform.position;
            playerPosition.y = other.bounds.max.y;
            transform.position = playerPosition;
        }
    }
}
