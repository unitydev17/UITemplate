using System;

namespace UITemplate.UI.MVP.View
{
    public interface IDestructable
    {
        void SetDestructor(Action action);
    }
}