using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace qb.Datas
{
    public abstract class SharedDatasListener<T> : MonoBehaviour
    {
        [Serializable]
        public class ListenerSettings
        {
            [SerializeField]
            SDProvider_R<T> dataProvider;
            [SerializeField]
            UnityEvent<T> onValueChange = new UnityEvent<T>();
            public void Bind()
            {
                dataProvider.AddListener(InvokeEvent);
            }
            public void Unbind()
            {
                dataProvider.RemoveListener(InvokeEvent);
            }
            private void InvokeEvent(T value)
            {
                onValueChange.Invoke(value);
            }
        }
        [SerializeField]
        List<ListenerSettings> listeners = new List<ListenerSettings>();
        private void OnEnable()
        {
            foreach (var listener in listeners)
                listener.Bind();
        }
        private void OnDisable()
        {
            foreach (var listener in listeners)
                listener.Unbind();

        }
    }
}
