using System;
using System.Collections;
using UnityEngine;

public class GenerateSeedsFromFruit : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private float timeForAnimalToPickFruit = 2f;
    private GameObject activeSeedPrefab;
    private const string PARAMETER_HORIZONTAL = "MoveX";
    private const string PARAMETER_VERTICAL = "MoveY";
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
     // activeSeedPrefab =  GameManager.instance.activeSlot.itemData.seedPrefab;

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            if (GameManager.instance.activeSlot.count == 0 || GameManager.instance.activeSlot.itemData == null)
            {
                return;
            }
        if (GameManager.instance.activeSlot.itemData.seedPrefab == null)
            {
                return;
            }

        activeSeedPrefab = GameManager.instance.activeSlot.itemData.seedPrefab;
            Collider2D collider = Physics2D.OverlapPoint(GameManager.instance.player.transform.position);
            if (collider != null)
            {
                GameObject player = collider.gameObject;
                Vector3 tempPos = player.transform.position + new Vector3(2f, 2f, 2f);
                Debug.Log("Inside the collide-able space: ");

               // if (GameManager.instance.activeSlot.itemData.type == CollectableType.Fruit)
                {
                    GameObject fruitPrefab = GameManager.instance.activeSlot.itemData.prefab;
                    GameObject instantiatedFruit = Instantiate(fruitPrefab, tempPos, Quaternion.identity);
                    Debug.Log("Got the fruit");

                    StartCoroutine(EatSeeds(instantiatedFruit));
                    GameManager.instance.inventory.RemoveItemsFromSlot(GameManager.instance.activeSlot.itemData, 1);
                }
            }
        }
    }

    private IEnumerator EatSeeds(GameObject fruit)
    {
        yield return new WaitForSeconds(timeForAnimalToPickFruit);

        if (fruit == null)
        {
            yield break; // Exit the coroutine if the fruit is null
        }

        Vector3 startPos = transform.position;
        Vector3 destinationPos = fruit.transform.position;
        float elapsedTime = 0f;

        if (startPos.x > destinationPos.x)
        {
            _animator.SetFloat("MoveX", -1f);
        }
        else
        {
            _animator.SetFloat("MoveX", 1f);
        }
       
        _animator.SetBool("IsAnimalMoving", true);

        while (elapsedTime < timeForAnimalToPickFruit)
        {
            // Move towards the fruit using Lerp
            transform.position = Vector3.Lerp(startPos, destinationPos, elapsedTime / timeForAnimalToPickFruit);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
        _animator.SetBool("IsAnimalMoving", false);

        if (fruit != null)
        {
            if (activeSeedPrefab != null)
            {
                Instantiate(activeSeedPrefab, fruit.transform.position, Quaternion.identity);
                Destroy(fruit);  
            }
        }
    }
}
