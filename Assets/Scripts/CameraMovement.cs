using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform targetPlayer; // ���� ī�޶� ���� �÷��̾� (�ʱⰪ ����)
    public Vector3 offset; // ī�޶�� �÷��̾� ������ �Ÿ� (Unity �����Ϳ��� ����)
    public float smoothSpeed; // ī�޶� �̵��� �ε巯�� (Unity �����Ϳ��� ����)

    void LateUpdate()
    {
        if (targetPlayer != null)
        {
            Vector3 calcPosition = targetPlayer.position + offset;
            transform.position = Vector3.Lerp(transform.position, calcPosition, smoothSpeed);
            transform.LookAt(targetPlayer);
        }
    }

    // objectId�� ������� Ÿ�� �÷��̾ �����ϴ� �Լ�
    // entityId�� ������� Ÿ�� �÷��̾ �����ϴ� �Լ�
    public void SetTargetByEntityId(int entityId)
    {
        // ���� ���� ��� ObjectData �˻�
        ObjectData[] allObjects = FindObjectsOfType<ObjectData>();
        foreach (ObjectData obj in allObjects)
        {
            if (obj.entityId == entityId)
            {
                targetPlayer = obj.transform; // entityId�� ��ġ�ϴ� Transform ����
                return;
            }
        }

        Debug.LogWarning($"No object found with entityId {entityId}.");
    }
}
