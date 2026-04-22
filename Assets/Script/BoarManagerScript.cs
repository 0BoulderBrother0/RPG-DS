//using Unity.Mathematics;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;
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
    Color boarColor;
    public float boarSpawnInterval = 3;
    public int minNumberOfBoars = 1;
    public int maxNumberOfBoars = 5;
    public float boarSpawnSpread = 1;
    public float turnInvisibleRate = 1f;

    public List<GameObject> activeBoars = new List<GameObject>();
    Coroutine spawnCoroutine;
    Coroutine fadeCoroutine;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = Camera.main;
        sr = boar.GetComponent<SpriteRenderer>();

        screenHeight = cam.orthographicSize;
        screenWidth = screenHeight * cam.aspect;

        boarWidth = sr.bounds.extents.x;
        boarHeight = sr.bounds.extents.y;
        boarColor = sr.color;
    }


    public void InitializeSpawnBoars()
    {
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }
        spawnCoroutine = StartCoroutine(SpawnBoars());
        Debug.Log("Started spawning boars!");
    }

    public void StopSpawnBoars()
    {
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
        }
        fadeCoroutine = StartCoroutine(FadeBoars());
        Debug.Log("Stopped spawning boars!");
    }



    IEnumerator SpawnBoars()
    {
        while (true)
        {
            Vector3 cameraPos = cam.transform.position;
            int nbrOfBoars = Random.Range(minNumberOfBoars, maxNumberOfBoars + 1);
            Vector2 flockSpawnPosition = new Vector2(cameraPos.x + screenWidth + boarWidth + boarSpawnSpread, cameraPos.y + Random.Range(-screenHeight + boarHeight, screenHeight - boarHeight));

            for (int i = 0; i < nbrOfBoars; i++)
            {
                GameObject newBoar = Instantiate(boar, flockSpawnPosition + Random.insideUnitCircle * boarSpawnSpread, Quaternion.identity);
                newBoar.GetComponent<SpriteRenderer>().color = boarColor;
                activeBoars.Add(newBoar);
            }

            yield return new WaitForSeconds(boarSpawnInterval);
        }
    }

    IEnumerator FadeBoars()
    {
        for (int i = activeBoars.Count - 1; i >= 0; i--)
        {
            if (activeBoars[i] != null)
            {
                activeBoars[i].GetComponent<BoarScript>().StartFade();
            }
        }
        yield break;
    }
}
