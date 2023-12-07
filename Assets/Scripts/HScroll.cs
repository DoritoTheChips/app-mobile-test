using System.Collections;
using System.Collections.Generic;
using System.Xml;
using TMPro;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Script that managed the horizontal scrolling / swiping of the screen.
/// Automaticaly change size based on the number of child.
/// </summary>
public class HScroll : MonoBehaviour
{
    private ScreenSizeProvider screenSizeProvider;
    private RectTransform rectTransform;

    public bool active = true;
    [SerializeField] private float sensitivity = 10;
    [SerializeField] private float smoothingSpeed = 5;
    [SerializeField] private TextMeshProUGUI bannerLabel;

    private bool forceSwipe;
    private float forceSwipeCooldown;
    private int forceSwipeToPage;

    private Vector2 startTouchPosition;
    private Vector2 startScreenPosition;
    private Vector2 nextPosition;

    private void Start()
    {
        screenSizeProvider = FindObjectOfType<ScreenSizeProvider>();
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        UpdateSwap();
    }

    /// <summary>
    /// Manually swipe the screen to a specific page.
    /// </summary>
    /// <param name="toPage"></param>
    public void ForceSwipe(int toPage)
    {
        forceSwipe = true;
        forceSwipeToPage = toPage;
        forceSwipeCooldown = .65f;
    }

    void UpdateSwap()
    {
        var trueScreenWidth = screenSizeProvider.screenSize.x;

        // Force swipe to a page
        if (forceSwipe)
        {
            nextPosition.x = trueScreenWidth * -forceSwipeToPage;
            forceSwipeCooldown -= Time.deltaTime;
            forceSwipe = forceSwipeCooldown > 0;
        }

        // Snap the position based on the screen size
        else if (Input.touchCount <= 0)
        {
            var snapedPos = Mathf.RoundToInt(rectTransform.anchoredPosition.x / trueScreenWidth) * trueScreenWidth;
            nextPosition.x = snapedPos;
        }

        // User touched screen
        else if (active)
        {
            var touchInput = Input.GetTouch(0);

            // Start tracking the first position of the finger
            if (touchInput.phase == TouchPhase.Began)
            {
                startTouchPosition = touchInput.position;
                startScreenPosition = rectTransform.anchoredPosition;
            }

            // Move the HContainer based on the current position and start position of the finger
            if (touchInput.phase == TouchPhase.Moved)
            {
                nextPosition.x = (touchInput.position.x - startTouchPosition.x) * sensitivity + startScreenPosition.x;
            }
        }

        // Clamp and make a fluid movement
        nextPosition.x = Mathf.Clamp(nextPosition.x, -((transform.childCount - 1) * trueScreenWidth), 0);
        rectTransform.anchoredPosition = Vector2.Lerp(rectTransform.anchoredPosition, nextPosition, Time.deltaTime * smoothingSpeed);

        // Change banner label based on position
        var pageIndex = Mathf.RoundToInt(Mathf.Abs(rectTransform.anchoredPosition.x / trueScreenWidth));
        pageIndex = Mathf.Clamp(pageIndex, 0, transform.childCount);
        bannerLabel.text = transform.GetChild(pageIndex).name;
    }
}
