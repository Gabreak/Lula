using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class AnimatorShantal : MonoBehaviour
{
    private Animator m_Animator;
    private void Start()
    {
        m_Animator = GetComponent<Animator>();
    }

    private void OnMouseDown()
    {
        m_Animator.SetTrigger("Next");
    }
}
