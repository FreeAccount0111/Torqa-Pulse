using System;
using System.Collections.Generic;
using Gameplay;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class PopupGameplay : BasePopup
    {
        [SerializeField] private Button btnHome, btnReplay;
        [SerializeField] private Text txtLevel, txtMoves;

        private void Awake()
        {
            btnHome.onClick.AddListener(() =>
            {
                CircleOutline.Instance.ScaleIn(() =>
                {
                    PopupCtrl.Instance.HideAllPopup();
                    PopupCtrl.Instance.GetPopupByType<PopupHome>().ShowImmediately(true);
                    CircleOutline.Instance.ScaleOut();
                });
            });
            
            btnHome.onClick.AddListener(() =>
            {
                GameManager.Instance.ResetLevel();
            });
        }

        public void UpdateLevel(int level)
        {
            txtLevel.text = level.ToString();
        }

        public void UpdateMoves(int current, int max)
        {
            txtMoves.text = $"Moves:{current}/{max}";
        }
    }
}
