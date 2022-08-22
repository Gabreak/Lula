using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoExitRoom : MonoBehaviour
{
    private void OnEnable()
    {
        TimeManager.OnTick += UpdateLight;
    }

    private void OnDisable()
    {
        TimeManager.OnTick -= UpdateLight;
    }

    private async void UpdateLight(bool isLight)
    {
        await UpdateScene();
    }
    private async Task UpdateScene()
    {

        await Task.Delay(1);

        if (DoorManager.Instance.Hour == 0)
        {
            Instantiate(LightHubController.Instance.SceneDataHub.PrefabObject);
            Destroy(gameObject);
        }
    }
}