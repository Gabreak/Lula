using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class GirlGroupManager : MonoBehaviour
{
    public static GirlGroupManager Instance;

    private void Awake()
    {
        Instance = this;
    }
}
