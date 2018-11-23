using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Security.Strategy;

namespace SolutionWithCustomWindow.Win {
    partial class SolutionWithCustomWindowWindowsFormsApplication {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.module1 = new DevExpress.ExpressApp.SystemModule.SystemModule();
            this.module2 = new DevExpress.ExpressApp.Win.SystemModule.SystemWindowsFormsModule();
            this.module3 = new SolutionWithCustomWindow.Module.SolutionWithCustomWindowModule();
            this.module4 = new SolutionWithCustomWindow.Module.Win.SolutionWithCustomWindowWindowsFormsModule();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // SolutionWithCustomWindowWindowsFormsApplication
            // 
            this.ApplicationName = "SolutionWithCustomWindow";
            this.CheckCompatibilityType = DevExpress.ExpressApp.CheckCompatibilityType.DatabaseSchema;
            this.Modules.Add(this.module1);
            this.Modules.Add(this.module2);
            this.Modules.Add(this.module3);
            this.Modules.Add(this.module4);
            this.UseOldTemplates = false;
            this.DatabaseVersionMismatch += new System.EventHandler<DevExpress.ExpressApp.DatabaseVersionMismatchEventArgs>(this.SolutionWithCustomWindowWindowsFormsApplication_DatabaseVersionMismatch);
            this.CustomizeLanguagesList += new System.EventHandler<DevExpress.ExpressApp.CustomizeLanguagesListEventArgs>(this.SolutionWithCustomWindowWindowsFormsApplication_CustomizeLanguagesList);

            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.ExpressApp.SystemModule.SystemModule module1;
        private DevExpress.ExpressApp.Win.SystemModule.SystemWindowsFormsModule module2;
        private SolutionWithCustomWindow.Module.SolutionWithCustomWindowModule module3;
        private SolutionWithCustomWindow.Module.Win.SolutionWithCustomWindowWindowsFormsModule module4;
    }
    public class AuthenticationStandardForTests : AuthenticationStandard
    {
        public override bool AskLogonParametersViaUI
        {
            get => false;
            // get { return string.IsNullOrEmpty(Program.LogonUserName); }
        }
        public override object Authenticate(IObjectSpace objectSpace)
        {
            //if (!string.IsNullOrEmpty(Program.LogonUserName))
            //{
            return objectSpace.FindObject<SecuritySystemUser>(new DevExpress.Data.Filtering.BinaryOperator("UserName", "wingabos\\jkosinki"));
            // }
         //   return base.Authenticate(objectSpace);
        }
    }


}
