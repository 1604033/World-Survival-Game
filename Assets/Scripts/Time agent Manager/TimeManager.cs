using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static event  Action newDateUpdated;
    public Timeagent _timeagent;
    private float _lastUpdatetime;
    void Start()
    {
        _lastUpdatetime = Time.time;
        newDateUpdated += UpdateNewDateTime;
    }

    // Update is called once per frame
    void Update()
    {
        float currentTime = Time.time;
        float secondsPassed = currentTime - _lastUpdatetime;
        float minutesPassed = secondsPassed / 30f;
        if ( minutesPassed >= _timeagent.MINUTESINADAY)
        {
            newDateUpdated?.Invoke();
        } 
    }

    void UpdateNewDateTime()
    { 
     _lastUpdatetime = Time.time;
     _timeagent.DatesPassed++;
     }
}
