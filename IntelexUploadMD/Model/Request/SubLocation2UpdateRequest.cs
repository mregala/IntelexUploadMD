namespace IntelexUploadMD.Model.Request
{
    public class UpdateSubLocs3
    {
        public string Id { get; set; }
    }

    public class SubLocation2UpdateRequest
    {
        //public string Id { get; set; }
        //public bool Deleted { get; set; }
        public object csInactive { get; set; }
        public string csName { get; set; }
        public UpdateSubLocs3[] SubLocs3 { get; set; }
    }
}
