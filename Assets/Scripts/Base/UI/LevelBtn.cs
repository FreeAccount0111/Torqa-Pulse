using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class LevelBtn : MonoBehaviour
    {
        [SerializeField] private Button btn;
        [SerializeField] private GameObject lockObj;
    

        public void AddListener(Action action)
        {
            btn.onClick.AddListener(() =>
            {
                action?.Invoke();
            });
        }

        public void SetLevelBtn(int index)
        {
            lockObj.SetActive(UserData.GetLevelLock((index)));;
        }
    }
}
