using SpeedTestApp.Models;

namespace SpeedTestApp.BL.Service
{
    public interface IStorageModel<T>
        where T: IApplicationModel
    {
        T ConvertToApplicationModel();
        IStorageModel<T> ConvertFromApplicationModel(T model);
    }
}
