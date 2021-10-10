using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    enum States {Default, StartActiveWebbing, ControlActiveWebbing};
    States playerCurrentState = States.Default;

    // Attack
    public List<GameObject> targets = new List<GameObject>(); 
    public float attackDamage = 1.0f;

    // Web Related
    Silk silkSystem;
    (Vector3, Vector3) webPositions;
    public Transform webOutput;
    public GameObject webPrefab;
    private GameObject spawnedObject;

    //bool spawnedWeb = false;
    //bool activeWeb = false;
    //public GameObject webPrefab;
    //GameObject spawnedObject;
    //float previousDistance = 0.0f;
    //float previousAngle = 0.0f;
    //float speed = 1.0f;
    //Transform webStartPosition;
    //Vector3 webSpawnPosition;
    //Vector3 centerPos;


    // Start is called before the first frame update
    void Start()
    {
        
        silkSystem = this.gameObject.GetComponent<Silk>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.gm.gameState == GameManager.gameStates.Playing)
        {
            InputHandler();

            StateUpdater(playerCurrentState);
        }
    }

    // Attack Basics
    void Attack()
    {
        for(int i = targets.Count-1; i >= 0; i--)
        {

            if(targets[i].GetComponent<EnemyHealth>().TakeDamage(attackDamage))
            { 
                targets.RemoveAt(i);
                GameManager.gm.EnemyEaten();
            }
        }
        
    }

    public bool onEnemyDetected(GameObject target)
    {
        // if target exists in list
        if(targets.Find(x => x == target) == null)
        {
            targets.Add(target);
            Debug.Log(targets.Count);
        }

        return true;
    }

    public bool onEnemyEscape(GameObject target)
    {
        // if target exists in list
        if (targets.Find(x => x == target) != null)
        {
            targets.Remove(target);
        }

        return true;
    }

    void InputHandler()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Attack();
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if(playerCurrentState == States.Default)
            {
                playerCurrentState = States.StartActiveWebbing;
                silkSystem.SetWebbingState(true);
                // Debug.Log("New State : " + playerCurrentState);
            }
            else if(playerCurrentState == States.ControlActiveWebbing)
            {
                OnActiveWebbingEnd();
                // Debug.Log("New State : " + playerCurrentState);
            }
        }
    }

    void OnActiveWebbingEnd()
    {
        spawnedObject.layer = LayerMask.NameToLayer("Default");
        RaycastHit hit;
        if (Physics.Raycast(webOutput.position, Vector3.down, out hit, LayerMask.NameToLayer("Player")))
        {
            webPositions.Item2 = hit.point;
            spawnedObject.transform.position = CalculateCenter(webPositions);
            spawnedObject.transform.up = CalculateOrientation(webPositions);
            spawnedObject.transform.localScale = new Vector3(spawnedObject.transform.localScale.x, CalculateScale(webPositions), spawnedObject.transform.localScale.x);
        }

        playerCurrentState = States.Default;
        silkSystem.SetWebbingState();
    }

    void StateUpdater(States currentState)
    {
        switch (currentState)
        {
            case States.StartActiveWebbing:

                RaycastHit hit;
                if (Physics.Raycast(webOutput.position, Vector3.down, out hit, LayerMask.NameToLayer("Player"))){
                    webPositions.Item1 = hit.point;
                    // Debug.Log("Chao");
                }
                else
                {
                    // Initial Position
                    webPositions.Item1 = webOutput.position;
                }
                webPositions.Item2 = this.transform.position;
                this.playerCurrentState = States.ControlActiveWebbing;
                // Debug.Log("New State : " + playerCurrentState);

                spawnedObject = Instantiate(webPrefab, CalculateCenter(webPositions), Quaternion.identity);
                spawnedObject.transform.up = CalculateOrientation(webPositions);
                spawnedObject.transform.localScale = new Vector3(spawnedObject.transform.localScale.x, CalculateScale(webPositions), spawnedObject.transform.localScale.x);
                spawnedObject.layer = LayerMask.NameToLayer("WebInCreation");
                break;

            case States.ControlActiveWebbing:
                if(!silkSystem.ValidateSilkConsumption((this.transform.position - webPositions.Item1).magnitude))
                {
                    OnActiveWebbingEnd();
                    return;
                }
                webPositions.Item2 = this.transform.position;


                spawnedObject.transform.position = CalculateCenter(webPositions);
                spawnedObject.transform.up = CalculateOrientation(webPositions);
                spawnedObject.transform.localScale = new Vector3(spawnedObject.transform.localScale.x, CalculateScale(webPositions), spawnedObject.transform.localScale.x);
                break;
        }
    }

    Vector3 CalculateCenter((Vector3, Vector3) vectors)
    {
        return vectors.Item1 + (vectors.Item2 - vectors.Item1) * 0.5f;
    }

    Vector3 CalculateOrientation((Vector3, Vector3) positions)
    {
        return (positions.Item2 - positions.Item1).normalized;    // Quaternion.LookRotation(positions.Item2 - positions.Item1); // Vector3.Cross(,)
    }

    float CalculateScale((Vector3, Vector3) positions)
    {
        return (positions.Item2 - positions.Item1).magnitude * 0.5f;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        if(webPositions.Item1 != null && Vector3.Distance(webPositions.Item1, webPositions.Item2) >= 0.5f)
        {
            Gizmos.DrawLine(webPositions.Item1, webPositions.Item2);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(CalculateCenter(webPositions), 0.2f);
            
        }
    }


    #region Previous Code

    //void Attack
    //{
    //    //foreach(GameObject enemy in targets)
    //    //{
    //    //    float distance = Vector3.Distance(enemy.transform.position, transform.position);
    //    //    Debug.Log(distance);

    //    //    if(distance <= attackRange)
    //    //    {
    //    //        Debug.Log("Strikable");
    //    //        EnemyHealth eh = (EnemyHealth)enemy.GetComponent("EnemyHealth");
    //    //        eh.TakeDamage(attackDamage);
    //    //        return;
    //    //    }
    //    //    else
    //    //    {
    //    //        Debug.Log("Too Far");
    //    //    }
    //    //}
    //}

    //void SpawnWeb()
    //{
    //    // If Web is currently active
    //    if (spawnedWeb)
    //    {
    //        // do something

    //        // drain silk amount

    //        // 
    //        spawnedWeb = false;
    //        spawnedObject = null;
    //    }
    //    else
    //    {
    //        Quaternion rot = Quaternion.Euler(90, 0, 0);
    //        spawnedObject = Instantiate(webPrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z), rot);
    //        // webSpawnPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    //        // webStartPosition.position = webStartPosition.position + webSpawnPosition;
    //        spawnedWeb = true;
    //    }
    //}

    //void WebControl()
    //{
    //    float distance = Vector3.Distance(spawnedObject.transform.position, transform.position);
    //    float angle = Vector3.SignedAngle(spawnedObject.transform.position, transform.position, Vector3.up);
    //    Debug.Log("Distance between Web and Spider : " + distance);
    //    Debug.Log("Angle between Web and Spider : " + angle);

    //    // if position of the player has changed
    //    if(previousDistance != distance)
    //    {
    //        float value = 0.008f;
    //        // The player went closer
    //        if (previousDistance > distance)
    //        {
    //            spawnedObject.transform.localScale -= new Vector3(0, value, 0);
    //        }
    //        // The player went further
    //        else
    //        {
    //            spawnedObject.transform.localScale += new Vector3(0, value, 0);
    //        }
    //        previousDistance = distance;

    //        // Adjust position to be in center
    //        Vector3 pointCenter = Vector3.Lerp(webSpawnPosition, new Vector3(transform.position.x, transform.position.y, transform.position.z), 0.5f);

    //        // spawnedObject.transform.position = webStartPosition.transform.position + pointCenter;

    //        spawnedObject.transform.position = centerPos;
    //    }

    //    // Angle > 0 -> Virou À esquerda
    //    // Angle < 0 -> Virou À Direita
    //    if(previousAngle != angle)
    //    {
    //        //if(previousAngle > angle)
    //        //{

    //        //}
    //        //else
    //        //{

    //        //}
    //        // Target Position -> Trasform.position

    //        //Vector3 targetPosition = new Vector3(spawnedObject.transform.position.x, transform.position.y, transform.position.z);

    //        //spawnedObject.transform.LookAt(targetPosition);

    //        //Quaternion rotation = 
    //        //spawnedObject.transform.rotation.z = angle;
    //        // spawnedObject.transform.eulerAngles.z
    //        //spawnedObject.transform.eulerAngles = new Vector3(
    //        //    spawnedObject.transform.eulerAngles.x,
    //        //    spawnedObject.transform.eulerAngles.y,
    //        //    angle
    //        //);

    //        spawnedObject.transform.rotation = Quaternion.Euler(90, 0, angle);

    //        previousAngle = angle;
    //    }



    //}
    #endregion
}

