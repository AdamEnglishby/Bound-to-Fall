using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{

    [SerializeField] private CharacterController controller;
    private Vector2 _moveDirection;

    public float speed = 1;
    
    public void OnMovement(InputValue value)
    {
        _moveDirection = value.Get<Vector2>();
    }

    public void FixedUpdate()
    {
        Debug.Log(_moveDirection);
        controller.Move(new Vector3(_moveDirection.x, 0, _moveDirection.y) * Time.fixedDeltaTime * speed);
    }
}