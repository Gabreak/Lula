using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

public class LightHubController : MonoBehaviour
{
    public static LightHubController Instance;
    public SceneData SceneDataHub;
    public Light PostLight;
    [SerializeField] private GameObject _hubLight;
    [SerializeField] private GameObject _hubNight;

    private void OnEnable() => TimeManager.OnTick += UpdateScene;
    private void OnDisable() => TimeManager.OnTick -= UpdateScene;

    private void Awake()
    {
        Instance = this;
        SceneDataHub.PrefabObject = _hubNight;
    }
    private void UpdateScene(bool isLight)
    {
        SceneDataHub.PrefabObject = isLight ? _hubLight : _hubNight;
    }

    //void Update()
    //{
    //    int hour = TimeManager.Instance.Hour;
    //    if (hour > _from || hour < _to)
    //    {
    //        //Debug.Log("s");
    //        //Instantiate(_scene);
    //        //Destroy(gameObject);
    //    }
    //}
}
