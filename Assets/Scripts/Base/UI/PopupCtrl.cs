using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace UI
{
    public class PopupCtrl : MonoBehaviour
    {
        public static PopupCtrl Instance;

        [FormerlySerializedAs("_popups")] [SerializeField] private List<BasePopup> popups=new List<BasePopup>();

        private void Awake()
        {
            if (PopupCtrl.Instance == null)
            {
                PopupCtrl.Instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

        public void HideAllPopup()
        {
            foreach (var popup in popups)
            {
                popup.HideImmediately(true);
            }
        }

        public T GetPopupByType<T>() where T : BasePopup
        {
            foreach (var popup in popups)
            {
                if(popup is T basePopup)
                    return basePopup;
            }

            return null;
        }
    }
}
