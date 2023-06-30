using System;
using System.Collections;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private Animator animator;

    private Camera _camera;
    private Vector2 _moveDirection;
    private float _downVelocity;

    public float mashIncrement = 0.25f;
    public float mashThreshold = 1f;
    public float mashMaxValue = 2f;
    public float mashDuration = 3f;

    private float _mashDurationTracked;
    private float _mashValue ;
    private bool _hasJacket = true;

    private bool _escapeStarted = false;
    public TextBox onEscapeStarted, onEscaped;

    private static event Action<InputValue> PrimaryButtonPressed;

    public static Action<InputValue> OnPrimaryButtonPressed
    {
        get => PrimaryButtonPressed;
        set => PrimaryButtonPressed = value;
    }

    public static event Action OnEscaped, OnEscapeStarted;

    public ParticleSystem speedLines;
    public float gravity = -1f / 6;
    public float maxVelocity = -50;
    public float speed = 7;

    public bool movementEnabled;
    public CinemachineVirtualCamera startCamera;

    private static readonly int Running = Animator.StringToHash("Running"),
        Forwards = Animator.StringToHash("Forwards"),
        Backwards = Animator.StringToHash("Backwards"),
        Left = Animator.StringToHash("Left"),
        Right = Animator.StringToHash("Right"),
        Struggle = Animator.StringToHash("Struggle"),
        StruggleSpeed = Animator.StringToHash("Struggle Speed");

    private void Awake()
    {
        _camera = Camera.main;
        StartCoroutine(EnableInput());
        OnEscapeStarted += () =>
        {
            if (_escapeStarted) return;
            
            Dialogue.Show(onEscapeStarted);
            _escapeStarted = true;
        };
        OnEscaped += () =>
        {
            Dialogue.Show(onEscaped);
        };
    }

    private IEnumerator EnableInput()
    {
        yield return new WaitForSeconds(0.1f);
        startCamera.Priority = -1;
        yield return new WaitForSeconds(1);
        movementEnabled = true;
    }

    public void OnMovement(InputValue value)
    {
        if (movementEnabled)
        {
            _moveDirection = value.Get<Vector2>();
        }
    }

    public void OnMashButton(InputValue value)
    {
        Debug.Log("Mash");
        if (!_hasJacket || !value.isPressed) return;
        if (_mashValue >= mashMaxValue) return;
        
        OnEscapeStarted?.Invoke();
        _mashValue += mashIncrement;
    }

    public void OnPrimaryButton(InputValue value)
    {
        PrimaryButtonPressed?.Invoke(value);
    }

    public void Update()
    {
        if (!movementEnabled)
        {
            _moveDirection = Vector2.zero;
        }
        if (_hasJacket)
        {
            animator.SetFloat(StruggleSpeed, Mathf.Max(0.5f, _mashValue));
            animator.SetBool(Struggle, _mashValue > 0f);
            movementEnabled = _mashValue == 0f;
            
            _mashValue -= Time.deltaTime;
            if (_mashValue < 0)
            {
                _mashValue = 0;
            }

            if (_mashValue > mashThreshold)
            {
                _mashDurationTracked += Time.deltaTime;
            }
            else
            {
                _mashDurationTracked = 0;
            }

            if (_mashDurationTracked >= mashDuration)
            {
                OnEscaped?.Invoke();
                animator.SetBool(Struggle, false);
                _hasJacket = false;
                movementEnabled = true;
            }
        }

        animator.SetBool(Running, _moveDirection.magnitude > 0.05f);

        var m = speedLines.emission;
        m.enabled = _downVelocity <= maxVelocity / 2.25f;

        // TODO: raycast a sphere instead to make edges more consistent?? tbd
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

        var moveDir = new Vector3(_moveDirection.x, 0, _moveDirection.y) * speed;

        var up = moveDir.z / speed;
        var down = -moveDir.z / speed;
        var left = -moveDir.x / speed;
        var right = moveDir.x / speed;

        // this figures out what animator parameters to set
        // dear god it's bad I know please fix eventually
        float diffVertical = 0f, diffHorizontal = 0f;
        if (down > up)
        {
            diffVertical = down - up;
        } 
        else if (up > down)
        {
            diffVertical = up - down;
        }
        
        if (left > right)
        {
            diffHorizontal = left - right;
        }
        else if (right > left)
        {
            diffHorizontal = right - left;
        }

        if (diffVertical > diffHorizontal)
        {
            animator.SetBool(Forwards, down > up);
            animator.SetBool(Backwards, up > down);
            animator.SetBool(Left, false);
            animator.SetBool(Right, false);
        } 
        else if (diffHorizontal > diffVertical)
        {
            animator.SetBool(Forwards, false);
            animator.SetBool(Backwards, false);
            animator.SetBool(Left, left > right);
            animator.SetBool(Right, right > left);
        }

        moveDir = _camera.transform.TransformDirection(moveDir);
        moveDir.y = _downVelocity;

        controller.Move(moveDir * Time.deltaTime);
    }
}