using System;
using JetBrains.Annotations;
using UITemplate.Presentation.Presenters.Common;
using UITemplate.View;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Object = UnityEngine.Object;

namespace UITemplate.Presentation.Factories
{
    [UsedImplicitly]
    public class WindowFactory
    {
        private static int _order;

        public static TPresenter Create<TPresenter, TView, TModel>(IObjectResolver container) where TModel : new() where TPresenter : BasePresenter<TView, TModel>, new() where TView : ISortedView
        {
            var prefab = GetPrefab<TView>();
            var view = Object.Instantiate(prefab).GetComponent<TView>();
            view.SetSortingOrder(_order++);

            var model = new TModel();
            var presenter = new TPresenter {view = view, model = model, container = container};

            (presenter as IInitializable)?.Initialize();
            return presenter;
        }

        private static GameObject GetPrefab<TView>()
        {
            try
            {
                var path = typeof(TView).ToString().Split(".");
                return Resources.Load<GameObject>(path[^1]);
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }

            throw new Exception("Prefab not found");
        }
    }
}