using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Video;
using System.Diagnostics;

public class RoomSensor : MonoBehaviour
{

    public List<SensorRedirector> Sensors;
    [SerializeField] private BaseSensor _sensorLight;
    [SerializeField] private BaseSensor _sensorPhoto;
    [SerializeField] private BaseSensor _sensorVideo;
    [SerializeField] private BaseSensor _sensorUsb;
    [SerializeField] private UnityEvent _onEnableHandle;



    [Space(20), Header("Usb")]
    [SerializeField] private TMP_InputField _usbName;

    [Space(20), Header("Video")]
    [SerializeField] private VideoGirlsData _dataVideo;

    private void OnEnable()
    {
        _onEnableHandle.Invoke();
    }

    private void Start()
    {
        BaseSensor[] sensors = GameDataBase.Instance.Sensor;

        for (int i = 0; i < sensors.Length; i++)
        {
            GameGoods? good = sensors[i].Good;
            bool isGood = good == null;
            if (isGood) continue;

            bool isIdGood = good.Value.Id == -1;
            if (isIdGood) continue;

            bool isUsb = sensors[i].Type.Index == GameDataBase.Instance.UsbSensor.Type.Index;
            if (isUsb) continue;

            GameObject createGood = Instantiate(good.Value.Product, transform);
            sensors[i].CreateGood = createGood;
        }
    }


    public void PlayPhotoVideo()
    {
        bool isGoodLight = _sensorLight.Good != null;
        bool isGoodPhoto = _sensorPhoto.Good != null;
        bool isGoodUsb = _sensorUsb.Good != null;

        if (isGoodUsb && isGoodPhoto && isGoodLight)
        {
            int price = _sensorLight.Good.Value.Price + _sensorPhoto.Good.Value.Price + Random.Range(-100, 0);
            int indexVideo = _sensorPhoto.Good.Value.Level;

            PlayDefault(0, indexVideo, price);
        }
    }

    public void PlayVideo()
    {
        bool isGoodLight = _sensorLight.Good != null;
        bool isGoodVideo = _sensorVideo.Good != null;
        bool isGoodUsb = _sensorUsb.Good != null;

        if (isGoodUsb && isGoodVideo && isGoodLight)
        {
            if (_dataVideo.IndexVideo == 0)
            {
                int price = _sensorLight.Good.Value.Price + _sensorVideo.Good.Value.Price + Random.Range(-1000, 0);
                int indexVideo = _sensorVideo.Good.Value.Level;

                PlayDefault(1, indexVideo, price);
            }
            else
                PlayOtherGirls();
        }
    }

    public void PlayDefault(int indexList, int indexVideo, int price)
    {
        VideoClip clip = VideoController.Instance.GetVideo(indexList, indexVideo);
        UsbRecord(price, clip);
        VideoController.Instance.VideoPlay(transform.gameObject, indexList, indexVideo);
    }

    public void PlayOtherGirls()
    {
        //проверка игрушки

        VideoClip clip = _dataVideo.Videos[_dataVideo.IndexVideo].Clip[0];

        int price = 0;
        UsbRecord(price, clip);
        VideoController.Instance.VideoPlay(transform.gameObject, clip);
    }


    private void UsbRecord(int price, VideoClip clip)
    {
        GameGoods[] usbGoods = _sensorUsb.Type.Acquired.ToArray();
        for (int i = 0; i < usbGoods.Length; i++)
        {
            if (_sensorUsb.Good.Value.Id == usbGoods[i].Id)
            {
                _sensorUsb.Type.Acquired.RemoveAt(i);
                _sensorUsb.Good = null;
                break;
            }
        }

        BaseRecord baseRecord = GameDataBase.Instance.UsbRecord;
        VideoRecord record = new VideoRecord();
        record.Name = _usbName.text;
        record.Price = price;
        record.Clip = clip;
        baseRecord.Records.Add(record);
    }
}
