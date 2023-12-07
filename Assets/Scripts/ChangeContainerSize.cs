using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeContainerSize : MonoBehaviour
{
    [SerializeField] int pageCount = 1;

    private ScreenSizeProvider screenSizeProvider;
    private RectTransform rectTransform;

    private void Start()
    {
        screenSizeProvider = FindObjectOfType<ScreenSizeProvider>();
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
        float trueScreenWidth = screenSizeProvider.screenSize.x * pageCount;
        rectTransform.sizeDelta = new Vector2(trueScreenWidth, rectTransform.sizeDelta.y);
    }
}
