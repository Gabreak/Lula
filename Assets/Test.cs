using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;

public class Test : MonoBehaviour
{
    private LocalizeStringEvent _test;
    void Start()
    {

        _test = GetComponent<LocalizeStringEvent>();
        //Debug.Log(_test.StringReference.ContainsKey(_test.StringReference.GetLocalizedString()));
        Debug.Log(_test.StringReference.GetLocalizedString()[0] == 'N');
        Debug.Log(_test.StringReference == null);
    }


}
