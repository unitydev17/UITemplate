using System;
using JetBrains.Annotations;
using UITemplate.Application.ScriptableObjects;
using UITemplate.Events;
using UITemplate.Presentation.MVP.Presenter;
using UniRx;
using UnityEngine;
using VContainer.Unity;

namespace UITemplate.Presentation.Windows.Hud
{
    [UsedImplicitly]
    public class HudPresenter : WindowPresenter<HudView, HudModel>, IInitializable, IStartable
    {
        private readonly UpgradeCfg _cfg;
        private IDisposable _tempDisposable;

        public HudPresenter(UpgradeCfg cfg)
        {
            _cfg = cfg;
        }

        public HudPresenter()
        {
        }

        public void Initialize()
        {
            RegisterStubList();
            Register(view.onSettingsBtnClick, OpenSettingsPopup);
            Register(MessageBroker.Default.Receive<UpdatePlayerDataEvent>(), UpdateIncome);
            Register(view.onSpeedBtnClick, ActivateSpeed);
        }

        public void Start()
        {
            ResetSpeedUpButton();
        }

        private void ResetSpeedUpButton()
        {
            view.UpdateSpeedUpTimer((int) _cfg.speedUpDuration, 1);
        }

        private void ActivateSpeed()
        {
            if (model.timerEnabled) return;

            MessageBroker.Default.Publish(new UISpeedUpRequestEvent(true));
            RunTimer(_cfg.speedUpDuration, () => MessageBroker.Default.Publish(new UISpeedUpRequestEvent(false)));
        }

        private void RunTimer(float targetTime, Action onStopCallback)
        {
            view.SetSpeedButton(true);
            
            model.timerEnabled = true;
            model.timer = 0f;

            _tempDisposable = RegisterTemporary(Observable.EveryUpdate(), () =>
            {
                model.timer += Time.deltaTime;

                if (model.timer > targetTime)
                {
                    StopTimer();
                    return;
                }

                var progress = 1 - model.timer / targetTime;
                var timeRemain = (int) (targetTime - model.timer);

                view.UpdateSpeedUpTimer(timeRemain, progress);
            });

            void StopTimer()
            {
                model.timerEnabled = false;
                view.SetSpeedButton(false);
                ResetSpeedUpButton();
                
                onStopCallback?.Invoke();
                Dispose(_tempDisposable);
            }
        }


        private void RegisterStubList()
        {
            foreach (var observable in view.onStubBtnClicks)
            {
                Register(observable, OpenStubPopup);
            }
        }

        private static void OpenStubPopup()
        {
            MessageBroker.Default.Publish(new HudStubOpenEvent());
        }

        private static void OpenSettingsPopup()
        {
            MessageBroker.Default.Publish(new HudSettingsOpenEvent());
        }

        private void UpdateIncome(UpdatePlayerDataEvent data)
        {
            view.UpdateCoins(data.dto.money);
        }
    }

    internal class HudStubOpenEvent
    {
    }

    internal class HudSettingsOpenEvent
    {
    }
}