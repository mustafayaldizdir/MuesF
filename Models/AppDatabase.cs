using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppDatabasesGuide.Models
{
    public class AppDatabase
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedIP { get; set; }

        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public string ModifiedIP { get; set; }

        public int Status { get; set; }

        public ICollection<AppDatabaseTable> AppDatabaseTables() { return MuesF.BLL.MuesBLL<AppDatabaseTable>.GetMany("AppDatabaseId", Id); }
    }
}
