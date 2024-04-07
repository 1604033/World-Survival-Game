using Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;

public class BuildingEnterAndExit : MonoBehaviour
{
    [SerializeField]private Transform center;
    [SerializeField]private Transform from;
    [SerializeField]private Transform to;
    [SerializeField]private Transition.Transition _transition;
    [SerializeField] private GameObject canvas;

    void Start()
    {
        _transition = FindObjectOfType<Transition.Transition>();
    }
    private void OnCollisionEnter2D(Collision2D otherColider2d)
    {
        if (otherColider2d != null && otherColider2d.gameObject.TryGetComponent( out Player player))
        {
          StartCoroutine(_transition.MoveObject(from, to, GameManager.instance.player.gameObject, center) );
        }
    }
}
