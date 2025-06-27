using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Controller
{
    public class TrajectoryController : MonoBehaviour
    {
        public static TrajectoryController Instance;
        [SerializeField] private List<GameObject> previewDots = new List<GameObject>();
        [SerializeField] private List<GameObject> trailDots = new List<GameObject>();
        [SerializeField] private int amountDot;

        private void Awake()
        {
            TrajectoryController.Instance = this;
        }

        public void Initialized()
        {
            for (int i = 0; i < amountDot; i++)
            {
                var newDot = ObjectPool.Instance.Get(ObjectPool.Instance.dot);
                previewDots.Add(newDot);
            }
        }

        public void ClearPreview()
        {
            if (previewDots.Count > 0)
            {
                foreach (var dot in previewDots)
                    ObjectPool.Instance.Return(dot,true);
                previewDots.Clear();
            }
        }

        public void ClearTrail()
        {
            if (trailDots.Count > 0)
            {
                foreach (var dot in trailDots)
                    ObjectPool.Instance.Return(dot,true);
                trailDots.Clear();
            }
        }

        public void UpdateTrail(Vector2 pos)
        {
            var newDot = ObjectPool.Instance.Get(ObjectPool.Instance.dot);
            newDot.transform.localScale = 0.05f * Vector3.one;
            newDot.transform.position = pos;
            trailDots.Add(newDot);
        }

        public void UpdatePreview(Vector2 posStart, Vector2 direction, float force)
        {
            for (int i = 0; i < amountDot; i++)
            {
                float t = i * 0.05f;
                previewDots[i].transform.localScale = Vector3.one * Mathf.Lerp(0.075f, 0.05f, (float)i / amountDot);
                previewDots[i].transform.position = posStart + direction * force * t + 0.5f * Physics2D.gravity * (t * t);
            }
        }
    }
}
