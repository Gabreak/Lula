using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;

using UnityEngine;
using UnityEngine.Video;

public class CharacterUpgradeVideo : MonoBehaviour
{
    //[SerializeField] private VideoPlayer[] _videoPlayer;
    [SerializeField] private GirlInfoRedirector _prefabUpgrade;
    [SerializeField] private CharacterVideoRedirector _prefabVideo;
    [SerializeField] private string _name = "name";
    public ObjectUpgradeVideo[] Objects;
    private GirlInfoRedirector _prefab;
    public int CurrentLevel { get; set; }
    //public SpriteRenderer RendererSprite { get; set; }
    public CharacterVideoRedirector[] Rediretors { get; set; }


    private void OnEnable()
    {
        CurrentLevel = PlayerPrefs.GetInt(transform.root.name + _name, 0);
        GenerateCharacterVideo();
    }

    public void GenerateCharacterVideo()
    {
        Rediretors = new CharacterVideoRedirector[Objects.Length];

        for (int i = CurrentLevel; i < Objects.Length; i++)
        {
            Rediretors[i] = Instantiate(_prefabVideo, Vector3.zero, Quaternion.identity, transform);
            Rediretors[i].transform.localPosition = Vector3.down * 100;
            Rediretors[i].Video.clip = Objects[i].Video;
        }
        Rediretors[CurrentLevel].transform.localPosition = Vector3.zero;
    }

    private void Start()
    {
        CreateUIUpgrade();
    }

    private void OnDisable()
    {
        PlayerPrefs.SetInt(transform.root.name + _name, CurrentLevel);

        if (_prefab != null)
            Destroy(_prefab.gameObject);
    }

    public void LevelNext()
    {
        if (!(CurrentLevel < Objects.Length)) return;
        if (!(MoneyProperties.Money >= Objects[CurrentLevel].Price)) return;

        MoneyProperties.Money -= Objects[CurrentLevel].Price;
        CurrentLevel++;

        StartCoroutine(GirlUpdateVideo(CurrentLevel));

        if (CurrentLevel < Objects.Length - 1)
        {
            _prefab.Price.text = Objects[CurrentLevel].Price + "$";
        }
        else
            Destroy(_prefab?.gameObject);
    }

    private IEnumerator GirlUpdateVideo(int index)
    {
        Rediretors[index - 1].transform.localPosition = Vector3.down * 100;
        Rediretors[index].transform.localPosition = Vector3.zero;
        yield return null;
    }



    private void CreateUIUpgrade()
    {
        if (CurrentLevel < Objects.Length - 1)
        {
            _prefab = Instantiate(_prefabUpgrade, GirlGroupManager.Instance.transform);
            _prefab.Name.text = _name;
            _prefab.Price.text = Objects[CurrentLevel].Price + "$";
            _prefab.Buy.onClick.AddListener(LevelNext);
        }
    }
}


[Serializable]
public struct ObjectUpgradeVideo
{
    public VideoClip Video;
    public int Price;
}

