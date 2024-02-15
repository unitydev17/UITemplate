using JetBrains.Annotations;
using UITemplate.Common;
using UITemplate.Core.DomainEntities;

namespace UITemplate.Core.Services
{
    [UsedImplicitly]
    public class SettingsService : ISettingsService
    {
        private readonly SettingsData _model;

        public SettingsService(SettingsData model)
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