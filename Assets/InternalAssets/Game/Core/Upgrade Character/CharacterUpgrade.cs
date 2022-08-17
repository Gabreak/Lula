using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class CharacterUpgrade : MonoBehaviour
{
    [SerializeField] private GirlInfoRedirector _prefabUpgrade;
    private GirlInfoRedirector _prefab;
    [SerializeField] private string _name = "name";
    private int CurrentLevel;
    public ObjectUpgrade[] Objects;

    private void OnEnable()
    {
        CurrentLevel = PlayerPrefs.GetInt(transform.root.name + _name, 0);
        StarGame();
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
        Objects[CurrentLevel].Object.SetActive(false);
        CurrentLevel++;
        if (CurrentLevel < Objects.Length)
            _prefab.Price.text = Objects[CurrentLevel].Price + "$";
        else
            Destroy(_prefab?.gameObject);


    }

    public void StarGame()
    {
        for (int i = 0; i < CurrentLevel; i++)
        {
            Objects[i].Object.SetActive(false);
        }
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
public struct ObjectUpgrade
{
    public GameObject Object;
    public int Price;
}
