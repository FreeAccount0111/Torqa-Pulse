using System.Collections.Generic;
using DG.Tweening;
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
        public Transform map;
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
            _currentLevelCtrl = Instantiate(levels[indexCurrentLevel], map).GetComponent<LevelCtrl>();
            PopupCtrl.Instance.GetPopupByType<PopupGameplay>().UpdateLevel(indexCurrentLevel + 1);
            CircleOutline.Instance.ScaleOut();
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
