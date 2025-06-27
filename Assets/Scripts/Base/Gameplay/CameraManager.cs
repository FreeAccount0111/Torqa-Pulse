using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class CameraManager : MonoBehaviour
    {
        public static CameraManager Instance;
        [SerializeField] private Camera uiCamera;
        [SerializeField] private Camera gameCamera;
        
        public Camera UiCamera => uiCamera;
        public Camera GameCamera => gameCamera;

        private void Awake()
        {
            if (CameraManager.Instance == null)
            {
                CameraManager.Instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }
}
