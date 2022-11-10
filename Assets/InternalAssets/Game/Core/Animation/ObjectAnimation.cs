using Sirenix.OdinInspector;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[RequireComponent(typeof(Animation))]
[RequireComponent(typeof(Collider2D))]
public class ObjectAnimation : MonoBehaviour
{
    [Required]
    [SerializeField] private ButtonData m_Button;
    private Animation m_Animation;


    //[SerializeField] private AnimationClip _animationEnter;
    //[SerializeField] private AnimationClip _animationExit;
    //[SerializeField] private AnimationClip _animationDown;
    //[SerializeField] private AnimationClip _animationUp;


    private void Awake() => Init();

    private void OnEnable() => Play(m_Button.AnimationExit);

    private void OnMouseEnter() => Play(m_Button.AnimationEnter);

    private void OnMouseExit() => Play(m_Button.AnimationExit);

    private void OnMouseDown() => Play(m_Button.AnimationDown);

    private void OnMouseUp() => Play(m_Button.AnimationUp);


    private void Init()
    {
        m_Animation = GetComponent<Animation>();

        AddClip(m_Button.AnimationDown);
        AddClip(m_Button.AnimationUp);
        AddClip(m_Button.AnimationEnter);
        AddClip(m_Button.AnimationExit);

    }

    private void AddClip(AnimationClip clip)
    {
        if (clip != null)
            m_Animation.AddClip(clip, clip.name);
    }

    private void Play(AnimationClip clip)
    {
        if (clip != null)
        {
            m_Animation.clip = clip;
            m_Animation.Play();
        }
    }
}
