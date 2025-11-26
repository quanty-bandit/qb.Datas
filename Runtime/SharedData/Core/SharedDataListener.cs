using TriInspector;
using UnityEngine;
using UnityEngine.Events;
namespace qb.Datas
{
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
