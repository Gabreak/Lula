using System.Collections;
using System.Threading.Tasks;
using System.Collections.Generic;

using UnityEngine;

public class HubLight : MonoBehaviour
{
    [SerializeField] private bool _isNightHub = true;

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
        if (_isNightHub)
        {
            Instantiate(LightHubController.Instance.SceneDataHub.PrefabObject);
            Destroy(gameObject);
        }
    }

}
