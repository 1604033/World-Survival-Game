using System;
using System.Collections;
using UnityEngine;

public class ResourceGatherer : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Transform storage;

    private Transform currentTarget;
    private float minDistance = 0.1f;
    [Min(0)] public float Speed = 2.0f;
    private GathererStates state;

    void Start()
    {
    }

    void Update()
    {
        switch (state)
        {
            case GathererStates.Idle:
                state = GathererStates.OnTransitToResource;
                currentTarget = target;
                MoveTowardsTarget();
                
                break;
            case GathererStates.OnTransitToResource:
                MoveTowardsTarget();
                if (ReachedDestination())
                {
                    state = GathererStates.GatheringResource;
                }
                break;
            case GathererStates.OnTransitToStorage:
                MoveTowardsTarget();
                Debug.Log($"Moving to storage " + ReachedDestination());
                if (ReachedDestination())
                {
                    Debug.Log($"Reached to storage ");
                    state = GathererStates.Idle;
                }
                break;
            case GathererStates.GatheringResource:
                currentTarget = storage;
                MoveTowardsTarget();
                state = GathererStates.OnTransitToStorage;
                break;
        }
    }


    void GoAndGatherResource()
    {
        state = GathererStates.OnTransitToResource;
        currentTarget = target;
        MoveTowardsTarget();
        // StartCoroutine(CheckDistanceFromTarget(
        //     onTargetReached: () =>
        //     {
        //         state = GathererStates.GatheringResource;
        //         GatherAction(
        //             onfinishedAction: () =>
        //             {
        //                 Debug.Log($"Gather action called");
        //                 currentTarget = storage;
        //                 MoveTowardsTarget();
        //                 state = GathererStates.OnTransitToStorage;
        //                 StartCoroutine(CheckDistanceFromTarget(
        //                     () =>
        //                     {
        //                         Debug.Log($"Reached storage");
        //                        //state = GathererStates.Idle; 
        //                     }
        //                 ));
        //             }
        //         );
        //     }));
    }
    void MoveTowardsTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, currentTarget.position, Speed * Time.deltaTime);
    }

    IEnumerator CheckDistanceFromTarget(Action onTargetReached)
    {
        while (Vector3.Distance(target.position, transform.position) > minDistance)
        {
            yield return new WaitForSeconds(1);
        }
        Debug.Log("Arrived");
        onTargetReached?.Invoke();
    }
bool ReachedDestination()
    {
        if (Vector3.Distance(target.position, transform.position) <= minDistance)
        {
            return true;
        }

        return false;
    }

    void GatherAction(Action onfinishedAction)
    {
        Debug.Log($"Started Gather Action");
        onfinishedAction?.Invoke();
    }

    enum GathererStates
    {
        Idle,
        OnTransitToResource,
        OnTransitToStorage,
        GatheringResource,
    }
}