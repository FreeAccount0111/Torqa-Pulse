using UnityEngine;

namespace Base.Gameplay
{
    public class LevelCtrl : MonoBehaviour
    {
        public static LevelCtrl Instance;
        
        private void Awake()
        {
            Instance = this;
            Initialize();
        }

        private void Initialize()
        {
        }
    }
}
