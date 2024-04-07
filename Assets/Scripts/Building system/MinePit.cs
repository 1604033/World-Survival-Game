using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinePit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent(out Building building))
        {
            Debug.Log($"s");
            other.gameObject.transform.position = transform.position;
        }
    }
}
