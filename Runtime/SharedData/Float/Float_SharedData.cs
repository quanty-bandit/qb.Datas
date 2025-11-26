
using UnityEngine;
namespace qb.Datas
{
    [CreateAssetMenu(fileName = "Float_SharedData", menuName = "qb/Datas/Shared/Float_SharedData")]
    public class Float_SharedData : NumericSharedData<float>
    {
        public override float Add(float operand)
        {
            this.value += operand;
            DispatchChangeEvent();
            return this.value;
        }

        public override float Multiply(float operand)
        {
            this.value *= operand;
            DispatchChangeEvent();
            return this.value;
        }

       
    }
}
