using UnityEngine;

public class MovimentPlayer : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 7f;
    public float climbSpeed = 3f;

    [Header("Controle")]
    [SerializeField] private bool isControlled = false;

    public bool IsControlled => isControlled;

    public void SetControlled(bool value)
    {
        isControlled = value;
    }

    private Rigidbody rb;
    private bool isGrounded;
    private bool isOnLadder;

    private Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

         
    }

    void Update()
    {
        if (!isControlled)
        return;
        // Pulo apenas se não estiver na escada
       if (Input.GetButtonDown("Jump"))
{
    if (isOnLadder)
    {
        isOnLadder = false;
        rb.useGravity = true;

        rb.linearVelocity = new Vector3(
            rb.linearVelocity.x,
            jumpForce,
            rb.linearVelocity.z
        );
    }
    else if (isGrounded)
    {
        rb.linearVelocity = new Vector3(
            rb.linearVelocity.x,
            jumpForce,
            rb.linearVelocity.z
        );
    }
}
    }

    void FixedUpdate()
    {

      

     if (!isControlled)
    {
        rb.linearVelocity = new Vector3(
            0,
            rb.linearVelocity.y,
            0);

        anim.SetBool("isWalking", false);

        return;
    }

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        // Movimento na escada
        if (isOnLadder)
        {
            rb.useGravity = false;

            bool isClimbing = Mathf.Abs(v) > 0.1f;

            anim.SetBool("isWalking", isClimbing);

            if (isClimbing)
            {
                anim.SetFloat("moveX", 0);
                anim.SetFloat("moveY", 1);
            }

            rb.linearVelocity = new Vector3(
                0f,
                v * climbSpeed,
                0f
            );

            return;
        }

        // Fora da escada
        rb.useGravity = true;

        Vector3 move = new Vector3(h, 0f, v);

        bool isWalking = move.magnitude > 0.1f;

        if (anim != null)
{
    anim.SetBool("isWalking", isWalking);
}

        if (isWalking)
        {
            anim.SetFloat("moveX", h);
            anim.SetFloat("moveY", v);
        }

        if (!isWalking)
        {
            rb.linearVelocity = new Vector3(
                0f,
                rb.linearVelocity.y,
                0f
            );
        }
        else
        {
            move = move.normalized;

            rb.linearVelocity = new Vector3(
                move.x * speed,
                rb.linearVelocity.y,
                move.z * speed
            );

            transform.forward = move;
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isGrounded = true;
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isGrounded = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ladder"))
        {
            isOnLadder = true;
            rb.linearVelocity = Vector3.zero;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ladder"))
        {
            isOnLadder = false;
        }
    }
}