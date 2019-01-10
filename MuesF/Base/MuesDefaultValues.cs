using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MuesF.Base
{
    public class MuesDefaultValues
    {

        public DateTime CreatedDate
        {
            get { return DateTime.Now; }
        }

        public string CreatedIP
        {
            get { return "::1"; }

        }
        public string CreatedBy
        {
            get { return "Admin"; }

        }

        public DateTime ModifiedDate
        {
            get { return DateTime.Now; }
        }


        public string ModifiedIP
        {
            get { return "::1"; }

        }


        public string ModifiedBy
        {
            get { return "Admin"; }

        }

        public int Status
        {
            get { return 0; }
        }
    }
}
