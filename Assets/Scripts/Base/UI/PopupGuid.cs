using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PopupGuid : BasePopup
    {
        [SerializeField] private Button btnExit;

        private void Awake()
        {
            btnExit.onClick.AddListener(() =>
            {
                PopupCtrl.Instance.GetPopupByType<PopupHome>().ShowImmediately(true);
                HideImmediately(true);
            });
        }
    }
}
