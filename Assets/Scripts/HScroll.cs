using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Script that managed the horizontal scrolling / swiping of the screen.
/// Automaticaly change size based on the number of child.
/// </summary>
public class HScroll : MonoBehaviour
{
    Main main;
    RectTransform rectTransform;

    public bool changeBannerName;

    public bool active;
    public float sensitivity = 10;
    public float smoothingSpeed = 5;

    bool forceSwipe;
    float forceSwipeCooldown;
    int forceSwipeToPage;

    Vector2 startTouchPosition;
    Vector2 startScreenPosition;
    Vector2 nextPosition;

    private void Start()
    {
        main = FindObjectOfType<Main>();
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
        // Force swipe to a page
        if (forceSwipe)
        {
            nextPosition.x = rectTransform.sizeDelta.x * -forceSwipeToPage;
            forceSwipeCooldown -= Time.deltaTime;
            forceSwipe = forceSwipeCooldown > 0;
        }

        // Snap the position based on the screen size
        else if (Input.touchCount <= 0)
        {
            var snapedPos = Mathf.RoundToInt(rectTransform.anchoredPosition.x / rectTransform.sizeDelta.x) * rectTransform.sizeDelta.x;
            nextPosition.x = snapedPos;
        }

        // User touched screen
        else
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
        nextPosition.x = Mathf.Clamp(nextPosition.x, -((transform.childCount - 1) * rectTransform.sizeDelta.x), 0);
        rectTransform.anchoredPosition = Vector2.Lerp(rectTransform.anchoredPosition, nextPosition, Time.deltaTime * smoothingSpeed);

        // Change banner label based on position
        var pageIndex = Mathf.RoundToInt(rectTransform.anchoredPosition.x / rectTransform.sizeDelta.x) + 1;
        pageIndex = Mathf.Clamp(pageIndex, 0, transform.childCount);
        main.bannerLabel.text = transform.GetChild(pageIndex).name;
    }
}
