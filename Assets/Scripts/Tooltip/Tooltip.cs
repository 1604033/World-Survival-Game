using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
[SerializeField] private TextMeshProUGUI headerText;
[SerializeField] private TextMeshProUGUI contentText;
[SerializeField] private TextMeshProUGUI adsText;
 [SerializeField] private LayoutElement layoutComponent;
 [SerializeField] private GameObject canvas;
 [SerializeField] private int characterCount;
    
    // Update is called once per frame
    void Update()
    {
        int headerTextLength = headerText.text.Length;
        int contentTextLength = contentText.text.Length;

        if (headerTextLength > characterCount || contentTextLength > characterCount )
        {
            layoutComponent.enabled  = true;

        }
        else
        {
            layoutComponent.enabled  = false;
 
        }

        transform.position = Input.mousePosition;

    }
    public Tooltip Show()
    {
        gameObject.SetActive(true);
        canvas.gameObject.SetActive(true);
        return this;
    }
    public Tooltip Hide()
    {
        gameObject.SetActive(false);
        return this;
    }
    public Tooltip SetText(string text, string content, List<string> multipleStrings )
    {
        // if (string.IsNullOrEmpty(text))
        {
            if (headerText != null)
            {
                headerText.text = text;
                headerText.gameObject.SetActive(true);
            }
            if (contentText != null)
            {
                contentText.gameObject.SetActive(true);
                contentText.text = content;
            }

            if ( multipleStrings != null && multipleStrings.Count > 0)
            {
                adsText.text = multipleStrings[0];
                adsText.gameObject.SetActive(true);
            }
        }

        return this;
    }
}
