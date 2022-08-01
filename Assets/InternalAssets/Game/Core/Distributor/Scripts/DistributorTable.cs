using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using TMPro;

public class DistributorTable : MonoBehaviour
{
    [SerializeField] private BaseRecord _video;
    [SerializeField] private VideoRedirector _redirecotorVideo;
    [SerializeField] private TextMeshProUGUI _priceSumText;
    private int _priceSum = 0;
    private void OnEnable()
    {
        _priceSum = 0;
        for (int i = 0; i < _video.Records.Count; i++)
        {
            VideoRedirector redirector = Instantiate(_redirecotorVideo, transform);
            redirector.Name.text = $"{i + 1}: {_video.Records[i].Name} {_video.Records[i].Price}$";
            _priceSum += _video.Records[i].Price;
        }
        _priceSumText.text = _priceSum + "$";
    }

    private void OnDisable()
    {
        for (int i = _video.Records.Count - 1; i >= 0; i--)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }

    public void SellClips()
    {

        MoneyProperties.Money += _priceSum;
        _priceSum = 0;
        StartCoroutine(Delay());
    }

    private IEnumerator Delay()
    {
        int count = _video.Records.Count;
        _video.Records.Clear();
        for (int i = count - 1; i >= 0; i--)
        {
            Destroy(transform.GetChild(i).gameObject);
            yield return new WaitForSeconds(0.06f);
        }
    }
}
