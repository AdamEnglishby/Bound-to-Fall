using UnityEngine;

public class Billboard : MonoBehaviour
{
    // cache these to avoid extern calls in Update()
    private Transform _transform, _cameraTransform;

    private float yAngle;

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
        rotation = Quaternion.Euler(rotation.eulerAngles.x, _cameraTransform.rotation.eulerAngles.y - 180, rotation.eulerAngles.z);
        _transform.rotation = rotation;
            
        yAngle = Vector3.SignedAngle(_transform.position, _cameraTransform.transform.position, Vector3.up);
        Debug.Log(yAngle);
    }
}