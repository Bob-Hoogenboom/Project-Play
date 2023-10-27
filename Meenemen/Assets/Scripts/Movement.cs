using UnityEngine;

public class Movement : MonoBehaviour
{
    private CharacterController _charCon;
    private Transform _cam;
    private float _turnSmoothVelocity;
    private Vector3 _gravity;
    
    [SerializeField] private float turnSmoothTime = 0.1f;
    [SerializeField] private float speed = 5f;

    private void Start()
    {
        _charCon = gameObject.GetComponent<CharacterController>();
        _cam = GameObject.FindWithTag("MainCamera").transform;
    }

    private void Update()
    {
        Move();
        ApplyGravity();
    }

    private void Move()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (!(direction.magnitude >= 0.1f)) return;
        
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + _cam.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, turnSmoothTime);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);

        Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        _charCon.Move(moveDir * speed * Time.deltaTime);
    }

    private void ApplyGravity()
    {
        _gravity += Physics.gravity * Time.deltaTime;
        _charCon.Move(_gravity);
        if (IsGrounded())
        {
            _gravity.y = 0;
        }
    }

    private bool IsGrounded() => _charCon.isGrounded;
}
