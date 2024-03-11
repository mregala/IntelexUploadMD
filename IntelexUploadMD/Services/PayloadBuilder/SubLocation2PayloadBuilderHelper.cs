using IntelexUploadMD.Model.DataSets;
using IntelexUploadMD.Model.Request;

namespace IntelexUploadMD.Services.PayloadBuilder
{
    public static class SubLocation2PayloadBuilderHelper
    {

        public static SubLocation2UpdateWithParentRequest BuildSubLocation2Payload_Update(this AssetDataSets dataSet, string? subLocation1Id, string name/*, IConfiguration configuration*/)
        {
            var subLocation2Id = dataSet.EHSIncidentMang_csSubLocation2ObjectSetResult.value.FirstOrDefault().Id;
            var subLocation2csName = name;//= dataSet.EHSIncidentMang_csSubLocation2ObjectSetResult.value.FirstOrDefault().csName;
            var subLocation2csInactive = dataSet.EHSIncidentMang_csSubLocation2ObjectSetResult.value.FirstOrDefault().csInactive;
            //var subLocation1CsSubLocs2 = dataSet.EHSIncidentMang_csSubLocation1ObjectSetResult.value.FirstOrDefault().csSubLocs2.Count;

            //if (subLocation1CsSubLocs2 > 0)
            //{
                var getPayload = new SubLocation2UpdateWithParentRequest
                {
                    //Id = subLocation1Id,
                    csName = subLocation2csName,//Name,
                    csInactive = subLocation2csInactive,//csInactive,s
                    //subLocs3 = new Model.Request.UpdateWithParentSubLocs3[]//List<CsMainLocation>
                    //{
                    //new Model.Request.UpdateWithParentSubLocs3
                    //    {
                    //        Id = subLocation1Id //parent
                    //    }
                    //}
                };
  
            return getPayload;
        }

        public static SubLocation2CreateWithParentRequest BuildSubLocation2Payload_Create(string Id, string Name, string csInactive, string subLocation1Id)//(this SubLocation1Request dataSet, string? locationId /*IConfiguration configuration*/)
        {
            var getPayload = new SubLocation2CreateWithParentRequest
            {
                Id = Id,
                csName = Name,
                csInactive = null,
                //subLocs3 = new Model.Request.CreateWithParentSubLocs3[]
                //{
                //        new Model.Request.CreateWithParentSubLocs3
                //        {
                //            Id = subLocation1Id //parent
                //        }
                //}
                //csSubLocs2 = new Model.Request.CsSubLocs2[]//List<CsMainLocation>
                //{
                //        new Model.Request.CsSubLocs2
                //        {
                //            Id = null //child
                //        }
                //}

            };

            return getPayload;
        }

        public static SubLocation2UpdateRequest BuildSubLocation2Payload_UpdateParent(this AssetDataSets dataSet, string? subLocation3Id/*, IConfiguration configuration*/)
        {
            //var subLocation2Id = dataSet.EHSIncidentMang_csSubLocation2ObjectSetResult.value.FirstOrDefault().Id;
            var subLocation2csName = dataSet.EHSIncidentMang_csSubLocation2ObjectSetResult.value.FirstOrDefault().csName;
            var subLocation2csInactive = dataSet.EHSIncidentMang_csSubLocation2ObjectSetResult.value.FirstOrDefault().csInactive;
            //var subLocation1CsSubLocs2 = dataSet.EHSIncidentMang_csSubLocation1ObjectSetResult.value.FirstOrDefault().csSubLocs2.Count;

            //if (subLocation1CsSubLocs2 > 0)
            //{
            var getPayload = new SubLocation2UpdateRequest
            {
                //Id = subLocation1Id,//"test",//"test",//rowGuid.ToString(), // Assuming rowGuid is of type Guid
                //Deleted = false,
                csName = subLocation2csName,//Name,
                csInactive = subLocation2csInactive,//csInactive,s
                //csMainLocation = new Model.Request.UpdateCsMainLocation[]//List<CsMainLocation>
                //{
                //    new Model.Request.UpdateCsMainLocation
                //        {
                //            Id = locationId //parent
                //        }
                //},
                SubLocs3 = new Model.Request.UpdateSubLocs3[]
                {
                    new Model.Request.UpdateSubLocs3
                        {
                            Id = subLocation3Id
                        }
                }
            };

            return getPayload;
        }

    }

}

