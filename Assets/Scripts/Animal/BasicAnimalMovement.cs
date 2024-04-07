using System;
using System.Collections;
using System.Collections.Generic;
using BuildingSystem.Models;
using UnityEngine;
using Random = UnityEngine.Random;

public class BasicAnimalMovement : MonoBehaviour
{
    public Collider2D Area;
    [Min(0)] public float MinIdleTime;
    [Min(0)] public float MaxIdleTime;
    [Min(0)] public float Speed = 2.0f;
    private float m_IdleTimer;
    [SerializeField] private MovementModes currentMovementMode;
    private float m_CurrentIdleTarget;
    private Vector3 m_CurrentTarget;
    private bool m_IsIdle;
    private bool isMoveToShelter;
    private bool movingToFoodStorage;
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform feedingPoint;

    private void Start()
    {
        DayTimeController.Instance.OnTimeForAnimalsToEat += TimeToEat;
        DayTimeController.Instance.OnTimeForAnimalsToFinishEating += TimeToFinishEating;
        if (MaxIdleTime <= MinIdleTime)
            MaxIdleTime = MinIdleTime + 0.1f;
        m_IsIdle = true;
        PickNewIdleTime();
    }


    private void Update()
    {
        if (currentMovementMode == MovementModes.Sleeping || currentMovementMode == MovementModes.Idle)

        {
            StopAnimatingPokemon();
            return;
        }

        if (m_IsIdle)
        {
            m_IdleTimer += Time.deltaTime;

            if (m_IdleTimer >= m_CurrentIdleTarget && !movingToFoodStorage)
            {
                PickNewTargetNew();
            }
        }
        else
        {
            AnimatePokemon();
            transform.position = Vector3.MoveTowards(transform.position, m_CurrentTarget, Speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, m_CurrentTarget) <= 3f)
            {
                PickNewIdleTime();
                if (isMoveToShelter)
                {
                    currentMovementMode = MovementModes.Sleeping;
                    isMoveToShelter = false;
                    m_IsIdle = false;
                }
            }
        }
    }

    void PickNewIdleTime()
    {
        if (_animator != null)
            StopAnimatingPokemon();
        m_IsIdle = true;
        m_CurrentIdleTarget = Random.Range(MinIdleTime, MaxIdleTime);
        m_IdleTimer = 0.0f;
    }

    void PickNewTarget()
    {
        m_IsIdle = false;
        var dir = Quaternion.Euler(0, 0, 360.0f * Random.Range(0.0f, 1.0f)) * Vector2.up;

        dir *= Random.Range(1.0f, 10.0f);

        var pos = (Vector2)transform.position;
        var pts = pos + (Vector2)dir;

        if (!Area.OverlapPoint(pts))
        {
            pts = Area.ClosestPoint(pts);
        }

        m_CurrentTarget = pts;
        var toTarget = m_CurrentTarget - transform.position;

        bool flipped = toTarget.x < 0;
        transform.localScale = new Vector3(flipped ? -1 : 1, 1, 1);

        // if (_animator != null)
        // _animator.SetFloat(SpeedHash, Speed);
    }

    void PickNewTargetNew()
    {
        m_IsIdle = false;
        Bounds bounds = Area.bounds;
        float minX = bounds.min.x;
        float minY = bounds.min.y;
        float minZ = bounds.min.z;
        float maxX = bounds.max.x;
        float maxY = bounds.max.y;
        float maxZ = bounds.max.z;
        m_CurrentTarget = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY),
            Random.Range(minZ, maxZ));
    }

    void AnimatePokemon()
    {
        Vector3 direction = m_CurrentTarget - transform.position;
        float moveX = 0;
        float moveY = 0;
        switch (direction.x)
        {
            case > 0:
                moveX = 1;
                break;
            case < 0:
                moveX = -1;
                break;
        }

        switch (direction.y)
        {
            case > 0:
                moveY = 1;
                break;
            case < 0:
                moveY = -1;
                break;
        }

        _animator.SetTrigger("IsMoving");
        if (_animator != null)
        {
            _animator.SetBool("IsMoving", true);
            _animator.SetFloat("MoveX", moveX);
            _animator.SetFloat("MoveY", moveY);
        }
    }

    void StopAnimatingPokemon()
    {
        if (_animator != null)
        {
            _animator.SetBool("IsMoving", false);
        }
    }

    private void TimeToEat()
    {
        // Debug.Log("This is the time to eat!!!");
        movingToFoodStorage = true;
        m_IsIdle = false;
        m_CurrentTarget = feedingPoint.position;
    }

    void CheckIfThereIsAShelter()
    {
        isMoveToShelter = true;
        m_IsIdle = false;
        var building = BuildingsManager.Instance.GetBuilding(BuildingType.Shelter);
        var position = building == null ? feedingPoint.position : building.transform.position;
        m_CurrentTarget = position;
    }

    private void TimeToFinishEating()
    {
        Debug.Log("Finished eating to eat!!!");
        movingToFoodStorage = false;
        CheckIfThereIsAShelter();
    }

    public enum MovementModes
    {
        Walking,
        Running,
        Idle,
        Sleeping,
    }
}