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

    // 현재 사용할 Speed 값을 반환하는 프로퍼티
    public float CurrentMoveSpeed
    {
        get
        {
            if (!animator.GetBool(AnimationStrings.CanMove))
            {
                return 0;
            }
            // 움직이지 않는 다면 0을 반환
            // 만약에 벽에 붙어 있다면 0을 반환
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
            // 만약에 뛰고 있다면 _runningSpeed를 반환
            if (IsRunning)
            {
                return _runningSpeed;
            }
            return _walkSpeed;
        }
    }

    //이동 방향(플레이어의 키 입력에 따른)
    Vector2 moveInput;

    // 프로퍼티
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
            // 만약 기존에 오른쪽을 바라보고 있었는 데 왼쪽 방향키를 누르면
            if (_isFacingRight != value) // 조건 True가 된다.
            {
                // 현재 LocalScale 값의 X 좌표를 -1로 곱한다.
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
        // 오른쪽 키를 눌렀는데 오른쪽을 바라보는 상태가 아니면
        if (moveInput.x > 0 && !IsFacingRight)
        {
            IsFacingRight = true;
        }
        // 왼쪽 키를 눌렀는데 오른쪽을 바라보는 상태라면
        else if (moveInput.x < 0 && IsFacingRight)
        {
            IsFacingRight = false;
        }
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        // Left Shift 버튼을 누르거나 뗐을 때 IsRunning 프로퍼티를 처리한다.
        // 버튼을 처음 눌렀을 때 true를 반환
        if (context.started)
        {
            IsRunning = true;
        }
        // 버튼에서 손을 뗐을 때 true를 반환
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
            // 공격 애니메이션 재생
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
