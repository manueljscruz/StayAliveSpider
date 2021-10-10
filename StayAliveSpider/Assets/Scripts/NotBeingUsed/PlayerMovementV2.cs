using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementV2 : MonoBehaviour
{

    [SerializeField] private float normalSpeed = 2.0f;
    [SerializeField] private float sprintSpeed = 4.0f;
    [SerializeField] private float turnSpeed = 30f;

    private Vector3 moveDirection;
    private CharacterController controller;

    // Start is called before the first frame update
    void Start()
    {
        controller.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        // Input Axis is UP and DOWN (W and S)
        float inputZ = Input.GetAxis("Vertical");

        moveDirection = new Vector3(0, 0, inputZ);
        moveDirection *= normalSpeed;

        controller.Move(moveDirection * Time.deltaTime);
    }
}
