using UnityEngine;
using Zenject;

public class MenuInstaller : MonoInstaller
{
    [SerializeField] private MenuController _menuController;

    public override void InstallBindings()
    {
        Container.Bind<IMenuController>().FromInstance(_menuController).AsSingle();
    }
}