using System;
using TriInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using qb.Pattern;

#if UNITY_EDITOR
using UnityEditor;
#endif
namespace qb.Datas
{
    public abstract class SharedData<T> : SOWithGUID, ISharedData
    {
        [SerializeField]
        protected bool verbose = true;

        [SerializeField]
        protected T defaultValue = default(T);
#pragma warning disable 67
        protected event UnityAction<T> onChange;
#pragma warning restore 67
        /// <summary>
        /// The event with one parameter that can be subscribed to and invoked
        /// </summary>
        public event UnityAction<T> OnChange
        {
            add
            {
#if !NO_DEBUG_LOG
                if (verbose)
                    Debug.Log($"<color=#00FF10>Add event listener on {this.name}</color>");
#endif
                if (onChange != null)
                {
                    var invocationList = onChange.GetInvocationList();
                    foreach (var invocation in invocationList)
                    {
                        if (invocation == value as Delegate)
                        {
#if !NO_DEBUG_LOG_WARNING
                            Debug.LogWarning($"Duplicate subscription to shared value: {this.name}");
#endif
                            return;
                        }
                    }
                }
                onChange += value;
            }
            remove
            {

                if (value != null && !(value.Target.Equals(null)))
                {
                    if (onChange != null)
                    {
                        var invocationList = onChange.GetInvocationList();
                        onChange = null;
                        foreach (var invocation in invocationList)
                        {
                            if (invocation != value as Delegate)
                                onChange += invocation as UnityAction<T>;
                        }
                    }
                    else
                    {
#if !NO_DEBUG_LOG_WARNING
                        Debug.LogWarning($"Try to unsubcribe from the empty observable value: {this.name}");
#endif
                    }
                }
                else
                {
#if !NO_DEBUG_LOG_WARNING
                    Debug.LogWarning($"Try to unsubcribe null action or null object action from event channel: {this.name}");
#endif
                    ClearInvalidSubscriptions();
                }
            }
        }

        [System.NonSerialized]
        object cleanUpLocker = new object();
        [System.NonSerialized]
        object locker = new object();

        /// <summary>
        /// Remove all invalid subscriptions in case of behaviours deletion
        /// </summary>
        public void ClearInvalidSubscriptions()
        {
            if (onChange != null)
            {
                lock (cleanUpLocker)
                {
                    var invocationList = onChange.GetInvocationList();
                    int validInvocationCount = 0;
                    foreach (var invocation in invocationList)
                    {
                        if (invocation != null && !(invocation.Target.Equals(null)))
                        {
                            validInvocationCount++;
                        }
                    }
                    if (validInvocationCount != invocationList.Length)
                    {
                        onChange = null;
                        foreach (var invocation in invocationList)
                        {
                            if (invocation != null && !(invocation.Target.Equals(null)))
                            {
                                onChange += invocation as UnityAction<T>;
                            }
                        }
                    }
                }
            }
        }
        [SerializeField,GUIColor(0f,1f,1f)]
        protected T value;

        /// <summary>
        /// The data value.
        /// When value is set the DispatchChangeEvent is automaticly called 
        /// </summary>
        public virtual T Value
        {
            get
            {
                return value;
            }

            set
            {
                this.value = value;
                DispatchChangeEvent();
            }
        }
        [Button(ButtonSizes.Large)]
        /// <summary>
        /// Invoke event change event safely by checking invalid subscriptor resulting of object destruction 
        /// </summary>
        public void DispatchChangeEvent() => DispatchChangeEvent(null);

        /// <summary>
        /// Invoke event change event safely by checking invalid subscriptor resulting of object destruction 
        /// </summary>
        /// <param name="sender">
        /// The object which invoke the dispatch event method.
        /// If the value is not null a test is done to avoid OnChange action call for the sender
        /// </param>
        public void DispatchChangeEvent(object sender)
        {
            if (onChange != null)
            {
                lock (locker)
                {
                    //reference.onValueChange.UpdateAndDispatchChangeEvent();
                    var invocationList = onChange.GetInvocationList();
                    int validInvocationCount = 0;
                    foreach (var invocation in invocationList)
                    {
                        if (invocation != null && !(invocation.Target.Equals(sender)))
                        {
                            validInvocationCount++;
                            (invocation as UnityAction<T>).Invoke(value);
                        }
                    }
                    if (validInvocationCount != invocationList.Length)
                    {
                        onChange = null;
                        foreach (var invocation in invocationList)
                        {
                            if (invocation != null && !(invocation.Target.Equals(sender)))
                                onChange += invocation as UnityAction<T>;
#if !NO_DEBUG_LOG_WARNING
                            else if(sender != null)
                            {
                                Debug.LogWarning($"The OnChange channel [{this.name}] try to invoke an action of a null object!");
                            }
#endif
                        }
                    }
                }
            }
        }


        /// <summary>
        /// Reset the value to defaultValue value and invoke event
        /// </summary>
        [Button(ButtonSizes.Large), GUIColor(1f, 0.5f, 0.1f)]
        public void ResetValueToDefault() => ResetValueToDefault(null);
        /// <summary>
        /// Reset the value to defaultValue value and invoke event
        /// </summary>
        /// <param name="sender">
        /// The object which invoke the method.
        /// If the value is not null a test is done to avoid OnChange action call for the sender
        /// </param>
        public void ResetValueToDefault(object sender) => SetValue(defaultValue, sender);

        /// <summary>
        /// Set the value and dispatch event onChange
        /// </summary>
        /// <param name="value">The new value to set</param>
        /// <param name="caller">
        /// The object which invoke the method.
        /// If the value is not null a test is done to avoid OnChange action call for the sender
        /// </param>
        public void SetValue(T value,object caller)
        {
            this.value = value;
            DispatchChangeEvent(caller);
        }

        /// <summary>
        /// Set the value and dispatch event onChange
        /// </summary>
        /// <param name="value">The new value to set</param>
        public void SetValue(T value) => SetValue(value, null);

        [System.NonSerialized]
        bool isBindedOnSceneUnloadEvent;
        protected override void OnEnable()
        {
            base.OnEnable();
            /*
#if UNITY_EDITOR
            SetEditorIcon();
#endif
            */
            if (!isBindedOnSceneUnloadEvent)
            {
                SceneManager.sceneUnloaded += OnSceneUnloaded;
                isBindedOnSceneUnloadEvent = true;
            }
        }
        protected override void OnDisable()
        {
            base.OnDisable();   
            if (isBindedOnSceneUnloadEvent)
            {
                SceneManager.sceneUnloaded -= OnSceneUnloaded;
                isBindedOnSceneUnloadEvent = false;
            }
        }
        private void OnSceneUnloaded(Scene scene)
        {
            ClearInvalidSubscriptions();
        }

        public static implicit operator T(SharedData<T> data) => data.Value;

        protected override void Awake()
        {
            base.Awake();
#if UNITY_EDITOR
            SetEditorIcon();
#endif
        }

#if UNITY_EDITOR

        void SetEditorIcon()
        {
            var currentPath = AssetDatabase.GetAssetPath(this);
            if (currentPath != null)
            {
                var obj = AssetDatabase.LoadAssetAtPath<SharedData<T>>(currentPath);
                var iconGuids = AssetDatabase.FindAssets($"{GetType().Name} t:texture2D", null);

                if (iconGuids != null && iconGuids.Length > 0)
                {
                    var icon = AssetDatabase.LoadAssetAtPath<Texture2D>(AssetDatabase.GUIDToAssetPath(iconGuids[0]));
                    EditorGUIUtility.SetIconForObject(obj, icon);
                }
                else
                {
                    iconGuids = AssetDatabase.FindAssets($"SharedData t:texture2D", null);
                    if (iconGuids != null && iconGuids.Length > 0)
                    {
                        var icon = AssetDatabase.LoadAssetAtPath<Texture2D>(AssetDatabase.GUIDToAssetPath(iconGuids[0]));
                        EditorGUIUtility.SetIconForObject(obj, icon);
                    }
                }
            }
        }

#endif
    }
}
