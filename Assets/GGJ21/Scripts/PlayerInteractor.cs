using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(BoxCollider))]
public class PlayerInteractor : MonoBehaviour
{

    private BoxCollider _collider; // should probably be a sphere but we ball
    private GameObject _torch;

    private bool _hasPickedUp;
    private bool _attached;
    private bool _canPickUp;

    public Vector3 posOffset;

    private void Awake()
    {
        _collider = GetComponent<BoxCollider>();
        StartCoroutine(SearchForTorch());
    }

    private IEnumerator SearchForTorch()
    {
        while (!_hasPickedUp)
        {
            Debug.Log("SEARCHING FOR TORCH");
            _torch = FindClosestTorch();
            yield return new WaitForSeconds(0.5f);
        }
    }

    private void FixedUpdate()
    {
        if (!_hasPickedUp)
        {
            _canPickUp = _collider.bounds.Contains(_torch.transform.position);
        }
        else if(!_attached)
        {
            var t = _torch;
            t.transform.position = transform.position + posOffset;
            t.transform.rotation = Quaternion.identity;
            t.transform.parent = transform;
            // _attached = true;
        }
    }
    
    public void OnPrimaryButton(InputValue value)
    {
        if (_canPickUp)
        {
            _hasPickedUp = true;
        }
    }
    
    private GameObject FindClosestTorch()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Torch");
        GameObject closest = null;
        var distance = Mathf.Infinity;
        var position = transform.position;
        foreach (var go in gos)
        {
            var diff = go.transform.position - position;
            var curDistance = diff.sqrMagnitude;
            if (!(curDistance < distance)) continue;
            
            closest = go;
            distance = curDistance;
        }
        return closest;
    }
    
}