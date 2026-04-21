using UnityEngine;

public class PlantScript : MonoBehaviour
{

    public Sprite[] sprites;
    public int state;
    public float pluckTime = 3;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Boar"))
        {
            Destroy(gameObject);
        }
    }

    public void WaterPlant()
    {
        Invoke("ChangePlant", pluckTime);
    }

    void ChangePlant()
    {
        if (state == 0)
        {
            GetComponent<SpriteRenderer>().sprite = sprites[0];
            state++;
        }
    }
}
