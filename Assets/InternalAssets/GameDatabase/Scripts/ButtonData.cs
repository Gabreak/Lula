using System.Collections;
using System.Collections.Generic;

using UnityEngine;


[CreateAssetMenu(fileName = "Animation", menuName = "GameData/Animation", order = 0260)]
public class ButtonData : ScriptableObject
{
    public AnimationClip AnimationDown;
    public AnimationClip AnimationUp;
    public AnimationClip AnimationEnter;
    public AnimationClip AnimationExit;
}

