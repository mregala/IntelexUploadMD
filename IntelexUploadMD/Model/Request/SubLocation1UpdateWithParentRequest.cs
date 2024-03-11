namespace IntelexUploadMD.Model.Request
{
    public class SubLocation1UpdateWithParentRequest
    {
        public object csInactive { get; set; }
        public string csName { get; set; }
        public UpdateWithParentCsMainLocation[] csMainLocation { get; set; }
    }
    public class UpdateWithParentCsMainLocation
    {
        public string Id { get; set; }
    }
}
