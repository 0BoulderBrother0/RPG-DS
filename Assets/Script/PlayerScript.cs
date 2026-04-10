using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    float xAxis;
    float yAxis;
    public float moveSpeed = 5;
    public float highGrassSpeedReduction = 0.5f;
    float speed;

    Rigidbody2D rb;
    Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        speed = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        xAxis = Input.GetAxisRaw("Horizontal");
        yAxis = Input.GetAxisRaw("Vertical");

        rb.linearVelocity = new Vector2(xAxis, yAxis) * speed;


        // ---- Player animator ----
        if (rb.linearVelocity == Vector2.zero)
        {
            animator.Play("player_idle");
        }

        else if (rb.linearVelocityX > 0)
        {
            animator.Play("player_right");
        }
        else if (rb.linearVelocityX < 0)
        {
            animator.Play("player_left");
        }

        else if (rb.linearVelocityY > 0)
        {
            animator.Play("player_up");
        }
        else if (rb.linearVelocityY < 0)
        {
            animator.Play("player_down");
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("HighGrass"))
        {
            speed = moveSpeed * highGrassSpeedReduction;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("HighGrass"))
        {
            speed = moveSpeed;
        }
    }
}
