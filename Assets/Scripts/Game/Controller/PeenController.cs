using UnityEngine;

namespace Game.Controller
{
    public class PeenController : MonoBehaviour,IHandle
    {
        [SerializeField] private TrajectoryController trajectoryController;
        [SerializeField] private TreeRootController treeRootController;
        [SerializeField] private Vector3 originalPosition;
        
        [Range(2f, 15f)] 
        [SerializeField] private float forceThrow;
        [SerializeField] private Vector2 direction;
        [SerializeField] private Rigidbody2D rb;
        private const float MAX_DISTANCE_DRAG = .75f;

        public void SetTreeRoot(TreeRootController treeRoot)
        {
            treeRootController = treeRoot;
            originalPosition = treeRootController.OriginalPosition;
        }
        
        
        public void OnPointDownHandle()
        {
            trajectoryController.Initialized();
        }

        public void OnPointDragHandle()
        {
            var point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            treeRootController.UpdateLine(point);
            
            Vector3 newPos = new Vector3(point.x, point.y, 0);
            if (Vector2.Distance(newPos, originalPosition) > MAX_DISTANCE_DRAG)
                newPos = originalPosition + MAX_DISTANCE_DRAG * (newPos - originalPosition).normalized;

            treeRootController.UpdateLine(newPos);
            transform.position = newPos;
            direction = (originalPosition - newPos).normalized;

                forceThrow = Mathf.Lerp(2f, 15f, Vector2.Distance(newPos, originalPosition) / MAX_DISTANCE_DRAG);
            trajectoryController.UpdatePosition(transform.position, direction, forceThrow);
        }

        public void OnPointUpHandle()
        {
            Shoot();
        }

        private void Shoot()
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.AddForce(direction * forceThrow * Time.deltaTime);
        }
    }
}
