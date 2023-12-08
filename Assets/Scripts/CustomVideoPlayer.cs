using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class CustomVideoPlayer : MonoBehaviour
{
    [Header("Interface :")]
    [SerializeField] private Image playPauseButton;
    [SerializeField] private Sprite playAsset;
    [SerializeField] private Sprite pauseAsset;
    [SerializeField] private TextMeshProUGUI timestamp;
    [SerializeField] private RectTransform progressBar;

    [Header("Other :")]
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private ToggleVideoFullscreen toggleVideoFullScreen;

    private ScreenSizeProvider screenSizeProvider;

    private void Start()
    {
        screenSizeProvider = FindObjectOfType<ScreenSizeProvider>();
    }

    void Update()
    {
        timestamp.text = videoPlayer.time.ToString("00:00");

        var normalizedProgress = (float)(videoPlayer.time / videoPlayer.length);
        progressBar.sizeDelta = new Vector2(screenSizeProvider.screenSize.x * normalizedProgress, progressBar.sizeDelta.y);
    }

    public void ToggleResume()
    {
        if (videoPlayer.isPlaying)
            Pause();
        else
            Resume();
    }

    private void Resume()
    {
        videoPlayer.Play();
        playPauseButton.sprite = pauseAsset;
    }

    private void Pause()
    {
        videoPlayer.Pause();
        playPauseButton.sprite = playAsset;
    }

    public void FullScreen()
    {
        toggleVideoFullScreen.Toggle();
    }
}
