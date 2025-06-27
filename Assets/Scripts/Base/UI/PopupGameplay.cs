using System;
using System.Collections.Generic;
using Gameplay;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI
{
    public class PopupGameplay : BasePopup
    {
        [SerializeField] private Button btnHome, btnReplay;
        [SerializeField] private Text txtEnemy;

        private void Awake()
        {
            btnHome.onClick.AddListener(() =>
            {
                CircleOutline.Instance.ScaleIn(() =>
                {
                    /*PopupCtrl.Instance.HideAllPopup();
                    PopupCtrl.Instance.GetPopupByType<PopupHome>().ShowImmediately(true);
                    SceneManager.LoadSceneAsync($"MainMenu");
                    CircleOutline.Instance.ScaleOut();*/
                });
            }); 
            
            btnHome.onClick.AddListener(() =>
            {
                GameManager.Instance.ResetLevel();
            });
        }

        public void UpdateEnemy(int amount)
        {
            txtEnemy.text = $"Enemies left : {amount}";
        }
    }
}
