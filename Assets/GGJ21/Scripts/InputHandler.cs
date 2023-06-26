﻿using System;
using System.Collections;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{

    [SerializeField] private CharacterController controller;

    private Camera _camera;
    private Vector2 _moveDirection;
    private float _downVelocity;

    private static event Action<InputValue> PrimaryButtonPressed;
    public static Action<InputValue> OnPrimaryButtonPressed
    {
        get => PrimaryButtonPressed;
        set => PrimaryButtonPressed = value;
    }

    public ParticleSystem speedLines;
    public float gravity = -1;
    public float maxVelocity = -5;
    public float speed = 1;

    public bool movementEnabled = false;
    public CinemachineVirtualCamera startCamera;

    private void Awake()
    {
        _camera = Camera.main;
        StartCoroutine(EnableInput());
    }

    private IEnumerator EnableInput()
    {
        yield return new WaitForSeconds(0.1f);
        startCamera.Priority = -1;
        yield return new WaitForSeconds(2);
        movementEnabled = true;
    }

    public void OnMovement(InputValue value)
    {
        if (movementEnabled)
        {
            _moveDirection = value.Get<Vector2>();
        }
    }
    
    public void OnPrimaryButton(InputValue value)
    {
        PrimaryButtonPressed?.Invoke(value);
    }

    public void Update()
    {
        if (_downVelocity <= maxVelocity / 1.5f)
        {
            var m = speedLines.emission;
            m.enabled = true;
        }
        else
        {
            var m = speedLines.emission;
            m.enabled = false;
        }
        // Debug.DrawLine(transform.position, transform.position - new Vector3(0, controller.height / 2 * transform.localScale.y + 0.01f, 0), Color.red);
        
        if (Physics.Raycast(transform.position, -transform.up, controller.height / 2 * transform.localScale.y + 0.01f, LayerMask.GetMask("Default")))
        {
            _downVelocity = 0f;
        }
        else
        {
            if (_downVelocity >= maxVelocity)
            {
                _downVelocity += gravity;
            }
        }

        var transformedInput = _camera.transform.TransformDirection(_moveDirection);
        var moveDir = new Vector3(transformedInput.x, 0, transformedInput.y) * speed;
        moveDir.y = _downVelocity;
        
        controller.Move(moveDir * Time.deltaTime);
    }
    
}