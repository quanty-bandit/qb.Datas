using System.Text;
using UnityEngine;
namespace qb.Datas
{
    /// <summary>
    /// Represents a serializable data asset containing a string array, with functionality to convert the array to a
    /// single string using a specified separator.
    /// </summary>
    [CreateAssetMenu(fileName = "StringArray_SerializedData", menuName = "qb/Datas/Serialized/StringArray_SerializedData")]
    public class StringArray_SerializedData : SerializedData<string[]>
    {
        public string ToString(string separator)
        {
            var v = Value;
            if (v == null || v.Length == 0)
                return "";

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(v[0]);
            for(int i=1;i<v.Length;i++)
            {
                var s = v[i];
                if (!string.IsNullOrEmpty(s))
                {
                    stringBuilder.Append(separator);
                    stringBuilder.Append(s);
                }
            }
            return stringBuilder.ToString();    
        }
    }
}
