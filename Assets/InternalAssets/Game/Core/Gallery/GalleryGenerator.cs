using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class GalleryGenerator : MonoBehaviour
{

    [SerializeField] private GalleryRedirector _prefab;
    [SerializeField] private GalleryData _data;
    private void Start()
    {
        OnGenerate();
    }

    private void OnGenerate()
    {
        for (int i = 0; i < _data.Bases.Length; i++)
        {
            GalleryRedirector redirector = Instantiate(_prefab, transform);
            redirector.Img.sprite = _data.Bases[i].Image;
            if(!_data.Bases[i].Text.IsEmpty)
                redirector.Txt.text = _data.Bases[i].Text.GetLocalizedString();
        }
    }
}
