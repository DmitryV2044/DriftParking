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
        }

        private void BindTireTrailController()
        {
            Container
                .Bind<TireTrailController>()
                .FromComponentInHierarchy(true)
                .AsSingle();
        }

    }

}

