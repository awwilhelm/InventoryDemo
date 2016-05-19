using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyAttack : CombatStats, Combat {

    public List<Items> dropItems;

    //public Items test;

    public override void Start()
    {
        base.Start();

    }

    // Update is called once per frame
    void Update ()
    {
	    
	}
    public void ItemDrop(Vector3 dropLocation)
    {
        for (int i = 0; i < dropItems.Count; i++)
        {
            float rand = Random.Range(0f, 1f);
            if(dropItems[i].dropPercentage > rand)
            {
                Instantiate(dropItems[i].obj, dropLocation, dropItems[i].obj.transform.rotation);
            }
        }
    }

}
