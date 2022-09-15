using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField] private string _NameBoolOne;
    [SerializeField] private Animator _animator;
    [SerializeField] private Animator _animator2;

    public void InvertionIsBoolOne()
    {
        _animator.SetBool(_NameBoolOne, !_animator.GetBool(_NameBoolOne));
        _animator2.SetBool(_NameBoolOne, false);
    }

}
