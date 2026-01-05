using TriInspector;
using UnityEngine;
using UnityEngine.Events;
namespace qb.Datas
{
    /// <summary>
    /// Abstract base class for listening to shared data changes and invoking Unity events when the data updates.
    /// </summary>
    /// <typeparam name="T">The type of the shared data being observed.</typeparam>
    public abstract class SharedDataListener<T> : MonoBehaviour
    {
        [SerializeField,Required]
        SDProvider_R<T> provider;
        
        [SerializeField]
        UnityEvent<T> onValueChange = new UnityEvent<T>();

        private void OnEnable()
        {
            provider.AddListener(InvokeEvent);
        }

        private void InvokeEvent(T value)
        {
            onValueChange.Invoke(value);
        }

        private void OnDisable()
        {
            provider.RemoveListener(InvokeEvent);
        }

    }
}
