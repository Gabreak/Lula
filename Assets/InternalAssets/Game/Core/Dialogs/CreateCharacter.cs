using Sirenix.OdinInspector;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.TextCore.Text;

public class CreateCharacter : MonoBehaviour
{
    public GameObject CharacterObject;
    [SerializeField] private CharacterData _character;
    [SerializeField] private int _characterIndex;

    [SerializeField] private bool _isRoom;

    [SerializeField] private bool _onEnable;

    private GameObject _dialog;

    private void OnEnable()
    {
        if (_onEnable)
        {
            VisibleCharacter(CharacterObject);
        }
    }

    public void VisibleCharacter(GameObject character)
    {
        bool isVisible = _character.Character[_characterIndex].IsRoomBuy && (_character.Character[_characterIndex].InRoom == _isRoom);
        character.SetActive(isVisible);
    }


    public void OpenDialog()
    {
        int dialogId = _character.Character[_characterIndex].CurrentIndexDialog;

        if (_dialog != null) Destroy(_dialog.gameObject);
        _dialog = Instantiate(_character.Character[_characterIndex].Dialogs[dialogId], transform);
    }

    public void DialogNext(int dialogIndex)
    {
        _character.Character[_characterIndex].CurrentIndexDialog = dialogIndex;
        _character.Character[_characterIndex].InRoom = _isRoom;
        CreateCharacter character = transform.root.GetComponent<CreateCharacter>();
        character.VisibleCharacter(character.CharacterObject);
    }

    public void BeginDialog()
    {
        _character.Character[_characterIndex].CurrentIndexDialog = 0;
        _character.Character[_characterIndex].InRoom = _isRoom;
        CreateCharacter character = transform.root.GetComponent<CreateCharacter>();
        character.VisibleCharacter(character.CharacterObject);
    }

}
