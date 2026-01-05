using System;
using TriInspector;
using UnityEngine;
using UnityEngine.Events;
using qb.Pattern;
namespace qb.Datas
{

    /// <summary>
    /// Abstract base class for providers that manage access to shared data of a specified type.
    /// Resolves scriptable object reference issue from addressable assets
    /// </summary>
    /// <typeparam name="T">The type of data managed by the provider.</typeparam>
    public abstract class SDProvider<T>
    {
        [Flags] public enum DataAccess { None = 0, Read = 1, Write = 2, ReadWrite = 3 };
        [SerializeField]
        protected SharedData<T> data;
        protected virtual DataAccess dataAccess => DataAccess.None;
        public bool IsFilled => data!=null;

        bool sourceIsChecked;
        protected void CheckSource()
        {
            if (sourceIsChecked) return;
            var d = SOWithGUID.GetSourceFromGUID(data) as SharedData<T>;
            if (d != null && d != data)
                data = d;
            sourceIsChecked = true;
        }

        protected string DATA_NOT_FILLED_ERROR => $"Data field from Type<{typeof(T)}> of Shared data provider is not filled!";
    }
    /// <summary>
    /// Shared data provider with read and write data access
    /// Manage shared data access and scriptable object source serialization issue from addressable object.
    /// </summary>
    /// <typeparam name="T">Type of data</typeparam>
    [Serializable]
    public class SDProvider_RW<T> : SDProvider<T>
    {
        [GUIColor("00FF00")]
        [ReadOnly, ShowInInspector, PropertyOrder(-1), HideLabel]
        protected override DataAccess dataAccess => DataAccess.ReadWrite;
        public T Value
        {
            get
            {
                if (data)
                {
                    return data.Value;
                }
                else
                {
                    Debug.LogError($"Data field from Type<{typeof(T)}> of Shared data provider reader is not filled, default value is returned");
                    return default(T);
                }
            }
            set
            {
                if (data)
                {
                    CheckSource();
                    data.Value = value;
                }
                else
                    Debug.LogError(DATA_NOT_FILLED_ERROR);
            }
        }
        public void AddListener(UnityAction<T> action)
        {
            if (data)
            {
                CheckSource();
                data.OnChange += action;
            }
            else
                Debug.LogError($"Can't add listener because data field from Type<{typeof(T)}> of Shared data provider reader is not filled");
        }
        public void RemoveListener(UnityAction<T> action)
        {
            if (data)
            {
                data.OnChange -= action;
            }
            else
            {
                Debug.LogError($"Can't remove listener because: {DATA_NOT_FILLED_ERROR}");
            }
        }

        /// <summary>
        /// Set the value and dispatch event onChange
        /// </summary>
        /// <param name="value">The new value to set</param>
        /// <param name="caller">
        /// The object which invoke the method.
        /// If the value is not null a test is done to avoid OnChange action call for the caller
        /// </param>
        public void SetValue(T value, object caller = null)
        {
            if (data)
            {
                CheckSource();
                data.SetValue(value, caller);
            }
            else
                Debug.LogError(DATA_NOT_FILLED_ERROR);
        }


        /// <summary>
        /// Reset the value to defaultValue value and invoke event
        /// </summary>
        /// <param name="caller">
        /// The object which invoke the method.
        /// If the value is not null a test is done to avoid OnChange action call for the caller
        /// </param>
        public void ResetValueToDefault(object caller = null)
        {
            if(data)
            {
                CheckSource();
                data.ResetValueToDefault(caller);
            }
            else
                Debug.LogError(DATA_NOT_FILLED_ERROR);

        }

        /// <summary>
        /// Update value and invoke event safely by checking invalid subscriptor resulting of object destruction 
        /// </summary>
        /// <param name="caller">
        /// The object which invoke the dispatch event method.
        /// If the value is not null a test is done to avoid OnChange action call for the caller
        /// </param>
        public void DispatchChangeEvent(object caller = null)
        {
            if (data)
            {
                CheckSource();
                data.DispatchChangeEvent(caller);
            }
            else
                Debug.LogError(DATA_NOT_FILLED_ERROR);
        }
    }
    /// <summary>
    /// Shared data provider with read only data access
    /// Manage shared data access and scriptable object source serialization issue from addressable object.
    /// </summary>
    /// <typeparam name="T">Type of data</typeparam>

    [Serializable]
    public class SDProvider_R<T> : SDProvider<T>
    {
        [GUIColor("00FFFF")]
        [ReadOnly, ShowInInspector, PropertyOrder(-1), HideLabel]
        protected override DataAccess dataAccess => DataAccess.Read;
        public T Value {
            get {
                if (data)
                {
                    CheckSource();
                    return data.Value;
                }
                else
                {
                    Debug.LogError($"Data field from Type<{typeof(T)}> of Shared data provider reader is not filled, default value is returned");
                    return default(T);
                }
            }
        }
        public void AddListener(UnityAction<T> action)
        {
            if (data)
            {
                CheckSource();
                data.OnChange += action;
            }
            else
                Debug.LogError($"Can't add listener because: {DATA_NOT_FILLED_ERROR}");
        }
        public void RemoveListener(UnityAction<T> action)
        {
            if (data)
            {
                data.OnChange -= action;
            }
            else
            {
                Debug.LogError($"Can't remove listener because: {DATA_NOT_FILLED_ERROR}");
            }
        }
    }

}
