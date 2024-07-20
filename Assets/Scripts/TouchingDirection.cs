using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��, õ��, ���� ��Ҵ����� üũ�ϴ� Ŭ����
public class TouchingDirection : MonoBehaviour
{
    public ContactFilter2D contactFilter; // �浹 üũ ������Ʈ ���͸�
    public float groundDistance = 0.05f; // ���� üũ�� �Ÿ�
    public float wallDistance = 0.2f; // ���� üũ�� �Ÿ�
    public float ceilingDistance = 0.05f; // õ��� üũ�� �Ÿ�

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
        // �Ʒ� �������� Collider�� ������ �ش� ��ġ�� ������Ʈ�� �浹 �Ǵ��� Ȯ���ϴ� �ڵ�
        IsGrounded = touchingCollider.Cast(Vector2.down, contactFilter, groundHits, groundDistance) > 0;
        // ����, ������ �������� Collider�� ������ �ش� ��ġ�� ������Ʈ�� �浹 �Ǵ��� Ȯ���ϴ� �ڵ�
        IsOnWall = touchingCollider.Cast(wallCheckDirection, contactFilter, wallHits, wallDistance) > 0;
        // �� �������� Collider�� ������ �ش� ��ġ�� ������Ʈ�� �浹 �Ǵ��� Ȯ���ϴ� �ڵ�
        IsOnCeiling = touchingCollider.Cast(Vector2.up, contactFilter, ceilingHits, ceilingDistance) > 0;
    }
}
