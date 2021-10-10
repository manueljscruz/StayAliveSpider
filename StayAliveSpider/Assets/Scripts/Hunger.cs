using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunger : MonoBehaviour
{
    public int curHunger = 0;
    public int maxHunger = 100;

    private float nextActionTime = 0.0f;
    public float period = 2.0f;

    public HungerBar hungerBar;
    public Health health;
    // public HealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        curHunger = maxHunger;
        hungerBar.SetHunger(maxHunger);
        health = this.GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    DrainHunger(10);
        //}
        TickHungerPerTime();
    }

    public void DrainHunger(int damage)
    {
        if (curHunger > 0)
        {
            curHunger -= damage;
            // Debug.Log("Hunger : " + curHunger);

            hungerBar.SetHunger(curHunger);
        }
        else
        {
            health.DamagePlayer(1);
        }
    }

    private void TickHungerPerTime()
    {
        if (Time.time > nextActionTime)
        {
            nextActionTime = Time.time + period;

            DrainHunger(2);
        }
    }

    public void RegeneratePlayer(int valueToRefill)
    {
        if (curHunger + valueToRefill > maxHunger)
        {
            curHunger = maxHunger;
        }
        else
        {
            curHunger += valueToRefill;
        }
    }

    public void ResetHunger()
    {
        curHunger = maxHunger;
        hungerBar.SetHunger(curHunger);
    }
}
