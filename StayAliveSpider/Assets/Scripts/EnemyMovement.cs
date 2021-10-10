using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    enum States { INMOTION, TRAPPED};
    States enemyCurrentState = States.INMOTION;

    public enum motionTypes { Vertical, Circular};
    public motionTypes motionState = motionTypes.Vertical;

    // Vertical Motion
    public float motionMagnitude = 1f;
    

    // Circular Motion
    float angle = 0;
    float speed = (2 * Mathf.PI) / 3;
    float radius = 0.06f;

    // Time System
    TargetExit timeSystem;

    // Start is called before the first frame update
    void Start()
    {
        timeSystem = gameObject.GetComponent<TargetExit>();
    }

    // Update is called once per frame
    void Update()
    {
        // If the enemy hasnt been stuck into a web of a player, do motion
        if(enemyCurrentState == States.INMOTION)
        {
            switch (motionState)
            {
                case motionTypes.Vertical:
                    gameObject.transform.Translate(Vector3.left * Mathf.Cos(Time.timeSinceLevelLoad) * motionMagnitude * Time.deltaTime) ;
                    // gameObject.transform.Translate(Mathf.Clamp(-motionMagnitude, -1, 1), 0, 0);
                    // transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -4.1f, 4.1f), transform.position.z);

                    break;

                case motionTypes.Circular:

                    angle += speed * Time.deltaTime; //if you want to switch direction, use -= instead of +=
                    float x = Mathf.Cos(angle) * radius;
                    // float y = Mathf.Sin(angle) * radius;
                    float y = 0;
                    float z = Mathf.Sin(angle) * radius;

                    Vector3 vector = new Vector3(x, y, z);

                    gameObject.transform.Translate(vector);
                    break;
            }
        }
        
    }

    void OnCollisionEnter(Collision collision)
    {
        // If it hits a playerWeb
        if(collision.gameObject.tag == "PlayerWeb")
        {
            StateUpdater(enemyCurrentState);
        }
    }

    void StateUpdater(States currentState)
    {
        switch (currentState)
        {
            case States.INMOTION:
                // Swith to trapped and reset dissapearing timer
                enemyCurrentState = States.TRAPPED;
                gameObject.GetComponent<Rigidbody>().isKinematic = true;
                timeSystem.SetTrappedTimer();
                break;

            //case States.TRAPPED:
            //    break;
        }
    }
}
