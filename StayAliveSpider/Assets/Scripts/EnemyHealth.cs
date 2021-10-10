using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    private float health = 1f;

    public float Health
    {
        get { return health; }
    }


    public bool TakeDamage(float damage)
    {
        // Debug.Log("Im being killed");
        health -= damage;
        if(health <= 0)
        {
            Debug.Log("Enemy Killed");
            GameObject.FindGameObjectWithTag("Player").GetComponent<Hunger>().RegeneratePlayer(10);
            Destroy(this.gameObject);
            return true;
        }
        return false;
    }
    
    
}
