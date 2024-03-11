using IntelexUploadMD.Model.DataSets;

namespace IntelexUploadMD.Services.ExtractHelper.Interface
{
    public interface IAssetExtractHelper
    {
        Task<AssetDataSets> GetAssetDataSet(string endPoint,string type, string searchString, string Parent, string username, string password);
        Task<AssetDataSets> GetParentAssetDataSet(string endPoint, string type, string searchString, string Parent, string username, string password);
        Task<AssetDataSets> GetChildAssetDataSet(string endPoint, string type, string searchString, string Parent, string username, string password);
    }
}

