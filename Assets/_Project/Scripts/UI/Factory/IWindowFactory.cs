using System;
using UITemplate.UI.Windows.Hud;
using UITemplate.UI.Windows.Popups.Promo;
using UITemplate.UI.Windows.Popups.Settings;
using UITemplate.UI.Windows.Popups.Starting;

namespace UITemplate.UI.Factory
{
    public interface IWindowFactory
    {
        public void GetStartingPopup(Action<StartingPopupPresenter> callback = null);
        public void GetSettingsPopup(Action<SettingsPopupPresenter> callback = null);
        public void GetPromoPopup(Action<PromoPopupPresenter> callback = null);
        public void GetPromoInfoPopup(Action<PromoInfoPopupPresenter> callback = null);
        public void GetHudWindow(Action<HudPresenter> callback = null);
        public void GetStubPopup(Action<StubPopupPresenter> callback = null);
    }
}