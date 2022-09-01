using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Video;

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

    [Space(20), Header("Messages")]
    [SerializeField] private LocalizedString _usbErrorText;
    [SerializeField] private LocalizedString _lightErrorText;
    [SerializeField] private LocalizedString _photoCameraErrorText;
    [SerializeField] private LocalizedString _videoCameraErrorText;


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

        if (LightMessage(isGoodLight)) return;
        if (PhotoCameraMessage(isGoodPhoto)) return;
        if (UsbMessage(isGoodUsb)) return;

        int price = _sensorLight.Good.Value.Price + _sensorPhoto.Good.Value.Price + UnityEngine.Random.Range(-100, 0);
        int indexVideo = _sensorPhoto.Good.Value.Level;

        PlayDefault(0, indexVideo, price);
    }

    public void PlayVideo()
    {
        bool isGoodLight = _sensorLight.Good != null;
        bool isGoodVideo = _sensorVideo.Good != null;
        bool isGoodUsb = _sensorUsb.Good != null;
        if (LightMessage(isGoodLight)) return;
        if (VideoCameraMessage(isGoodVideo)) return;
        if (UsbMessage(isGoodUsb)) return;

        int price = _sensorLight.Good.Value.Price + _sensorVideo.Good.Value.Price + UnityEngine.Random.Range(-1000, 0);
        if (RoomGirls.VideoIndex == 0)
        {
            int indexVideo = _sensorVideo.Good.Value.Level;

            PlayDefault(1, indexVideo, price);
        }
        else
            PlayOtherGirls(price);
    }

    public void PlayDefault(int indexList, int indexVideo, int price)
    {
        VideoClip clip = VideoController.Instance.GetVideo(indexList, indexVideo);
        UsbRecord(price, clip);
        VideoController.Instance.VideoPlay(transform.gameObject, indexList, indexVideo);
    }

    private void PlayOtherGirls(int price)
    {
        int girl = RoomGirls.GetCount(RoomGirls.VideoIndex);
        bool isShantal = RoomGirls.FindShantal(RoomGirls.GetBinary(RoomGirls.VideoIndex));

        if (isShantal) girl--;

        if (MoneyProperties.NoMoneyMessage(girl * 200)) return;

        VideoClip clip = RoomGirls.GetVideo(RoomGirls.IsToy());
        if (clip == null) return;

        int sumPrice = price + (girl * 250);

        MoneyProperties.Money -= girl * 200;

        UsbRecord(sumPrice, clip);

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

    private bool UsbMessage(bool isUsb)
    {
        if (!isUsb)
        {
            WindowMessage.Message(_usbErrorText.GetLocalizedString(), WindowIcon.Warning, Color.yellow);
            return true;
        }
        return false;
    }

    private bool VideoCameraMessage(bool isCamera)
    {
        if (!isCamera)
        {
            WindowMessage.Message(_photoCameraErrorText.GetLocalizedString(), WindowIcon.Warning, Color.yellow);
            return true;
        }
        return false;
    }

    private bool PhotoCameraMessage(bool isPhoto)
    {
        if (!isPhoto)
        {
            WindowMessage.Message(_photoCameraErrorText.GetLocalizedString(), WindowIcon.Warning, Color.yellow);
            return true;
        }
        return false;
    }

    private bool LightMessage(bool isLight)
    {
        if (!isLight)
        {
            WindowMessage.Message(_lightErrorText.GetLocalizedString(), WindowIcon.Warning, Color.yellow);
            return true;
        }
        return false;
    }
}
