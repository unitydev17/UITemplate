using System;
using JetBrains.Annotations;
using UITemplate.UI.MVP.Model;
using UITemplate.UI.MVP.Presenter;


namespace UITemplate.UI.Windows.Popups.Settings
{
    [UsedImplicitly]
    public class SimpleMessagePopupPresenter : PopupPresenter<SimpleMessagePopupView, BaseModel>
    {
        private Action _closeAction;

        public override void Initialize()
        {
            base.Initialize();
            RegisterCloser(view.onClosePopupClick);
        }

        protected override void CloseAction()
        {
            _closeAction?.Invoke();
        }

        public SimpleMessagePopupPresenter SetCloseAction(Action closeAction)
        {
            _closeAction = closeAction;
            return this;
        }

        public SimpleMessagePopupPresenter SetHeader(string header)
        {
            view.SetHeader(header);
            return this;
        }

        public SimpleMessagePopupPresenter SetMessage(string message)
        {
            view.SetMessage(message);
            return this;
        }
    }
}