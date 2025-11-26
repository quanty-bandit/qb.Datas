using UnityEngine;
namespace qb.Datas
{
    [CreateAssetMenu(fileName = "Int_SharedData", menuName = "qb/Datas/Shared/Int_SharedData")]
    public class Int_SharedData : NumericSharedData<int>
    {
        public override int Add(int operand)
        {
            this.value += operand;
            DispatchChangeEvent();
            return this.value;
        }

        public override int Multiply(int operand)
        {
            this.value *= operand;
            DispatchChangeEvent();
            return this.value;
        }
    }
}