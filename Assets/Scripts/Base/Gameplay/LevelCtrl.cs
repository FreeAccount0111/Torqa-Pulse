using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UI;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Gameplay
{
    public class LevelCtrl : MonoBehaviour
    {
        public static LevelCtrl Instance;
        
        private void Awake()
        {
            Instance = this;
            Initialize();
        }

        private void Initialize()
        {
        }
    }
}
