using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallTrigger : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision != null)
        {
            if (collision.collider.CompareTag("Enemy"))
            {
                if (collision.collider.GetComponent<EnemySkeletSystem>())
                {
                    collision.collider.GetComponent<EnemySkeletSystem>().TakeDamage(20);
                    Destroy(this.gameObject);
                }
            }
        }
    }
}
