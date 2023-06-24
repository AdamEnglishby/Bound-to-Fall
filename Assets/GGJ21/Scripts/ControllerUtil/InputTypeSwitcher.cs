using UnityEngine;
using UnityEngine.InputSystem;

public class InputTypeSwitcher : MonoBehaviour
{
    private InputDevice _lastInputDevice;

    public delegate void ActiveControllerTypeChange(InputDevice newDevice);

    public static event ActiveControllerTypeChange OnActiveControllerTypeChange;

    private void Awake()
    {
        InputSystem.onActionChange += (obj, change) =>
        {
            if (change != InputActionChange.ActionPerformed) return;
            var inputAction = (InputAction) obj;
            var lastControl = inputAction.activeControl;
            var lastDevice = lastControl.device;

            if (_lastInputDevice != lastDevice)
            {
                OnActiveControllerTypeChange?.Invoke(lastDevice);
            }
        };

        OnActiveControllerTypeChange += newDevice =>
        {
            _lastInputDevice = newDevice;
            Debug.Log("Switched to " + newDevice.name);
        };
    }
}