using UnityEngine;

public class BoarScript : MonoBehaviour
{
    
    Rigidbody2D rb;
    Animator animator;

    public float boarSpeed = -3;
    public float highGrassSpeedReduction = 0.5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        rb.linearVelocityX = boarSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("HighGrass"))
        {
            rb.linearVelocityX = boarSpeed * highGrassSpeedReduction;
            animator.speed = 1 * highGrassSpeedReduction;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("HighGrass"))
        {
             rb.linearVelocityX = boarSpeed * highGrassSpeedReduction;
            animator.speed = 1 * highGrassSpeedReduction;
        }
    }
}
