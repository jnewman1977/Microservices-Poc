using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Msvc.IAdapter.Abstraction;

public class MasterAdapterSettings
{
    public string HeaderVersion
    {
        get;
        set;
    }

    public string IAdapterInstitutionId
    {
        get;
        set;
    }

    public bool IsSSL
    {
        get;
        set;
    }

    public string Key
    {
        get;
        set;
    }

    public int Port
    {
        get;
        set;
    }

    public string Server
    {
        get;
        set;
    }

    public string EncryptedKey { get; set; }

    public string ServicePrincipalName { get; set; }
}
