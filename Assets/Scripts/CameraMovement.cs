using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform targetPlayer; // 현재 카메라가 따라갈 플레이어 (초기값 없음)
    public Vector3 offset; // 카메라와 플레이어 사이의 거리 (Unity 에디터에서 설정)
    public float smoothSpeed; // 카메라 이동의 부드러움 (Unity 에디터에서 설정)

    void LateUpdate()
    {
        if (targetPlayer != null)
        {
            Vector3 calcPosition = targetPlayer.position + offset;
            transform.position = Vector3.Lerp(transform.position, calcPosition, smoothSpeed);
            transform.LookAt(targetPlayer);
        }
    }

    // objectId를 기반으로 타겟 플레이어를 설정하는 함수
    // entityId를 기반으로 타겟 플레이어를 설정하는 함수
    public void SetTargetByEntityId(int entityId)
    {
        // 현재 씬의 모든 ObjectData 검색
        ObjectData[] allObjects = FindObjectsOfType<ObjectData>();
        foreach (ObjectData obj in allObjects)
        {
            if (obj.entityId == entityId)
            {
                targetPlayer = obj.transform; // entityId가 일치하는 Transform 설정
                return;
            }
        }

        Debug.LogWarning($"No object found with entityId {entityId}.");
    }
}
