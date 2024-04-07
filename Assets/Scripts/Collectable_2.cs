using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable_2 : MonoBehaviour
{
    // Start is called before the first frame update
   private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if(player)
        {
            player.numPaddy++;
            //player.numPaddy++;
            Destroy(this.gameObject);
        }
    }
}
