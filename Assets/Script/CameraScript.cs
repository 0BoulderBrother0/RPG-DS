using UnityEngine;

public class CameraScript : MonoBehaviour
{

    GameObject target;
    public float moveFactor = 0.01f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 playerToCameraVector = target.transform.position - transform.position;
        transform.position += new Vector3(playerToCameraVector.x * moveFactor, playerToCameraVector.y * moveFactor);
    }
}
