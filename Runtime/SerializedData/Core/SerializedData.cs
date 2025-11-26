using qb.Pattern;
using UnityEngine;
namespace qb.Datas
{
    public abstract class SerializedData<T> : SOWithGUID
    {
        [SerializeField]
        T value;
        public T Value { get { return value; } }
    }
}
