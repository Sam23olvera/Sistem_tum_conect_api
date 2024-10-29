using static ConectDB.Models.LogUser;
using System.Xml.Linq;

namespace ConectDB.Models
{
    public class Elements
    {
        public string property { get; set; }
        public string value { get; set; }
    }
    public class RootData
    {
        private Data zvarcioData = new Data();
        private List<Elements> zvarfilter = new List<Elements>();
        public Data data
        {

            get { return zvarcioData; }

            set { zvarcioData = value; }

        }
        public List<Elements> filter
        {
            get
            {
                return zvarfilter;
            }
            set
            {
                zvarfilter = value;
            }
        }
    }
}
