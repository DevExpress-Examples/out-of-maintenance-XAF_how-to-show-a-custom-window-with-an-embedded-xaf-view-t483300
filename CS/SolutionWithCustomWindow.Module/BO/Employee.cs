using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Security;
using DevExpress.Persistent.Validation;
using DevExpress.Persistent.Base.Security;
using System.ComponentModel;
using DevExpress.ExpressApp.Utils;
using DevExpress.ExpressApp;
using DevExpress.Data.Filtering;


namespace Enimo.Module.BusinessObjects {
    [DefaultClassOptions]
    public class Employee : Person, ISecurityUser,
        IAuthenticationStandardUser, IAuthenticationActiveDirectoryUser,
        ISecurityUserWithRoles, IPermissionPolicyUser//, ICanInitialize
    {
        public Employee(Session session)
            : base(session) { }
        [Association("Employee-Task")]
        public XPCollection<EmployeeTask> OwnTasks {
            get { return GetCollection<EmployeeTask>("OwnTasks"); }
        }

        #region ISecurityUser Members
        private string userName = String.Empty;
        [RuleRequiredField("EmployeeUserNameRequired", DefaultContexts.Save)]
        [RuleUniqueValue("EmployeeUserNameIsUnique", DefaultContexts.Save,
            "The login with the entered user name was already registered within the system.")]
        public string UserName {
            get { return userName; }
            set { SetPropertyValue("UserName", ref userName, value); }
        }
        private bool isActive = true;
        public bool IsActive {
            get { return isActive; }
            set { SetPropertyValue("IsActive", ref isActive, value); }
        }
        #endregion

        #region IAuthenticationStandardUser Members
        private bool changePasswordOnFirstLogon;
        public bool ChangePasswordOnFirstLogon {
            get { return changePasswordOnFirstLogon; }
            set {
                SetPropertyValue("ChangePasswordOnFirstLogon", ref changePasswordOnFirstLogon, value);
            }
        }
        private string storedPassword;
        [Browsable(false), Size(SizeAttribute.Unlimited), Persistent, SecurityBrowsable]
        protected string StoredPassword {
            get { return storedPassword; }
            set { storedPassword = value; }
        }
        public bool ComparePassword(string password) {
            return PasswordCryptographer.VerifyHashedPasswordDelegate(this.storedPassword, password);
        }
        public void SetPassword(string password) {
            this.storedPassword = PasswordCryptographer.HashPasswordDelegate(password);
            OnChanged("StoredPassword");
        }
        #endregion

        
        #region ISecurityUserWithRoles Members
        IList<ISecurityRole> ISecurityUserWithRoles.Roles {
            get {
                IList<ISecurityRole> result = new List<ISecurityRole>();
                foreach (EmployeeRole role in EmployeeRoles) {
                    result.Add(role);
                }
                return result;
            }
        }
        #endregion

        [Association("Employees-EmployeeRoles")]
        [RuleRequiredField("EmployeeRoleIsRequired", DefaultContexts.Save,
            TargetCriteria = "IsActive",
            CustomMessageTemplate = "An active employee must have at least one role assigned")]
        public XPCollection<EmployeeRole> EmployeeRoles {
            get {
                return GetCollection<EmployeeRole>("EmployeeRoles");
            }
        }

        #region IPermissionPolicyUser Members
        IEnumerable<IPermissionPolicyRole> IPermissionPolicyUser.Roles {
            get { return EmployeeRoles.OfType<IPermissionPolicyRole>(); }
        }
        #endregion

        //#region IOperationPermissionProvider Members
        //IEnumerable<IOperationPermission> IOperationPermissionProvider.GetPermissions()
        //{
        //    return new IOperationPermission[0];
        //}
        //IEnumerable<IOperationPermissionProvider> IOperationPermissionProvider.GetChildren()
        //{
        //    return new EnumerableConverter<IOperationPermissionProvider, EmployeeRole>(EmployeeRoles);
        //}
        //#endregion

        #region ICanInitialize Members
        //void ICanInitialize.Initialize(IObjectSpace objectSpace, SecurityStrategyComplex security) {
        //    EmployeeRole newUserRole = (EmployeeRole)objectSpace.FindObject<EmployeeRole>(
        //        new BinaryOperator("Name", security.NewUserRoleName));
        //    if (newUserRole == null) {
        //        newUserRole = objectSpace.CreateObject<EmployeeRole>();
        //        newUserRole.Name = security.NewUserRoleName;
        //        newUserRole.IsAdministrative = true;
        //        newUserRole.Employees.Add(this);
        //    }
        //}
        #endregion

        #region Actions
       // [Action(Caption ="Przepisz security")]
       //public void PrzepiszSecurity()
       // {
       //     SecurityUtil su = new SecurityUtil();
       //     su.CopySecurity2Employees(this.Session );
       // }

        #endregion

    }
}
