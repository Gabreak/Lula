using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{

    [SerializeField] private bool isSetting = true;
    [SerializeField] Source _audioSource;
    [SerializeField] DistortionFilter _distortionFilter;
    [SerializeField] HighPassFilter _highPassFilter;

    private void OnEnable()
    {
        OnLoad();

        if (isSetting)
        {
            _audioSource.SliderVolume.onValueChanged.AddListener(AudioVolume);
            _audioSource.SliderVolume.value =(_audioSource.Filter.volume);

            _audioSource.SliderPriority.onValueChanged.AddListener(AudioPriority);
            _audioSource.SliderPriority.value =(_audioSource.Filter.priority);

            _audioSource.SliderStereoPen.onValueChanged.AddListener(AudioStereoPan);
            _audioSource.SliderStereoPen.value =(_audioSource.Filter.panStereo);

            _audioSource.SliderSpatialBlend.onValueChanged.AddListener(AudioSpatialBlend);
            _audioSource.SliderSpatialBlend.value = (_audioSource.Filter.spatialBlend);

            _highPassFilter.Slider.onValueChanged.AddListener(AudioHighPass);
            _highPassFilter.Slider.value = (_highPassFilter.Filter.cutoffFrequency);

            _distortionFilter.Slider.onValueChanged.AddListener(AudioDistort);
            _distortionFilter.Slider.value = (_distortionFilter.Filter.distortionLevel);

        }
    }

    private void OnDisable()
    {
        if (isSetting)
            OnSave();
    }

    private void OnSave()
    {
        PlayerPrefs.SetFloat("AudioVolume", _audioSource.Filter.volume);
        PlayerPrefs.SetInt("AudioPriority", _audioSource.Filter.priority);
        PlayerPrefs.SetFloat("AudioStereoPan", _audioSource.Filter.panStereo);
        PlayerPrefs.SetFloat("AudioSpatialBlend", _audioSource.Filter.spatialBlend);

        PlayerPrefs.SetFloat("AudioHighPass", _highPassFilter.Filter.cutoffFrequency);
        PlayerPrefs.SetFloat("AudioDistort", _distortionFilter.Filter.distortionLevel);
    }
    
    private void OnLoad()
    {
        _audioSource.Filter.volume = PlayerPrefs.GetFloat("AudioVolume", _audioSource.Filter.volume);
        _audioSource.Filter.priority = PlayerPrefs.GetInt("AudioPriority", _audioSource.Filter.priority);
        _audioSource.Filter.panStereo = PlayerPrefs.GetFloat("AudioStereoPan", _audioSource.Filter.panStereo);
        _audioSource.Filter.spatialBlend = PlayerPrefs.GetFloat("AudioSpatialBlend", _audioSource.Filter.spatialBlend);

        _highPassFilter.Filter.cutoffFrequency = PlayerPrefs.GetFloat("AudioHighPass", _highPassFilter.Filter.cutoffFrequency);
        _distortionFilter.Filter.distortionLevel = PlayerPrefs.GetFloat("AudioDistort", _distortionFilter.Filter.distortionLevel);
    }

    #region
    private void AudioVolume(float value) => _audioSource.Filter.volume = value;

    private void AudioPriority(float value) => _audioSource.Filter.priority = (int)value;

    private void AudioStereoPan(float value) => _audioSource.Filter.panStereo = value;

    private void AudioSpatialBlend(float value) => _audioSource.Filter.spatialBlend = value;
    #endregion


    #region
    private void AudioHighPass(float value) => _highPassFilter.Filter.cutoffFrequency = value;
    private void AudioDistort(float value) => _distortionFilter.Filter.distortionLevel = value;
    #endregion


    [Serializable]
    private struct DistortionFilter
    {
        public AudioDistortionFilter Filter;
        public Slider Slider;
    }

    [Serializable]
    private struct HighPassFilter
    {
        public AudioHighPassFilter Filter;
        public Slider Slider;
    }


    [Serializable]
    private struct Source
    {
        public AudioSource Filter;
        public Slider SliderVolume;
        public Slider SliderPriority;
        public Slider SliderStereoPen;
        public Slider SliderSpatialBlend;

    }
}

