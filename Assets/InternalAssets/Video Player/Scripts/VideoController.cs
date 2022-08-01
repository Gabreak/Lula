using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    [SerializeField] private VideoData _videoData;
    public static VideoController Instance { get; set; }

    [SerializeField] private VideoPlayer _videoPlayer;
    [SerializeField] private Slider _slider;
    private GameObject _oldScene;

    private const int SpeedTime = 4;
    private void Awake() => Instance = this;

    public void VideoPlay(GameObject rootScene, int indexType, int indexVideo)
    {
        _oldScene = rootScene;
        _oldScene.SetActive(false);
        _videoPlayer.gameObject.SetActive(true);
        _videoPlayer.clip = _videoData.Video[indexType].Clip[indexVideo];
        TimeManager.Instance.Speed *= SpeedTime;
        _videoPlayer.Play();
    }
    public void VideoPlay(GameObject rootScene, VideoClip clip)
    {
        _oldScene = rootScene;
        _oldScene.SetActive(false);
        _videoPlayer.gameObject.SetActive(true);
        _videoPlayer.clip = clip;
        TimeManager.Instance.Speed *= SpeedTime;
        _videoPlayer.Play();
    }

    public void VideoReset() => _videoPlayer.Play();

    public void CloseVideo()
    {
        _videoPlayer.Stop();
        TimeManager.Instance.Speed /= SpeedTime;
        _oldScene.SetActive(true);
        _videoPlayer.gameObject.SetActive(false);
    }
    public void VideoSpeed() => _videoPlayer.playbackSpeed = _slider.value;

    public VideoClip GetVideo(int indexType, int indexVideo) => _videoData.Video[indexType].Clip[indexVideo];
}
