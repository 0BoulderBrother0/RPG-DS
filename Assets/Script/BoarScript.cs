using UnityEngine;

public class BoarScript : MonoBehaviour
{
    
    Rigidbody2D rb;

    public float boarSpeed = 3;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocityX = -boarSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
