using JetBrains.Annotations;

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