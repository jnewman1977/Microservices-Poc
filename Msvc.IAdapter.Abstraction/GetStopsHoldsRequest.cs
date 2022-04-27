namespace Msvc.IAdapter.Abstraction;

public class GetStopsHoldsRequest
{
    public string RequestXml { get; set; }

    public MasterAdapterSettings? MasterAdapterSettings { get; set; }

    public string InstitutionId { get; set; }
}
