using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Random = UnityEngine.Random;

public class LightDimmer : MonoBehaviour
{
    [SerializeField] private Light2D light;
    private bool isDay;

    private void Start()
    {
        DayTimeController.Instance.HourChanged += DayNightSwitch;
        StartCoroutine(TimerLight());
    }

    void DayNightSwitch(object obj, int hour)
    {
        if (hour == 6)
        {
            light.intensity = 0;
            isDay = true;
            StartCoroutine(TimerLight());

        }
        else if (hour == 18)
        {
            isDay = false;
            StartCoroutine(TimerLight());
        }
    }

    IEnumerator TimerLight()
    {
        while (!isDay)
        {
            float currentIntensity = light.intensity;
            light.intensity = Math.Max(currentIntensity + Random.Range(-0.15f, 0.15f), 0);
            yield return new WaitForSeconds(.5f);
        }
    }
}