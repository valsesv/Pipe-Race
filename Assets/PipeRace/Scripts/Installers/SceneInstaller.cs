using UI;
using UnityEngine;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    [SerializeField] private WindowsManager windowsManager;
    [SerializeField] private LevelController levelController;
    
    public override void InstallBindings()
    {
        Container.Bind<WindowsManager>().FromInstance(windowsManager).AsSingle();
        Container.Bind<LevelController>().FromInstance(levelController).AsSingle();
    }
}