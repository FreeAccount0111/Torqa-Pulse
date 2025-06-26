using System.Collections.Generic;
using UnityEngine;

namespace Game.Controller
{
    public class TreeRootController : MonoBehaviour
    {
        [SerializeField] private LineRenderer line;
        public Vector2 OriginalPosition => line.GetPosition(1);

        public void UpdateLine(Vector2 point)
        {
            line.SetPosition(2, point);
        }
    }
}
