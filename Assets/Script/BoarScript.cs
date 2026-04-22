using System.Collections;
using TMPro;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Rendering;

public class BoarScript : MonoBehaviour
{

    Rigidbody2D rb;
    Animator animator;
    SpriteRenderer sr;

    List<GameObject> activeBoars;
    int activeBoarIndex;

    Coroutine fadeBoar;
    public bool isFading = false;

    Coroutine calculateStandingStill;


    [Header("Boar Speed")]
    public Vector2 boarSpeed = new Vector2(-2, 0);
    public float highGrassSpeedReduction = 0.5f;
    bool isInHighGrass;
    public float animationSpeedIncrease;
    public float speedThresholdForIdle = 0.1f;
    public float boarCabbageReactSpeed = 2;


    bool hasCollidedWithCabbage = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();

        activeBoars = GameObject.FindGameObjectWithTag("BoarManager").GetComponent<BoarManagerScript>().activeBoars;


        rb.linearVelocity = boarSpeed;
    }

    // Update is called once per frame
    void Update()
    {

        animator.speed = Mathf.Abs(rb.linearVelocity.magnitude) * animationSpeedIncrease;

        if (rb.linearVelocity.magnitude <= speedThresholdForIdle && calculateStandingStill == null)
        {
            calculateStandingStill = StartCoroutine(CalculateStandingStill());
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("HighGrass"))
        {
            rb.linearVelocity = rb.linearVelocity * highGrassSpeedReduction;
            isInHighGrass = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("HighGrass"))
        {
            rb.linearVelocity = rb.linearVelocity / highGrassSpeedReduction;
            isInHighGrass = false;
        }
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Cabbage") && hasCollidedWithCabbage == false && isInHighGrass)
        {
            hasCollidedWithCabbage = true;
            rb.linearVelocity = collision.collider.GetComponent<Rigidbody2D>().linearVelocity * highGrassSpeedReduction * boarCabbageReactSpeed;
        }
        else if (collision.collider.CompareTag("Cabbage") && hasCollidedWithCabbage == false)
        {
            hasCollidedWithCabbage = true;
            rb.linearVelocity = collision.collider.GetComponent<Rigidbody2D>().linearVelocity * boarCabbageReactSpeed;
        }


        if (rb.linearVelocityX > 0)
        {
            sr.flipX = true;
        }
        else
        {
            sr.flipX = false;
        }
    }

    IEnumerator CalculateStandingStill()
    {
        int seconds = 0;

        while (seconds < 3)
        {
            if (rb.linearVelocity.magnitude > speedThresholdForIdle)
            {
                calculateStandingStill = null;
                yield break;
            }

            seconds++;
            yield return new WaitForSeconds(1);
        }

        StartFade();

        calculateStandingStill = null;
    }

    public void StartFade()
{
    if (isFading == false)
    {
        if (calculateStandingStill != null)
        {
            StopCoroutine(calculateStandingStill);
            calculateStandingStill = null;
        }
        fadeBoar = StartCoroutine(FadeBoar());
    }
}

    IEnumerator FadeBoar()
    {
        isFading = true;
        float currentAlpha = 1f;

        float turnInvisibleRate = GameObject.FindGameObjectWithTag("BoarManager").GetComponent<BoarManagerScript>().turnInvisibleRate;

        while (currentAlpha > 0f)
        {
            currentAlpha = Mathf.Max(0f, currentAlpha - turnInvisibleRate * Time.deltaTime);
            Color boarColor = sr.color;
            boarColor.a = currentAlpha;
            sr.color = boarColor;
            
            yield return null;
        }
        activeBoars.RemoveAt(activeBoars.IndexOf(gameObject));
        Destroy(gameObject);
    }
}
