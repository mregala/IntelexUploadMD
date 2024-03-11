namespace IntelexUploadMD.Model.Request
{
    public class SubLocation1CreateWithParentRequest
    {
        public string Id { get; set; }
        //public bool Deleted { get; set; }
        public object csInactive { get; set; }
        public string csName { get; set; }
        public CreateWithParentCsMainLocation[] csMainLocation { get; set; }
        //public CreateCsSubLocs2[] csSubLocs2 { get; set; }
    }
    public class CreateWithParentCsMainLocation
    {
        public string Id { get; set; }
    }
    //public class CreateCsSubLocs2
    //{
    //    public string Id { get; set; }
    //}
}
