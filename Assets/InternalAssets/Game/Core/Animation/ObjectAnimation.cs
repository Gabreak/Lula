using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[RequireComponent(typeof(Animation))]
[RequireComponent(typeof(Collider2D))]
public class ObjectAnimation : MonoBehaviour
{
    private Animation _animation;

    [SerializeField] private AnimationClip _animationEnter;
    [SerializeField] private AnimationClip _animationExit;
    [SerializeField] private AnimationClip _animationDown;
    [SerializeField] private AnimationClip _animationUp;


    private void Awake() => Init();

    private void OnEnable() => Play(_animationExit);

    private void OnMouseEnter() => Play(_animationEnter);

    private void OnMouseExit() => Play(_animationExit);

    private void OnMouseDown() => Play(_animationDown);

    private void OnMouseUp() => Play(_animationUp);


    private void Init()
    {
        _animation = GetComponent<Animation>();

        AddClip(_animationEnter);
        AddClip(_animationExit);
        AddClip(_animationDown);
        AddClip(_animationUp);
    }

    private void AddClip(AnimationClip clip)
    {
        if (clip != null)
            _animation.AddClip(clip, clip.name);
    }

    private void Play(AnimationClip clip)
    {
        if (clip != null)
        {
            _animation.clip = clip;
            _animation.Play();
        }
    }
}
