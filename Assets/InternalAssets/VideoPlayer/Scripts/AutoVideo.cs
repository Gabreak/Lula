using Sirenix.OdinInspector;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class AutoVideo : MonoBehaviour
{
    [VerticalGroup]
    [SerializeField] private VideoData _video;
    [VerticalGroup]
    [SerializeField] private int _indexCharacter;
    [VerticalGroup]
    [SerializeField] private int _indexVideo;

    public void VideoPlay()
    {
        
        VideoController.Instance.VideoPlay(transform.root.gameObject, _video.Video[_indexCharacter].Clip[_indexVideo]);
    }
}
