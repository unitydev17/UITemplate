using JetBrains.Annotations;
using UITemplate.Common;
using UITemplate.Infrastructure;
using UITemplate.UI.MVP.Presenter;


namespace UITemplate.UI.Windows.Popups.Settings
{
    [UsedImplicitly]
    public class SettingsPopupPresenter : PopupPresenter<SettingsPopupView, SettingsPopupModel>
    {
        private ISettingsService _settingsService;
        private IWebService _webService;


        public override void Initialize()
        {
            _settingsService = Resolve<ISettingsService>();
            _webService = Resolve<IWebService>();

            base.Initialize();
            Register(view.onChangeMusic, ChangeMusic);
            Register(view.onChangeSound, ChangeSound);
            Register(view.onChangeVibro, ChangeVibro);
            Register(view.onClickPrivacy, ClickPrivacy);

            InitModel();
        }

        private void InitModel()
        {
            model.musicOn = _settingsService.musicOn;
            model.soundOn = _settingsService.soundOn;
            model.vibroOn = _settingsService.vibroOn;

            view.UpdateSettings(model);
        }

        private void ChangeMusic()
        {
            model.musicOn = !model.musicOn;
            _settingsService.musicOn = model.musicOn;
            view.UpdateSettings(model);
        }

        private void ChangeSound()
        {
            model.soundOn = !model.soundOn;
            _settingsService.soundOn = model.soundOn;
            view.UpdateSettings(model);
        }

        private void ChangeVibro()
        {
            model.vibroOn = !model.vibroOn;
            _settingsService.vibroOn = model.vibroOn;
            view.UpdateSettings(model);
        }


        private void ClickPrivacy()
        {
            _webService.OpenWebUrl("https://www.google.com/404");
        }
    }
}