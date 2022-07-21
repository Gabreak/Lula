using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class GameDataBase : MonoBehaviour
{
    public BaseProduct[] BaseProducts;

    public PawnShopBase Pawnshop;

    public static GameDataBase Instance;

    void Awake()
    {
        Instance = this;
    }
}
