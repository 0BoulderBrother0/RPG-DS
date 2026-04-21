using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class BoarScript : MonoBehaviour
{

    Rigidbody2D rb;
    Animator animator;
    SpriteRenderer sr;


    public Vector2 boarSpeed = new Vector2(-2, 0);
    public float highGrassSpeedReduction = 0.5f;
    bool isInHighGrass;
    public float animationSpeedIncrease;
    public float speedThresholdForIdle = 0.1f;


    bool hasCollidedWithCabbage = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();

        rb.linearVelocity = boarSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (isInHighGrass)
        {
            animator.speed = Mathf.Abs(rb.linearVelocity.magnitude) * animationSpeedIncrease * highGrassSpeedReduction;
        }
        else
        {
            animator.speed = Mathf.Abs(rb.linearVelocity.magnitude) * animationSpeedIncrease;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("HighGrass"))
        {
            rb.linearVelocity = rb.linearVelocity * highGrassSpeedReduction;
            animator.speed = animator.speed * highGrassSpeedReduction;
            isInHighGrass = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("HighGrass"))
        {
            rb.linearVelocity = rb.linearVelocity / highGrassSpeedReduction;
            animator.speed = animator.speed / highGrassSpeedReduction;
            isInHighGrass = false;
        }
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Cabbage") && hasCollidedWithCabbage == false && isInHighGrass)
        {
            hasCollidedWithCabbage = true;
            rb.linearVelocity = collision.collider.GetComponent<Rigidbody2D>().linearVelocity * highGrassSpeedReduction;
        }
        else if (collision.collider.CompareTag("Cabbage") && hasCollidedWithCabbage == false)
        {
            hasCollidedWithCabbage = true;
            rb.linearVelocity = collision.collider.GetComponent<Rigidbody2D>().linearVelocity;
        }

        /*
        if (rb.linearVelocityX > 0)
        {
            sr.flipX = true;
        }
        else
        {
            sr.flipX = false;
        }

        if (animator.speed + Mathf.Abs(rb.linearVelocity.magnitude) * animationSpeedIncrease < 0 || rb.linearVelocity.magnitude < speedThresholdForIdle)
        {
            animator.speed = 0;

            rb.linearVelocity = Vector2.zero;

            // Spela idle animation här!
        }
        else if (isInHighGrass)
        {
            animator.speed = (animator.speed + Mathf.Abs(rb.linearVelocity.magnitude) * animationSpeedIncrease) * highGrassSpeedReduction;
        }
        else
        {
            animator.speed = animator.speed + Mathf.Abs(rb.linearVelocity.magnitude) * animationSpeedIncrease;
        }*/
    }
}
