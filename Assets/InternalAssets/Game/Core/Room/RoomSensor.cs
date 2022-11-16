using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Localization;
//using TMPro;
using UnityEngine.Video;
using UnityEngine.UI;

public class RoomSensor : MonoBehaviour
{
    [SerializeField] private Transform _parent;

    public List<SensorRedirector> Sensors; /*{ get; set; }*/
    [SerializeField] private BaseSensor _sensorLight;
    [SerializeField] private BaseSensor _sensorPhoto;
    [SerializeField] private BaseSensor _sensorVideo;
    [SerializeField] private BaseSensor _sensorUsb;
    //[SerializeField] private UnityEvent _onEnableHandle;



    [Space(20), Header("Usb")]
    [SerializeField] private Text _usbName;

    [Space(20), Header("Messages")]
    [SerializeField] private LocalizedString _usbErrorText;
    [SerializeField] private LocalizedString _usbErrorNo;
    [SerializeField] private LocalizedString _lightErrorText;
    [SerializeField] private LocalizedString _photoCameraErrorText;
    [SerializeField] private LocalizedString _videoCameraErrorText;
    [SerializeField] private LocalizedString _cameraError;

    [Space(20), Header("Price")]
    [SerializeField] private int _priceGirl = 200;
    [SerializeField] private int _incomeGirl = 350;

    [SerializeField] private TaskClick _task;

    [SerializeField] private Slider _sliderChoice;
    private int _sliderValue = 0;
    private Coroutine _sliderCoroutine;





    private void OnEnable()
    {
        // _onEnableHandle.Invoke();
    }

    private void Start()
    {
        //BaseSensor[] sensors = GameDataBase.Instance.Sensor;

        //for (int i = 0; i < sensors.Length; i++)
        //{
        //    GameGoods? good = sensors[i].Good;
        //    bool isGood = good == null;
        //    if (isGood) continue;

        //    bool isIdGood = good.Value.Id == -1;
        //    if (isIdGood) continue;

        //    bool isUsb = sensors[i].Type.Index == GameDataBase.Instance.UsbSensor.Type.Index;
        //    if (isUsb) continue;

        //    GameObject createGood = Instantiate(good.Value.Product, _parent);
        //    sensors[i].CreateGood = createGood;
        //}
    }

    public void StartVideo()
    {
        if (AutoInstall())
            PlayVideo();

    }

    public void StartPhoto()
    {
        if (AutoInstall())
            PlayPhotoVideo();
    }

    public bool AutoInstall()
    {
        return AutoLightInstall() && AutoUSBInstall();
    }


    public void Changed(float single)
    {
        int valueRound = (Mathf.RoundToInt(single));
        if (_sliderValue != valueRound)
        {
            _sliderValue = valueRound;
            if (_sliderCoroutine == null)
                _sliderCoroutine = StartCoroutine(SliderNormalize());
        }

    }

    private IEnumerator SliderNormalize()
    {

        while (Mathf.Abs(_sliderChoice.value - _sliderValue) > 0.001f)
        {
            _sliderChoice.value = Mathf.MoveTowards(_sliderChoice.value, _sliderValue, Time.deltaTime * 4f);
            yield return null;
        }
        _sliderCoroutine = null;
        yield return null;
    }

    private bool AutoLightInstall()
    {
        if (_sensorLight.Type.Acquired.Count > 0)
        {

            int price = _sensorLight.Type.Acquired.Max(a => a.Price);

            _sensorLight.Good = (from g in _sensorLight.Type.Acquired
                                 where g.Price == price
                                 select g).First();
            return true;
        }
        WindowMessage.Message(_lightErrorText.GetLocalizedString(), WindowIcon.Warning, Color.yellow);
        return false;

    }

    private bool AutoUSBInstall()
    {
        GameGoods? good;
        if (_sliderValue == 0)
        {
            if (_sensorPhoto.Type.Acquired.Count == 0)
            {
                WindowMessage.Message(_photoCameraErrorText.GetLocalizedString(), WindowIcon.Warning, Color.yellow);
                return false;
            }

            good = _sensorPhoto.Good;
        }
        else
        {
            if (_sensorVideo.Type.Acquired.Count == 0)
            {
                WindowMessage.Message(_videoCameraErrorText.GetLocalizedString(), WindowIcon.Warning, Color.yellow);
                return false;
            }
            good = _sensorVideo.Good;
        }
        Debug.Log(good);
        if (good != null)
        {
            if (_sensorUsb.Type.Acquired.Count == 0)
            {
                WindowMessage.Message(_usbErrorNo.GetLocalizedString(), WindowIcon.Warning, Color.yellow);
                //Debug.LogError("Купите Usb в Video Shop!");
                return false;
            }


            var usbGoods = from u in _sensorUsb.Type.Acquired
                           where u.Level >= good.Value.Level
                           select u;

            if (usbGoods.Count() == 0)
            {
                WindowMessage.Message(_usbErrorText.GetLocalizedString(), WindowIcon.Warning, Color.yellow);
                //Debug.LogError("Недостаточно место в USb");
                return false;
            }
            int min = usbGoods.Min(u => u.Level);
            foreach (var usbGood in usbGoods)
            {
                if (usbGood.Level == min)
                {
                    _sensorUsb.Good = usbGood;
                    return true;
                }
            }
        }
        WindowMessage.Message(_cameraError.GetLocalizedString(), WindowIcon.Warning, Color.yellow);
        //Debug.LogError("Камера не выбрана!");
        return false;
    }

    private void PlayPhotoVideo()
    {
        bool isGoodLight = _sensorLight.Good != null;
        bool isGoodPhoto = _sensorPhoto.Good != null;
        bool isGoodUsb = _sensorUsb.Good != null;

        if (LightMessage(isGoodLight)) return;
        if (PhotoCameraMessage(isGoodPhoto)) return;
        if (UsbMessage(isGoodUsb)) return;

        int price = _sensorLight.Good.Value.Price + _sensorPhoto.Good.Value.Price + UnityEngine.Random.Range(0, 050);
        int indexVideo = _sensorPhoto.Good.Value.Level;
        _task?.Action(0);
        PlayDefault(0, indexVideo, price);
    }

    private void PlayVideo()
    {
        bool isGoodLight = _sensorLight.Good != null;
        bool isGoodVideo = _sensorVideo.Good != null;
        bool isGoodUsb = _sensorUsb.Good != null;
        if (LightMessage(isGoodLight)) return;
        if (VideoCameraMessage(isGoodVideo)) return;
        if (UsbMessage(isGoodUsb)) return;

        int price = _sensorLight.Good.Value.Price + _sensorVideo.Good.Value.Price + UnityEngine.Random.Range(0, 100);
        if (RoomGirls.GetIndexVideo() == 0)
        {
            int indexVideo = _sensorVideo.Good.Value.Level;

            PlayDefault(1, indexVideo, price);
        }
        else
            PlayOtherGirls(price);
    }

    public void PlayDefault(int indexList, int indexVideo, int price)
    {
        if (PoliceManager.Instance.LoadJail(transform.gameObject, 5))
        {
            VideoClip clip = VideoController.Instance.GetVideo(indexList, indexVideo);
            UsbRecord(price, clip);
            VideoController.Instance.VideoPlay(transform.gameObject, indexList, indexVideo);
        }
    }


    private void PlayOtherGirls(int price)
    {
        int girl = RoomGirls.GetCount(RoomGirls.GetIndexVideo());
        bool isShantal = RoomGirls.FindShantal(RoomGirls.GetBinary(RoomGirls.GetIndexVideo()));

        if (isShantal) girl--;

        if (MoneyProperties.NoMoneyMessage(girl * _priceGirl)) return;

        VideoClip clip = RoomGirls.GetVideo(RoomGirls.IsToy());
        if (clip == null) return;

        int sumPrice = price + (girl * _incomeGirl);

        MoneyProperties.Money -= girl * _priceGirl;

        UsbRecord(sumPrice, clip);
        if (PoliceManager.Instance.LoadJail(transform.gameObject, 5))
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
            WindowMessage.Message(_videoCameraErrorText.GetLocalizedString(), WindowIcon.Warning, Color.yellow);
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
