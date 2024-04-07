using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public static Action<Vector3> OnPlayerMovement;
    public static Action OnPlayerMovementStop;
    public float speed;
    public Animator animator;
    public Vector3 direction;
    
    private void Update()
    {
       float horizontal = Input.GetAxisRaw("Horizontal");
       float vertical = Input.GetAxisRaw("Vertical");
       /*if (horizontal != 0)
       {
           vertical = 0;
       }

       if (vertical != 0)
       {
           horizontal = 0;
       }*/

       direction = new Vector3(horizontal,vertical);
       direction = direction.normalized;

       AnimateMovement(direction);

       transform.position += direction*speed*Time.deltaTime;
    }

    public Vector3 GetDirectionVector()
    {
        return direction;
    }
    void AnimateMovement(Vector3 direction)
    {
        if(animator != null)
        {
            if(direction.magnitude > 0)
            {
                OnPlayerMovement?.Invoke(direction);
                animator.SetBool("IsMoving",true);
                animator.SetFloat("MoveX",direction.x);
                animator.SetFloat("MoveY",direction.y);
            }
            else
            {
                OnPlayerMovementStop?.Invoke();
                animator.SetBool("IsMoving",false);
            }
        }
    }
}
