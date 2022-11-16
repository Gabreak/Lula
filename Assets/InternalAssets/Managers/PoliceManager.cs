using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class PoliceManager : MonoBehaviour
{
    public static PoliceManager Instance;
    public GameObject JailPrefab;
    public const int MaxInPrison = 20;
    public const int MaxInGameOver = 3;
    public const float Stars = 60;

    [SerializeField] private Image Bar;

    public int CountViolation { get; set; } = 0;


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

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        ToJail = PlayerPrefs.GetInt("ToJail", 0);
        WasInJail = PlayerPrefs.GetInt("WasInJail", 0);
        CountViolation = PlayerPrefs.GetInt("CountViolation", 0);
        Bar.fillAmount = CountViolation / Stars;
    }

    private void OnDisable()
    {
        PlayerPrefs.SetInt("ToJail", _toJail);
        PlayerPrefs.SetInt("WasInJail", WasInJail);
        PlayerPrefs.SetInt("CountViolation", CountViolation);
    }



    public bool LoadJail(GameObject currentScene, int fine)
    {
        ToJail += fine;
        CountViolation += fine;
        UpdateBar();
        if (ToJail >= MaxInPrison)
        {
            WasInJail++;
            ToJail = 0;
            if (WasInJail >= MaxInGameOver)
            {
                GameOver();
            }
            else
            {
                Instantiate(JailPrefab);
                Destroy(currentScene);
            }
                return false;
        }
        return true;
    }

    public void UpdateBar()
    {
        Bar.fillAmount = (float)CountViolation / Stars;
        Debug.Log(Bar.fillAmount);

    }

    public void GameOver()
    {
        CountViolation = 0;
        SaveManager.Instance.DeleteSave();
    }

}
