using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Common;

public class PaginatedCommand
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}
