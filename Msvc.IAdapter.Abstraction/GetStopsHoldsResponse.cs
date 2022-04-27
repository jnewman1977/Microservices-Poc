using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msvc.IAdapter.Abstraction;

public class GetStopsHoldsResponse
{
    public bool Success { get; set; }

    public string[] Errors { get; set; }

    public int RecordCount { get; set; }
}
