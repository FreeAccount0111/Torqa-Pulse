using Gameplay;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class PopupWin : BasePopup
    {
        [SerializeField] private Button btnReplay, btnHome, btnNext;
        [SerializeField] private Text txtScore;

        private void Awake()
        {
            btnReplay.onClick.AddListener(() =>
            {
                CircleOutline.Instance.ScaleIn(() =>
                {
                    HideImmediately(true);
                    GameManager.Instance.LoadGame(GameManager.Instance.indexCurrentLevel);
                    CircleOutline.Instance.ScaleOut();
                });
            });

            btnHome.onClick.AddListener(() =>
            {
                CircleOutline.Instance.ScaleIn(() =>
                {
                    HideImmediately(true);
                    PopupCtrl.Instance.GetPopupByType<PopupHome>().ShowImmediately(false);
                    SceneManager.LoadSceneAsync("MainMenu");
                    CircleOutline.Instance.ScaleOut();
                });
            });
            
            btnNext.onClick.AddListener(() =>
            {
                CircleOutline.Instance.ScaleIn(() =>
                {
                    HideImmediately(true);
                    GameManager.Instance.LoadGame(GameManager.Instance.indexCurrentLevel + 1);
                    CircleOutline.Instance.ScaleOut();
                });
            });
        }

        public void UpdateScore(int score)
        {
            txtScore.text = $"Score : {score}";
        }
    }
}
