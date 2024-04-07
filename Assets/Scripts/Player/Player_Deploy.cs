using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player_Deploy : MonoBehaviour
{
     public static Action<Vector3> OnPlayerMovement;
    public static Action OnPlayerMovementStop;
    public float speed;
    public Animator animator;
    public Vector3 direction;
    
    [SerializeField]
    private bool isDeployed = false;
    [SerializeField]
    private GameObject[] kreechus;
   [SerializeField]
    private Vector3[] kreechuPositions;

    private void Start()
    {
        kreechus = GameObject.FindGameObjectsWithTag("Kreechu");
        GetKreechuPositions();
    }

    private void Update()
    {
        HandleDeployment();

        if (!isDeployed)
        {
            HandlePlayerMovement();
        }
    }

    private void HandleDeployment()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isDeployed)
            {
                UndeployKreechus();
                ResumePlayerMovement();
            }
            else
            {
                DeployKreechus();
                StopPlayerMovement();
            }
        }
    }

    private void DeployKreechus()
    {
        isDeployed = true;
        for (int i = 0; i < 4 ; i++)
        {
            kreechus[i].transform.position = transform.position + kreechuPositions[i];
            kreechus[i].GetComponent<Kreechu_Deploy>().Deploy();
            Debug.Log("Krechu no");
        }
    }

    private void UndeployKreechus()
    {
        isDeployed = false;
        for (int i = 0; i < 4; i++)
        {
            kreechus[i].GetComponent<Kreechu_Deploy>().Undeploy();
            Debug.Log(kreechus.Length);
        }
    }

    private void GetKreechuPositions()
    {
        // Define the positions relative to the player for kreechus deployment
        float distance = 0.05f; // 3-5 centimeters

        Vector3 eastPosition = new Vector3(distance, 0f, 0f);
        Vector3 westPosition = new Vector3(-distance, 0f, 0f);
        Vector3 northPosition = new Vector3(0f, 0f, distance);
        Vector3 southPosition = new Vector3(0f, 0f, -distance);

        kreechuPositions = new Vector3[] { eastPosition, westPosition, northPosition, southPosition };
    }

    private void HandlePlayerMovement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        direction = new Vector3(horizontal, vertical);
        direction = direction.normalized;

        AnimateMovement(direction);

        transform.position += direction * speed * Time.deltaTime;
    }

    private void AnimateMovement(Vector3 direction)
    {
        if (animator != null)
        {
            if (direction.magnitude > 0)
            {
                OnPlayerMovement?.Invoke(direction);
                animator.SetBool("IsMoving", true);
                animator.SetFloat("MoveX", direction.x);
                animator.SetFloat("MoveY", direction.y);
            }
            else
            {
                OnPlayerMovementStop?.Invoke();
                animator.SetBool("IsMoving", false);
            }
        }
    }

    private void StopPlayerMovement()
    {
        // Stop player movement when deployed
        speed = 0f;
    }

    private void ResumePlayerMovement()
    {
        // Resume player movement when undeployed
        speed = 3f/* Your original speed value */;
    }
}
