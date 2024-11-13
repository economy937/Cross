using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.ParticleSystem;

public interface IHit
{
    public void GetHit();
}


public class PlayerMovement : MonoBehaviour, IHit
{
    private PlayerControls playerControls;
    private Vector3 targetPosition;
    private Quaternion targetRotation;
    public float moveSpeed = 5f;
    public float rotateSpeed = 10f;

    public bool isDead = false;
    private void Start()
    {
        targetPosition = transform.position;
        targetRotation = transform.rotation;
    }

    private void OnEnable()
    {
        playerControls = new PlayerControls();
        playerControls.Enable();

        // 각 방향별로 입력 이벤트 연결
        playerControls.Movement.MoveUp.performed += ctx => SetTarget(Vector3.forward);
        playerControls.Movement.MoveDown.performed += ctx => SetTarget(Vector3.back);
        playerControls.Movement.MoveLeft.performed += ctx => SetTarget(Vector3.left);
        playerControls.Movement.MoveRight.performed += ctx => SetTarget(Vector3.right);
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    private void SetTarget(Vector3 direction)
    {
        // 목표 위치와 방향을 설정
        targetPosition = transform.position + direction;
        targetRotation = Quaternion.LookRotation(direction);
    }

    private void Update()
    {
        // Lerp를 사용하여 점진적으로 이동
        transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // Slerp를 사용하여 점진적으로 회전
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
    }
    public void GetHit()
    {
        GameManager.instance.GameOver();
        isDead = true;
    }
}
