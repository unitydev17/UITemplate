using JetBrains.Annotations;
using UITemplate.Model.Application;

namespace UITemplate.Services
{
    [UsedImplicitly]
    public class SettingsService : ISettingsService
    {
        private readonly AppModel _model;

        public SettingsService(AppModel model)
        {
            _model = model;
        }

        public bool musicOn { get; set; }
        public bool soundOn { get; set; }
        public bool vibroOn { get; set; }
    }
}