namespace UITemplate.Core.Interfaces
{
    public interface ICopyable<in T>
    {
        void CopyFrom(T data);
    }
}