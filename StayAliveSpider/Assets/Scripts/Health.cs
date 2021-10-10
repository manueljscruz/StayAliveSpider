using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public int curHealth = 0;
    public int maxHealth = 1;

    public string LevelToLoad;

    // Start is called before the first frame update
    void Start()
    {
        curHealth = maxHealth;

    }

    // Update is called once per frame
    void Update()
    {
        CheckIFPlayerFell();
    }

    public void DamagePlayer(int damage)
    {
        if(curHealth > 0)
        {
            curHealth -= damage;
            Debug.Log("Health : " + curHealth);

        }
        else
        {
            Debug.Log("GAME OVER YOU'RE DEAD!");
        }
    }

    public bool isAlive()
    {
        if (curHealth > 0)
        {
            return true;
        }
        else return false;
    }

    public void setHealth()
    {
        curHealth = maxHealth;
    }

    void CheckIFPlayerFell()
    {
        if(gameObject.transform.position.y <= -1)
        {
            DamagePlayer(1);
        }
    }

    public void ResetHealth()
    {
        curHealth = maxHealth;
    }
    
}
