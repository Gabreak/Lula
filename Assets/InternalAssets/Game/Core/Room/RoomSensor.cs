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

    [Space(20), Header("Video")]
    [SerializeField] private VideoGirlsData _dataVideo;
    [SerializeField] private Transform parentGirls;

    [Space(20), Header("Toys")]
    [SerializeField] private TypeProducts _toys;
    [SerializeField] private LocalizedString _noMoney;

    private void OnEnable()
    {
        _onEnableHandle.Invoke();
    }

    private void Start()
    {
        CreateGirls();
        //int girl = Convert.ToString(_dataVideo.IndexVideo, 2).Count(ch => ch == '1');
        //string idGirl = Convert.ToString(_dataVideo.IndexVideo, 2);
        //bool isShantal = idGirl.Substring(idGirl.Length) == "1";
        //Debug.Log(idGirl.Length - 1);
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

    private void CreateGirls()
    {
        int length = _dataVideo.Girls.Length;

        for (int i = 0; i < length; i++)
        {
            if (_dataVideo.Girls[i].IsActive)
            {

                RoomGirlController girl = Instantiate(_dataVideo.Girls[i].Object, parentGirls);
                girl.IdBinary = _dataVideo.Girls[i].Id;
            }
        }
    }

    public void PlayPhotoVideo()
    {
        bool isGoodLight = _sensorLight.Good != null;
        bool isGoodPhoto = _sensorPhoto.Good != null;
        bool isGoodUsb = _sensorUsb.Good != null;

        if (isGoodUsb && isGoodPhoto && isGoodLight)
        {
            int price = _sensorLight.Good.Value.Price + _sensorPhoto.Good.Value.Price + UnityEngine.Random.Range(-100, 0);
            int indexVideo = _sensorPhoto.Good.Value.Level;

            PlayDefault(0, indexVideo, price);
        }
    }

    public void PlayVideo()
    {
        bool isGoodLight = _sensorLight.Good != null;
        bool isGoodVideo = _sensorVideo.Good != null;
        bool isGoodUsb = _sensorUsb.Good != null;

        //if (isGoodUsb && isGoodVideo && isGoodLight)
        {
            //int price = _sensorLight.Good.Value.Price + _sensorVideo.Good.Value.Price + UnityEngine.Random.Range(-1000, 0);
            int price = 0;//_sensorLight.Good.Value.Price + _sensorVideo.Good.Value.Price + UnityEngine.Random.Range(-1000, 0);
            if (_dataVideo.IndexVideo == 0)
            {
                int indexVideo = _sensorVideo.Good.Value.Level;

                PlayDefault(1, indexVideo, price);
            }
            else
                PlayOtherGirls(price);
        }
    }

    public void PlayDefault(int indexList, int indexVideo, int price)
    {
        VideoClip clip = VideoController.Instance.GetVideo(indexList, indexVideo);
        UsbRecord(price, clip);
        VideoController.Instance.VideoPlay(transform.gameObject, indexList, indexVideo);
    }

    public void PlayOtherGirls(int price)
    {
        string idGirl = Convert.ToString(_dataVideo.IndexVideo, 2);
        bool isShantal = idGirl.Substring(idGirl.Length - 1) == "1";
        int girl = Convert.ToString(_dataVideo.IndexVideo, 2).Count(ch => ch == '1');
        if (isShantal) girl--;


        if (MoneyProperties.Money < girl * 200)
        {
            string text = _noMoney.GetLocalizedString();
            WindowInfoRedirector.Message(text, WindowIcon.Warning, Color.yellow);
            return;
        }
        VideoClip clipNoToy = _dataVideo.Videos[_dataVideo.IndexVideo].Clip[0];
        VideoClip clipToy = _dataVideo.Videos[_dataVideo.IndexVideo].Clip[1];
        string textError = _dataVideo.Videos[_dataVideo.IndexVideo].Warning.GetLocalizedString();
        string textToyError = _dataVideo.Videos[_dataVideo.IndexVideo].WarningToy.GetLocalizedString();
        bool isToy = false;

        foreach (var toy in _toys.Acquired)
        {
            isToy = toy.Id == _dataVideo.Videos[_dataVideo.IndexVideo].IndexToy;
            if (isToy) break;
        }
        Debug.Log(isToy);
        VideoClip clip = (isToy) ? clipToy : clipNoToy;

        bool isClipNoToy = clipNoToy == null;
        bool isClipToy = clipToy == null;

        if (isClipToy)
        {
            clip = clipNoToy;
            if (isClipNoToy)
            {
                WindowInfoRedirector.Message(textError, WindowIcon.Warning, Color.yellow);
                return;
            }
        }

        if (!isToy && isClipNoToy)
        {
            WindowInfoRedirector.Message(textToyError, WindowIcon.Warning, Color.yellow);
            return;
        }

        int sumPrice = price + (girl * 250);
        MoneyProperties.Money -= girl * 200;
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
