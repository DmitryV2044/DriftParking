using Scripts.UI;
using Scripts.Effects;
using Scripts.InputHandling;
using Zenject;

namespace Scripts.General
{
    public class GameplayInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindTireTrailController();
            BindUIInteractor();
        }

        private void BindTireTrailController()
        {
            Container
                .Bind<TireTrailController>()
                .FromComponentInHierarchy(true)
                .AsSingle();
        }

        private void BindUIInteractor()
        {
            Container
                .Bind<UIInteractor>()
                .FromComponentInHierarchy()
                .AsSingle();
        }

    }

}

