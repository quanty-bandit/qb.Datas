using UnityEngine;

namespace qb.Datas
{
    [CreateAssetMenu(fileName = "Vector2_SharedData", menuName = "qb/Datas/Shared/Vector2_SharedData")]
    public class Vector2_SharedData : VectorSharedData<Vector2>
    {
        public override Vector2 Add(Vector2 operand, bool dispatchChangeEvent = false)
        {
            this.value += operand;
            if (dispatchChangeEvent)
                DispatchChangeEvent();
            return this.value;
        }

        public override Vector2 Multiply(float operand, bool dispatchChangeEvent = false)
        {
            this.value *= operand;
            if (dispatchChangeEvent)
                DispatchChangeEvent();
            return this.value;
        }

        public override Vector2 Scale(Vector2 operand, bool dispatchChangeEvent = false)
        {
            this.value.Scale(operand);
            if (dispatchChangeEvent)
                DispatchChangeEvent();
            return this.value;
        }

    }
}
