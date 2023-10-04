using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    public GameObject block;
    public GameObject blockAnimation;

    public override void InstallBindings()
    {
        Container.Bind<BlockSpawner>().FromComponentInHierarchy().AsSingle();
        Container.Bind<BlockNumber>().FromComponentInNewPrefab(block).AsSingle();
        Container.Bind<BlockAnimation>().FromComponentInNewPrefab(blockAnimation).AsSingle();

        Container.Bind<BlockAnimationController>().FromComponentInHierarchy().AsCached();
        Container.Bind<ScoreController>().FromComponentInHierarchy().AsCached();
        Container.Bind<GameController>().FromComponentInHierarchy().AsSingle();
        
    }
}
