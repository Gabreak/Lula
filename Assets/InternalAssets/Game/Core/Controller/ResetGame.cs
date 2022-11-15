using Sirenix.OdinInspector;

using UnityEngine;

public class ResetGame : MonoBehaviour
{
    [Required]
    [SerializeField] private VideoGirlsData _videoGirls;
    [Required]
    [SerializeField] private GameDataBase _dataBase;
    [Button("NewGame")]
    public void NewGame()
    {
        DefaultGirls();
        TaskDefault();

    }

    private void DefaultGirls()
    {
        for (int i = 0; i < _videoGirls.Girls.Length; i++)
        {
            GirlBase girl = _videoGirls.Girls[i];
            _videoGirls.Girls[i].IsActive = (girl.Id == 1);
        }

        Debug.Log("Ready Girls");
    }

    private void TaskDefault()
    {
        foreach (var taskType in _dataBase.TaskBase)
            for (int i = 0; i < taskType.Tasks.Length; i++)
                taskType.Tasks[i].CurrentProgress = 0;
    }
}
