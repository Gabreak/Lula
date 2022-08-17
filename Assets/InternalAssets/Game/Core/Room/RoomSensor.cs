using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

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
        Debug.Log(_sensorLight.Good);
        Debug.Log(_sensorUsb.Good);
        bool isGoodLight = _sensorLight.Good != null;
        bool isGoodPhoto = _sensorPhoto.Good != null;
        bool isGoodUsb = _sensorUsb.Good != null;

        if (isGoodUsb && isGoodPhoto && isGoodLight)
        {
            int price = _sensorLight.Good.Value.Price + _sensorPhoto.Good.Value.Price + Random.Range(0, 200);
            int indexVideo = _sensorPhoto.Good.Value.Level;
            UsbRecord(price, 0, indexVideo);
            VideoController.Instance.VideoPlay(transform.gameObject, 0, indexVideo);
        }
    }

    public void PlayVideo()
    {
        bool isGoodLight = _sensorLight.Good != null;
        bool isGoodVideo = _sensorVideo.Good != null;
        bool isGoodUsb = _sensorUsb.Good != null;

        if (isGoodUsb && isGoodVideo && isGoodLight)
        {
            int price = _sensorLight.Good.Value.Price + _sensorVideo.Good.Value.Price + Random.Range(0, 200);
            int indexVideo = _sensorVideo.Good.Value.Level;
            UsbRecord(price, 1, indexVideo);
            VideoController.Instance.VideoPlay(transform.gameObject, 1, indexVideo);
        }
    }

    private void UsbRecord(int price, int indexType, int indexVideo)
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
        record.Clip = VideoController.Instance.GetVideo(indexType, indexVideo);
        baseRecord.Records.Add(record);
    }



}
