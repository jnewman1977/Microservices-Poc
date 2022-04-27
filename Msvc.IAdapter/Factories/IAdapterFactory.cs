using JackHenry.Banking.iAdapter;
using JackHenry.JHAContractTypes;

using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msvc.IAdapter.Factories;

public class IAdapterFactory : IIAdapterFactory
{
    public IIAdapterService Build(MasterAdapterSetting adapter)
    {
        return new IAdapterService(adapter);
    }
}


public interface IIAdapterFactory
{
    IIAdapterService Build(MasterAdapterSetting adapter);
}
