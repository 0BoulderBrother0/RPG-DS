//using Unity.Mathematics;
using System.Collections;
using UnityEngine;
//using UnityEngine.PlayerLoop;

public class BoarManagerScript : MonoBehaviour
{
    Camera cam;
    float screenWidth;
    float screenHeight;
    float boarWidth;
    float boarHeight;

    public GameObject boar;
    SpriteRenderer sr;
    public float boarSpawnInterval = 3;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = Camera.main;
        sr = boar.GetComponent<SpriteRenderer>();

        screenHeight = cam.orthographicSize;
        screenWidth = screenHeight * cam.aspect;

        boarWidth = sr.bounds.extents.x;
        boarHeight = sr.bounds.extents.y;

        StartCoroutine(SpawnBoar());
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    IEnumerator SpawnBoar()
    {
        while (true)
        {
            Vector3 cameraPos = cam.transform.position;

            Instantiate(boar, new Vector3(cameraPos.x + screenWidth + boarWidth, cameraPos.y + Random.Range(-screenHeight + boarHeight, screenHeight - boarHeight)), Quaternion.identity);

            yield return new WaitForSeconds(boarSpawnInterval); 
        }
    }
}
