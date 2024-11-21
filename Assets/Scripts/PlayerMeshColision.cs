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
            collider.center = new Vector3(0, -0.5f, 0);  // Collider의 중심을 발 끝 부분에 맞춤
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground")) // 지면과 충돌 시
        {
            // 지면 위로 강제로 플레이어의 Y 위치를 조정
            Vector3 playerPosition = transform.position;
            playerPosition.y = other.bounds.max.y; // Ground 오브젝트의 최대 Y값으로 설정
            transform.position = playerPosition;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Ground")) // 지면과 계속 접촉 중일 때
        {
            // 지속적으로 플레이어를 지면 위로 조정 (필요시)
            Vector3 playerPosition = transform.position;
            playerPosition.y = other.bounds.max.y;
            transform.position = playerPosition;
        }
    }
}
