using UnityEngine;

public class PickUpManager : MonoBehaviour
{
    private RaycastHit _hit;
    
    [SerializeField] private GameObject _currentOBJ;
    [SerializeField] private LayerMask pickUp;
    
    [SerializeField] private float rayDistance = 2f;
    [SerializeField] private bool _hitDetect;

    private void FixedUpdate()
    {
        CheckPickUp();
    }

    private void CheckPickUp()
    {
        _hitDetect = Physics.BoxCast(transform.position, transform.localScale, transform.forward, out _hit,
            transform.rotation, rayDistance, pickUp);
        
        if (_hitDetect)
        {
            GetPickUp();
        }
        
        if (_currentOBJ && !_hitDetect)
        {
            _currentOBJ.GetComponent<PickUpOBJ>()?.ToggleHighligh(false);
            _currentOBJ = null;
        }
    }
    
    private void GetPickUp()
    {
        _currentOBJ = _hit.collider.gameObject;
        _currentOBJ.GetComponent<PickUpOBJ>()?.ToggleHighligh(true);
    }

    private void OnDrawGizmos()
    {
        if (_hitDetect)
        {
            //Draw a Ray forward from GameObject toward the hit
            Gizmos.DrawRay(transform.position, transform.forward * _hit.distance);
            //Draw a cube that extends to where the hit exists
            Gizmos.DrawWireCube(transform.position + transform.forward * _hit.distance, transform.localScale);
        }
        //If there hasn't been a hit yet, draw the ray at the maximum distance
        else
        {
            //Draw a Ray forward from GameObject toward the maximum distance
            Gizmos.DrawRay(transform.position, transform.forward * rayDistance);
            //Draw a cube at the maximum distance
            Gizmos.DrawWireCube(transform.position + transform.forward * rayDistance, transform.localScale);
        }
    }
}
