using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateInput : MonoBehaviour
{

    [SerializeField] private float turnSpeed = 3f;
    //Vector3 currentEulerAngles;
    //Quaternion currentRotation;
    //float x, y, z;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            // transform.Rotate(Vector3.left * turnSpeed * Vector3.up, Space.World);

            //y = 1 - y;

            //// Modify the vector 3 based input multiplied by turn speed and time
            //// currentEulerAngles += new Vector3(x, y, z) * Time.deltaTime * turnSpeed;
            //currentEulerAngles += new Vector3(x, y, z) * turnSpeed;

            //currentRotation.eulerAngles = currentEulerAngles;

            //transform.Rotate(currentEulerAngles);

            float horizontal = Input.GetAxis("Horizontal");
            transform.Rotate(-horizontal * turnSpeed * Vector3.down, Space.World);
        }

        if (Input.GetKey(KeyCode.D))
        {
            // transform.Rotate(Vector3.left * turnSpeed * Vector3.up, Space.World);

            //y = 1 + y;

            //// Modify the vector 3 based input multiplied by turn speed and time
            //// currentEulerAngles += new Vector3(x, y, z) * Time.deltaTime * turnSpeed;

            //currentEulerAngles += new Vector3(x, y, z) * turnSpeed;

            //currentRotation.eulerAngles = currentEulerAngles;

            //transform.Rotate(currentEulerAngles);
            float horizontal = Input.GetAxis("Horizontal");
            transform.Rotate(horizontal* turnSpeed * Vector3.up, Space.World);
        }


    }
}
