using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine.AI;

public class FoomMove : MonoBehaviour
{
   public Animator animator;
    public NavMeshAgent _navMeshAgent;
    public Vector2 mousePosition;
    public Vector2 mousePositionVector;
    public Transform playerTransform;
    
    private Vector2 playerTransformPosition;
    private Vector2 currentTransformPosition;

    public float distance = .50f;
    public float speed = 2f;
    private bool canMove = false;
    private bool isPlayerBehind = false;

    // Distance from the player


    private void Start()
    {
        _navMeshAgent.updatePosition = false;
        _navMeshAgent.updateUpAxis = false;
        //animator = GetComponent<Animator>();
    }

    void Update()
    {
        playerTransformPosition = playerTransform.position;
        currentTransformPosition = transform.position;
        canMove = Vector3.Distance(currentTransformPosition, playerTransformPosition) > distance;
        if (canMove)
            StartCoroutine(MoveTowardsPlayer());
        mousePosition = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (mousePosition.x != 0 || mousePosition.y != 0)
            mousePositionVector = mousePosition;


        if (mousePosition.magnitude == 0 && !canMove)
        {
            StopAnimateMotion();
            animator.SetBool("isRunning",false);
            return;
        }
        if (mousePosition.magnitude != 0 && canMove)
        {
            animator.SetBool("isRunning",true);
            animator.SetFloat("horizontal", mousePositionVector.x);
            animator.SetFloat("vertical", mousePositionVector.y);
        }

        AnimateMotion();
    }

    private void FixedUpdate()
    {
    }

    void AnimateMotion()
    {
        if (animator != null)
        {
            animator.SetBool("IsMoving", true);
            animator.SetBool("IsHit", false);
            animator.SetFloat("MoveX", mousePositionVector.x);
            animator.SetFloat("MoveY", mousePositionVector.y);
        }
    }

    void StopAnimateMotion()
    {
        if (animator != null)
        {
            animator.SetBool("IsMoving", false);
            animator.SetBool("IsHit", false);
        }
    }
    
   IEnumerator MoveTowardsPlayer()
    {
        while (Vector3.Distance(transform.position, playerTransform.position) > distance)
        {
            transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, speed * Time.deltaTime);
            yield return null;
        }
    }


    void AnimateHitMotion()
    {
        if (animator != null)
        {
            animator.SetBool("IsHit", true);
        }
    }
}
