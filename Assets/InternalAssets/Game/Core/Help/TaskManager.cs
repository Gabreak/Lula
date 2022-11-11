using System;
using System.Linq;
using System.Collections.Generic;


using UnityEditor;

using UnityEngine;
using UnityEngine.Localization;

public class TaskManager : MonoBehaviour
{
    public static TaskManager Instance;
    [SerializeField] private TaskRedirector _prefab;
    [SerializeField] private Transform _parent;
    [SerializeField] private LocalizedString[] _difficulty;
    [SerializeField] private List<Tasks> m_taskProgress;
    [SerializeField] private List<Tasks> _taskProgressDefault;

    private List<TaskRedirector> _redirectors;

    private void Awake()
    {
        Instance = this;
        _redirectors = new List<TaskRedirector>();
        m_taskProgress = new List<Tasks>();
        Loading();
    }

    private void OnDisable()
    {
        OnSave();
    }


    public void Loading()
    {
        int count = PlayerPrefs.GetInt("CountTask", 0);
        Debug.Log(count);
        if (count > 0)
        {
            for (int i = 0; i < count; i++)
            {
                Tasks task = new Tasks();
                int index = PlayerPrefs.GetInt("TaskType" + i, 0);
                task.Task = GameDataBase.Instance.TaskBase[index];
                task.Index = PlayerPrefs.GetInt("TaskIndex" + i, 0);
                task.Task.Tasks[task.Index].CurrentProgress = PlayerPrefs.GetInt("TaskProgress" + i, 0);
                m_taskProgress.Add(task);
            }
        }
        else
        {
            m_taskProgress.AddRange(_taskProgressDefault);
        }

        for (int i = 0; i < m_taskProgress.Count; i++)
        {
            _redirectors.Add(Instantiate(_prefab, _parent));
            Tasks taskList = m_taskProgress[i];
            _redirectors[i].Header.text = OnDifficulty(taskList);
            _redirectors[i].Text.text = taskList.Task.Tasks[taskList.Index].Text.GetLocalizedString();
            _redirectors[i].Reward.text = taskList.Task.Tasks[taskList.Index].RewardMoney + "$";
            _redirectors[i].Prograss.fillAmount = (float)taskList.Task.Tasks[taskList.Index].CurrentProgress / taskList.Task.Tasks[taskList.Index].MaxProgress;
        }
    }

    public void OnSave()
    {
        PlayerPrefs.SetInt("CountTask", m_taskProgress.Count);
        for (int i = 0; i < m_taskProgress.Count; i++)
        {
            for (int j = 0; j < GameDataBase.Instance.TaskBase.Length; j++)
            {
                if (m_taskProgress[i].Task == GameDataBase.Instance.TaskBase[j])
                {
                    PlayerPrefs.SetInt("TaskType" + i, j);
                    break;
                }
            }
            PlayerPrefs.SetInt("TaskIndex" + i, m_taskProgress[i].Index);
            PlayerPrefs.SetInt("TaskProgress" + i, m_taskProgress[i].Task.Tasks[m_taskProgress[i].Index].CurrentProgress);
        }
    }

    public void OnNext(TasksPrograss tasks, int indexTask)
    {
        Tasks task = m_taskProgress[indexTask];
        task.Index++;
        m_taskProgress[indexTask] = task;

        MoneyProperties.Money += tasks.Task.Tasks[tasks.Index].RewardMoney;
        Debug.Log(indexTask);
        if (m_taskProgress[indexTask].Index < tasks.Task.Tasks.Length)
        {
            _redirectors[indexTask].Header.text = OnDifficulty(task);
            _redirectors[indexTask].Text.text = task.Task.Tasks[task.Index].Text.GetLocalizedString();
            _redirectors[indexTask].Reward.text = task.Task.Tasks[task.Index].RewardMoney + "$";
            _redirectors[indexTask].Prograss.fillAmount = (float)task.Task.Tasks[task.Index].CurrentProgress / task.Task.Tasks[task.Index].MaxProgress;
            return;
        }
        m_taskProgress.RemoveAt(indexTask);
        Destroy(_redirectors[indexTask].gameObject);
        _redirectors.RemoveAt(indexTask);

        for (int i = 0; i < tasks.Task.TaskNext.Length; i++)
        {
            Tasks newTesk = new Tasks();
            newTesk.Task = task.Task.TaskNext[i];
            newTesk.Index = 0;
            m_taskProgress.Add(newTesk);
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
        action = (i, j) => m_taskProgress[i].Task.Tasks[m_taskProgress[i].Index].CurrentProgress += task[j].Prograss;

        return TakeProgress(task, action);
    }

    public bool OnProgress(TasksPrograss[] task, int prograss)
    {
        Action<int, int> action;
        action = (i, j) => m_taskProgress[i].Task.Tasks[m_taskProgress[i].Index].CurrentProgress = prograss;

        return TakeProgress(task, action);
    }

    public bool TakeProgress(TasksPrograss[] task, Action<int, int> action)
    {
        for (int i = 0; i < m_taskProgress.Count; i++)
        {
            for (int j = 0; j < task.Length; j++)
            {
                bool isTask = m_taskProgress[i].Task == task[j].Task;
                if (isTask)
                {


                    if (m_taskProgress[i].Index == task[j].Index)
                    {


                        bool isPrograss = m_taskProgress[i].Task.Tasks[task[j].Index].IsShowProgress;

                        if (!isPrograss)
                        {
                            OnNext(task[j], i);
                            return true;
                        }

                        action.Invoke(i, j);


                        int max = m_taskProgress[i].Task.Tasks[m_taskProgress[i].Index].MaxProgress;
                        int current = m_taskProgress[i].Task.Tasks[m_taskProgress[i].Index].CurrentProgress;

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
