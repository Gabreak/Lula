using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;


public class Turntable : MonoBehaviour
{
    [SerializeField] private BaseRecord Video;
    [SerializeField] private VideoRedirector redirectorVideo;

    private void OnEnable()
    {
        for (int i = 0; i < Video.Records.Count; i++)
        {
            VideoRedirector redirecotor = Instantiate(redirectorVideo, transform);
            redirecotor.Name.text = $"{i + 1}: {Video.Records[i].Name}";
            redirecotor.Clip = Video.Records[i].Clip;
        }
    }
    private void OnDisable()
    {
        for (int i = Video.Records.Count - 1; i >= 0; i--)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }

}
