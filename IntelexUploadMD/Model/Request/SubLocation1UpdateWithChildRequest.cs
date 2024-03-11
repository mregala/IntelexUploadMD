namespace IntelexUploadMD.Model.Request
{
    public class SubLocation1UpdateWithChildRequest
    {
        //public string Id { get; set; }
        //public bool Deleted { get; set; }
        public object csInactive { get; set; }
        public string csName { get; set; }
        //public UpdateCsMainLocation[] csMainLocation { get; set; }
        public UpdateWithChildCsSubLocs2[] csSubLocs2 { get; set; }

    }

    //public class UpdateCsMainLocation
    //{
    //    public string Id { get; set; }
    //}

    public class UpdateWithChildCsSubLocs2
    {
        public string Id { get; set; }
    }

}
