using Game.Controller;
using Game.Enemy;
using UnityEngine;
using Zenject;
using Zenject.SpaceFighter;

namespace Game.Installers
{
    public class GameplayInstaller : MonoInstaller
    {
        [SerializeField] private TreeRootController treeRootController;
        [SerializeField] private SpawnEnemyController spawnEnemyController;
        public override void InstallBindings()
        {
            Container.Bind<TreeRootController>().FromInstance(treeRootController).AsSingle();
            Container.Bind<SpawnEnemyController>().FromInstance(spawnEnemyController).AsSingle();
        }
    }
}
