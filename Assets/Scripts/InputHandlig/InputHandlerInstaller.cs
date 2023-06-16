using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
