using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CombatStats : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public int damage = 10;
    public float range = 2;
    public float attackRate = 1;
    public float objectWidth = 1f;

    public GameObject enemyTarget;
    private float lastTimeAttacked;

    public virtual void Start()
    {
        currentHealth = maxHealth;
    }

    public bool Attack()
    {
        if (Time.time - lastTimeAttacked > attackRate && enemyTarget != null && enemyTarget.GetComponent<EnemyAttack>() && Vector3.Distance(transform.position, enemyTarget.transform.position) < range)
        {
            lastTimeAttacked = Time.time;
            print("take damage");
            enemyTarget.GetComponent<EnemyAttack>().TakeDamage(damage);
        }
        return Vector3.Distance(transform.position, enemyTarget.transform.position) < range;
    }

    public void TakeDamage(int damageTaken)
    {
        currentHealth -= damageTaken;
        if(currentHealth <= 0)
        {
            Death();
        }
    }

    //void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawSphere(transform.position, 1);

    //    Gizmos.color = Color.blue;
    //    Gizmos.DrawSphere(enemyTarget.transform.position, 1);
    //}

    public void Death()
    {
        GetComponent<EnemyAttack>().ItemDrop(new Vector3(transform.position.x, 0, transform.position.z));
        Destroy(transform.gameObject);
    }

}
public interface Combat
{
    void ItemDrop(Vector3 dropLocation);
}
