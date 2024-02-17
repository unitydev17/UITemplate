using System;
using JetBrains.Annotations;
using UITemplate.UI.MVP.Model;
using UITemplate.UI.MVP.Presenter;


namespace UITemplate.UI.Windows.Popups.Settings
{
    [UsedImplicitly]
    public class SimpleMessagePopupPresenter : PopupPresenter<SimpleMessagePopupView, BaseModel>
    {
        private Action _continueCallback;

        public override void Initialize()
        {
            base.Initialize();
            RegisterCloser(view.onClosePopupClick);
        }

        protected override void CloseAction()
        {
            _continueCallback?.Invoke();
        }

        public void Setup(string header, string message, Action continueCallback)
        {
            _continueCallback = continueCallback;
            view.SetHeader(header);
            view.SetMessage(message);
        }
        
    }
}