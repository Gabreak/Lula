using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class CreateCharacter : MonoBehaviour
{
    [SerializeField] private CharacterData _character;
    [SerializeField] private int _characterIndex;
    [SerializeField] private GameObject _characterObject;

    [SerializeField] private bool _isRoom;

    [SerializeField] private bool _onEnable;

    private void OnEnable()
    {
        if (_onEnable)
        {

            bool isVisible = _character.Character[_characterIndex].IsRoomBuy && (_character.Character[_characterIndex].InRoom == _isRoom);
            _characterObject.SetActive(isVisible);
        }


    }


    public void OpenDialog()
    {
        int dialogId = _character.Character[_characterIndex].CurrentIndexDialog;
        Instantiate(_character.Character[_characterIndex].Dialogs[dialogId]);
    }

}
