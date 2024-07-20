using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    public UnityEvent<Vector2> KnockbackEvent;
    public UnityEvent<float, float> HealthChangeEvent;
    public bool LockVelocity
    {
        get { return animator.GetBool(AnimationStrings.LockVelocity); }
        set { animator.SetBool(AnimationStrings.LockVelocity, value); }
    }
    [SerializeField] private float _maxHealth = 100;
    [SerializeField] private float _health = 100;
    [SerializeField] private bool _isAlive = true;

    [SerializeField] private bool isInvincible = false;
    [SerializeField] private float invincibilityTime = 0.5f;

    private Animator animator;
    private float timeSinceHit = 0;

    /// <summary>
    /// 최대 체력
    /// </summary>
    public float MaxHealth
    {   get { return _maxHealth; }
        set { _maxHealth = value; } 
    }

    public float Health
    {
        get { return _health; }
        set
        {
            _health = value;
            if (_health <= 0)
            {
                _isAlive = false;
            }
        }
    }

    public bool IsAlive
    {   get { return _isAlive; }
        set 
        {
            _isAlive = value;
            // 죽는 애니메이션 처리
            animator.SetBool(AnimationStrings.IsAlive, _isAlive);
        }
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            GetHit(33);
        }

        // 현재 무적 상태이다.
        if (isInvincible)
        {
            // timeSinceHit이 지정된 무적 시간보다 커진 경우
            if (timeSinceHit > invincibilityTime)
            {
                // 무적 상태를 false로 변환하고
                isInvincible = false;
                // timeSinceHit를 초기화한다.
                timeSinceHit = 0;
            }
            // 매 프레임마다 Time.deltaTime을 더해준다.
            timeSinceHit += Time.deltaTime;
            return;
        }

    }

    /// <summary>
    /// 데미지를 추가하는 함수
    /// </summary>
    /// <param name="damage"></param>
    /// <returns></returns>
    public bool GetHit(int damage)
    {
        if (IsAlive && isInvincible)
        {
            Health -= damage;
            isInvincible = true;
        }
        return false;
    }

    public bool GetHit(int damage, Vector2 knockback)
    {
        if(IsAlive && isInvincible)
        {
            Health -= damage;
            isInvincible = true;

            animator.SetTrigger(AnimationStrings.Hit);

            HealthEvents.characterDamaged?.Invoke(gameObject,damage);

            LockVelocity = true;

            KnockbackEvent.Invoke(knockback);
            HealthChangeEvent.Invoke(Health, MaxHealth);

            return true;
        }
        return false;
    }
   
    public bool Heal(int healthRestore)
    {
        if(IsAlive && Health < MaxHealth)
        {
            // 내가 실제로 체력을 얼마큼 회복했는지에 대한 값이 없다
            //Health += healthRestore;
            //if(Health > MaxHealth)
            //   Health = MaxHealth;

            // 체력을 최대 얼마큼 회복시킬 수 있는지에 대한 값
            float maxHeal = Mathf.Max(MaxHealth - Health, 0);
            // 실제로 체력을 회복시킬수 있는 값
            float actualHeal = Mathf.Min(maxHeal, healthRestore);
            Health += actualHeal;

            HealthChangeEvent.Invoke(Health, MaxHealth);
            HealthEvents.characterHealed.Invoke(gameObject, (int)actualHeal);
            return true;
        }
        return false;
    }

}