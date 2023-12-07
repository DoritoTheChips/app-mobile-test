using UnityEngine;
using TMPro;

public class Main : MonoBehaviour
{
    public HScroll hScroll;

    public GameObject banner;
    public TextMeshProUGUI bannerLabel;

    private ScreenOrientation currentOrientation;
    private RectTransform rectTransform;
    public Vector2 screenSize;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        screenSize = rectTransform.sizeDelta;
        Screen.orientation = ScreenOrientation.AutoRotation;
    }

    private void Update()
    {
        // Change the screen size if the orientation changed
        if (currentOrientation != Screen.orientation)
        {
            currentOrientation = Screen.orientation;
            screenSize = rectTransform.sizeDelta;
        }
    }
}
