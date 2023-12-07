using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleVideoFullscreen : MonoBehaviour
{
    [SerializeField] private Button swipeButton;
    [SerializeField] private ScrollRect vScroll;
    [SerializeField] private HScroll hScroll;

    private Main main;
    private bool inFullscreen;
    private RectTransform rectTransform;
    private RectTransform scrollBarRect;
    private float defaultScrollbarWidth;

    private Vector2 defaultSize;

    public void Start()
    {
        main = FindObjectOfType<Main>();
        rectTransform = GetComponent<RectTransform>();
        scrollBarRect = vScroll.verticalScrollbar.GetComponent<RectTransform>();
        defaultScrollbarWidth = scrollBarRect.sizeDelta.x;
        defaultSize = rectTransform.sizeDelta;
    }

    private void Update()
    {
        // Change size to fit screen (every frame...)
        rectTransform.sizeDelta =
            inFullscreen
            ? new Vector2(main.screenSize.x, main.screenSize.y)
            : defaultSize;
    }

    public void Toggle()
    {
        if (inFullscreen)
            Restore();
        else
            ForceFullScreen();
    }

    void ForceFullScreen()
    {
        inFullscreen = true;
        
        // Hide objects in the way
        main.banner.SetActive(false);
        swipeButton.gameObject.SetActive(false);
        vScroll.verticalScrollbarSpacing = -defaultScrollbarWidth;
        scrollBarRect.sizeDelta *= Vector2.up;

        // Disable scrolling
        vScroll.vertical = false;
        vScroll.verticalNormalizedPosition = 1;
        hScroll.active = false;
        hScroll.ForceSwipe(1);

        // Force landscape mode
        Screen.orientation = ScreenOrientation.Landscape;
    }

    void Restore()
    {
        inFullscreen = false;

        // Show objects in the way
        main.banner.SetActive(true);
        swipeButton.gameObject.SetActive(true);
        scrollBarRect.sizeDelta += Vector2.right * defaultScrollbarWidth;
        vScroll.verticalScrollbarSpacing = -3;

        // Enable scrolling
        vScroll.vertical = true;
        hScroll.active = true;
        hScroll.ForceSwipe(1);

        // Set auto rotation
        Screen.orientation = ScreenOrientation.AutoRotation;
    }
}
