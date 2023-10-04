using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody _rb;
    private float _currentVelocity;
    
    
    [SerializeField] private float moveSpeed;
    [SerializeField] private float smoothTime;
    [SerializeField] private GameObject playerGRFX;
    
    
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }
    
    private void Update()
    {
        Vector3 dir = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));

        if (dir.magnitude >= 0.1f)
        {
            Walk(dir);
        }
    }

    private void Walk(Vector3 dir)
    {
        float targetAngle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
        float angle = Mathf.SmoothDampAngle(playerGRFX.transform.eulerAngles.y, targetAngle, ref _currentVelocity , smoothTime);
        playerGRFX.transform.rotation = Quaternion.Euler(0f, angle, 0f);
        
        _rb.MovePosition(transform.position + dir * Time.deltaTime * moveSpeed);
    }
}
