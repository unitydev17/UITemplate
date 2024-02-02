using UnityEngine;

namespace UITemplate.Services
{
    public class WebService : IWebService
    {
        public void OpenWebUrl(string url)
        {
            Application.OpenURL(url);
        }
    }
}