using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.Persistent.BaseImpl;
using DevExpress.XtraLayout;
using System.Windows.Forms;

namespace SolutionWithCustomWindow.Module.Win.Controllers {
    public partial class LinkViewController : ViewController {
        public LinkViewController() {
            SimpleAction showWindowAction = new SimpleAction(this, "Show Window", DevExpress.Persistent.Base.PredefinedCategory.View);
            showWindowAction.ImageName = "ModelEditor_Views";
            showWindowAction.Execute += new SimpleActionExecuteEventHandler(showWindowAction_Execute);
        }
        void showWindowAction_Execute(object sender, SimpleActionExecuteEventArgs e) {
            NonXAFForm form = new NonXAFForm();
            form.Text = "Form with the XAF ListView";
  
            LayoutControl layoutControl = new LayoutControl();
            layoutControl.Dock = System.Windows.Forms.DockStyle.Fill;
            form.Controls.Add(layoutControl);

            LayoutControlItem item1 = layoutControl.Root.AddItem();
            TextBox textBox1 = new TextBox();
            item1.Text = "Company";
            item1.Control = textBox1;

            DevExpress.ExpressApp.View listView = Application.CreateListView(Application.CreateObjectSpace(typeof(Person)), typeof(Person), false);
            listView.CreateControls();
            LayoutControlItem item2 = layoutControl.Root.AddItem();
            item2.Text = "Persons";
            item2.Control = (Control)listView.Control;

            form.ShowDialog();
        }
    }
}
