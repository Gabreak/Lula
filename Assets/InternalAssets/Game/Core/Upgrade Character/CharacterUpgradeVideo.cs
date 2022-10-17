using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;

using UnityEngine;
using UnityEngine.Video;

public class CharacterUpgradeVideo : MonoBehaviour
{
    [SerializeField] private VideoPlayer _videoPlayer;
    [SerializeField] private GirlInfoRedirector _prefabUpgrade;
    [SerializeField] private string _name = "name";
    public ObjectUpgradeVideo[] Objects;
    private GirlInfoRedirector _prefab;
    private int CurrentLevel;
    private SpriteRenderer _spriteRenderer;

    private void OnEnable()
    {
        CurrentLevel = PlayerPrefs.GetInt(transform.root.name + _name, 0);
        if (CurrentLevel > 0)
            StarGame();
    }
    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
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
        StartCoroutine(GirlUpdateVideo(CurrentLevel));
        //_videoPlayer.clip = Objects[CurrentLevel].Video;
        CurrentLevel++;
        if (CurrentLevel < Objects.Length)
        {
            _prefab.Price.text = Objects[CurrentLevel].Price + "$";

        }
        else
            Destroy(_prefab?.gameObject);
    }

    private IEnumerator GirlUpdateVideo(int index)
    {
        _spriteRenderer.enabled = false;
        _videoPlayer.clip = Objects[index].Video;
        yield return new WaitForSeconds(0.05f);
        _spriteRenderer.enabled = true;
    }

    public void StarGame()
    {
        _videoPlayer.clip = Objects[CurrentLevel - 1].Video;
    }

    private void CreateUIUpgrade()
    {
        if (CurrentLevel < Objects.Length)
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

