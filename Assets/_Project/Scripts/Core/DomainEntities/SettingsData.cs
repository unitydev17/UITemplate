using System;
using UITemplate.Core.Interfaces;

namespace UITemplate.Core.DomainEntities
{
    [Serializable]
    public class SettingsData : ICopyable<SettingsData>
    {
        public bool musicOn;
        public bool soundOn;
        public bool vibroOn;
        public void CopyFrom(SettingsData data)
        {
            musicOn = data.musicOn;
            soundOn = data.soundOn;
            vibroOn = data.vibroOn;
        }
    }
}