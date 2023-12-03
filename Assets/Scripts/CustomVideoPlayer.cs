using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class CustomVideoPlayer : MonoBehaviour
{
    [Header("Interface :")]
    public GameObject fadeInterface;
    public Image playPauseButton;
    public Sprite playAsset;
    public Sprite pauseAsset;
    public TextMeshProUGUI timestamp;
    public RectTransform progressBar;

    [Header("Other :")]
    public VideoPlayer videoPlayer;
    public ScrollRect vScroll;

    Main main;
    bool fullscreen;
    RectTransform rectTransform;
    Vector2 screenSize;
    Vector2 defaultSize;

    private void Start()
    {
        main = FindObjectOfType<Main>();
        rectTransform = GetComponent<RectTransform>();
        screenSize = main.GetComponent<RectTransform>().sizeDelta;
        defaultSize = rectTransform.sizeDelta;
    }

    void Update()
    {
        timestamp.text = videoPlayer.time.ToString("00:00");

        var normalizedProgress = (float)(videoPlayer.time / videoPlayer.length);
        progressBar.sizeDelta = new Vector2(screenSize.x * normalizedProgress, progressBar.sizeDelta.y);
    }

    public void ToggleResume()
    {
        if (videoPlayer.isPlaying)
            Pause();
        else
            Resume();
    }

    public void Resume()
    {
        videoPlayer.Play();
        playPauseButton.sprite = pauseAsset;
    }

    public void Pause()
    {
        videoPlayer.Pause();
        playPauseButton.sprite = playAsset;
    }

    public void FullScreen()
    {
        fullscreen = !fullscreen;

        if (fullscreen)
        {
            Screen.orientation = ScreenOrientation.Landscape;
            main.banner.SetActive(false);
            rectTransform.sizeDelta = new Vector2(screenSize.y, screenSize.x);
            vScroll.enabled = false;
            vScroll.verticalNormalizedPosition = 0;
            main.hScroll.active = false;
        }

        else
        {
            Screen.orientation = ScreenOrientation.AutoRotation;
            main.banner.SetActive(true);
            rectTransform.sizeDelta = defaultSize;
            vScroll.enabled = true;
            main.hScroll.active = true;
        }
    }
}
