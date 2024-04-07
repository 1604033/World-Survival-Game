using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AI;

public class BuilderController : MonoBehaviour
{
    
    private NavMeshAgent _navMeshAgent;
    private List<Transform> points;
    private int currentPointIndex;
    private bool isMoving;
    private bool isContructing = false;
    private int counter;
    private int callCounter;
    private int moveCallCounter = 0;
    private float destinationThreshold = 1f;
    private float startingLevel = 20f;
    [SerializeField]private float timeToPlayHitAnimation = 10f;
    private BuildingBase buildingConstructed;
    public BuildingsManager BuildingManager;
    [SerializeField] private Animator animator;
    private Vector3 target;
    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.updateRotation = false;
        _navMeshAgent.updateUpAxis = false;
    }

    private void Start()
    {
        BuildingManager = BuildingsManager.Instance;
    }


    // private void Update()
    // {
    //     isMoving = _navMeshAgent.velocity.magnitude >= destinationThreshold;
    //
    //     if (points !=  null)
    //     {
    //         if (points.Count == 0)
    //         {
    //             return;
    //             
    //         }
    //     }
    //
    //     if (isMoving)
    //     {
    //         AnimateMotion();
    //     }
    //     else
    //     {
    //         StopAnimateMotion();
    //
    //         if (!isMoving && currentPointIndex < points.Count)
    //         {
    //             //StartCoroutine(NextPoint());
    //         }
    //     }
    // }
    public IEnumerator Move( List<Transform> pointsToMoveTo,  BuildingBase constructedBuilding)
    {
        int counterIndex = 0;
        moveCallCounter++;
        yield return new WaitForSeconds(2);
        StartCoroutine(MoveCoroutine(pointsToMoveTo[counterIndex].position ,result =>
        {
            if (result)
            {
                constructedBuilding.ConstructBuilding();
                Debug.Log($"First call");
                moveCallCounter++;
                counterIndex++;
                if (moveCallCounter == 2)
                {
                    StartCoroutine(MoveCoroutine(pointsToMoveTo[counterIndex].position , result => 
                    {
                        if (result)
                        {
                            StartCoroutine(StopHitMotion(timeToPlayHitAnimation));
                            Debug.Log($"Second call");
                            constructedBuilding.ConstructBuilding();
                            BuildingManager.RemoveCompleteBuilding(constructedBuilding);
                            isContructing = false;
                            Destroy(gameObject);
                        } 
                    }));
                }
            }
        }));
    }

    public IEnumerator MoveCoroutine(Vector3 targetParam ,Action<bool> callback)
    {
        bool isRunning = Vector3.Distance(transform.position, targetParam) > destinationThreshold;
        while (isRunning && isContructing)
        {
            if (_navMeshAgent == null)
            {
                Debug.Log("Nav mpty");    
            }
            _navMeshAgent.SetDestination(new Vector3(targetParam.x, targetParam.y, 0f));
            AnimateMotion();
            isRunning = Vector3.Distance(transform.position, targetParam) > destinationThreshold;
            if (!isRunning)
            {
                StopAnimateMotion();
                AnimateHitMotion();
            }
            yield return new WaitForSeconds(1f);
        }

       yield return StopHitMotion(timeToPlayHitAnimation);
        callback.Invoke(true);
        StopAnimateMotion();
    }

    private IEnumerator Wait(float time)
    {
       int counterWait = 0;
       int maxLevelIncrementPerStage = 40; 
      float callCountFloat = 0f;
       float increment = maxLevelIncrementPerStage / time  ;
       while (counterWait < time)
        {
            
                callCountFloat += increment;
                buildingConstructed.CalculateConstructionLevel(increment);
                counterWait++;
            yield return new WaitForSeconds(1f);
        }
    }
    // public bool MoveToPointOne(Vector3 transformPointOne)
    // {
    //
    //     return false;
    // }
    
    public void MoveBuilderAcrossPoints(List<Transform> movePoints, BuildingBase BuildingBase)
    {
        isContructing = true;
        moveCallCounter = 0;
        buildingConstructed = BuildingBase;
        buildingConstructed.builderController = this;
        buildingConstructed.constructionLevel = startingLevel;
        StartCoroutine(Move(movePoints, BuildingBase));
        
    }

    private IEnumerator StopConstruction(float time)
    {
        while (currentPointIndex < points.Count)
        {
       
        }

        return null;
    }

    private void SetTargetPosition(Vector3 position)
    {
        target = position;
    }

    void AnimateMotion()
    {
        if (animator != null)
        {
            animator.SetBool("IsMoving", true);
            animator.SetBool("IsHit", false);
            animator.SetFloat("MoveX", _navMeshAgent.velocity.x);
            animator.SetFloat("MoveY", _navMeshAgent.velocity.y);
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
    void AnimateHitMotion()
    {
        if (animator != null)
        {
            animator.SetBool("IsHit", true);
        }
    }
    IEnumerator StopHitMotion(float timeToWait)
    {
        yield return StartCoroutine(Wait(timeToWait));
        if (animator != null)
        {
            animator.SetBool("IsHit", true);
        }
    }
}