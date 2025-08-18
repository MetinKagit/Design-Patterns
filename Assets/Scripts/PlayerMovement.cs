using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 6f;

    [Header("Jump")]
    [SerializeField] private float jumpForce = 6.5f;
    [SerializeField] private Transform groundCheck;          // Ayak altına boş bir obje
    [SerializeField, Min(0.01f)] private float groundRadius = 0.15f;
    [SerializeField] private LayerMask groundMask;           // "Ground" katmanı vb.

    private Rigidbody rb;
    private Vector2 moveInput;
    private bool jumpRequested;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.constraints = RigidbodyConstraints.FreezeRotation;

        if (groundCheck == null)
        {
            var gc = new GameObject("GroundCheck");
            gc.transform.SetParent(transform);
            gc.transform.localPosition = new Vector3(0f, -0.9f, 0f);
            groundCheck = gc.transform;
        }

        // // groundMask'e oyuncu katmanı yanlışlıkla ekliyse çıkar
        // int playerLayerMask = 1 << gameObject.layer;
        // if ((groundMask.value & playerLayerMask) != 0)
        //     groundMask &= ~playerLayerMask;
    }

    private void Update()
    {
        // Girişler
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        if (Input.GetKeyDown(KeyCode.Space)) jumpRequested = true;
    }
    
     private void FixedUpdate()
    {
        // Zemin kontrolü
        bool isGrounded = Physics.CheckSphere(
            groundCheck.position,
            groundRadius,
            groundMask,
            QueryTriggerInteraction.Ignore
        );

        // Hareket (dünya ekseni)
        Vector3 vel = rb.linearVelocity;

        if (moveInput.sqrMagnitude > 0.0001f)
        {
            Vector3 dir = new Vector3(moveInput.x, 0f, moveInput.y).normalized;
            vel.x = dir.x * moveSpeed;
            vel.z = dir.z * moveSpeed;
        }
        else
        {
            // Yerdeyken kaymayı kes
            if (isGrounded)
            {
                vel.x = 0f;
                vel.z = 0f;
            }
        }

        // Zıplama
        if (jumpRequested && isGrounded)
        {
            jumpRequested = false;
            vel.y = 0f; 
            rb.linearVelocity = vel;
            rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);

            // TO DO: Observer
            
            return;
        }

        rb.linearVelocity = vel;
        jumpRequested = false;
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
    }
}
