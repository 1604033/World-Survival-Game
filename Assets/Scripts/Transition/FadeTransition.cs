using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeTransition : MonoBehaviour
{
    public static Action OnAnimationCompleted;
    private Animator _animator;
    bool isFading = false;
    [SerializeField] private Canvas canvas;
    private void Start()
    {
        _animator = GetComponent<Animator>();
    }
    public IEnumerator FadeIn()
    {
        isFading = true;
        canvas.enabled = true;
        _animator.SetTrigger($"FadeIn");
        while (isFading)
        {
           yield return null; 
        }
    } 
    public IEnumerator FadeOut()
    {
        canvas.enabled = true;
        isFading = true;
        _animator.SetTrigger($"FadeOut");
        while (isFading)
        {
           yield return null; 
        }
    }

    public void AnimationComplete()
    {
        canvas.enabled = false;
        isFading = false;
    }
}
