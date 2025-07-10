using Gameplay;
using UnityEngine;

namespace Game.Controller
{
    public class LeafController : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer leafRenderer;
        [SerializeField] private Transform original;
        [SerializeField] private bool isEating;
        [SerializeField] private bool hasEat;

        public bool HasEat => hasEat;
        public bool IsEating => isEating;
        public Vector3 Original => original.position;

        public void SetUpLeaf()
        {
            isEating = false;
            leafRenderer.enabled = true;
        }

        public void LeafIsEating()
        {
            isEating = true;
        }

        public void LeafHadEat()
        {
            hasEat = true;
            leafRenderer.enabled = false;
            TreeController.Instance.TreeHasLeaf();
        }
    }
}
