using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatAnimation : MonoBehaviour
{
     public Sprite[] catSprites;
    private SpriteRenderer spriteRenderer;
    private int currentSpriteIndex = 0;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Start the sprite swapping coroutine
        StartCoroutine(SwapSprites());
    }

    IEnumerator SwapSprites()
    {
        while (true)
        {
            // Change sprite every 0.3 seconds (adjust the time as needed)
            yield return new WaitForSeconds(0.01f);

            // Change to the next sprite
            currentSpriteIndex = (currentSpriteIndex + 1) % catSprites.Length;
            spriteRenderer.sprite = catSprites[currentSpriteIndex];
        }
    }
}
