using UnityEngine;
using TMPro;

public class Main : MonoBehaviour
{
    public HScroll hScroll;

    public GameObject banner;
    public TextMeshProUGUI bannerLabel;

    public TMP_InputField episodeNameInput;
    public TMP_InputField viewsCountInput;

    public TextMeshProUGUI episodeNameLabel;
    public TextMeshProUGUI viewsCountLabel;

    private void Start()
    {
        Screen.orientation = ScreenOrientation.AutoRotation;
    }

    public void ApplyVideoInformation()
    {
        episodeNameLabel.text = episodeNameInput.text;
        viewsCountLabel.text = viewsCountInput.text;
    }
}
