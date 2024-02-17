using Cysharp.Threading.Tasks;
using UITemplate.UI.Windows.Hud;
using UITemplate.UI.Windows.Popups.Promo;
using UITemplate.UI.Windows.Popups.Settings;
using UITemplate.UI.Windows.Popups.Starting;

namespace UITemplate.UI.Factory
{
    public interface IWindowFactory
    {
        public UniTask<StartingPopupPresenter> GetStartingPopup();
        public UniTask<SettingsPopupPresenter> GetSettingsPopup();
        public UniTask<PromoPopupPresenter> GetPromoPopup();
        public UniTask<PromoInfoPopupPresenter> GetPromoInfoPopup();
        public UniTask<HudPresenter> GetHudWindow();
        public UniTask<StubPopupPresenter> GetStubPopup();
        public UniTask<WelcomePopupPresenter> GetWelcomePopup();

        public UniTask<SimpleMessagePopupPresenter> GetSimpleMessagePopup();
    }
}