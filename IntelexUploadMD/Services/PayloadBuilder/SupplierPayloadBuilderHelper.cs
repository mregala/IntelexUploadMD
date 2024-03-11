using IntelexUploadMD.Model.DataSets;
using IntelexUploadMD.Model.Request;

namespace IntelexUploadMD.Services.PayloadBuilder
{
    public static class SupplierPayloadBuilderHelper
    {
        public static SupplierCreateRequest BuildSupplierPayload_Create(string id, string inactive, string name, /*int recordNo,*/ string supplierNumber)//(this SubLocation1Request dataSet, string? locationId /*IConfiguration configuration*/)
        {

            var getPayload = new SupplierCreateRequest
            {
                Id = id,
                Inactive = inactive == "inactive" ? true : false,
                Name = name,
                //RecordNo = recordNo,
                ContractorNo = supplierNumber
            };
            return getPayload;
        }

        public static SupplierUpdateRequest BuildSupplierPayload_Update(string id, string inactive, string name, /*int recordNo,*/ string supplierNumber)//(this SubLocation1Request dataSet, string? locationId /*IConfiguration configuration*/)
        {

            var getPayload = new SupplierUpdateRequest
            {
                //Id = id,
                Inactive = inactive == "inactive" ? true : false,
                Name = name,
                //RecordNo = recordNo,
                ContractorNo = supplierNumber
            };
            return getPayload;
        }

        //public static SupplierDeleteRequest BuildSupplierPayload_Delete(this SupplierDataSets dataSet, string name )
        //{

        //    var getPayload = new SupplierDeleteRequest
        //    {

        //        Name = name

        //    };
        //    return getPayload;
        //}
    }
}

