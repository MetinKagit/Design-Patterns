using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 6f;
    public bool cameraRelative = true;
    public Transform cameraTransform;

    [Header("Jump")]
    [Tooltip("Hedef zıplama yüksekliği (metre).")]
    public float jumpHeight = 0.9f;
    [Tooltip("Zeminden ayrıldıktan sonra kısa süre zıplamaya izin (saniye).")]
    public float coyoteTime = 0.1f;
    [Tooltip("Zıpladıktan hemen sonra zemini kısa süre yok say (saniye). Sekmeyi engeller.")]
    public float postJumpGroundIgnore = 0.06f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundRadius = 0.18f;
    public LayerMask groundMask = 0;

    Rigidbody rb;

    // Durum
    bool grounded;
    bool jumpQueued;
    bool jumpHeld;
    bool jumpConsumed;
    float lastGroundedTime;
    float lastJumpTime;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (rb != null) rb.freezeRotation = true;

        // groundMask yanlışlıkla Player layer'ını içeriyorsa uyar
        int playerLayer = gameObject.layer;
        if ((groundMask.value & (1 << playerLayer)) != 0)
        {
            Debug.LogWarning("[PlayerMovement] groundMask oyuncu layer'ını içeriyor. Lütfen çıkarın.");
        }

        if (groundCheck == null)
        {
            Debug.LogWarning("[PlayerMovement] groundCheck atanmadı. Lütfen ayak hizasında bir boş obje atayın.");
        }
    }

    void Update()
    {
        // Sadece Space ile kuyrukla. Input Manager 'Jump' tanımındaki sürprizleri dışarıda bırak.
        if (!jumpHeld && Input.GetKeyDown(KeyCode.Space))
        {
            jumpQueued = true;
            jumpHeld = true;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            jumpHeld = false;
        }
    }

    void FixedUpdate()
    {
        // 1) Ground
        bool allowGround = Time.time - lastJumpTime > postJumpGroundIgnore;
        grounded = allowGround && IsGrounded();
        if (grounded)
        {
            lastGroundedTime = Time.time;
            jumpConsumed = false;
        }

        // 2) Hareket
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 moveDir;
        if (cameraRelative && cameraTransform != null)
        {
            Vector3 f = cameraTransform.forward; f.y = 0f; f.Normalize();
            Vector3 r = cameraTransform.right;   r.y = 0f; r.Normalize();
            moveDir = (f * v + r * h);
        }
        else
        {
            moveDir = new Vector3(h, 0f, v);
        }
        if (moveDir.sqrMagnitude > 1f) moveDir.Normalize();

        Vector3 vel = rb.linearVelocity;
        // Küçük girdilerde drift'i kes
        if (moveDir.sqrMagnitude < 0.0001f && grounded)
        {
            vel.x = 0f;
            vel.z = 0f;
        }
        else
        {
            vel.x = moveDir.x * moveSpeed;
            vel.z = moveDir.z * moveSpeed;
        }
        rb.linearVelocity = vel;

        // 3) Zıplama (coyote + tek basış)
        bool canCoyote = Time.time - lastGroundedTime <= coyoteTime;
        if (!jumpConsumed && jumpQueued && (grounded || canCoyote))
        {
            Jump();
            jumpConsumed = true;
        }
        jumpQueued = false; // tek karelik istek
    }

    bool IsGrounded()
    {
        if (groundCheck == null) return false;
        return Physics.CheckSphere(
            groundCheck.position,
            groundRadius,
            groundMask,
            QueryTriggerInteraction.Ignore
        );
    }

    void Jump()
    {
        float jumpVel = Mathf.Sqrt(2f * Physics.gravity.magnitude * jumpHeight);
        Vector3 v = rb.linearVelocity;
        if (v.y < 0f) v.y = 0f; // aşağı iniyorsa sıfırla
        v.y = jumpVel;
        rb.linearVelocity = v;
        lastJumpTime = Time.time; // post-jump ground ignore kilidi
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
    }
}
