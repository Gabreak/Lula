using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Console : MonoBehaviour
{
    [SerializeField] private GameObject _panelConsole;
    [SerializeField] private TMP_InputField _pointer;
    [SerializeField] private TextMeshProUGUI _textStory;

    private string textInput = "CONSOLE";
    private string _previousCommand = "";

    private int indexInput = 0;


    private void OnGUI()
    {
        if (Event.current.keyCode != KeyCode.None && Event.current.type == EventType.KeyDown)
        {
            KeyDetector(Event.current.keyCode);
        }
    }

    public void OnEndEdit()
    {
        _textStory.text += $"\n {SetData()}";
        _pointer.text = "";
        _pointer.ActivateInputField();
    }


    private string SetData()
    {
        switch (_previousCommand)
        {
            case "time":
                _previousCommand = "";
                try
                {
                    int timeSpeed = int.Parse(_pointer.text);
                    if (timeSpeed > 0)
                    {
                        TimeManager.Instance.Speed = timeSpeed;
                        return $"Time Speed: {timeSpeed}";
                    }
                    else
                        return "\n Error: value > 0";


                }
                catch
                {
                    return "Error time";
                }

            case "money":
                _previousCommand = "";
                try
                {
                    int money = int.Parse(_pointer.text);
                    MoneyProperties.Money = money;
                    return $"\n Money: {money}$";
                }
                catch
                {
                    return "Error money";
                }

            default:
                return Command();

        }
    }

    private string Command()
    {
        string value = _pointer.text.ToLower();

        switch (value)
        {
            case "money":
                _previousCommand = "money";
                return "Money:";

            case "time":
                _previousCommand = "time";
                return "Time:";

            case "close":
                _panelConsole.SetActive(false);
                return "exit";

            case "exit":
                _panelConsole.SetActive(false);
                return "exit";
        }

        _previousCommand = "";
        return _pointer.text;
    }



    private void KeyDetector(KeyCode key)
    {

        if (key.ToString() == textInput[indexInput].ToString())
        {
            indexInput++;
            if (indexInput == 7)
            {
                indexInput = 0;
                _panelConsole.SetActive(true);
            }
        }
        else
            indexInput = 0;


    }
}
