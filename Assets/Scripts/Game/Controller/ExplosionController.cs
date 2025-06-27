using UnityEngine;

namespace Game.Controller
{
    public class ExplosionController : MonoBehaviour
    {
        private void ReturnPool()
        {
            ObjectPool.Instance.Return(gameObject, true);
        }
    }
}
