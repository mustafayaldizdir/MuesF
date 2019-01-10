using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuesF.Base
{
    public enum OrderStatus
    {
        SiparisBeklemede = 0,
        SiparisOnaylandi = 1,
        KargoyaVerildi = 2,
        İptalEdildi = 3,
        İadeEdildi=4,
        TeslimEdildi = 5
    }
}
