using UnityEngine;
namespace qb.Datas
{
    [CreateAssetMenu(fileName = "Vector3_SharedData", menuName = "qb/Datas/Shared/Vector3_SharedData")]
    public class Vector3_SharedData : VectorSharedData<Vector3>
    {
        public override Vector3 Add(Vector3 operand, bool dispatchChangeEvent = false)
        {
            this.value += operand;
            if (dispatchChangeEvent)
                DispatchChangeEvent();
            return this.value;
        }

        public override Vector3 Multiply(float operand, bool dispatchChangeEvent = false)
        {
            this.value *= operand;
            if (dispatchChangeEvent)
                DispatchChangeEvent();
            return this.value;
        }

        public override Vector3 Scale(Vector3 operand, bool dispatchChangeEvent = false)
        {
            this.value.Scale(operand);
            if (dispatchChangeEvent)
                DispatchChangeEvent();
            return this.value;
        }
    }
}
