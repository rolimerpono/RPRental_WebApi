using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataServices.Common.RepositoryInterface
{
    public interface IWorker
    {
        IRoomRepository tbl_Rooms { get; }

    }
}
