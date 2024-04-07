using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioVariation : MonoBehaviour
{

    public Transform playerTransform; // Reference to the player's transform
    public AudioSource audioSource; // Reference to the AudioSource
    public float maxDistance = 10f; // The maximum distance at which the sound is audible
    public float minVolume = 0.01f; // The minimum volume when the player is at max distance

    private void Start()
    {
        playerTransform = GameManager.instance.player.transform;
    }

    private void Update()
    {
        if (playerTransform == null)
        {
            return;
        }
        float distance = Vector2.Distance(transform.position, playerTransform.position);
        float volume = 1f - Mathf.Clamp01(distance / maxDistance);
        volume = Mathf.Lerp(minVolume, 1f, volume);
        audioSource.volume = volume;
    }
}
