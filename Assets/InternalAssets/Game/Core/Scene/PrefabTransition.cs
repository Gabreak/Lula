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

        ColorManager.Instance.CurrentScene = (_prefab != null) ? _prefab : _prefabScene.PrefabObject;
        ColorManager.Instance.DestroyScene = transform.root;
        ColorManager.Instance.Play();
    }


    //public void OpenPrefab(GameObject prefab)
    //{
    //    LoadManager.OpenPrefab(transform.root, prefab);
    //}
}
