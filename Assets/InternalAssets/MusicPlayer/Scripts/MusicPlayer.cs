using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] private BaseAudio _baseAudio;
    [SerializeField] private Slider _slider;

    private AudioSource _audio;
    private int _index = 0;
    private bool _isPause = false;

    public static MusicPlayer Instance;
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _audio = GetComponent<AudioSource>();
        _audio.clip = _baseAudio.Audios[_index];
        _audio.Play();
    }

    private void Update()
    {
        if (!_audio.isPlaying && !_isPause)
        {
            AudioNext();
        }
    }

    public void AudioPause()
    {
        _isPause = !_isPause;
        if (_isPause)
        {
            _audio.Pause();
        }
        else
            _audio.UnPause();

    }

    public void AudioNext()
    {
        _index += 1;
        if (_index >= _baseAudio.Audios.Count)
            _index = 0;
        MusicPlay(_index);
    }

    public void AudioPrevious()
    {
        _index -= 1;
        if (_index <= 0)
            _index = _baseAudio.Audios.Count - 1;
        MusicPlay(_index);
    }

    public void Volume()
    {
        _audio.volume = _slider.value;
    }

    private void MusicPlay(int index)
    {
        _audio.clip = _baseAudio.Audios[index];
        _audio.Play();
    }

    public void AudioStop()
    {
        _audio.Stop();
    }

    public void AudioPlay()
    {
        _audio.Play();
    }

}
