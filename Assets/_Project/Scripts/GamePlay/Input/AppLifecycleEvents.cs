using System;
using UniRx;
using UnityEngine;

namespace _Project.Scripts.GamePlay.Input
{
    public class AppLifecycleEvents : MonoBehaviour
    {
        private void Start()
        {
            Observable.EveryApplicationFocus().Subscribe().AddTo(this);
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            throw new NotImplementedException();
        }
    }
}