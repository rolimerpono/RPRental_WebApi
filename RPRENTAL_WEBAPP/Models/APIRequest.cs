
using static DataServices.Common.Static.SD;

namespace RPRENTAL_WEBAPP.Models
{
    public class APIRequest
    {
        public ApiType ApiType { get; set; } = ApiType.GET;

        public string Url { get;set; }

        public object Data { get; set; }


    }
}
