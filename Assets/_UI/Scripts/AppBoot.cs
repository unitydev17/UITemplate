using VContainer.Unity;

namespace UITemplate
{
    public class AppBoot : IStartable
    {
        private UIManager _ui;

        public AppBoot(UIManager ui)
        {
            _ui = ui;
        }

        public void Start()
        {
            _ui.Run();
        }
    }
}