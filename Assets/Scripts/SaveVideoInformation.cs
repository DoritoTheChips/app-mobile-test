using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SaveVideoInformation : MonoBehaviour
{
    [SerializeField] private TMP_InputField episodeNameInput;
    [SerializeField] private TMP_InputField viewsCountInput;

    [SerializeField] private TextMeshProUGUI episodeNameLabel;
    [SerializeField] private TextMeshProUGUI viewsCountLabel;

    public void Save()
    {
        episodeNameLabel.text = episodeNameInput.text;
        viewsCountLabel.text = viewsCountInput.text;
    }
}
