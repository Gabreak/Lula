using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class CreateCharacter : MonoBehaviour
{
    [SerializeField] private CharacterData _character;

    [SerializeField] private int _characterIndex;
    private void Start()
    {
        if (_character.Character[_characterIndex].InRoom)
        {
            Instantiate(_character.Character[_characterIndex].Prefab, transform);

        }

    }

}
