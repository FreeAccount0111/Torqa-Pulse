using System;
using System.Collections.Generic;
using Gameplay;
using UnityEngine;

namespace Game.Controller
{
    public class TreeController : MonoBehaviour
    {
        public static TreeController Instance;
        [SerializeField] private List<LeafController> leafs = new List<LeafController>();
        private void Awake()
        {
            Instance = this;
        }

        public LeafController GetLeaf()
        {
            foreach (var leaf in leafs)
            {
                if (!leaf.IsEating)
                    return leaf;
            }

            return null;
        }

        public void TreeHasLeaf()
        {
            foreach (var leaf in leafs)
            {
                if(!leaf.HasEat)
                    return;
            }
            
            GameManager.Instance.CheckWin();
        }
    }
}
