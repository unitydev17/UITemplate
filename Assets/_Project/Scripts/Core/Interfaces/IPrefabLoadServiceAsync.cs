using System.Threading.Tasks;
using UnityEngine;

namespace UITemplate.Core.Interfaces
{
    public interface IPrefabLoadServiceAsync
    {
        public Task<GameObject> GetPrefab<TView>();
    }
}