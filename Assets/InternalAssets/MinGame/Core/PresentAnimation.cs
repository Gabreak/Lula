using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PresentAnimation : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _prefab;
    
    private void Start()
    {
        
    }

    public virtual void Play()
    {
        TextMesh text;
        Rigidbody2D prefab = Instantiate(_prefab, transform);
        text = prefab.GetComponent<TextMesh>();
        prefab.bodyType = RigidbodyType2D.Dynamic;
        prefab.AddForce(Vector2.up * 500f);
        StartCoroutine(TextUpdate(text));
    }

    private IEnumerator TextUpdate(TextMesh text)
    {
        while (text.fontSize < 80)
        {
            text.fontSize += 1;
            yield return new WaitForSeconds(0.01f);
        }
    }
}
