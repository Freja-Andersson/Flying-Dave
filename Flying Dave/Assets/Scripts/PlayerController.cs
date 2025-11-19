using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float pushForce = 10f;

    [Header("TMPro")]
    [SerializeField] TextMeshProUGUI speedText;

    [Header("GroundCheck")]
    [SerializeField] Transform groundTransform;
    [SerializeField] Vector2 groundCheckSize;
    [SerializeField] LayerMask groundLayer;

    float timer = 0;
    float speed;

    Rigidbody2D playerRigidbody;

    void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer > 0.5f)
        {
            CheckSpeed();
            timer = 0;
        }
        
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            PushDown();
        }
    }

    void PushDown()
    {
        if (!CheckGrounded())
        {
            playerRigidbody.AddForce(Vector2.down * pushForce, ForceMode2D.Force);
        }

        if (CheckGrounded())
        {
            playerRigidbody.AddForce(Vector2.right * pushForce, ForceMode2D.Force);
        }
        
    }

    void CheckSpeed()
    {
        /* Vector3 lastPosition = Vector3.zero;
        float speed = Vector3.Distance(transform.position, lastPosition) / Time.deltaTime;
        lastPosition = transform.position; */

        float speedPerSecond = playerRigidbody.linearVelocity.magnitude;

        // Convert units/second to km/h (1 m/s = 3.6 km/h)
        speed = speedPerSecond * 3.6f;

        speedText.text = speed.ToString("F0");
    }

    bool CheckGrounded()
    {
        Collider2D isGrounded = Physics2D.OverlapBox(groundTransform.position, groundCheckSize, 0f, groundLayer);
        return isGrounded;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(groundTransform.position, groundCheckSize);
    }

}
