using JetBrains.Annotations;
using UITemplate.Domain.Model;

namespace UITemplate.Application.Services
{
    [UsedImplicitly]
    public class SettingsService : ISettingsService
    {
        private readonly AppModel _model;

        public SettingsService(AppModel model)
        {
            _model = model;
        }

        public bool musicOn
        {
            get => _model.musicOn;
            set => _model.musicOn = value;
        }

        public bool soundOn
        {
            get => _model.soundOn;
            set => _model.soundOn = value;
        }

        public bool vibroOn
        {
            get => _model.vibroOn;
            set => _model.vibroOn = value;
        }
    }
}