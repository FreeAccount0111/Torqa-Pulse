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

        [SerializeField] private Transform posStart;
        [SerializeField] private Transform posEnd;
        [SerializeField] private Transform target;
        [SerializeField] private Vector3 distance;

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

        public void SetDefault(Transform target)
        {
            gameCamera.transform.position = posStart.position;
            this.target = target;
            distance = gameCamera.transform.position - target.position;
        }

        public void MoveOut()
        {
            StartCoroutine(MoveOutCoroutine());
        }

        IEnumerator MoveOutCoroutine()
        {
            float amount = 0;
            while (amount < 4f * 0.35f)
            {
                amount += Time.deltaTime;
                gameCamera.transform.position = target.transform.position + distance;
                yield return null;
            }
        }
    }
}
