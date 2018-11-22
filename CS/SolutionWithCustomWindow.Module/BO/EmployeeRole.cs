using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.ExpressApp.Security.Strategy;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;

using DevExpress.Persistent.BaseImpl.PermissionPolicy;
using DevExpress.ExpressApp.DC;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace Enimo.Module.BusinessObjects {
    [ImageName("BO_Role")]
    public class EmployeeRole : PermissionPolicyRoleBase, IPermissionPolicyRoleWithUsers {
        public EmployeeRole(Session session)
            : base(session) {
        }
        [Association("Employees-EmployeeRoles")]
        public XPCollection<Employee> Employees {
            get {
                return GetCollection<Employee>("Employees");
            }
        }
        IEnumerable<IPermissionPolicyUser> IPermissionPolicyRoleWithUsers.Users {
            get { return Employees.OfType<IPermissionPolicyUser>(); }
        } 
    }


    [DomainComponent, Serializable]
    [System.ComponentModel.DisplayName("Log On")]
    public class CustomLogonParameters : INotifyPropertyChanged, ISerializable
    {

        private Employee employee;
        private string password;


        [DataSourceProperty("Company.Employees"), ImmediatePostData]
        public Employee Employee
        {
            get { return employee; }
            set
            {

                employee = value;
                if (employee != null)
                {
                    UserName = employee.UserName;
                }
                OnPropertyChanged("Employee");
            }
        }
        public CustomLogonParameters() { }
        // ISerializable  
        public CustomLogonParameters(SerializationInfo info, StreamingContext context)
        {
            if (info.MemberCount > 0)
            {
                UserName = info.GetString("UserName");
                Password = info.GetString("Password");
            }
        }
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        [System.Security.SecurityCritical]
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("UserName", UserName);
            info.AddValue("Password", Password);
        }
        [Browsable(false)]
        public String UserName { get; set; }
        [PasswordPropertyText(true)]
        public string Password
        {
            get { return password; }
            set
            {
                if (password == value) return;
                password = value;
            }
        }
    }

    public static class StartValues
    {

        public static string param1 { get; set; }
        public static string param2 { get; set; }
        public static string param3 { get; set; }
        public static int  nrKlienta { get; set; }
    }

}
