using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace MinGame
{
    [CreateAssetMenu(menuName = "GameData/MinGame", fileName = "Present", order = 260)]
    public class FortuneData : ScriptableObject
    {
        public PresentAnimation[] Prefabs;
        public Vector3 StartPosition;

    }
}
