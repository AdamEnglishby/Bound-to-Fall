using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour, InputActions.IActionMapActions
{
    public void OnSpace(InputAction.CallbackContext context)
    {
        if (context.started)
            Debug.Log("Action was started");
        else if (context.performed)
            Debug.Log("Action was performed");
        else if (context.canceled)
            Debug.Log("Action was cancelled");
    }
}