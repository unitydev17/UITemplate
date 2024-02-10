namespace UITemplate.Core.Interfaces
{
    public interface ISettingsService
    {
        bool musicOn { get; set; }
        bool soundOn { get; set; }
        bool vibroOn { get; set; }
    }
}