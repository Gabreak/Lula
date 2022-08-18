using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PoliceManager : MonoBehaviour
{
    public static PoliceManager Instance;
    public GameObject JailPrefab;
    public const int MaxInPrison = 3;


    private int _toJail;
    public int ToJail
    {
        get => _toJail;
        set
        {
            _toJail = value;
        }
    }
    public int WasInJail { get; set; }


    private void OnEnable()
    {
        ToJail = PlayerPrefs.GetInt("ToJail", 0);
        WasInJail = PlayerPrefs.GetInt("WasInJail", 0);
    }

    private void OnDisable()
    {
        PlayerPrefs.SetInt("ToJail", _toJail);
        PlayerPrefs.SetInt("WasInJail", WasInJail);
    }

    private void Awake()
    {
        Instance = this;
    }

    public void LoadJail(GameObject currentScene)
    {
        ToJail++;
        if (ToJail >= MaxInPrison)
        {
            WasInJail++;
            ToJail = 0;
            if (WasInJail >= MaxInPrison)
            {
                GameOver();
            }
            else
            {

                Instantiate(JailPrefab);
                Destroy(currentScene);
            }
        }
    }

    public void GameOver()
    {
        SaveManager.Instance.DeleteSave();
    }

}
