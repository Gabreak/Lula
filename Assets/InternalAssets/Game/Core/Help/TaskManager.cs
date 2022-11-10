using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

using UnityEditor;

using UnityEngine;
using UnityEngine.Localization;

public class TaskManager : MonoBehaviour
{
    public static TaskManager Instance;
    [SerializeField] private TaskRedirector _prefab;
    [SerializeField] private Transform _parent;
    [SerializeField] private LocalizedString[] _difficulty;
    [SerializeField] private List<Tasks> _taskPrograss;

    [SerializeField] private List<TaskRedirector> _redirectors;

    private void Awake()
    {
        Instance = this;
        _redirectors = new List<TaskRedirector>();
        Loading();
    }

    private void Start()
    {

    }

    public void Loading()
    {
        for (int i = 0; i < _taskPrograss.Count; i++)
        {
            _redirectors.Add(Instantiate(_prefab, _parent));
            Tasks taskList = _taskPrograss[i];
            _redirectors[i].Header.text = OnDifficulty(taskList);
            _redirectors[i].Text.text = taskList.Task.Tasks[taskList.Index].Text.GetLocalizedString();
            _redirectors[i].Reward.text = taskList.Task.Tasks[taskList.Index].RewardMoney + "$";
            _redirectors[i].Prograss.fillAmount = (float)taskList.Task.Tasks[taskList.Index].CurrentProgress / taskList.Task.Tasks[taskList.Index].MaxProgress;
        }
    }

    public static void OnSave()
    {

    }

    public void OnNext(TasksPrograss tasks, int indexTask)
    {
        Tasks task = _taskPrograss[indexTask];
        task.Index++;
        _taskPrograss[indexTask] = task;

        MoneyProperties.Money += tasks.Task.Tasks[tasks.Index].RewardMoney;
        Debug.Log(indexTask);
        if (_taskPrograss[indexTask].Index < tasks.Task.Tasks.Length)
        {
            _redirectors[indexTask].Header.text = OnDifficulty(task);
            _redirectors[indexTask].Text.text = task.Task.Tasks[task.Index].Text.GetLocalizedString();
            _redirectors[indexTask].Reward.text = task.Task.Tasks[task.Index].RewardMoney + "$";
            _redirectors[indexTask].Prograss.fillAmount = (float)task.Task.Tasks[task.Index].CurrentProgress / task.Task.Tasks[task.Index].MaxProgress;
            return;
        }
        _taskPrograss.RemoveAt(indexTask);
        Destroy(_redirectors[indexTask].gameObject);
        _redirectors.RemoveAt(indexTask);

        for (int i = 0; i < tasks.Task.TaskNext.Length; i++)
        {
            Tasks newTesk = new Tasks();
            newTesk.Task = task.Task.TaskNext[i];
            newTesk.Index = 0;
            _taskPrograss.Add(newTesk);
            TaskRedirector redirector = Instantiate(_prefab, _parent);
            redirector.Header.text = OnDifficulty(newTesk);
            redirector.Text.text = newTesk.Task.Tasks[0].Text.GetLocalizedString();
            redirector.Reward.text = newTesk.Task.Tasks[0].RewardMoney + "$";
            redirector.Prograss.fillAmount = (float)newTesk.Task.Tasks[newTesk.Index].CurrentProgress / newTesk.Task.Tasks[newTesk.Index].MaxProgress;
            _redirectors.Add(redirector);
        }

        Debug.Log("Good");
    }

    public bool OnProgress(TasksPrograss[] task)
    {
        Action<int, int> action;
        action = (i, j) => _taskPrograss[i].Task.Tasks[_taskPrograss[i].Index].CurrentProgress += task[j].Prograss;

        return TakeProgress(task, action);
    }

    public bool OnProgress(TasksPrograss[] task, int prograss)
    {
        Action<int, int> action;
        action = (i, j) => _taskPrograss[i].Task.Tasks[_taskPrograss[i].Index].CurrentProgress = prograss;

        return TakeProgress(task, action);
    }

    public bool TakeProgress(TasksPrograss[] task, Action<int, int> action)
    {
        for (int i = 0; i < _taskPrograss.Count; i++)
        {
            for (int j = 0; j < task.Length; j++)
            {
                bool isTask = _taskPrograss[i].Task == task[j].Task;
                if (isTask)
                {


                    if (_taskPrograss[i].Index == task[j].Index)
                    {


                        bool isPrograss = _taskPrograss[i].Task.Tasks[task[j].Index].IsShowProgress;

                        if (!isPrograss)
                        {
                            OnNext(task[j], i);
                            return true;
                        }

                        action.Invoke(i, j);


                        int max = _taskPrograss[i].Task.Tasks[_taskPrograss[i].Index].MaxProgress;
                        int current = _taskPrograss[i].Task.Tasks[_taskPrograss[i].Index].CurrentProgress;

                        _redirectors[i].Prograss.fillAmount = (float)current / max;

                        if (current >= max)
                        {

                            OnNext(task[j], i);
                            return true;
                        }
                    }
                }
            }
        }
        return false;
    }



    private string OnDifficulty(Tasks task)
    {
        TaskGroup type = task.Task.Tasks[task.Index].TypeGroup;
        return _difficulty[(int)type].GetLocalizedString();
    }

}


[Serializable]
public struct Tasks
{
    public TaskData Task;
    public int Index;
}


[Serializable]
public struct TasksPrograss
{
    public TaskData Task;
    public int Index;
    public int Prograss;
}
