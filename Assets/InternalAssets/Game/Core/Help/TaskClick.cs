using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

public class TaskClick : MonoBehaviour
{
    [SerializeField] private TasksPrograss[] _tasks;

    public UnityAction<int> Action;


    private void OnEnable()
    {
        Action += Play;
    }

    private void OnDisable()
    {
        Action += Play;
    }

    private void OnMouseDown()
    {
        TaskManager.Instance.OnProgress(_tasks);
    }

    public void Play(int prograss)
    {
        TaskManager.Instance.OnProgress(_tasks,prograss);
    }
}
