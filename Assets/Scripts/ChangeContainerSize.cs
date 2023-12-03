using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeContainerSize : MonoBehaviour
{
    public int pageCount = 1;

    Main main;
    RectTransform rectTransform;

    private void Start()
    {
        main = FindObjectOfType<Main>();
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        UpdateContainerSize();
    }

    /// <summary>
    /// Update the size of the container to fit to the screen.  
    /// </summary>
    void UpdateContainerSize()
    {
        float trueScreenWidth = main.GetComponent<RectTransform>().sizeDelta.x * pageCount;
        rectTransform.sizeDelta = new Vector2(trueScreenWidth, rectTransform.sizeDelta.y);
    }
}
