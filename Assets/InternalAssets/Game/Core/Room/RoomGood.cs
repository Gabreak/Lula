using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class RoomGood : MonoBehaviour
{
    [SerializeField] private RoomGoodRedirector _prefab;
    [SerializeField] private Transform _parent;
    public BaseSensor Sensor;
    public GameGoods? SelectGood { get; set; }

    private ToggleGroup _toggleGroup;


    private void OnEnable()
    {
        SelectGood = null;
        _toggleGroup = GetComponent<ToggleGroup>();
        for (int i = 0; i < Sensor.Type.Acquired.Count; i++)
        {
            int min = int.MaxValue;
            string nameUsbMin = "";
            BaseSensor sensorUsb = GameDataBase.Instance.UsbSensor;
            RoomGoodRedirector redirector = Instantiate(_prefab, transform);
            redirector.ToggleComponent.group = _toggleGroup;
            redirector.Good = Sensor.Type.Acquired[i];

            if (Sensor.Type.Index != sensorUsb.Type.Index)
            {

                var sizeNormal = from g in sensorUsb.Type.Acquired
                                 where g.Level >= redirector.Good.Value.Level
                                 select g;

                foreach (GameGoods good in sizeNormal)
                {
                    if (min > good.Level)
                    {
                        min = good.Level;
                        nameUsbMin = good.Key.GetLocalizedString();
                    }
                }
                nameUsbMin = (nameUsbMin == "") ? "No Usb!" : nameUsbMin;
                redirector.Lable.text = Sensor.Type.Acquired[i].Key.GetLocalizedString() + $": { nameUsbMin}";
            }
            else
                redirector.Lable.text = Sensor.Type.Acquired[i].Key.GetLocalizedString();

            if (Sensor.Good != null)
            {
                if (Sensor.Good.Value.Id == redirector.Good.Value.Id)
                {
                    redirector.ToggleComponent.isOn = true;
                }
            }
        }

    }
    private void OnDisable()
    {
        for (int i = Sensor.Type.Acquired.Count - 1; i >= 0; i--)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }

    public void UpdateSensor()
    {

        RoomSensor roomSensor = transform.root.GetComponent<RoomSensor>();
        BaseSensor typeUsb = GameDataBase.Instance.UsbSensor;

        if (SelectGood == null)
        {
            Destroy(Sensor.CreateGood);
            Sensor.Good = null;
            foreach (var sensor in roomSensor.Sensors)
            {
                if (sensor.Sensor.Type.Index != Sensor.Type.Index) continue;
                sensor.ImageComponent.sprite = (Sensor.Good != null) ? sensor.Sensor.OK : sensor.Sensor.Error;
                return;
            }
        }

        Sensor.Good = SelectGood;

        foreach (var sensor in roomSensor.Sensors)
        {
            if (sensor.Sensor.Type.Index != Sensor.Type.Index) continue;

            if (typeUsb.Type.Index != Sensor.Type.Index)
            {
                CreateObject(Sensor.Good);
                if (typeUsb.Good != null)
                {
                    if (typeUsb.Good.Value.Level < sensor.Sensor.Good.Value.Level)
                    {
                        UsbReset(typeUsb, roomSensor);
                    }
                }
            }
            else
            {
                int max = roomSensor.Sensors.Max(m =>
                (m.Sensor.Good != null &&
                m.Sensor.Type.Index != typeUsb.Type.Index) ?
                m.Sensor.Good.Value.Level :
                0);

                if (typeUsb.Good != null && max > typeUsb.Good.Value.Level)
                {
                    Sensor.Good = null;
                }

            }

            sensor.ImageComponent.sprite = (Sensor.Good != null) ? sensor.Sensor.OK : sensor.Sensor.Error;

        }
    }

    private void UsbReset(BaseSensor usb, RoomSensor sensor)
    {
        usb.Good = null;

        SensorRedirector sensorUsb = (from s in sensor.Sensors
                                      where s.Sensor.Type.Index == usb.Type.Index
                                      select s).First();
        sensorUsb.ImageComponent.sprite = sensorUsb.Sensor.Error;
    }

    private void CreateObject(GameGoods? good)
    {
        if (Sensor.CreateGood != null)
            Destroy(Sensor.CreateGood.gameObject);
        if (good != null)
            Sensor.CreateGood = Instantiate(good.Value.Product, _parent);

    }
}
