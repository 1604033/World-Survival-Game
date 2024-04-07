using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kreechu_Deploy : MonoBehaviour
{
   public Animator animator;
    public UnityEngine.AI.NavMeshAgent _navMeshAgent;
    public Transform playerTransform;

    private bool isDeployed = false;
    private bool isMoving = false;

    private void Start()
    {
        _navMeshAgent.updatePosition = false;
        _navMeshAgent.updateUpAxis = false;
    }

    void Update()
    {
        if (!isDeployed)
        {
            MoveTowardsPlayer();
        }

        if (!isMoving)
        {
            StopAnimateMotion();
        }
    }

    void AnimateMotion(Vector2 direction)
    {
        if (animator != null)
        {
            animator.SetBool("IsMoving", true);
            animator.SetFloat("MoveX", direction.x);
            animator.SetFloat("MoveY", direction.y);
        }
    }

    void StopAnimateMotion()
    {
        if (animator != null)
        {
            animator.SetBool("IsMoving", false);
        }
    }

    public void Deploy()
    {
        isDeployed = true;
        // Implement deployment logic
    }

    public void Undeploy()
    {
        isDeployed = false;
        // Implement undeployment logic
    }

    IEnumerator MoveTowardsPlayer()
    {
        while (!isDeployed)
        {
            Vector3 direction = (playerTransform.position - transform.position).normalized;
            _navMeshAgent.Move(direction * Time.deltaTime);
            AnimateMotion(new Vector2(direction.x, direction.z));
            isMoving = true;
            yield return null;
        }
        isMoving = false;
    }
}
