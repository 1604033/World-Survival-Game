using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioControllerGlobal : MonoBehaviour
{
    [SerializeField] private AudioSource mealBellAudioSource;

    void Start()
    {
        DayTimeController.Instance.OnTimeForAnimalsToEat += PlayMealTimeAudio;

    }

    void PlayMealTimeAudio()
    {
        mealBellAudioSource.Play();
    }
   
}
