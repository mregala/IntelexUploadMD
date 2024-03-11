namespace IntelexUploadMD.Model.Request
{
    public class SupplierCreateRequest
    {
        public string Id { get; set; }
        public bool? Inactive { get; set; }
        public object Name { get; set; }
        public string ContractorNo { get; set; }
    }

    public class SupplierUpdateRequest
    {
        public bool? Inactive { get; set; }
        public object Name { get; set; }
        public string ContractorNo { get; set; }
    }

    public class SupplierDeleteRequest
    {
        public object Name { get; set; }

    }

}


//Id, Inactive, Name, RecordNo, ContractorNo