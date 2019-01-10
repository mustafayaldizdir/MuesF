using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuesF.Base
{
    public class MuesJoin<TSource,TTarget>
    {
        public TSource Source { get; set; }
        public TTarget Target { get; set; }
    }
}
