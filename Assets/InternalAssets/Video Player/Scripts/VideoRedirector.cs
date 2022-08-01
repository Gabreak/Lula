using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using TMPro;

public class VideoRedirector : MonoBehaviour
{
    public VideoClip Clip { get; set; }
    public TextMeshProUGUI Name;
    private Image _image;
    private void OnEnable()
    {
        _image = GetComponent<Image>();
        _image.color = Random.ColorHSV() - new Color(0,0,0,0.5f);
    }

    public void Play()
    {
        VideoController.Instance.VideoPlay(transform.root.gameObject, Clip);
    }
}
