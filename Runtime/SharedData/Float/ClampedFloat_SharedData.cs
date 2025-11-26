using UnityEngine;
namespace qb.Datas
{
    [CreateAssetMenu(fileName = "ClampedFloat_SharedData", menuName = "qb/Datas/Shared/ClampedFloat_SharedData")]
    public class ClampedFloat_SharedData : Float_SharedData
    {
        [SerializeField]
        protected float min, max;
        public float Min => min;
        public float Max => max;

        public override float Value { get => base.Value; set => base.Value = Mathf.Clamp(value, min, max); }
       
        public override float Add(float operand)
        {
            var v  = Mathf.Clamp(operand+value,min,max);
            if (v != value)
            {
                value = v;
                DispatchChangeEvent();
            }
            return this.value;
        }

        public override float Multiply(float operand)
        {
            var v = Mathf.Clamp(operand * value, min, max);
            if (v!=value)
            {
                value = v;
                DispatchChangeEvent();
            }
            return this.value;
        }
    }
}
