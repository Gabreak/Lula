using System.Threading.Tasks;

using UnityEngine;

public class AutoExitRoom : MonoBehaviour
{
    private void OnEnable()
    {
        TimeManager.OnTickHour += UpdateLight;
    }

    private void OnDisable()
    {
        TimeManager.OnTickHour -= UpdateLight;
    }

    private async void UpdateLight(int hear)
    {
        await UpdateScene();
    }
    private async Task UpdateScene()
    {

        await Task.Delay(0);

        Debug.Log(DoorManager.Instance.Hour);
        if (DoorManager.Instance.Hour == 0)
        {
            Instantiate(LightHubController.Instance.SceneDataHub.PrefabObject);
            Destroy(gameObject);
        }
    }
}
