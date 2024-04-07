using System;
using TMPro;
using UnityEngine;

public class ClockDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timeDisplay;
    [SerializeField] private TextMeshProUGUI dateDisplay;
    [SerializeField] private Transform clockChart;
    void Start()
    {
        dateDisplay.text = $"Day {DayTimeController.Instance.currentDayIndex}";
        DayTimeController.Instance.WorldTimeChanged += UpdateClock;
        DayTimeController.Instance.OnnewDayStart += NewDay;
    }

    private void NewDay()
    {
        if (clockChart != null) clockChart.Rotate(0, 0, 0);
        dateDisplay.text = $"Day {DayTimeController.Instance.currentDayIndex}";
    }

    private void UpdateClock(object sender, TimeSpan e)
    {
        if (timeDisplay != null)
            if(e.Seconds % 10 == 0)
                timeDisplay.text = e.ToString();
        float angle = 180f /(DayTimeController.Instance.minutesInRealTime * 60f) ;
        if (clockChart != null) clockChart.Rotate(0, 0, angle);
    }
     
}
