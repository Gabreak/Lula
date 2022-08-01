using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;

public class SensorRedirector : LocalizedMonoBehaviour
{
    public Image ImageComponent;
    public ObjectInformation Info;
    public BaseSensor Sensor;
}
