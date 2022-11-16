using System;
using System.Linq;


using UnityEngine;
using UnityEngine.Video;

public class RoomGirls : MonoBehaviour
{
    [SerializeField] private VideoGirlsData _data;
    [SerializeField] private Transform _parentGirls;
    [Space(20), Header("Toys")]
    [SerializeField] private TypeProducts _toys;
    private static RoomGirls _instance;
    //private string _idBinary = "0000";

    public GameBed[] GameBeds;


    public char[] IdBinary = new char[4] { '0', '0', '0', '0' };
    //{
    //    get => _idBinary;
    //    set
    //    {
    //        _idBinary = value;
    //        _data.IndexVideo = (Convert.ToInt32(IdBinary, 2));
    //    }
    //}
    //public static int VideoIndex { get => _instance._data.IndexVideo; }

    private void Awake()
    {
        _instance = this;
    }

    private void OnEnable()
    {
        for (int i = 0; i < GameBeds.Length; i++)
        {
            GameBeds[i].Check = PlayerPrefs.GetInt("GirlBed" + i, GameBeds[i].Check);
            IdBinary[i] = GameBeds[i].Check.ToString()[0];
            GameBeds[i].Girl.SetActive(Convert.ToBoolean(GameBeds[i].Check));
        }
    }

    private void Start()
    {
        int length = _data.Girls.Length;
        //Debug.Log(GetIndexVideo());
        CreateGirls(length, _parentGirls);
    }


    private void CreateGirls(int length, Transform parent)
    {
        for (int i = 0; i < length; i++)
        {
            if (_data.Girls[i].IsActive)
            {
                Instantiate(_data.Girls[i].Object, parent);
            }
        }
    }

    public static int GetIndexVideo()
    {
        string s = "";
        for (int i = _instance.IdBinary.Length-1; i >= 0; i--)
        {
            s += _instance.IdBinary[i];
        }
        _instance._data.IndexVideo = Convert.ToInt32(s, 2); 
        return _instance._data.IndexVideo;

    }

    public void UpdateBed(int index)
    {
        PlayerPrefs.SetInt("GirlBed" + index, GameBeds[index].Check);
        GameBeds[index].Girl.SetActive(Convert.ToBoolean(GameBeds[index].Check));
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
