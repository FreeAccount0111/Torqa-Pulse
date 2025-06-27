using System;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Game.Enemy
{
    public enum DirectionType
    {
        None = 0,
        Left = 1,
        Right = 2
    }
    
    public class SimpleEnemyController : BaseEnemy
    {
        public DirectionType direction;
        public float radius = 2f;
        public float angularSpeed = 10f; 

        [SerializeField] private float angle = 0f;

        public static event Action OnDead;

        public void Initialized()
        {
            angle = -90;
            var randomDirection = UnityEngine.Random.Range(0, 10);
            direction = randomDirection % 2 == 0 ? DirectionType.Right : DirectionType.Left;
            angularSpeed = direction == DirectionType.Right ? 10 : -10;
        }

        void Update()
        {
            angle += angularSpeed * Time.deltaTime;

            float x = radius * Mathf.Cos(angle * Mathf.Deg2Rad);
            float y = radius * Mathf.Sin(angle * Mathf.Deg2Rad);

            transform.position = new Vector3(x, y, transform.position.z);
            
            if (direction == DirectionType.Right)
                transform.right = transform.position.normalized;
            else
                transform.right = -transform.position.normalized;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            OnDead?.Invoke();
            
            ObjectPool.Instance.Get(ObjectPool.Instance.explosion).transform.position = transform.position;
            ObjectPool.Instance.Return(gameObject,true);
        }
    }
}
