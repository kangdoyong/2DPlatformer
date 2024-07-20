using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 벽, 천장, 땅에 닿았는지를 체크하는 클래스
public class TouchingDirection : MonoBehaviour
{
    public ContactFilter2D contactFilter; // 충돌 체크 오브젝트 필터링
    public float groundDistance = 0.05f; // 땅을 체크할 거리
    public float wallDistance = 0.2f; // 벽을 체크할 거리
    public float ceilingDistance = 0.05f; // 천장과 체크할 거리

    private bool _isGrounded = true;
    public bool IsGrounded
    {
        get { return _isGrounded; } 
        set 
        {
            _isGrounded = value;
            animator.SetBool(AnimationStrings.IsGrounded, _isGrounded);
        }
    }

    private bool _isOnWall = false;
    public bool IsOnWall
    {
        get { return _isOnWall; }
        set
        {
            _isOnWall = value;
            animator.SetBool(AnimationStrings.IsOnWall, _isOnWall);
        }
    }

    private bool _isOnCeiling = false;
    
    public bool IsOnCeiling
    {
        get { return _isOnCeiling; }
        set
        {
            _isOnCeiling= value;
            animator.SetBool(AnimationStrings.IsOnCeiling, _isOnCeiling);
        }
    }

    private Vector2 wallCheckDirection => transform.localScale.x > 0 ? Vector2.right : Vector2.left;

    RaycastHit2D[] groundHits = new RaycastHit2D[5];
    RaycastHit2D[] wallHits = new RaycastHit2D[5];
    RaycastHit2D[] ceilingHits = new RaycastHit2D[5];

    private Animator animator;
    private CapsuleCollider2D touchingCollider;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        touchingCollider = GetComponent<CapsuleCollider2D>();
    }

    private void FixedUpdate()
    {
        // 아래 방향으로 Collider를 보내서 해당 위치에 오브젝트가 충돌 되는지 확인하는 코드
        IsGrounded = touchingCollider.Cast(Vector2.down, contactFilter, groundHits, groundDistance) > 0;
        // 왼쪽, 오른쪽 방향으로 Collider를 보내서 해당 위치에 오브젝트가 충돌 되는지 확인하는 코드
        IsOnWall = touchingCollider.Cast(wallCheckDirection, contactFilter, wallHits, wallDistance) > 0;
        // 윗 방향으로 Collider를 보내서 해당 위치에 오브젝트가 충돌 되는지 확인하는 코드
        IsOnCeiling = touchingCollider.Cast(Vector2.up, contactFilter, ceilingHits, ceilingDistance) > 0;
    }
}
