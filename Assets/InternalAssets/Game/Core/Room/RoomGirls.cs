using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Video;

public class RoomGirls : MonoBehaviour
{
    [SerializeField] private VideoGirlsData _data;
    [SerializeField] private Transform _parentGirls;
    [Space(20), Header("Toys")]
    [SerializeField] private TypeProducts _toys;
    private static RoomGirls _instance;
    private int _idBinary = 0;

    public GameBed[] _gameBed;


    public int IdBinary
    {
        get => _idBinary;
        set
        {
            _idBinary = value;
            _data.IndexVideo = (Convert.ToInt32(IdBinary.ToString(), 2));
        }
    }
    public static int VideoIndex { get => _instance._data.IndexVideo; }

    private void Awake()
    {
        _instance = this;
    }

    private void OnEnable()
    {
        for (int i = 0; i < _gameBed.Length; i++)
        {
            _gameBed[i].Check = PlayerPrefs.GetInt("GirlBed" + i, 0);
            _gameBed[i].Girl.SetActive(Convert.ToBoolean(_gameBed[i].Check));
        }
    }

    private void Start()
    {
        int length = _data.Girls.Length;

        CreateGirls(length, _parentGirls);
    }


    private void CreateGirls(int length, Transform parent)
    {

        for (int i = 0; i < length; i++)
        {
            if (_data.Girls[i].IsActive)
            {

                RoomGirlController girl = Instantiate(_data.Girls[i].Object, parent);
                girl.IdBinary = _data.Girls[i].Id;
            }
        }
    }

    public void UpdateBed(int index)
    {
        PlayerPrefs.SetInt("GirlBed" + index, _gameBed[index].Check);
        _gameBed[index].Girl.SetActive(Convert.ToBoolean(_gameBed[index].Check));
    }

    public static int GetCount(int index) => Convert.ToString(index, 2).Count(ch => ch == '1');

    public static string GetBinary(int index) => Convert.ToString(index, 2);

    public static bool FindShantal(string binary) => binary[binary.Length - 1].ToString() == "1";

    public static bool IsToy()
    {
        foreach (var toy in _instance._toys.Acquired)
        {
            bool isToy = toy.Id == _instance._data.Videos[_instance._data.IndexVideo].IndexToy;
            if (isToy) return true;
        }
        return false;
    }

    public static VideoClip GetVideo(bool isToy)
    {
        VideoGirlsData data = _instance._data;
        VideoClip clipNoToy = data.Videos[data.IndexVideo].Clip[0];
        VideoClip clipToy = data.Videos[data.IndexVideo].Clip[1];

        if (clipToy == null)
        {
            if (clipNoToy == null)
            {
                string message = data.Videos[data.IndexVideo].Warning.GetLocalizedString();
                WindowMessage.Message(message, WindowIcon.Warning, Color.yellow);
                return null;
            }
        }

        if (isToy)
        {
            if (clipToy == null) return clipNoToy;
            return clipToy;
        }
        else
        {
            if (clipNoToy != null)
            {
                return clipNoToy;
            }
            else if (clipToy != null)
            {
                string message = data.Videos[data.IndexVideo].WarningToy.GetLocalizedString();
                WindowMessage.Message(message, WindowIcon.Warning, Color.yellow);
                return null;
            }
        }
        return null;
    }
}

[Serializable]
public struct GameBed
{
    public GameObject Girl;
    public int Check;
}
