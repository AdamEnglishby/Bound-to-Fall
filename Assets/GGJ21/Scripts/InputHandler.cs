using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    public void OnMovement(InputValue value)
    {
        Debug.Log(value.Get<Vector2>());
    }
    
}