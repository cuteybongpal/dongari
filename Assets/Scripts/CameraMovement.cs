using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform player; // �÷��̾��� Transform
    public Vector3 offset = new Vector3(-0.5f, 2.3f, -0.5f); // ī�޶�� �÷��̾� ������ �Ÿ�
    public float smoothSpeed = 0.125f; // ī�޶� �̵��� �ε巯��
    void LateUpdate()
    {
        Vector3 calcPosition = player.position + offset;
        transform.position = Vector3.Lerp(transform.position, calcPosition, smoothSpeed);
        transform.LookAt(player);
    }
}
