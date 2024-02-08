using UITemplate.Presentation.Windows.Hud;
using UITemplate.Presentation.Windows.Popups.Promo;
using UITemplate.Presentation.Windows.Popups.Settings;
using UITemplate.Presentation.Windows.Popups.Starting;

namespace UITemplate.Presentation.MVP.Factory
{
    public interface IWindowFactory
    {
        public StartingPopupPresenter GetStartingPopup();

        public SettingsPopupPresenter GetSettingsPopup();

        public PromoPopupPresenter GetPromoPopup();

        public PromoInfoPopupPresenter GetPromoInfoPopup();

        public HudPresenter GetHud();
    }
}