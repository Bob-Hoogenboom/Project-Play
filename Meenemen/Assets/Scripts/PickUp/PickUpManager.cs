using UnityEngine;
using UnityEngine.UIElements;

public class PickUpManager : MonoBehaviour
{
    private RaycastHit _hit;
    [SerializeField] private GameObject inventoryOBJ;
    
    [SerializeField] private GameObject _currentOBJ;
    [SerializeField] private GameObject canvasMsg;
    [SerializeField] private LayerMask pickUp;
    
    [SerializeField] private float rayDistance = 2f;
    [SerializeField] private bool _hitDetect;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (inventoryOBJ != null)
            {
                PlaceObject();
            }
            else if(_currentOBJ != null)
            {
                PickUpObject();
            }
        }
    }
    
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

    private void PickUpObject()
    {
        if (_currentOBJ != null)
        {
            Debug.Log("PickedUp");
            inventoryOBJ = _currentOBJ;
            _currentOBJ.SetActive(false);
            canvasMsg.SetActive(false);
        }
    }

    private void PlaceObject()
    {
        Debug.Log("PutDown");
        inventoryOBJ.transform.position = transform.position + transform.forward * rayDistance;
        canvasMsg.SetActive(true);
        inventoryOBJ.SetActive(true);
        inventoryOBJ = null;
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
