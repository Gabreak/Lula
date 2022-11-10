using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class ObjectAnimator : MonoBehaviour
{
    private Animator _animator;


    private void Awake() => Init();

    private void OnEnable() => Play(false);

    private void OnMouseEnter() => Play(true);

    private void OnMouseExit() => Play(false);

    //private void OnMouseDown() => Play(true, true);

    //private void OnMouseUp() => Play(true, false);


    private void Init() => _animator = GetComponent<Animator>();

    //private void Play(bool isActive) => _animator.SetBool("Enter", isActive);
    private void Play(bool isEnter)
    {
        _animator.SetBool("Enter", isEnter);
        //_animator.SetBool("Exit", isEnter);

    }
}
