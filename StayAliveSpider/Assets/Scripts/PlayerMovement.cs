using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    
    // SerializeField allows the change of speed in the unity UI
    [SerializeField] private float normalSpeed = 2.0f;
    [SerializeField] private float turnSpeed = 3f;
    float verticalVelocity;
    [SerializeField] private float gravity = 2.0f;

    private float inputX;
    private float inputZ;
    private Vector3 movement;
    private Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {
        GameObject tempPlayer = GameObject.FindGameObjectWithTag("Player");
        controller = tempPlayer.GetComponent<CharacterController>();
        // controller.GetComponent<CharacterController>();   
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.gm.gameState == GameManager.gameStates.Playing)
        {
            inputX = Input.GetAxis("Horizontal");
            inputZ = Input.GetAxis("Vertical");

            if (!CheckGround())
            {
                ApplyGravity();
            }

            // MoveTransformPosition();
            // MoveTransformPositionWithInputLimit();
            // MoveTransformSmoothly();
            // MoveTransformSmoothlyAndRotate();
            // MoveTransformAndRotatev2();
            MoveAndRotate();
        }
    }

    #region Move Transform Position

    /// <summary>
    /// Basic movement that would be used on a grid tile type of game
    /// It increments value one to the increment Positions
    /// </summary>
    void MoveTransformPosition()
    {
        // If the keys that were pressed are :
        // Right
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += Vector3.right;
        }

        // Left
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position += Vector3.left;
        }

        // Up
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.position += Vector3.forward;
        }

        // Down
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            transform.position += Vector3.back;
        }

        // Jump
        if (Input.GetKeyDown(KeyCode.Space)){
            transform.position = new Vector3(0, 0.5f, 0);
        }
    }

    #endregion

    #region Move Transform Position With Input Limit

    /// <summary>
    /// Basic movement that would be used on a grid tile type of game
    /// It increments value one to the increment Positions
    /// It also limits the movement to a certain amount between left, right, forward and back
    /// </summary>
    void MoveTransformPositionWithInputLimit()
    {
        // If the keys that were pressed and the object current position is does not go over the limit :
        // Right
        if (Input.GetKeyDown(KeyCode.RightArrow) && transform.position.x < 5)
        {
            transform.position += Vector3.right;
        }

        // Left
        if (Input.GetKeyDown(KeyCode.LeftArrow) && transform.position.x > -5)
        {
            transform.position += Vector3.left;
        }

        // Up
        if (Input.GetKeyDown(KeyCode.UpArrow) && transform.position.z < 5)
        {
            transform.position += Vector3.forward;
        }

        // Down
        if (Input.GetKeyDown(KeyCode.DownArrow) && transform.position.z > -1)
        {
            transform.position += Vector3.back;
        }

        // Jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            transform.position = new Vector3(0, 0.5f, 0);
        }
    }

    #endregion

    #region Move Transform Position With Input Smoothly
    /// <summary>
    /// Basic Movement that its affected by the defined player speed by the amount of current time
    /// </summary>
    void MoveTransformSmoothly()
    {
        // Input.GetKey vs Input.GetKeyDown
        // Input.GetKeyDown is used for instances where the button is gonna be pressed once
        // Input.GetKey is used for instances where the button is gonna be pressed continuously

        // Right
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += Time.deltaTime * normalSpeed * Vector3.right;
        }

        // Left
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += Time.deltaTime * normalSpeed * Vector3.left;
        }

        // Up
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.position += Time.deltaTime * normalSpeed * Vector3.forward;
        }

        // Down
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.position += Time.deltaTime * normalSpeed * Vector3.back;
        }

        // Jump
        if (Input.GetKey(KeyCode.Space))
        {
            transform.position = new Vector3(0, 0.5f, 0);
        }
    }

    #endregion

    #region Move Transform Smoothly And Rotate
    void MoveTransformSmoothlyAndRotate()
    {
        // Right
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += Time.deltaTime * normalSpeed * Vector3.right;
        }

        // Left
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += Time.deltaTime * normalSpeed * Vector3.left;
        }

        // Up
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += Time.deltaTime * normalSpeed * Vector3.forward;
        }

        // Down
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += Time.deltaTime * normalSpeed * Vector3.back;
        }

        // Rotate Left
        // In normal circumnstances this would work but due to the nature of the prefab
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(Time.deltaTime * turnSpeed * Vector3.down);
        }

        // Rotate Right
        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(Time.deltaTime * turnSpeed * Vector3.up);
        }

        // Jump
        if (Input.GetKey(KeyCode.Space))
        {
            transform.position = new Vector3(0, 0.5f, 0);
        }
    }

    #endregion

    #region Move Transform and Rotate v2

    void MoveTransformAndRotatev2()
    {
        // Up
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += Time.deltaTime * normalSpeed * Vector3.forward;
        }

        // Down
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += Time.deltaTime * normalSpeed * Vector3.back;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            // transform.Rotate(Vector3.left * turnSpeed * Vector3.up, Space.World);

            //y = 1 - y;

            //// Modify the vector 3 based input multiplied by turn speed and time
            //// currentEulerAngles += new Vector3(x, y, z) * Time.deltaTime * turnSpeed;
            //currentEulerAngles += new Vector3(x, y, z) * turnSpeed;

            //currentRotation.eulerAngles = currentEulerAngles;

            //transform.Rotate(currentEulerAngles);

            float horizontal = Input.GetAxis("Horizontal");
            // transform.Rotate(-horizontal * turnSpeed * Vector3.down, Space.World);
            transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y - 1, transform.rotation.z);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            // transform.Rotate(Vector3.left * turnSpeed * Vector3.up, Space.World);

            //y = 1 + y;

            //// Modify the vector 3 based input multiplied by turn speed and time
            //// currentEulerAngles += new Vector3(x, y, z) * Time.deltaTime * turnSpeed;

            //currentEulerAngles += new Vector3(x, y, z) * turnSpeed;

            //currentRotation.eulerAngles = currentEulerAngles;

            //transform.Rotate(currentEulerAngles);
            float horizontal = Input.GetAxis("Horizontal");
            // transform.Rotate(horizontal * turnSpeed * Vector3.up, Space.World);
            transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y + 1, transform.rotation.z);
        }
    }

    #endregion

    //private void FixedUpdate()
    //{


    //}
    void MoveAndRotate()
    {
        // Input Movement
        movement = controller.transform.forward * inputZ;

        // Rotation
        controller.transform.Rotate(Vector3.up * inputX * turnSpeed * Time.deltaTime);

        // Movement
        controller.Move(movement * normalSpeed * Time.deltaTime);
    }

    bool CheckGround()
    {
        Ray ray = new Ray(this.transform.position, Vector3.down);
        return (Physics.Raycast(ray, 400f, LayerMask.NameToLayer("Player")) || controller.isGrounded); 
        // return Physics.SphereCast(ray, 0.05f, 200f, LayerMask.NameToLayer("Player"));
    }



    void ApplyGravity()
    {
        verticalVelocity -= gravity * Time.deltaTime;
        Vector3 moveVector = new Vector3(0, verticalVelocity, 0);
        controller.Move(moveVector);
    }

    private void OnDrawGizmos()
    {
        if (controller == null) return;
        Gizmos.color = CheckGround() ? Color.green : Color.red;

        Gizmos.DrawWireSphere(this.transform.position + Vector3.up * 0.3f, 0.05f);

    }
}
