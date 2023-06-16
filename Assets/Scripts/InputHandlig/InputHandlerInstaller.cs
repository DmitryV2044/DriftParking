using Zenject;

namespace Scripts.InputHandling
{
    public class InputHandlerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {

            Container
                .BindInterfacesAndSelfTo<InputHandler>()
                .FromNew()
                .AsSingle();

        }
    }

}
