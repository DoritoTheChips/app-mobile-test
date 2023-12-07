using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Video;

[RequireComponent(typeof(CanvasGroup))]
public class FadeInterface : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private float fadeInSpeed = 2;
    [SerializeField] private float fadeOutSpeed = 5;
    [SerializeField] private float fadeHoldTime = 3;
    [SerializeField] private VideoPlayer videoPlayer;
    
    private CanvasGroup canvasGroup;

    private float fadeCooldown = 0;

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Update()
    {
        // Fade in (visible)
        if (fadeCooldown > 0 || !videoPlayer.isPlaying)
        {
            if (videoPlayer.isPlaying)
                fadeCooldown -= Time.deltaTime;
            else
                fadeCooldown = fadeHoldTime;
            canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, 1, Time.deltaTime * fadeInSpeed);
        }

        // Fade out (hiding)
        else
        {
            canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, 0, Time.deltaTime * fadeOutSpeed);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        fadeCooldown = fadeHoldTime;
    }
}
