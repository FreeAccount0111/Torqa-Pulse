using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

namespace UI
{
    public class PopupLevel : BasePopup
    {
        [SerializeField] private List<LevelBtn> levelBtns = new List<LevelBtn>();
        [SerializeField] private Button btnHome;

        private void Awake()
        {
            for(int i = 0; i < levelBtns.Count; i++)
            {
                int index = i;
                levelBtns[index].AddListener(() =>
                {
                    if (!UserData.GetLevelLock(index))
                    {
                        UserData.SetCurrentLevel(index);
                        CircleOutline.Instance.ScaleIn(() =>
                        {
                            HideImmediately(true);
                            SceneManager.LoadSceneAsync($"Gameplay");
                        });
                    }
                });
            }

            btnHome.onClick.AddListener(() =>
            {
                HideImmediately(true);
                PopupCtrl.Instance.GetPopupByType<PopupHome>().ShowImmediately(false);
            });
        }

        public override void ShowImmediately(bool showImmediately, Action actionComplete = null)
        {
            base.ShowImmediately(showImmediately, actionComplete);
            UpdateLockLevel();
        }

        private void UpdateLockLevel()
        {
            for (int i = 0; i < levelBtns.Count; i++)
            {
                levelBtns[i].SetLevelBtn(i);
            }
        }
    }
}
