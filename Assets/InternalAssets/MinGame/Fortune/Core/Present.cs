using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace MinGame
{
    public class Present : MonoBehaviour
    {
        private FortuneGameData _fortune;

        private void Start()
        {
            StartCoroutine(Delay());
        }

        private void GeneratorPresent()
        {
            PresentAnimation[] prefabs = _fortune.PresentData.Prefabs;
            int length = prefabs.Length;
            _fortune.Diff = 360f / (float)length;
            for (int i = 0; i < length; i++)
            {

                _fortune.Presents[i] = Instantiate(prefabs[i]);
                _fortune.Presents[i].transform.localPosition = _fortune.PresentData.StartPosition;
                transform.eulerAngles = new Vector3(0f, 0f, (360f / (float)length) * (i + 1f));
                _fortune.Presents[i].transform.parent = transform;
            }
            transform.eulerAngles = Vector3.zero;
        }


        private IEnumerator Delay()
        {
            yield return new WaitForSeconds(0.1f);
            _fortune = FortuneGameData.Instane;
            GeneratorPresent();
        }
    }
}
