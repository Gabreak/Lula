using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    [SerializeField] private string _NameBoolOne;
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void InvertionIsBoolOne()
    {
        _animator.SetBool(_NameBoolOne, !_animator.GetBool(_NameBoolOne));
    }
}
