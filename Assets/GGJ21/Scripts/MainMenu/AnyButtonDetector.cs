using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.LowLevel;

public class AnyButtonDetector : MonoBehaviour
{

    private InputAction _action;
    
    private void Start()
    {
        InputSystem.onEvent +=
            (eventPtr, device) =>
            {
                if (!eventPtr.IsA<StateEvent>() && !eventPtr.IsA<DeltaStateEvent>())
                    return;
                var controls = device.allControls;
                var buttonPressPoint = InputSystem.settings.defaultButtonPressPoint;
                foreach (var t in controls)
                {
                    var control = t as ButtonControl;
                    if (control == null || control.synthetic || control.noisy)
                        continue;
                    if (!control.ReadValueFromEvent(eventPtr, out var value) || !(value >= buttonPressPoint)) continue;
                    Debug.Log("Pressed");
                    break;
                }
            };
    }
}