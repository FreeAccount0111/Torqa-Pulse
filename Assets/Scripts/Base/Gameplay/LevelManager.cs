using System;
using System.Collections.Generic;
using Base.Gameplay;
using UnityEngine;

namespace Gameplay
{
    public class LevelManager : MonoBehaviour
    {
        public static LevelManager Instance;
        [SerializeField] private List<GameObject> prefabsLevel = new List<GameObject>();
        public LevelCtrl currentLevel;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            LoadLevel(2);
        }

        private void LoadLevel(int indexLevel)
        {
            if(currentLevel != null)
                Destroy(currentLevel.gameObject);

            currentLevel = Instantiate(prefabsLevel[indexLevel]).GetComponent<LevelCtrl>();
        }
    }
}
