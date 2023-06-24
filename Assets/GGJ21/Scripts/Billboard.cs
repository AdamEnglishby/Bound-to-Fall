using UnityEngine;

public class Billboard : MonoBehaviour
{
    // cache these to avoid extern calls in Update()
    private Transform _transform, _cameraTransform;

    private void Awake()
    {
        _transform = transform;

        if (Camera.main)
        {
            _cameraTransform = Camera.main.transform;
        }
    }

    private void LateUpdate()
    {
        if (!_cameraTransform) return;
        
        var rotation = _transform.rotation;
        rotation = Quaternion.Euler(rotation.eulerAngles.x, _cameraTransform.rotation.eulerAngles.y - 90, rotation.eulerAngles.z);
        _transform.rotation = rotation;
    }
    
}