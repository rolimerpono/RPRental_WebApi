using Utility;
using static Utility.SD;

namespace RPRENTAL_WEBAPP.Models.DTO
{
    public class APIRequest
    {
        public ApiType ApiType { get; set; } = ApiType.GET;

        public string Url { get; set; }

        public object Data { get; set; }

    }
}
