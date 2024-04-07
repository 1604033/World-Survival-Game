using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalSoundController : MonoBehaviour
{
    [SerializeField] private AudioSource chawinChewingsound;

    private void Start()
    {
        DayTimeController.Instance.OnTimeForAnimalsToEat += TimeToEat;
        DayTimeController.Instance.OnTimeForAnimalsToFinishEating += TimeToFinishEating;
    }

    private void TimeToEat()
    {
        chawinChewingsound.Play();
    }
    private void TimeToFinishEating()
    {
        chawinChewingsound.Stop();
    }
}
