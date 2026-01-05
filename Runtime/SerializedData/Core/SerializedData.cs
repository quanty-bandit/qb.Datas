using qb.Pattern;
using UnityEngine;
namespace qb.Datas
{
    /// <summary>
    /// Represents a serializable data container with a GUID, storing a value of the specified type.
    /// </summary>
    /// <typeparam name="T">The type of the value to be stored and serialized.</typeparam>
    public abstract class SerializedData<T> : SOWithGUID
    {
        [SerializeField]
        T value;
        public T Value { get { return value; } }
    }
}
