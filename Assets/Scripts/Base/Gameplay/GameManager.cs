using System.Collections.Generic;
using Base.Gameplay;
using DG.Tweening;
using Game.Event;
using UI;
using UnityEngine;
using UnityEngine.UIElements;

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
    
        private void Awake()
        {
            Instance = this;
            PopupCtrl.Instance.GetPopupByType<PopupGameplay>().ShowImmediately(false);
            LoadGame(UserData.GetCurrentLevel());
        }

        public void LoadGame(int indexLevel)
        {
            if (_currentLevelCtrl != null)
                Destroy(_currentLevelCtrl.gameObject);
            
            isComplete = false;
            indexCurrentLevel = indexLevel < levels.Count ? indexLevel : 0;
            _currentLevelCtrl = Instantiate(levels[indexCurrentLevel]).GetComponent<LevelCtrl>();
            PopupCtrl.Instance.GetPopupByType<PopupGameplay>().UpdateEnemy(indexCurrentLevel + 1);
            CircleOutline.Instance.ScaleOut(()=> GameEvent.RaiseStartWave());
        }

        public void ResetLevel()
        {
            LoadGame(indexCurrentLevel);
        }

        public void CheckWin()
        {
            if(isComplete)
                return;

            isComplete = true;
            UserData.SetLevelLock(indexCurrentLevel + 1, false);
            DOVirtual.DelayedCall(1, () =>
            {
                CircleOutline.Instance.ScaleIn(() =>
                {
                    LoadGame(indexCurrentLevel + 1);
                });
            });
        }
    }
}
