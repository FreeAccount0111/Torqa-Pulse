using System;
using DG.Tweening;
using Gameplay;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Game.Controller
{
    public class PeenController : MonoBehaviour,IHandle
    {
        [SerializeField] private TreeRootBranch treeRootBranch;
        [SerializeField] private Vector3 originalPosition;
        
        [Range(2f, 10f)] 
        [SerializeField] private float forceThrow;
        [SerializeField] private Vector2 direction;
        [SerializeField] private Rigidbody2D rb;
        private const float MAX_DISTANCE_DRAG = .75f;

        private bool _showTrailTrajectory;
        private float _amountTime;
        private const float TIME_SHOW_TRAIL = 0.05f;
        [SerializeField] private bool showPreviewTrajectory;

        public static event Action<PeenController> OnShootPeen;

        public void SetTreeRoot(TreeRootBranch treeRoot)
        {
            rb.gravityScale = 0;
            treeRootBranch = treeRoot;
            originalPosition = treeRootBranch.OriginalPosition;
            transform.position = treeRootBranch.spawnPosition;
        }

        public void Instantiate()
        {
            transform.localScale = Vector3.zero;
            GetComponent<Collider2D>().enabled = false;
            transform.DOScale(Vector3.one, 0.75f).SetEase(Ease.OutBounce).OnComplete(() =>
            {
                GetComponent<Collider2D>().enabled = true;
            });
        }
        
        public void OnPointDownHandle()
        {
            if(showPreviewTrajectory)
                TrajectoryController.Instance.Initialized();
        }

        public void OnPointDragHandle()
        {
            var point = CameraManager.Instance.GameCamera.ScreenToWorldPoint(Input.mousePosition);
            treeRootBranch.UpdateLine(point);
            
            Vector3 newPos = new Vector3(point.x, point.y, 0);
            if (Vector2.Distance(newPos, originalPosition) > MAX_DISTANCE_DRAG)
                newPos = originalPosition + MAX_DISTANCE_DRAG * (newPos - originalPosition).normalized;

            treeRootBranch.UpdateLine(newPos);
            transform.position = newPos;
            direction = (originalPosition - newPos).normalized;

                forceThrow = Mathf.Lerp(2f, 10f, Vector2.Distance(newPos, originalPosition) / MAX_DISTANCE_DRAG);
                
                if(showPreviewTrajectory)
                    TrajectoryController.Instance.UpdatePreview(transform.position, direction, forceThrow);
        }

        public void OnPointUpHandle()
        {
            Shoot();
        }

        private void Shoot()
        {
            TrajectoryController.Instance.ClearTrail();
            TrajectoryController.Instance.ClearPreview();
            
            rb.gravityScale = 1;
            rb.linearVelocity = (direction * forceThrow);
            
            treeRootBranch.SetDefault();
            treeRootBranch = null;
            
            
            _showTrailTrajectory = true;
            OnShootPeen?.Invoke(this);
        }

        private void FixedUpdate()
        {
            _amountTime -= Time.fixedDeltaTime;
            if (_showTrailTrajectory && _amountTime<0)
            {
                _amountTime = TIME_SHOW_TRAIL;
                TrajectoryController.Instance.UpdateTrail(transform.position);
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            _showTrailTrajectory = false;
        }
    }
}
