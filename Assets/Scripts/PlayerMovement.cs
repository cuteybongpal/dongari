using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Transform cameraTransform;
    public float speed = 5f;
    public float smoothSpeed = 0.1f;
    public float stopMinimum = 0.1f;
    public float smoothTime = 0.1f;
    private Rigidbody rb;

    private Vector3 velocity = Vector3.zero;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    // �⺻ PlayerMovement
    void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 inputDirection = new Vector3(horizontalInput, 0, verticalInput).normalized;

        if (inputDirection.magnitude >= 0.1f) // �Է��� ���� ����
        {
            // ī�޶� ���� �������� �̵� ���� ���
            Vector3 cameraForward = cameraTransform.forward;
            Vector3 cameraRight = cameraTransform.right;

            cameraForward.y = 0;
            cameraRight.y = 0;
            cameraForward.Normalize();
            cameraRight.Normalize();

            // ī�޶� ���� �̵� ����
            Vector3 moveDirection = cameraForward * inputDirection.z + cameraRight * inputDirection.x;

            // �̵� �ӵ�
            Vector3 targetVelocity = moveDirection * speed;

            // SmoothDamp�� ����Ͽ� �ε巴�� �̵� ó��
            rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, smoothTime);
        }
        else
        {
            // �Է��� ������ ����
            rb.velocity = Vector3.SmoothDamp(rb.velocity, Vector3.zero, ref velocity, smoothTime);
        }

        // �ӵ��� ���� threshold ���Ϸ� �������� ��Ȯ�� 0���� ����
        if (rb.velocity.magnitude < stopMinimum)
        {
            rb.velocity = Vector3.zero;
        }

    }
}
