using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    public GameObject block;

    public override void InstallBindings()
    {
        Container.Bind<BlockNumber>().FromComponentInNewPrefab(block).AsSingle();
        Container.Bind<BlockSpawner>().FromComponentInHierarchy().AsSingle();
        Container.Bind<ScoreController>().FromComponentInHierarchy().AsSingle();
        Container.Bind<GameController>().FromComponentInHierarchy().AsSingle();
        
    }
}
