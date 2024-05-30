using Zenject;

public class GameInstaller : MonoInstaller<GameInstaller>
{
    public override void InstallBindings()
    {
        Container.Bind<IInputManager>().To<InputManager>().FromComponentsInHierarchy().AsSingle();
    }
}
