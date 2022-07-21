using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PoliceManager : MonoBehaviour
{
    public static PoliceManager Instance;
    public GameObject JailPrefab;
    public const int MaxInPrison = 3;


    private static int _toJail;
    public static int ToJail
    {
        get => _toJail;
        set
        {
            _toJail = value;
        }
    }
    public static int WasInJail { get; set; }


    private void Start()
    {
        Instance = this;
    }

    public void LoadJail(GameObject currentScene)
    {
        ToJail++;
        if (WasInJail > MaxInPrison)
        {
            GameOver();
        }
        else if (ToJail > MaxInPrison)
        {
            WasInJail++;
            ToJail = 0;
            Instantiate(JailPrefab);
            Destroy(currentScene);
        }
    }

    public void GameOver()
    {

    }

}
