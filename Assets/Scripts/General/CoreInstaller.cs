using Scripts.InputHandling;
using Zenject;

namespace Scripts.General
{
    public class CoreInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindInput();
            BindEventBus();
        }

        private void BindInput()
        {
            Container
                .BindInterfacesAndSelfTo<InputHandler>()
                .FromNew()
                .AsSingle();
        }

        private void BindEventBus()
        {
            Container
                .Bind<EventBus>()
                .FromNew()
                .AsSingle();
        }
    }

}

