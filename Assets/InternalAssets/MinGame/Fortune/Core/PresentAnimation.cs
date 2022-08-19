using System;
using System.Collections;
using System.Collections.Generic;

using Unity.VisualScripting;

using UnityEngine;
using UnityEngine.Events;

public class PresentAnimation : MonoBehaviour
{

    [SerializeField] private TypePresent _type;
    [SerializeField] private int _money;
    [SerializeField] private Rigidbody2D _prefab;
    [SerializeField] private GoodPresent _good;

    private TextMesh _textMesh;

    private UnityAction Event;


    private void OnEnable()
    {
        switch (_type)
        {
            case TypePresent.Good:
                Event += PlayGood;
                break;
            case TypePresent.Money:
                Event += PlayMoney;
                _textMesh = _prefab.GetComponent<TextMesh>();
                _textMesh.text = _money + "$";
                break;
        }
    }

    private void OnDisable()
    {
        Event -= PlayMoney;
    }


    public void Play()
    {

        Event.Invoke();

    }

    private void PlayMoney()
    {

        Rigidbody2D prefab = Instantiate(_prefab, transform);
        prefab.bodyType = RigidbodyType2D.Dynamic;
        prefab.AddForce(Vector2.up * 500f);
        MoneyProperties.Money += _money;
        StartCoroutine(TextUpdate(prefab.gameObject));
    }

    private IEnumerator TextUpdate(GameObject prefab)
    {
        TextMesh text;
        text = prefab.GetComponent<TextMesh>();

        while (text.fontSize < 80)
        {
            text.fontSize += 1;
            yield return new WaitForSeconds(0.01f);
        }
        Destroy(prefab.gameObject, 2);
    }

    private void PlayGood()
    {

        Rigidbody2D prefab = Instantiate(_prefab, transform);
        prefab.bodyType = RigidbodyType2D.Dynamic;
        prefab.AddForce(Vector2.up * 500f);
        _good.Good.Type.Acquired.Add(_good.Good.Goods[_good.IndexGood]);
        StartCoroutine(GoodUpdate(prefab.transform));
    }

    private IEnumerator GoodUpdate(Transform tran)
    {
        float xyz = 0.01f;
        while (tran.localScale.x < 8)
        {
            tran.localScale += new Vector3(xyz, xyz, xyz);
            yield return new WaitForSeconds(0.01f);
        }
        Destroy(tran.gameObject, 2);
    }
    private IEnumerator DestroyPrefab(GameObject gm)
    {
        Destroy(gm, 1);
        yield return null;
    }
}

public enum TypePresent { Money, Good }

[Serializable]
public struct GoodPresent
{
    public bool IsAcitve;
    public BaseProduct Good;
    public int IndexGood;

}