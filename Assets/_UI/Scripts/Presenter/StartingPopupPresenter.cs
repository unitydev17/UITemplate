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
    public class StartingPopupPresenter : PopupPresenter<StartingPopupView, StartingPopupModel>
    {
        private Action _onClaimPressed;
        private Action _onSkipPressed;

        public StartingPopupPresenter(StartingPopupView view, StartingPopupModel model) : base(view, model)
        {
        }

        public override void Start()
        {
            Debug.Log("Start StartingPopupPresenter");
            base.Start();
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

        protected override void CloseAction()
        {
            Debug.Log("Close starting popup (presenter)");
        }
    }
}