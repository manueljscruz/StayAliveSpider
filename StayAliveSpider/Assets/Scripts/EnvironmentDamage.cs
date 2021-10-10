using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentDamage : MonoBehaviour
{
    public float damageAmount = 1.0f;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Health>().DamagePlayer((int)damageAmount);
        }
    }
}
