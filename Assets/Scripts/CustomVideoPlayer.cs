using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class CustomVideoPlayer : MonoBehaviour
{
    [Header("Interface :")]
    [SerializeField] private GameObject fadeInterface;
    [SerializeField] private Image playPauseButton;
    [SerializeField] private Sprite playAsset;
    [SerializeField] private Sprite pauseAsset;
    [SerializeField] private TextMeshProUGUI timestamp;
    [SerializeField] private RectTransform progressBar;

    [Header("Other :")]
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private ToggleVideoFullscreen toggleVideoFullScreen;

    Main main;

    private void Start()
    {
        main = FindObjectOfType<Main>();
    }

    void Update()
    {
        timestamp.text = videoPlayer.time.ToString("00:00");

        var normalizedProgress = (float)(videoPlayer.time / videoPlayer.length);
        progressBar.sizeDelta = new Vector2(main.screenSize.x * normalizedProgress, progressBar.sizeDelta.y);
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
        toggleVideoFullScreen.Toggle();
    }
}
