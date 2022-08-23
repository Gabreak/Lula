using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEditor;

public class GirlManager : MonoBehaviour
{
    public GirlHef Invited;

    public static GirlManager Instance;

    private void Awake()
    {
        Instance = this;
    }
}

[Flags]
public enum GirlHef
{
    Chantal = 0,
    Amara = 1 << 0,
    Meilee = 1 << 1,
    Meilee1 = 1 << 2,
    Meilee2 = 1 << 3,
    Meilee3 = 1 << 4,
    Gabriel = 1 << 5
}
