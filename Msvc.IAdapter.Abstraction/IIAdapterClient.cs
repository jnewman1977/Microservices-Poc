using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msvc.IAdapter.Abstraction;

public interface IIAdapterClient
{
    Task<GetStopsHoldsResponse> GetStopsHoldsAsync(GetStopsHoldsRequest request, CancellationToken cancellationToken);
}
