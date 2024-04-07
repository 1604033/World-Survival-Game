using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class DayTimeController : MonoBehaviour
{
    public static DayTimeController Instance;

    public  Action OnTimeForAnimalsToEat;
    public  Action OnTimeForAnimalsToFinishEating;
    public  Action OnnewDayStart;
    
    public EventHandler<TimeSpan> WorldTimeChanged;
    public EventHandler<int> HourChanged;
    public EventHandler<WorkingShifts> WorkingShiftChanged;
   
    [SerializeField] public float minutesInRealTime = 1;
    float dayLeghtInSecs => minutesInRealTime * 60;
    public float minutesInDay = 1440f;
    private int currentHour;
    private int previousHour;
    private float MinutesLength => dayLeghtInSecs / minutesInDay;
    private TimeSpan _currentTime;
    public WorkingShifts currentWorkingShift;
    private bool hasTriggeredFoodBell = false;
    private bool hasTriggeredMealEnd = false;
    private bool hasTriggeredDayWorkShift = false;
    private bool hasTriggeredNightWorkShift = false;
   
  
    public int currentDayIndex;
    

    private void Awake()
    {
        Instance = this;
      
    }

    private void Start()
    {
        NewDayUpdate();
        StartCoroutine(AddMinutes());
    }

    private IEnumerator AddMinutes()
    {
       
        if (_currentTime.TotalMinutes == minutesInDay)
        {
            NewDayUpdate();
            hasTriggeredNightWorkShift = false;
            hasTriggeredDayWorkShift = false;
        }

        _currentTime += TimeSpan.FromMinutes(1);
        WorldTimeChanged?.Invoke(this,_currentTime);
        currentHour = _currentTime.Hours;
        
        if (currentHour != previousHour)
        {
            HourChanged?.Invoke(this, currentHour);
            previousHour = currentHour;
            if (currentHour >= 20 || currentHour <= 8  && !hasTriggeredNightWorkShift)
            {
                currentWorkingShift = WorkingShifts.Night;
                WorkingShiftChanged?.Invoke(this, currentWorkingShift);
                hasTriggeredNightWorkShift = true;
            }
            else if (currentHour > 8 && currentHour < 20  && !hasTriggeredDayWorkShift)
            {
                currentWorkingShift = WorkingShifts.Day;
                WorkingShiftChanged?.Invoke(this, currentWorkingShift);
                hasTriggeredDayWorkShift  = true;
            }
        }
        yield return new WaitForSeconds(MinutesLength);
        if( !hasTriggeredFoodBell && Math.Abs(GetTimeRation() - 0.83f) < 0.01f) TimeForAnimalsToEat() ;
        if( !hasTriggeredMealEnd && Math.Abs(GetTimeRation() - 0.917f) < 0.01f) TimeForAnimalsToFinishEating() ;
        StartCoroutine(AddMinutes());

       
    }

    void NewDayUpdate()
    {
        currentDayIndex++;
        _currentTime = TimeSpan.Zero;
        OnnewDayStart?.Invoke();
        hasTriggeredFoodBell = false;
        hasTriggeredMealEnd = false;
        
    }
    public float GetTimeRation()
    {
        return (float)_currentTime.TotalMinutes % minutesInDay / minutesInDay;

    }

    void TimeForAnimalsToEat()
    {
        hasTriggeredFoodBell = true;
        OnTimeForAnimalsToEat?.Invoke();
    }  
    void TimeForAnimalsToFinishEating()
    {
        OnTimeForAnimalsToFinishEating?.Invoke();
        hasTriggeredMealEnd = true;
    }
    public enum WorkingShifts
    {
        Day,
        Night,
    }
   
   }
 
   
   
   
  
