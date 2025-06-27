using System;
using System.Collections;
using System.Collections.Generic;
using Game.Event;
using UnityEngine;

namespace Game.Controller
{
    public class TreeRootController : MonoBehaviour
    {
        [SerializeField] private TreeRootBranch[] branches = new TreeRootBranch[4];
        [SerializeField] private PeenController[] peens = new PeenController[4];

        private Coroutine _coroutine;

        private void OnEnable()
        {
            GameEvent.OnStartWave += SpawnPeen;
            GameEvent.OnStartWave += StopPeen;
            
            PeenController.OnShootPeen += ReturnPeen;
        }

        private void OnDisable()
        {
            GameEvent.OnStartWave -= SpawnPeen;
            GameEvent.OnStartWave -= StopPeen;
            
            PeenController.OnShootPeen -= ReturnPeen;
        }

        private void SpawnPeen()
        {
            for (int i = 0; i < 4; i++)
            {
                var newPeen = ObjectPool.Instance.Get(ObjectPool.Instance.peen).GetComponent<PeenController>();
                peens[i] = newPeen;
                
                newPeen.SetTreeRoot(branches[i]);
                newPeen.Instantiate();
            }
        }

        private void ReturnPeen(PeenController peen)
        {
            for (int i = 0; i < 4; i++)
            {
                if (peens[i] == peen)
                {
                    StartCoroutine(SpawnPeenCoroutine(i));
                }
            }
        }

        IEnumerator SpawnPeenCoroutine(int index)
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(2f,4f));
            var newPeen = ObjectPool.Instance.Get(ObjectPool.Instance.peen).GetComponent<PeenController>();
            peens[index] = newPeen;
            
            newPeen.SetTreeRoot(branches[index]);
            newPeen.Instantiate();
        }

        private void StopPeen()
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }
        }
    }
}
