using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // 몬스터에게 줄 데미지
    public int damage = 10;
    // 몬스터 넉백 Vector
    public Vector2 knockBack = Vector2.zero;
    // 이동 속도
    public Vector2 moveSpeed = new Vector2(3.0f, 0);

    // Rigidbody
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        rb.velocity = new Vector2(moveSpeed.x * transform.localScale.x, moveSpeed.y); 
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();
        if (damage != null)
        {
            Vector2 deliveredKnockBack = transform.localEulerAngles.x > 0 ?
                knockBack : new Vector2(-knockBack.x, knockBack.y);
        }
    }
}
