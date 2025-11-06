using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private float gravityStrength;

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

    private void Gravity()
    {
        rb.AddForce(Vector3.down * gravityStrength, ForceMode.Force);
    }
    private void Move()
    {
        Vector2 finalMove = moveAxis.normalized;
        Vector3 finalMove3D = new Vector3(finalMove.x, 0, finalMove.y);
        rb.AddForce(finalMove3D * speed, ForceMode.Force);
    }
    private void Jump()
    {
        if(GetGrounded())
        {
            rb.AddForce(transform.up * jumpPower, ForceMode.Impulse);
        }
    }

    public void UpdateMoveInput(InputAction.CallbackContext context)
    {
        moveAxis = context.ReadValue<Vector2>();
        Debug.Log(moveAxis);
    }
    public void JumpInput(InputAction.CallbackContext context)
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
