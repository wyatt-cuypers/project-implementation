using ECOMap.Models;

namespace ECOMap
{
    public class ESRIClient
    {
        private readonly State state;
        public ESRIClient(State state)
        {
            RequestParamsList = new List<ESRIRequestParams>();
            this.state = state;
        }

        public ICollection<ESRIRequestParams> RequestParamsList
        {
            get;
            private set;
        }
    }
}