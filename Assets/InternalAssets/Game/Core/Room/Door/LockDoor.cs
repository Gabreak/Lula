using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Localization;

public class LockDoor : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private LocalizedString _massageRoomLock;
    private void OnMouseDown()
    {
        if (DoorManager.Instance.isDoor)
            LoadManager.OpenPrefab(transform.root, _prefab);
        else
        {
            ControlSystemProperties.DisableInvoke();
            WindowMessage.Message(_massageRoomLock.GetLocalizedString(), WindowIcon.Information);

        }
    }

}
