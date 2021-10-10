using UnityEngine;
using UnityEngine.UI;

public class SilkBar : MonoBehaviour
{

    public Slider silkBar;
    public Silk playerSilk;

    // Start is called before the first frame update
    void Start()
    {
        playerSilk = GameObject.FindGameObjectWithTag("Player").GetComponent<Silk>();
        silkBar = GetComponent<Slider>();
        silkBar.maxValue = playerSilk.maxSilk;
        silkBar.value = playerSilk.maxSilk;
    }

    // Update is called once per frame
    public void SetSilk(float silkValue)
    {
        silkBar.value = silkValue;
    }


}
