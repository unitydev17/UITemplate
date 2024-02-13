using UITemplate.UI.Windows.Hud;
using UITemplate.UI.Windows.Popups.Promo;
using UITemplate.UI.Windows.Popups.Settings;
using UITemplate.UI.Windows.Popups.Starting;

namespace UITemplate.UI.Factory
{
    public interface IWindowFactory
    {
        public StartingPopupPresenter GetStartingPopup();

        public SettingsPopupPresenter GetSettingsPopup();

        public PromoPopupPresenter GetPromoPopup();

        public PromoInfoPopupPresenter GetPromoInfoPopup();

        public HudPresenter GetHud();
        
        public StubPopupPresenter GetStubPopup();
    }
}