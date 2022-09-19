using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.EventSystems;

public class PrefabTransition : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private SceneData _prefabScene;


    private void OnMouseDown()
    {
        OpenPrefab();
    }


    public void OpenPrefab()
    {
        if (ColorManager.Instance.CurrentScene == null && ColorManager.Instance.DestroyScene == null)
        {

            ColorManager.Instance.CurrentScene = (_prefab != null) ? _prefab : _prefabScene.PrefabObject;
            ColorManager.Instance.DestroyScene = transform.root;
            ColorManager.Instance.Play();
        }
    }

}
