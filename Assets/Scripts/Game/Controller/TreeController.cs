using System.Collections.Generic;
using UnityEngine;

namespace Game.Controller
{
    public class TreeController : MonoBehaviour
    {
        [SerializeField] private List<LeafController> leafs = new List<LeafController>();
    }
}
