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
            if (_menuGame.activeSelf)
            {
                Time.timeScale = 0;
                ControlSystemProperties.DisableInvoke();
            }
            else
            {
                Time.timeScale = 1;
                ControlSystemProperties.EnableInvoke();
            }
        }
    }

    public void GamePlay()
    {
        Time.timeScale = 1;
        ControlSystemProperties.EnableInvoke();
    }

}
