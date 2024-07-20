using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _walkSpeed = 5.0f;
    [SerializeField] private float _runningSpeed = 8.0f;
    [SerializeField] private float _airWalkSpeed = 3.0f;
    [SerializeField] private float _airRunningSpeed = 6.0f;
    [SerializeField] private float _jumpImpulse = 10f;

    // ���� ����� Speed ���� ��ȯ�ϴ� ������Ƽ
    public float CurrentMoveSpeed
    {
        get
        {
            if (!animator.GetBool(AnimationStrings.CanMove))
            {
                return 0;
            }
            // �������� �ʴ� �ٸ� 0�� ��ȯ
            // ���࿡ ���� �پ� �ִٸ� 0�� ��ȯ
            if (!IsMoving || touchingDirections.IsOnWall)
            {
                return 0;
            }
            if (!touchingDirections.IsGrounded)
            {
                if (IsRunning)
                    return _airRunningSpeed;
                return _airWalkSpeed;
            }
            // ���࿡ �ٰ� �ִٸ� _runningSpeed�� ��ȯ
            if (IsRunning)
            {
                return _runningSpeed;
            }
            return _walkSpeed;
        }
    }

    //�̵� ����(�÷��̾��� Ű �Է¿� ����)
    Vector2 moveInput;

    // ������Ƽ
    private bool _isMoving = false;
    public bool IsMoving
    {
        get { return _isMoving; }
        set
        {
            _isMoving = value;
            animator.SetBool(AnimationStrings.IsMoving, _isMoving);
        }
    }

    private bool _isRunning = false;
    public bool IsRunning
    {
        get { return _isRunning; }
        set
        {
            _isRunning = value;
            animator.SetBool(AnimationStrings.IsRunning, _isRunning);
        }
    }

    private bool _isFacingRight = true;
    public bool IsFacingRight
    {
        get { return _isFacingRight; }
        set
        {
            // ���� ������ �������� �ٶ󺸰� �־��� �� ���� ����Ű�� ������
            if (_isFacingRight != value) // ���� True�� �ȴ�.
            {
                // ���� LocalScale ���� X ��ǥ�� -1�� ���Ѵ�.
                transform.localScale *= new Vector2(-1, 1);
            }
            _isFacingRight = value;
        }
    }

    Rigidbody2D rb;
    Animator animator;
    TouchingDirection touchingDirections;
    Damageable damageable;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDirections = GetComponent<TouchingDirection>();
        damageable = GetComponent<Damageable>();

        damageable.KnockbackEvent.AddListener(OnKnockback);
    }

    private void FixedUpdate()
    {
        if (!damageable.IsAlive)
            return;
        if (!damageable.LockVelocity)
        {
            rb.velocity = new Vector3(moveInput.x * CurrentMoveSpeed, rb.velocity.y);
        }
        animator.SetFloat(AnimationStrings.yVelocity, rb.velocity.y);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

        IsMoving = moveInput != Vector2.zero;
        SetFacingDirection(moveInput);
    }

    private void SetFacingDirection(Vector2 moveInput)
    {
        if (!damageable.IsAlive)
            return;
        // ������ Ű�� �����µ� �������� �ٶ󺸴� ���°� �ƴϸ�
        if (moveInput.x > 0 && !IsFacingRight)
        {
            IsFacingRight = true;
        }
        // ���� Ű�� �����µ� �������� �ٶ󺸴� ���¶��
        else if (moveInput.x < 0 && IsFacingRight)
        {
            IsFacingRight = false;
        }
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        // Left Shift ��ư�� �����ų� ���� �� IsRunning ������Ƽ�� ó���Ѵ�.
        // ��ư�� ó�� ������ �� true�� ��ȯ
        if (context.started)
        {
            IsRunning = true;
        }
        // ��ư���� ���� ���� �� true�� ��ȯ
        else if (context.canceled)
        {
            IsRunning = false;
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started && touchingDirections.IsGrounded)
        {
            animator.SetTrigger(AnimationStrings.Jump);
            rb.velocity = new Vector2(rb.velocity.x, _jumpImpulse);
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started && touchingDirections.IsGrounded)
        {
            // ���� �ִϸ��̼� ���
            animator.SetTrigger(AnimationStrings.Attack);
        }
    }

    private void OnKnockback(Vector2 knockback)
    {
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        if(context.started && touchingDirections.IsGrounded)
        {
            animator.SetTrigger(AnimationStrings.Shoot);
        }
    }
}
