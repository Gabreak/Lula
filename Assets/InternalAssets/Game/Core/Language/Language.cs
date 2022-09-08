using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class Language : MonoBehaviour
{

    public void ChangeLocale(int id)
    {
        StartCoroutine(SetLocale(id));
    }
    public IEnumerator SetLocale(int id)
    {
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[id];
    }
}
