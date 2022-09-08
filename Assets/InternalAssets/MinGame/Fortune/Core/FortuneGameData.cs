using System.Collections;
using System.Collections.Generic;

using UnityEngine;


namespace MinGame
{

    public class FortuneGameData : MonoBehaviour
    {

        public FortuneData PresentData;
        public PresentAnimation[] Presents { get; set; }

        public float Diff { get; set; }

        public static FortuneGameData Instane;

        private void Awake()
        {
            Instane = this;
            Presents = new PresentAnimation[PresentData.Prefabs.Length];
        }

    }
}
