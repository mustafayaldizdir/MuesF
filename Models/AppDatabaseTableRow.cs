using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppDatabasesGuide.Models
{
    public class AppDatabaseTableRow
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsAllowNull { get; set; }
        public string RowType { get; set; }
        public int AppDatabaseTableId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedIP { get; set; }

        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public string ModifiedIP { get; set; }

        public int Status { get; set; }

        protected AppDatabaseTable appDatabaseTable; public virtual AppDatabaseTable AppDatabaseTable { get { return MuesF.BLL.MuesBLL<AppDatabaseTable>.GetTo(AppDatabaseTableId); } set { value = appDatabaseTable; } }
       
    }
}
