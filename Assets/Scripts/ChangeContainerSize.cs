using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeContainerSize : MonoBehaviour
{
    [SerializeField] int pageCount = 1;

    private Main main;
    private RectTransform rectTransform;

    private void Start()
    {
        main = FindObjectOfType<Main>();
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        UpdateContainerSize();
    }

    /// <summary>
    /// Update the size of the container to fit to the screen.  
    /// </summary>
    private void UpdateContainerSize()
    {
        float trueScreenWidth = main.screenSize.x * pageCount;
        rectTransform.sizeDelta = new Vector2(trueScreenWidth, rectTransform.sizeDelta.y);
    }
}
