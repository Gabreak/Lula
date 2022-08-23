using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class GirlController : MonoBehaviour
{
    [SerializeField] private GirlHef _girl;
    public void InviteGirl()
    {
        GirlManager.Instance.Invited = _girl;
    }
}
