using BuildingSystem;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    private Animator animator;
    private Vector3 playerPositionDirection;
    private Vector3 playerPosition;
    private Vector2 _vector2;
    float maxBuildingArea = 2f; 
    [SerializeField] ConstructionLayer constructionLayer;
    private Player player;
    Movement playerMovement;
    [SerializeField] private Vector4 offset;

    private void Start()
    {
        
        playerMovement = FindObjectOfType<Movement>();
        _vector2 = Vector2.up;
        animator = GetComponent<Animator>();
        Movement.OnPlayerMovement += AnimatePokemon;
        Movement.OnPlayerMovementStop += StopAnimatingPokemon;
    }

    private void Update()
    {
        playerPositionDirection = playerMovement.direction;
        RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position, _vector2, maxBuildingArea, LayerMask.GetMask("Building"));
        SetPositionToDisplayPreview();
        ShowPositionToDisplayMonster();
        if (raycastHit2D.collider != null)
        {
            if (raycastHit2D.collider.gameObject.TryGetComponent<Building>(out Building building))
            {    
                building.CanNowBuild(false);
                if (Input.GetMouseButtonDown(0) && building.CanConstructBuilding())
                {
                    building.ConstructBuilding();
                }else if (Input.GetMouseButtonDown(1))
                {
                    building.DestroyBuilding();
                }
            }
        }
    }

    void ShowPositionToDisplayMonster()
    {
      var _playerMoveDirection = playerMovement.direction;
      playerPosition = GameManager.instance.player.transform.position;
        if (_playerMoveDirection.y == 1)
        {
            transform.position = playerPosition + new Vector3(0f, offset.w, 0f);
        }
    
        if (_playerMoveDirection.y == -1)
        {
            transform.position = playerPosition +  new Vector3(0f, -offset.z, 0f);
        }
    
        if (_playerMoveDirection.x == -1)
        {
            transform.position = playerPosition +  new Vector3(-offset.x, 0F, 0f);
        }
    
        if (_playerMoveDirection.x == 1)
        {
            transform.position = playerPosition +  new Vector3(offset.y, 0f, 0f);
        }
    }
    void SetPositionToDisplayPreview()
    {
        if (playerPositionDirection.y == 1f)
        {
            _vector2 = Vector2.up;
        }

        if (playerPositionDirection.y == -1f)
        {
            _vector2 = Vector2.down;
        }

        if (playerPositionDirection.x == -1f)
        {
            _vector2 = Vector2.left;
        }

        if (playerPositionDirection.x == 1f)
        {
            _vector2 = Vector2.right;
        }
    }


    void AnimatePokemon(Vector3 direction)
    {
        if (animator != null)
        {
            animator.SetBool("IsMoving", true);
            animator.SetFloat("MoveX", direction.x);
            animator.SetFloat("MoveY", direction.y);
        }
    }

    void StopAnimatingPokemon()
    {
        if (animator != null)
        {
            animator.SetBool("IsMoving", false);
        }
    }
    
    
}