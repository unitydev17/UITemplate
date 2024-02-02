using System;
using JetBrains.Annotations;
using UITemplate.Model;
using UITemplate.View;
using UniRx;
using UnityEngine;

namespace UITemplate.Presenter
{
    [UsedImplicitly]
    public class StartingPopupPresenter : PopupPresenter<StartingPopupView, StartingPopupModel>
    {
        private Action _onClaimPressed;


        public StartingPopupPresenter(StartingPopupView view, StartingPopupModel model) : base(view, model)
        {
        }

        public override void Initialize()
        {
            base.Initialize();
            Register(view.onClaimBtnClick, OnClaimClick);
        }

        private void OnClaimClick(Unit value)
        {
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