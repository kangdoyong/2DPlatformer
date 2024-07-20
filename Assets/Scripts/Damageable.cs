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
    /// �ִ� ü��
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
            // �״� �ִϸ��̼� ó��
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

        // ���� ���� �����̴�.
        if (isInvincible)
        {
            // timeSinceHit�� ������ ���� �ð����� Ŀ�� ���
            if (timeSinceHit > invincibilityTime)
            {
                // ���� ���¸� false�� ��ȯ�ϰ�
                isInvincible = false;
                // timeSinceHit�� �ʱ�ȭ�Ѵ�.
                timeSinceHit = 0;
            }
            // �� �����Ӹ��� Time.deltaTime�� �����ش�.
            timeSinceHit += Time.deltaTime;
            return;
        }

    }

    /// <summary>
    /// �������� �߰��ϴ� �Լ�
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
            // ���� ������ ü���� ��ŭ ȸ���ߴ����� ���� ���� ����
            //Health += healthRestore;
            //if(Health > MaxHealth)
            //   Health = MaxHealth;

            // ü���� �ִ� ��ŭ ȸ����ų �� �ִ����� ���� ��
            float maxHeal = Mathf.Max(MaxHealth - Health, 0);
            // ������ ü���� ȸ����ų�� �ִ� ��
            float actualHeal = Mathf.Min(maxHeal, healthRestore);
            Health += actualHeal;

            HealthChangeEvent.Invoke(Health, MaxHealth);
            HealthEvents.characterHealed.Invoke(gameObject, (int)actualHeal);
            return true;
        }
        return false;
    }

}