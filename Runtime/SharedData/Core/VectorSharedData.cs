
namespace qb.Datas
{
    public abstract class VectorSharedData<T> : SharedData<T>
    {
        public abstract T Add(T operand, bool dispatchChangeEvent = false);
        public abstract T Scale(T operand, bool dispatchChangeEvent = false);
        public abstract T Multiply(float operand, bool dispatchChangeEvent = false);

    }
}
