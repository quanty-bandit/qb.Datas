namespace qb.Datas
{
    public abstract class NumericSharedData<T> : SharedData<T>
    {
        public abstract T Add(T operand);
        public abstract T Multiply(T operand);

        
    }
}
