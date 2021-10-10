using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EaterManager : MonoBehaviour
{
    private PlayerCombat playerCombat;
    // Start is called before the first frame update
    void Start()
    {
        playerCombat = transform.parent.GetComponent<PlayerCombat>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            playerCombat.onEnemyDetected(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            playerCombat.onEnemyEscape(other.gameObject);
        }
    }

}
