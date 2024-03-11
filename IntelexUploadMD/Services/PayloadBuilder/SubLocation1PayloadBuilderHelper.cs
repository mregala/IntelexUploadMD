using IntelexUploadMD.Model.DataSets;
using IntelexUploadMD.Model.Request;

namespace IntelexUploadMD.Services.PayloadBuilder
{
    public static class SubLocation1PayloadBuilderHelper
    {
        public static SubLocation1UpdateRequest BuildSubLocation1Payload_Update(this AssetDataSets dataSet, string name/*, IConfiguration configuration*/)
        {
            var subLocation1Id = dataSet.EHSIncidentMang_csSubLocation1ObjectSetResult.value.FirstOrDefault().Id;
            var subLocation1csName = name;//= dataSet.EHSIncidentMang_csSubLocation1ObjectSetResult.value.FirstOrDefault().csName;
            var subLocation1csInactive = dataSet.EHSIncidentMang_csSubLocation1ObjectSetResult.value.FirstOrDefault().csInactive;
            //var subLocation1CsSubLocs2 = dataSet.EHSIncidentMang_csSubLocation1ObjectSetResult.value.FirstOrDefault().csSubLocs2.Count;

            //if (subLocation1CsSubLocs2 > 0)
            //{
            var getPayload = new SubLocation1UpdateRequest
            {
                //Id = subLocation1Id,
                csName = subLocation1csName,//Name,
                csInactive = subLocation1csInactive,//csInactive,s
            };

            return getPayload;
        }
        public static SubLocation1UpdateWithParentRequest BuildSubLocation1Payload_UpdateParent(this AssetDataSets dataSet, string? locationId, string name/*, IConfiguration configuration*/)
        {
            var subLocation1Id = dataSet.EHSIncidentMang_csSubLocation1ObjectSetResult.value.FirstOrDefault().Id;
            var subLocation1csName = name;//= dataSet.EHSIncidentMang_csSubLocation1ObjectSetResult.value.FirstOrDefault().csName;
            var subLocation1csInactive = dataSet.EHSIncidentMang_csSubLocation1ObjectSetResult.value.FirstOrDefault().csInactive;
            //var subLocation1CsSubLocs2 = dataSet.EHSIncidentMang_csSubLocation1ObjectSetResult.value.FirstOrDefault().csSubLocs2.Count;

            //if (subLocation1CsSubLocs2 > 0)
            //{
                var getPayload = new SubLocation1UpdateWithParentRequest
                {
                    //Id = subLocation1Id,
                    csName = subLocation1csName,//Name,
                    csInactive = subLocation1csInactive,//csInactive,s
                    csMainLocation = new Model.Request.UpdateWithParentCsMainLocation[]//List<CsMainLocation>
                    {
                    new Model.Request.UpdateWithParentCsMainLocation
                        {
                            Id = locationId //parent
                        }
                    }
                };
  
            return getPayload;
        }

        public static SubLocation1CreateWithParentRequest BuildSubLocation1Payload_Create(string Id, string Name, string csInactive, string locationId)//(this SubLocation1Request dataSet, string? locationId /*IConfiguration configuration*/)
        {
            var getPayload = new SubLocation1CreateWithParentRequest
            {
                Id = Id,
                csName = Name,
                csInactive = null,
                csMainLocation = new Model.Request.CreateWithParentCsMainLocation[]
                {
                        new Model.Request.CreateWithParentCsMainLocation
                        {
                            Id = locationId //parent
                        }
                }
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

        public static SubLocation1UpdateWithChildRequest BuildSubLocation1Payload_UpdateChild(this AssetDataSets dataSet, string? locationId/*, IConfiguration configuration*/)
        {
            var subLocation1Id = dataSet.EHSIncidentMang_csSubLocation1ObjectSetResult.value.FirstOrDefault().Id;
            var subLocation1csName = dataSet.EHSIncidentMang_csSubLocation1ObjectSetResult.value.FirstOrDefault().csName;
            var subLocation1csInactive = dataSet.EHSIncidentMang_csSubLocation1ObjectSetResult.value.FirstOrDefault().csInactive;
            //var subLocation1CsSubLocs2 = dataSet.EHSIncidentMang_csSubLocation1ObjectSetResult.value.FirstOrDefault().csSubLocs2.Count;

            //if (subLocation1CsSubLocs2 > 0)
            //{
            var getPayload = new SubLocation1UpdateWithChildRequest
            {
                //Id = subLocation1Id,//"test",//"test",//rowGuid.ToString(), // Assuming rowGuid is of type Guid
                //Deleted = false,
                csName = subLocation1csName,//Name,
                csInactive = subLocation1csInactive,//csInactive,s
                //csMainLocation = new Model.Request.UpdateCsMainLocation[]//List<CsMainLocation>
                //{
                //    new Model.Request.UpdateCsMainLocation
                //        {
                //            Id = locationId //parent
                //        }
                //},
                csSubLocs2 = new Model.Request.UpdateWithChildCsSubLocs2[]
                {
                    new Model.Request.UpdateWithChildCsSubLocs2
                        {
                            Id = locationId
                        }
                }
            };

            return getPayload;
        }

    }

}

