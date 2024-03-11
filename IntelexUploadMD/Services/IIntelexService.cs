namespace TmsSrvBus2Honeywell.Services
{
    public interface IIntelexService
    {
        Task<List<T>> GetAsync<T>(string oDataFilter, string? selectFields = null, int? top = null, string? orderBy = null, bool? isFreeSpacePayloadSet = false, bool? isLongTextPayloadSet = false);
    }
}
