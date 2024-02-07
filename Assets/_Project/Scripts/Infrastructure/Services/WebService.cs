using JetBrains.Annotations;
using UITemplate.Infrastructure.Interfaces;

namespace UITemplate.Infrastructure.Services
{
    [UsedImplicitly]
    public class WebService : IWebService
    {
        public void OpenWebUrl(string url)
        {
            UnityEngine.Application.OpenURL(url);
        }
    }
}