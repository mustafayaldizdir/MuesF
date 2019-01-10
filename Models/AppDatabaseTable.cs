using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppDatabasesGuide.Models
{
    public class AppDatabaseTable
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int AppDatabaseId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedIP { get; set; }

        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public string ModifiedIP { get; set; }

        public int Status { get; set; }

        
        protected AppDatabase appDatabase; public virtual AppDatabase AppDatabase { get { return MuesF.BLL.MuesBLL<AppDatabase>.GetTo(AppDatabaseId); } set { value = appDatabase; } }

        public ICollection<AppDatabaseTableRow> AppDatabaseTableRows() { return MuesF.BLL.MuesBLL<AppDatabaseTableRow>.GetMany("AppDatabaseTableId", Id); }
    }
}
