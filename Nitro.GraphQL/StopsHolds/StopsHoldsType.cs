using GraphQL.Types;
using Msvc.IAdapter.Abstraction;

namespace Nitro.GraphQL;

public class StopsHoldsType : ObjectGraphType<GetStopsHoldsResponse>
{
    public StopsHoldsType()
    {
        Field(x => x.RecordCount, nullable: false, type: typeof(IntGraphType));
    }
}

public class MasterAdapterSettingsType : InputObjectGraphType<MasterAdapterSettings>
{
    public MasterAdapterSettingsType()
    {
        Field(x => x.EncryptedKey, nullable: false, type: typeof(StringGraphType));
        Field(x => x.ServicePrincipalName, nullable: false, type: typeof(StringGraphType));
        Field(x => x.IAdapterInstitutionId, nullable: false, type: typeof(StringGraphType));
        Field(x => x.Key, nullable: false, type: typeof(StringGraphType));
        Field(x => x.Port, nullable: false, type: typeof(IntGraphType));
        Field(x => x.Server, nullable: false, type: typeof(StringGraphType));
        Field(x => x.HeaderVersion, nullable: false, type: typeof(StringGraphType));
        Field(x => x.IsSSL, nullable: false, type: typeof(BooleanGraphType));
    }
}
