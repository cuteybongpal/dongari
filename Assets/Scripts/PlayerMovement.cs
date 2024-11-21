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
    // 기본 PlayerMovement
    void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 inputDirection = new Vector3(horizontalInput, 0, verticalInput).normalized;

        if (inputDirection.magnitude >= 0.1f) // 입력이 있을 때만
        {
            // 카메라 방향 기준으로 이동 방향 계산
            Vector3 cameraForward = cameraTransform.forward;
            Vector3 cameraRight = cameraTransform.right;

            cameraForward.y = 0;
            cameraRight.y = 0;
            cameraForward.Normalize();
            cameraRight.Normalize();

            // 카메라 기준 이동 방향
            Vector3 moveDirection = cameraForward * inputDirection.z + cameraRight * inputDirection.x;

            // 이동 속도
            Vector3 targetVelocity = moveDirection * speed;

            // SmoothDamp를 사용하여 부드럽게 이동 처리
            rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, smoothTime);
        }
        else
        {
            // 입력이 없으면 감속
            rb.velocity = Vector3.SmoothDamp(rb.velocity, Vector3.zero, ref velocity, smoothTime);
        }

        // 속도가 일정 threshold 이하로 떨어지면 정확히 0으로 설정
        if (rb.velocity.magnitude < stopMinimum)
        {
            rb.velocity = Vector3.zero;
        }

    }
}
