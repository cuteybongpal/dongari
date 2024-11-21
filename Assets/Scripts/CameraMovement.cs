using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform player; // 플레이어의 Transform
    public Vector3 offset = new Vector3(-0.5f, 2.3f, -0.5f); // 카메라와 플레이어 사이의 거리
    public float smoothSpeed = 0.125f; // 카메라 이동의 부드러움
    void LateUpdate()
    {
        Vector3 calcPosition = player.position + offset;
        transform.position = Vector3.Lerp(transform.position, calcPosition, smoothSpeed);
        transform.LookAt(player);
    }
}
