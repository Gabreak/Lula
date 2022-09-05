using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class GameMenu : MonoBehaviour
{
    [SerializeField] private GameObject _menuGame;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _menuGame.SetActive(!_menuGame.activeSelf);
        }
    }
}
