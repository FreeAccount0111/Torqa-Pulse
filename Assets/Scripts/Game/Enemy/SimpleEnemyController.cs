using System;
using System.Collections.Generic;
using DG.Tweening;
using Game.Controller;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Serialization;

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
        [SerializeField] private float _rangeAttack;

        private bool _isAttacking;
        public static event Action OnDead;

        [SerializeField] private List<Sprite> icons = new List<Sprite>();
        [SerializeField] private SpriteRenderer enemyRenderer;

        public void Initialized()
        {
            angle = -90;
            var randomDirection = UnityEngine.Random.Range(0, 10);
            direction = randomDirection % 2 == 0 ? DirectionType.Right : DirectionType.Left;
            angularSpeed = direction == DirectionType.Right ? 10 : -10;
            enemyRenderer.sprite = icons[UnityEngine.Random.Range(0, icons.Count)];

            _isAttacking = false;
        }

        void Update()
        {
            if(!CanAttack() && !_isAttacking)
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
            else if(!_isAttacking)
            {
                _isAttacking = true;
                var leaf = TreeController.Instance.GetLeaf();
                if (leaf != null)
                {
                    leaf.LeafIsEating();
                    transform.DOMove(leaf.Original, 1f).SetSpeedBased(true)
                        .OnUpdate(() =>
                        {
                            transform.up = ((Vector2)leaf.Original - (Vector2)transform.position).normalized;
                        }).OnComplete(() =>
                        {
                            leaf.LeafHadEat();
                            ObjectPool.Instance.Return(gameObject, true);
                        });
                }
            }
        }

        private bool CanAttack()
        {
            if (Vector2.Distance((Vector2)transform.position, new Vector2(0, 2)) < _rangeAttack)
                return true;
            else
                return false;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            OnDead?.Invoke();
            
            ObjectPool.Instance.Get(ObjectPool.Instance.explosion).transform.position = transform.position;
            ObjectPool.Instance.Return(gameObject,true);
        }
    }
}
