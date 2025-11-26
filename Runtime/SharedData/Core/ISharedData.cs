
namespace qb.Datas
{
    public interface ISharedData
    {
        void ClearInvalidSubscriptions();
        void DispatchChangeEvent(object caller=null);
        
        void ResetValueToDefault(object caller = null);
    }
}
