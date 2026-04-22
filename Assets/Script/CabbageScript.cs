using System.Collections;
using UnityEngine;

public class CabbageScript : MonoBehaviour
{

    public float decayRate;
    public float decayAmount;

    public Coroutine decayCabbage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (decayCabbage == null)
        {
            decayCabbage = StartCoroutine(DecayCabbage());
        }
    }

    public IEnumerator DecayCabbage()
    {
        while (transform.localScale.x >= 0.1f)
        {
            transform.localScale -=  new Vector3(decayAmount, decayAmount);
            yield return new WaitForSeconds(1 / decayRate);
        }
        Destroy(gameObject);
    }
}
