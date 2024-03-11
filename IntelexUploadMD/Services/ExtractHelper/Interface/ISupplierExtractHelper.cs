using IntelexUploadMD.Model.DataSets;

namespace IntelexUploadMD.Services.ExtractHelper.Interface
{
    public interface ISupplierExtractHelper
    {
        Task<SupplierDataSets> GetSupplierDataSet(string endPoint, string Type, string searchString, string username, string password, int ctr, string contractorNo);
        //Task<SupplierDataSets> GetParentSupplierDataSet(string endPoint, string type, string searchString, string Parent, string username, string password);
        //Task<SupplierDataSets> GetChildSupplierDataSet(string endPoint, string type, string searchString, string Parent, string username, string password);
    }
}

