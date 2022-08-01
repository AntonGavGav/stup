using UnityEngine;
using UnityEngine.Accessibility;

namespace Resources.Scripts
{
    public interface ITakeable
    {
        void Take();
        void Place();
        GameObject returnObject();
        bool IsReadyToBeHold(Transform transform);
    }
}