using UI;
using UnityEngine;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    [SerializeField] private WindowsManager windowsManager;
    
    public override void InstallBindings()
    {
        Container.Bind<WindowsManager>().FromInstance(windowsManager).AsSingle();
    }
}