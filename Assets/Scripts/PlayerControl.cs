using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] InputAction movement;
    [SerializeField] InputAction fire;
    [SerializeField] GameObject leftLaser;
    [SerializeField] GameObject rightLaser;
    [SerializeField] float movementSpeed = 20f;
    [SerializeField] float xAxisMovementRange = 7f;
    [SerializeField] float yAxisBottomMovementRange = -3f;
    [SerializeField] float yAxisTopMovementRange = 9f;
    [SerializeField] float rotationFactorY = -2f;
    [SerializeField] float rotationFactorX = 3f;
    [SerializeField] float verticalManeuverFactor = -20f;
    [SerializeField] float horizontalManeuverFactor = -15f;

    float horizontalMovement;
    float verticalMovement;
    float xRotationForYPosition;
    float yRotationForXposition;
    float zRollForXMovement;
    ParticleSystem.EmissionModule leftEmission;
    ParticleSystem.EmissionModule rightEmission;

    private void OnEnable()
    {
        movement.Enable();
        fire.Enable();
    }

    void Start()
    {
        leftEmission = leftLaser.GetComponent<ParticleSystem>().emission;
        rightEmission = rightLaser.GetComponent<ParticleSystem>().emission;
    }

    private void OnDisable()
    {
        movement.Disable();
    }

    void Update()
    {
        ProcessMovement();
        ProcessRotation();
        ProcessFiring();
    }

    private void ProcessMovement()
    {
        horizontalMovement = movement.ReadValue<Vector2>().x * movementSpeed * Time.deltaTime;
        verticalMovement = movement.ReadValue<Vector2>().y * movementSpeed * Time.deltaTime;
        //pg 2
        transform.localPosition = new Vector2(Mathf.Clamp((horizontalMovement + transform.localPosition.x),
            -xAxisMovementRange, xAxisMovementRange),
            Mathf.Clamp((verticalMovement + transform.localPosition.y),
            yAxisBottomMovementRange, yAxisTopMovementRange));
    }

    private void ProcessRotation()
    {
        //pg 4
        xRotationForYPosition = transform.localPosition.y * rotationFactorY + 
            (movement.ReadValue<Vector2>().y * verticalManeuverFactor );
        yRotationForXposition = transform.localPosition.x * rotationFactorX;
        zRollForXMovement = movement.ReadValue<Vector2>().x * horizontalManeuverFactor;
        transform.localRotation = Quaternion.Euler(xRotationForYPosition, yRotationForXposition,
            zRollForXMovement);
    }

    private void ProcessFiring() //pg6
    {
        if(fire.ReadValue<float>() > 0.1)
        {
            leftEmission.enabled = true;
            rightEmission.enabled = true;
        }
        else
        {
            leftEmission.enabled = false;
            rightEmission.enabled = false;
        }
        
    }

}
