using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class LoadGame : MonoBehaviour
{

    public GameObject _infoOldGameDelete;
    public ScenesManager _scene;

    private bool _loaded;
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _loaded = PlayerPrefs.HasKey("Goods");
        if (_loaded)
        {
            _animator.SetBool("IsLoading", true);
        }

    }

    public void NewGame()
    {
        if (_loaded)
        {
            _infoOldGameDelete.SetActive(true);
        }
        else
        {
            _scene.NewGame(1);
        }
            
    }
}
