using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class RedirectorPawnshop : MonoBehaviour
{
    public DataProduct Data;
    public Image Icon;
    public Text TextPrice;

    public void ChooseProduct()
    {
        PawnProperties.ChooseProduct(Data, gameObject);
    }

    public void ChooseBuyBack()
    {
        PawnProperties.ChooseBuyBack(Data, gameObject);
    }
}
