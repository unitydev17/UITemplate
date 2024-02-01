using System;
using JetBrains.Annotations;
using UITemplate.Model;
using UITemplate.View;
using UniRx;
using UnityEngine;
using VContainer.Unity;

namespace UITemplate.Presenter
{
    [UsedImplicitly]
    public class StartingWindowPresenter : WindowPresenter<StartingWindowView, StartingWindowModel>, IInitializable
    {
        private Action _onClaimPressed;
        private Action _onSkipPressed;

        public StartingWindowPresenter(StartingWindowView view, StartingWindowModel model) : base(view, model)
        {
        }

        public void Initialize()
        {
            Register(view.onClaimBtnClick, OnClaimClick);
            Register(view.onSkipBtnClick, OnSkipClick);
        }

        private void OnSkipClick()
        {
            Debug.Log("Skip");
            CloseView(() => _onSkipPressed?.Invoke());
        }

        private void OnClaimClick(Unit value)
        {
            Debug.Log("Claim");
            CloseView(() => _onClaimPressed?.Invoke());
        }

        public void Setup(Action onClaimPressed)
        {
            _onClaimPressed = onClaimPressed;

            model.timeAbsent = "11h 22m";
            view.Refresh(model);
        }
    }
}