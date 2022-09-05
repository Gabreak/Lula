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
        ColorManager.Instance.Play(OpenPrefab);
    }

    public void OpenPrefab()
    {
        if (_prefab != null)
            LoadManager.OpenPrefab(transform.root, _prefab);
        else if (_prefabScene != null)
            LoadManager.OpenPrefab(transform.root, _prefabScene.PrefabObject);
    }
}
