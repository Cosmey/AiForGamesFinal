using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController : MonoBehaviour
{

    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private float gravityStrength;
    [SerializeField] private float linearDamping;

    private Vector2 moveAxis;
    private int numGroundObjects;

    private Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
        Gravity();
    }
    private void Update()
    {
        Dampen();
    }


    //custom dampen function because rb dampening was affecting jumping
    private void Dampen()
    {
        rb.linearVelocity = (new Vector3(rb.linearVelocity.x,0,rb.linearVelocity.z) * Mathf.Pow(linearDamping, Time.deltaTime)) + new Vector3(0, rb.linearVelocity.y, 0);
    }
    private void Gravity()
    {
        rb.AddForce(Vector3.down * gravityStrength, ForceMode.Force);
    }
    private void Move()
    {
        Vector2 finalMove = moveAxis.normalized;
        Vector3 finalMove3D = new Vector3(finalMove.x, 0, finalMove.y);
        if(rb.linearVelocity.x != 0 || rb.linearVelocity.z != 0) transform.forward = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
        rb.AddForce(finalMove3D * speed, ForceMode.Force);
    }
    private void Jump()
    {
        if(GetGrounded())
        {
            rb.AddForce(transform.up * jumpPower, ForceMode.Impulse);
        }
    }

    public void UpdateMoveInput(InputAction.CallbackContext context = new InputAction.CallbackContext())
    {
        moveAxis = context.ReadValue<Vector2>();
    }
    public void JumpInput(InputAction.CallbackContext context = new InputAction.CallbackContext())
    {
        if(context.started)
        {
            Jump();
        }
    }



    private bool GetGrounded()
    {
        return numGroundObjects > 0;
    }    
    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Ground")
        {
            numGroundObjects++;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Ground")
        {
            numGroundObjects--;
        }
    }
}
