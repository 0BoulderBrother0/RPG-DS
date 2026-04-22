using System.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
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

    [Header("Cabbage")]
    public GameObject cabbageObject;
    public float cabbageThrowSpeed;
    public float cabbageRotationSpeed;
    public float secondsStartCabbageDecay = 3;


    bool wateringPlant;
    bool haltMovement;



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
        PlayAnimations();

        if (Input.GetKeyUp(KeyCode.E))
        {
            ThrowCabbage();
        }
    }


    void PlayAnimations()
    {
        if (rb.linearVelocity == Vector2.zero)
        {
            animator.Play("player_idle");
        }


        else if (rb.linearVelocityY > 0)
        {

            animator.Play("player_up");
        }

        else if (rb.linearVelocityY < 0) 
        {
            animator.Play("player_down");
        }


        else if (rb.linearVelocityX > 0)
        {
            animator.Play("player_right");
        }

        else if (rb.linearVelocityX < 0)
        {
            animator.Play("player_left");
        }
    }

    private void ThrowCabbage()
    {
        Vector2 playerPos = transform.position;
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mouseDirection = (mousePos - playerPos).normalized;

        GameObject newCabbage = Instantiate(cabbageObject, playerPos + mouseDirection, Quaternion.identity);
        Rigidbody2D newCabbageRB = newCabbage.GetComponent<Rigidbody2D>();

        if (mouseDirection.x < 0)
        {
            newCabbage.transform.right = -mouseDirection;
            newCabbageRB.AddTorque(-cabbageRotationSpeed);
        }
        else
        {
            newCabbage.transform.right = mouseDirection;
            newCabbageRB.AddTorque(cabbageRotationSpeed);
        }

        newCabbageRB.linearVelocity = mouseDirection * cabbageThrowSpeed;
        StartCoroutine(StartDecayCabbage(newCabbage));
    }

    IEnumerator StartDecayCabbage(GameObject cabbage)
    {
        CabbageScript cs = cabbage.GetComponent<CabbageScript>();

        yield return new WaitForSeconds(secondsStartCabbageDecay);

        if (cs.decayCabbage == null)
        {
            cs.decayCabbage = StartCoroutine(cs.DecayCabbage());
        }
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("HighGrass"))
        {
            speed = moveSpeed * highGrassSpeedReduction;
            animator.speed = 1 * highGrassSpeedReduction;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("HighGrass"))
        {
            speed = moveSpeed;
            animator.speed = 1;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Plant") && Input.GetKeyUp(KeyCode.Space))
        {
            if (collision.GetComponent<PlantScript>().state == 0)
            {
                collision.GetComponent<PlantScript>().WaterPlant();
                Debug.Log("Plant");
            }
        }
    }

}
