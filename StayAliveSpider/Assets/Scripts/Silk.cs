using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Silk : MonoBehaviour
{
    private enum KSILKSTATES { REGENERATING, WEBBING };
    private KSILKSTATES currentState = KSILKSTATES.REGENERATING;
    
    public float curSilk = 0f;
    public float currentConsumption = 0;
    public float maxSilk = 100f;
    public float consumptionRate = 40f;

    private float silkRegenValue = 0f;
    public bool isSpawningWeb = false;

    private PlayerCombat playerCombat;
    private Hunger playerHunger;

    private float nextActionTime = 0.0f;
    public float period = 2.0f;

    public SilkBar silkBar;
    // Start is called before the first frame update
    void Start()
    {
        playerCombat = this.GetComponent<PlayerCombat>();
        playerHunger = this.GetComponent<Hunger>();

        nextActionTime = Time.time + period;

        curSilk = 50f;
        silkBar.SetSilk(curSilk);
    }

    // Update is called once per frame
    void Update()
    {
        if(currentState == KSILKSTATES.REGENERATING)
        {
            Regen();
        }
    }

    private void SilkRegen()
    {
        if (Time.time > nextActionTime)
        {
            nextActionTime += period;

            curSilk += silkRegenValue;
            curSilk = Mathf.Clamp(curSilk, 0f, 100f);
            silkBar.SetSilk(curSilk);
        }
    }

    public void SetWebbingState(bool state = false)
    {
        if (state)
        {
            currentState = KSILKSTATES.WEBBING;
            Debug.Log("New State : " + currentState);
        }
        else
        {
            currentState = KSILKSTATES.REGENERATING;
            nextActionTime = Time.time + period;

            Debug.Log("New State : " + currentState);
            FinishWebCreation();
            
        }
    }

    void Regen()
    {
        silkRegenValue = playerHunger.curHunger / 7.5f;
        SilkRegen();
       
    }


    public bool ValidateSilkConsumption(float size)
    {
        currentConsumption = size * consumptionRate;
        // Debug.Log(currentConsumption);
        if(curSilk - currentConsumption < 0)
        {
            currentConsumption = curSilk;
        }

        silkBar.SetSilk(curSilk - currentConsumption);
        // Debug.Log(curSilk - currentConsumption);
        return curSilk - currentConsumption != 0;
    }

    public void FinishWebCreation()
    {
        DrainSilk(currentConsumption);
        currentConsumption = 0f;
    }


    void DrainSilk(float amount)
    {
        curSilk -= amount;
        silkBar.SetSilk(curSilk);
    }

    public void ResetSilk()
    {
        curSilk = 50f;
        silkBar.SetSilk(curSilk);
    }
}
