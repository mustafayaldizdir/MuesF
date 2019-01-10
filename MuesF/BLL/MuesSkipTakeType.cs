using MuesF.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuesF.BLL
{
    public class MuesSkipTakeType : MuesBLLExtensions, IMuesSkipTake, IMuesPaging, IMuesBLL
    {
        private string _value;
        public string Value
        {
            get { return _value; }
            set { _value = value; }
        }
        private string expression;
        public string Expression
        {
            get { return expression; }
            set { expression = value; }
        }
        private string pagingValue;
        public string PagingValue
        {
            get { return pagingValue; }
            set { pagingValue = value; }
        }
        private string orderByValue;
        public string OrderByValue
        {
            get { return orderByValue; }
            set { orderByValue = value; }
        }
        private string selectValue;
        public string SelectValue
        {
            get { return selectValue; }
            set { selectValue = value; }
        }
    }
}
