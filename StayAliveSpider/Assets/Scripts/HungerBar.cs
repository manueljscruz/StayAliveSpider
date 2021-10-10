using UnityEngine;
using UnityEngine.UI;

public class HungerBar : MonoBehaviour
{

    public Slider hungerBar;
    public Hunger playerHunger;

    private void Start()
    {
        playerHunger = GameObject.FindGameObjectWithTag("Player").GetComponent<Hunger>();
        hungerBar = GetComponent<Slider>();
        hungerBar.maxValue = playerHunger.maxHunger;
        hungerBar.value = playerHunger.maxHunger;
    }

    public void SetHunger(int hp)
    {
        hungerBar.value = hp;
    }
}
