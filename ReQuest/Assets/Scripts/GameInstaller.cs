using Zenject;

public class GameInstaller : MonoInstaller<GameInstaller>
{
    public override void InstallBindings()
    {
        Container.Bind<IInputManager>().To<InputManager>().FromComponentsInHierarchy().AsSingle();
        Container.Bind<IInputMapper>().To<InputMapper>().FromComponentsInHierarchy().AsSingle();
        Container.Bind<ITimeManager>().To<TimeManager>().FromComponentsInHierarchy().AsSingle();
        Container.Bind<IPathfinding>().To<Pathfinding>().FromComponentsInHierarchy().AsSingle();
    }
}
