using System;
using System.Collections;
using Game.Event;
using UnityEngine;

namespace Game.Enemy
{
    public class SpawnEnemyController : MonoBehaviour
    {
        [SerializeField] private Transform posSpawn;
        private float MAX_TIME_SPAWN = 5;
        private float currentTimeSpawn = 0;
        private int enemySpawn;
        private Coroutine _coroutine;

        private void OnEnable()
        {
            GameEvent.OnStartWave += StartSpawn;
            GameEvent.OnStopWave += StopSpawn;
        }

        private void OnDisable()
        {
            GameEvent.OnStartWave -= StartSpawn;
            GameEvent.OnStopWave -= StopSpawn;
        }

        private void StartSpawn()
        {
            currentTimeSpawn = MAX_TIME_SPAWN;
            _coroutine = StartCoroutine(SpawnCoroutine());
        }

        private void StopSpawn()
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);
        }

        IEnumerator SpawnCoroutine()
        {
            while (true)
            {
                var newEnemy = ObjectPool.Instance.Get(ObjectPool.Instance.enemy).GetComponent<SimpleEnemyController>();
                newEnemy.transform.position = posSpawn.position;
                newEnemy.Initialized();
                enemySpawn += 1;
                if(currentTimeSpawn>2)
                    currentTimeSpawn -= (enemySpawn / 10) * 0.1f;
                yield return new WaitForSeconds(UnityEngine.Random.Range(1f, currentTimeSpawn));
            }
        }
    }
}
