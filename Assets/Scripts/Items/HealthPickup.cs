using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    // 체력 회복량
    public int healthRestore = 20;
    public Vector3 spinRotationSpeed = new Vector3(0, 180, 0);

    private void LateUpdate()
    {
        transform.eulerAngles += spinRotationSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();
        if(damageable)
        {
            bool wasHeal = damageable.Heal(healthRestore);
            if (wasHeal)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
