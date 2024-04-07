using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LoggingCamp : BuildingBase
{
   [SerializeField]private ItemData outputItem;
   [SerializeField] GameObject prefab; 
   [SerializeField] Transform parentObject;
   [SerializeField] Transform logsGenPoint;
   public List<Pokemon> assignedPokemons;
   public List<GameObject> AssignedPokemonsGameObjects;
   
   [SerializeField] float loggingCampdetectionRadius = 7f;

   public LoggingCampState loggingCampState = LoggingCampState.Idle;
   public int outputCount = 1;
   public int treeInputMultiplier = 5;
   public int inputStockCount = 5;
   public int timeForProduction = 3;
   public Collectable _collectable;
   private string treeTag = "Tree";
   private int maxAllowedBeast = 2;
   public override void Start()
   {
      base.Start();
      SetLoggingCampInputStock();
   }

   void SetLoggingCampInputStock()
   {
      Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, loggingCampdetectionRadius);
      HashSet<Collider2D> neededColliders = new HashSet<Collider2D>();

      foreach (var collider in colliders)
      {
         if (collider.CompareTag(treeTag))
         {
            neededColliders.Add(collider);

            if (collider.TryGetComponent(out ItemHighlight itemHighlight))
            {
               itemHighlight.HideHighlight();
               Debug.Log("Removing: " + itemHighlight);
            }
         }
      }

      inputStockCount = treeInputMultiplier * neededColliders.Count;
   }

   
   void StartLoggingCamp()
   {
      if (loggingCampState == LoggingCampState.Running)
      {
         foreach (var _gameObject in AssignedPokemonsGameObjects)
         {
            _gameObject.SetActive(true);
         }
         StartCoroutine(nameof(RunLoggingCamp));
      
         SpawnObject(); 
      }
     
   }
   IEnumerator  RunLoggingCamp()
   {
      IncrementCollectableCount();
      yield return new WaitForSeconds(timeForProduction);
      if (loggingCampState == LoggingCampState.Running)
      {
         StartCoroutine(nameof(RunLoggingCamp));
      }
      
   }

   void IncrementCollectableCount()
   {
      if (_collectable == null)
      {
         SpawnObject();
      }
      
      if (inputStockCount > 0 && outputItem != null && _collectable != null)
      {
          inputStockCount--;
          _collectable.IncrementCount(1, 99);
      }

      if (_collectable.count == 10 || inputStockCount == 0)
      {
        // loggingCampState = LoggingCampState.Idle;
      }
   }


public void AssignPokemon(Pokemon pokemon){
   if(!assignedPokemons.Contains(pokemon)){
      assignedPokemons.Add(pokemon);
      outputCount = assignedPokemons != null && assignedPokemons.Count == 1 ? 3 : 6;

     if (assignedPokemons.Count == 2)
      {
         loggingCampState = LoggingCampState.Running;
      StartLoggingCamp();
      }
   }

}
   void SpawnObject() {
      
      GameObject instance = Instantiate(prefab, logsGenPoint.position,Quaternion.identity);
      instance.transform.parent = parentObject; 
      if (instance.TryGetComponent(out Collectable collectable))
      {
         _collectable = collectable;
      }
   }

   public enum LoggingCampState
   {
     Running,
     Idle
   }
   private void OnDrawGizmos()
   {
      Gizmos.color = Color.red;
      Gizmos.DrawWireSphere(transform.position, loggingCampdetectionRadius);
   }
}

