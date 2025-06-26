using System.Collections.Generic;
using UnityEngine;

namespace Game.Controller
{
    public class TrajectoryController : MonoBehaviour
    {
        [SerializeField] private List<GameObject> dots = new List<GameObject>();
        [SerializeField] private int amountDot;

        public void Initialized()
        {
            for (int i = 0; i < amountDot; i++)
            {
                var newDot = ObjectPool.Instance.Get(ObjectPool.Instance.dot);
                dots.Add(newDot);
            }
        }

        public void UpdatePosition(Vector2 posStart, Vector2 direction, float force)
        {
            for (int i = 0; i < amountDot; i++)
            {
                float t = i * 0.05f;
                dots[i].transform.localScale = Vector3.one * Mathf.Lerp(0.1f, 0.05f, (float)i / amountDot);
                dots[i].transform.position = posStart + direction * force * t + 0.5f * Physics2D.gravity * (t * t);
            }
        }
    }
}
