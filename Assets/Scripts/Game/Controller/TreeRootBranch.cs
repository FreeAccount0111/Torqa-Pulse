using System.Collections.Generic;
using UnityEngine;

namespace Game.Controller
{
    public class TreeRootBranch : MonoBehaviour
    {
        [SerializeField] private LineRenderer line;
        public Vector2 OriginalPosition => line.GetPosition(1);
        public Vector2 spawnPosition;

        public void UpdateLine(Vector2 point)
        {
            line.SetPosition(2, point);
        }

        public void SetDefault()
        {
            line.SetPosition(2,spawnPosition);
        }
    }
}
