using System;
using Gameplay;
using UnityEngine;

namespace Game.Controller
{
    public class BackgroundController : MonoBehaviour
    {
        private void Start()
        {
            GetComponent<Canvas>().worldCamera = CameraManager.Instance.GameCamera;
        }
    }
}
