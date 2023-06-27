using Scripts.InputHandling;
using Zenject;

namespace Scripts.General
{
    public class CoreInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindInput();
        }

        private void BindInput()
        {
            Container
                .BindInterfacesAndSelfTo<InputHandler>()
                .FromNew()
                .AsSingle();
        }
    }

}

