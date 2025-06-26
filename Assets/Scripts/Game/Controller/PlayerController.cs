using System;
using UnityEngine;

namespace Game.Controller
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private LayerMask peenLayer;
        [SerializeField] private IHandle iHandle;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
                HandleDown();
            if (Input.GetMouseButtonUp(0))
                HandleUp();

            if (iHandle != null)
                HandleDrag();
        }

        private void HandleDown()
        {
            var point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var col = Physics2D.OverlapPoint(point, peenLayer);
            if (col != null)
            {
                iHandle = col.GetComponent<IHandle>();
                iHandle.OnPointDownHandle();
            }
        }

        private void HandleUp()
        {
            if (iHandle != null)
            {
                iHandle.OnPointUpHandle();
                iHandle = null;
            }
        }

        private void HandleDrag()
        {
            iHandle.OnPointDragHandle();
        }
    }
}
