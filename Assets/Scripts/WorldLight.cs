using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Serialization;

public class WorldLight : MonoBehaviour
{
 [SerializeField] private DayTimeController _dayTimeController;
 //Day Light
 [SerializeField] private Light2D dayLight;
 [SerializeField] private Gradient _dayGradient;
  private void Start()
  {
      _dayTimeController.WorldTimeChanged += OnDayTimeChanged;
  }

  private void OnDayTimeChanged(object sender, TimeSpan newTime)
  {
    StimulateLight(newTime);
    
  }
  
  void StimulateLight(TimeSpan newTime)
  {
    float time = CalculatePercent(newTime); 
    dayLight.color = _dayGradient.Evaluate(time);
  }
  

  private void OnDestroy()
  {
    _dayTimeController.WorldTimeChanged -= OnDayTimeChanged;
  }

  private float CalculatePercent(TimeSpan timeSpan)
  {
     return (float)timeSpan.TotalMinutes % _dayTimeController.minutesInDay / _dayTimeController.minutesInDay;
  }
  
  
}
