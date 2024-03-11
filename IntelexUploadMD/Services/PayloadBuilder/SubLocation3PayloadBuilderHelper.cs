using IntelexUploadMD.Model.DataSets;
using IntelexUploadMD.Model.Request;

namespace IntelexUploadMD.Services.PayloadBuilder
{
    public static class SubLocation3PayloadBuilderHelper
    {

        public static SubLocation3UpdateWithParentRequest BuildSubLocation3Payload_Update(this AssetDataSets dataSet, string? subLocation1Id, string name/*, IConfiguration configuration*/)
        {
            var subLocation2Id = dataSet.EHSIncidentMang_csSubLocation3ObjectSetResult.value.FirstOrDefault().Id;
            var subLocation3csName = dataSet.EHSIncidentMang_csSubLocation3ObjectSetResult.value.FirstOrDefault().csName;
            var subLocation3csInactive = dataSet.EHSIncidentMang_csSubLocation3ObjectSetResult.value.FirstOrDefault().csInactive;
            //var subLocation1CsSubLocs2 = dataSet.EHSIncidentMang_csSubLocation1ObjectSetResult.value.FirstOrDefault().csSubLocs2.Count;

                var getPayload = new SubLocation3UpdateWithParentRequest
                {
                    //Id = subLocation1Id,
                    csName = name,//subLocation3csName,//Name,
                    csInactive = subLocation3csInactive,//csInactive,s

                };
  
            return getPayload;
        }

        public static SubLocation3CreateWithParentRequest BuildSubLocation3Payload_Create(string Id, string Name, string csInactive, string subLocation1Id)//(this SubLocation1Request dataSet, string? locationId /*IConfiguration configuration*/)
        {
            var getPayload = new SubLocation3CreateWithParentRequest
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

        public static SubLocation3UpdateRequest BuildSubLocation3Payload_UpdateParent(this AssetDataSets dataSet, string? subLocation1Id/*, IConfiguration configuration*/)
        {
            var subLocation2Id = dataSet.EHSIncidentMang_csSubLocation3ObjectSetResult.value.FirstOrDefault().Id;
            var subLocation3csName = dataSet.EHSIncidentMang_csSubLocation3ObjectSetResult.value.FirstOrDefault().csName;
            var subLocation3csInactive = dataSet.EHSIncidentMang_csSubLocation3ObjectSetResult.value.FirstOrDefault().csInactive;
            //var subLocation1CsSubLocs2 = dataSet.EHSIncidentMang_csSubLocation1ObjectSetResult.value.FirstOrDefault().csSubLocs2.Count;

            //if (subLocation1CsSubLocs2 > 0)
            //{
            var getPayload = new SubLocation3UpdateRequest
            {
                //Id = subLocation1Id,//"test",//"test",//rowGuid.ToString(), // Assuming rowGuid is of type Guid
                //Deleted = false,
                csName = subLocation3csName,//Name,
                csInactive = subLocation3csInactive,//csInactive,s

            };

            return getPayload;
        }

    }

}

