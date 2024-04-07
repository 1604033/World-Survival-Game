using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AudioController : MonoBehaviour, IPointerClickHandler
{
   [SerializeField] AudioSource audioSource;
   
   [SerializeField] private Image image;
   [SerializeField] private Sprite audioPlayingSprite;
   [SerializeField] private Sprite audioStoppedSprite;
   private bool isPlaying;
   private void Awake()
   {
       isPlaying = true;
   }

   public void OnPointerClick(PointerEventData eventData)
    {
        if (isPlaying)
        {
            audioSource.Stop();
            image.sprite = audioStoppedSprite;
        }
        else
        {
            audioSource.Play();
            image.sprite = audioPlayingSprite;

        }

        isPlaying = !isPlaying;
    }
}
