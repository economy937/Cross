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

        // �� ���⺰�� �Է� �̺�Ʈ ����
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
        // ��ǥ ��ġ�� ������ ����
        targetPosition = transform.position + direction;
        targetRotation = Quaternion.LookRotation(direction);
    }

    private void Update()
    {
        // Lerp�� ����Ͽ� ���������� �̵�
        transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // Slerp�� ����Ͽ� ���������� ȸ��
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
    }
    public void GetHit()
    {
        GameManager.instance.GameOver();
        isDead = true;
    }
}
