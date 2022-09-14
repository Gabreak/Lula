using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController2 : MonoBehaviour
{
    [SerializeField] private string _NameBoolOne;
    private Animator _animator;
    [SerializeField] private Animator _animator2;
    private void Start()
    {
        _animator = GetComponent<Animator>();
        _animator2 = GetComponent<Animator>();
    }

    public void InvertionIsBoolOne()
    {
        _animator.SetBool(_NameBoolOne,!_animator.GetBool(_NameBoolOne));

        //_animator2.SetBool(_NameBoolOne, false);
    }
}
