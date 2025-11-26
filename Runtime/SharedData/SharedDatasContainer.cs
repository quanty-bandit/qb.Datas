using System.Collections.Generic;
using TriInspector;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace qb.Datas
{
    [CreateAssetMenu(fileName = "SharedDatasContainer", menuName = "qb/Datas/Shared/SharedDatasContainer")]
    public class SharedDatasContainer : ScriptableObject
    {
#if UNITY_EDITOR
        private void CheckEntriesType()
        {
            List<ScriptableObject> valid = new List<ScriptableObject>();
            for (int i = 0; i < entries.Count; i++)
            {
                var entry = entries[i];
                if(entry==null || entry is ISharedData)
                    valid.Add(entry);
                else
                {
                    EditorUtility.DisplayDialog("Invalid entry type", $"The {entry.name} is not a shared data!", "Close");
                }
            }
            entries = valid;
        }

        [OnValueChanged(nameof(CheckEntriesType))]
#endif
        [SerializeField, InlineEditor,Required]
        List<ScriptableObject> entries = new List<ScriptableObject>();
    }
}
