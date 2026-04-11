using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class GlobalLightScript : MonoBehaviour
{

    Light2D light2D;

    public float daylightIntensity = 1;
    public float eveninglightIntensity = 0.1f;
    public float intensityChange = 0.1f;

    public float timeBetweenIntensityChanges = 1;
    public float secondsBetween = 2;

    bool isEvening;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       light2D = GetComponent<Light2D>();
       InvokeRepeating("ToggleDuskDawn", 0, secondsBetween);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ToggleDuskDawn()
    {
        StopAllCoroutines();
        
        if (isEvening)
        {
            StartCoroutine(Dusk());
        }
        else
        {
            StartCoroutine(Dawn());
        }

        isEvening = !isEvening;
    }

    IEnumerator Dusk()
    {
        while (light2D.intensity > eveninglightIntensity)
        {
            light2D.intensity -= intensityChange;
            yield return new WaitForSeconds(timeBetweenIntensityChanges);
        }
    }
    IEnumerator Dawn()
    {
        while (light2D.intensity < daylightIntensity)
        {
            light2D.intensity += intensityChange;
            yield return new WaitForSeconds(timeBetweenIntensityChanges);
        }
    }
}
