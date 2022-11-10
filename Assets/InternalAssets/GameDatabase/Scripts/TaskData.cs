using Sirenix.OdinInspector;

using System;

using UnityEngine;
using UnityEngine.Localization;


[CreateAssetMenu(fileName = "Task", menuName = "GameData/Task", order = 280)]
public class TaskData : ScriptableObject
{
    public TaskData[] TaskNext;
    [ListDrawerSettings(ShowIndexLabels = true), TableList]
    public TaskList[] Tasks;
}


[Serializable]
public struct TaskList
{
    [TabGroup("Text")]
    public LocalizedString Text;
    //[TabGroup("Text")]
    //public LocalizedString Header;
    [TabGroup("Text")]
    public int RewardMoney;
    [EnumToggleButtons, GUIColor(0.4f, 1, 1), TabGroup("Type")]
    public TaskGroup TypeGroup;
    [TabGroup("Progress")]
    public bool IsShowProgress;
    [TabGroup("Progress"), ShowIf("IsShowProgress")]
    public int MaxProgress;
    [TabGroup("Progress"), ShowIf("IsShowProgress"), ProgressBar(0, "MaxProgress")]
    public int CurrentProgress;
}
