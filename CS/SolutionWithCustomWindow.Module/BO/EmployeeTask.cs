using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.SystemModule;

namespace Enimo.Module.BusinessObjects {
    [DefaultClassOptions, ImageName("BO_Task")]
    [ListViewFilter("Wszystkie zadania", "")]
    [ListViewFilter("Moje zadania", "[Owner.Oid] = CurrentUserId()")]
    public class EmployeeTask : Task {
        public EmployeeTask(Session session)
            : base(session) { }
        private Employee owner;
        [Association("Employee-Task")]
        public Employee Owner {
            get {

                return owner; }
            set { SetPropertyValue("Owner", ref owner, value); }
        }
        
    }
}
