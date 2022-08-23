using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class GirlController : MonoBehaviour
{
    [SerializeField] private GirlsData _data;
    public void InviteGirl(int indexGirl)
    {
        _data.Girls[indexGirl].IsActive = true;
    }
}
