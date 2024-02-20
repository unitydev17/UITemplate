using Cysharp.Threading.Tasks;

namespace UITemplate.Common.Interfaces
{
    public interface IFactory<T>
    {
        public UniTask<T> Create();
    }
}