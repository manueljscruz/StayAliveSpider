using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetExit : MonoBehaviour
{
    public float exitAfterSeconds = 10f; // how long to exist in the world
    private float targetTime;

    // Start is called before the first frame update
    void Start()
    {
        targetTime = Time.time + exitAfterSeconds;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= targetTime)
        {
            Destroy(gameObject);
        }
    }

    public void SetTrappedTimer()
    {
        targetTime = Time.time + exitAfterSeconds;
    }

}
