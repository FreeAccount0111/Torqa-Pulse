using System;
using System.Collections.Generic;
using Base.Gameplay;
using DG.Tweening;
using Game.Enemy;
using Game.Event;
using UI;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject.SpaceFighter;

namespace Gameplay
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        [SerializeField] private List<GameObject> levels = new List<GameObject>();
        private LevelCtrl _currentLevelCtrl;
        public int indexCurrentLevel;
        public bool isComplete;
        
        public LevelCtrl CurrentLevel => _currentLevelCtrl;
        private int score;
    
        private void Awake()
        {
            Instance = this;
            PopupCtrl.Instance.GetPopupByType<PopupGameplay>().ShowImmediately(false);
            LoadGame(UserData.GetCurrentLevel());
        }

        private void OnEnable()
        {
            SimpleEnemyController.OnDead += AddScore;
        }

        private void OnDisable()
        {
            SimpleEnemyController.OnDead -= AddScore;
        }

        private void AddScore()
        {
            score += 1;
            PopupCtrl.Instance.GetPopupByType<PopupGameplay>().UpdateEnemy(score);
        }

        public void LoadGame(int indexLevel)
        {
            if (_currentLevelCtrl != null)
                Destroy(_currentLevelCtrl.gameObject);
            
            isComplete = false;
            indexCurrentLevel = indexLevel < levels.Count ? indexLevel : 0;
            _currentLevelCtrl = Instantiate(levels[indexCurrentLevel]).GetComponent<LevelCtrl>();
            score = 0;
            PopupCtrl.Instance.GetPopupByType<PopupGameplay>().UpdateEnemy(score);
            CircleOutline.Instance.ScaleOut(()=> GameEvent.RaiseStartWave());
        }

        public void ResetLevel()
        {
            LoadGame(indexCurrentLevel);
        }

        public void CheckWin()
        {
            /*if(isComplete)
                return;

            isComplete = true;
            UserData.SetLevelLock(indexCurrentLevel + 1, false);
            DOVirtual.DelayedCall(1, () =>
            {
                CircleOutline.Instance.ScaleIn(() =>
                {
                    LoadGame(indexCurrentLevel + 1);
                });
            });*/
            PopupCtrl.Instance.GetPopupByType<PopupWin>().UpdateScore(score);
            PopupCtrl.Instance.GetPopupByType<PopupWin>().ShowImmediately(true);
        }
    }
}
