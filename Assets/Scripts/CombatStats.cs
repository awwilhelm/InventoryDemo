using UnityEngine;
using System.Collections;
public class CombatStats : MonoBehaviour
{
    private int maxHealth;
    public int MaxHealth
    {
        get
        {
            return maxHealth;
        }
        set
        {
            maxHealth = value;
        }
    }

    private int currentHealth;
    public int CurrentHealth
    {
        get
        {
            return currentHealth;
        }
        set
        {
            currentHealth = value;
        }
    }
    private int damage;
    public int Damage
    {
        get
        {
            return damage;
        }
        set
        {
            damage = value;
        }
    }

    private float range;
    public float Range
    {
        get
        {
            return range;
        }
        set
        {
            range = value;
        }
    }

    private GameObject enemyTarget;
    public GameObject EnemyTarget
    {
        get
        {
            return enemyTarget;
        }
        set
        {
            enemyTarget = value;
        }
    }


    void Start()
    {
        maxHealth = 100;
        currentHealth = maxHealth;
        damage = 10;
        range = 10;
    }

    public void Attack()
    {
        if (enemyTarget != null && enemyTarget.GetComponent<CombatStats>() && Vector3.Distance(transform.position, enemyTarget.transform.position) < range)
        {
            enemyTarget.GetComponent<CombatStats>().TakeDamage(damage);
        }
    }

    public void TakeDamage(int damageTaken)
    {
        currentHealth -= damageTaken;
    }

    public void Death()
    {
        print("GoodBye");
    }
}
