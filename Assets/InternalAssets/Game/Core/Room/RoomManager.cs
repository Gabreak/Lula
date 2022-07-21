using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class RoomManager : MonoBehaviour
{
    public Sprite Enable;
    public Sprite Disable;

    [Space(20)]
    public Image SensorUSB;
    public Image SensorVedeoCamera;
    public Image SensorPhotoCamera;
    public Image SensorLight;

    [Space(20)]
    public TypeProducts GoodUSB;
    public TypeProducts GoodVideoCamera;
    public TypeProducts GoodPhotoCamera;
    public TypeProducts GoodLight;

    private void Start()
    {
        SensorInfo();
    }

    public void SensorInfo()
    {
        if (GoodUSB.SelectId == -1)
            SensorUSB.sprite = Disable;
        else
            SensorUSB.sprite = Enable;

        if (GoodVideoCamera.SelectId == -1)
            SensorVedeoCamera.sprite = Disable;
        else
            SensorVedeoCamera.sprite = Enable;

        if (GoodPhotoCamera.SelectId == -1)
            SensorPhotoCamera.sprite = Disable;
        else
            SensorPhotoCamera.sprite = Enable;

        if (GoodLight.SelectId == -1)
            SensorLight.sprite = Disable;
        else
            SensorLight.sprite = Enable;


    }
}
