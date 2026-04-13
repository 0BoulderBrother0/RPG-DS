using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class BoarScript : MonoBehaviour
{
    
    Rigidbody2D rb;
    Animator animator;


    Vector2 speed;
    public Vector2 boarSpeed = new Vector2(-2, 0);
    public float highGrassSpeedReduction = 0.5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        speed = boarSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("HighGrass"))
        {
            speed = boarSpeed * highGrassSpeedReduction;
            animator.speed = 1 * highGrassSpeedReduction;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("HighGrass"))
        {
            speed = boarSpeed * highGrassSpeedReduction;
            animator.speed = 1 * highGrassSpeedReduction;
        }
    }
}
