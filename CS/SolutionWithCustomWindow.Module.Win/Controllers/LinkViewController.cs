using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Win;
using DevExpress.Persistent.BaseImpl;
using DevExpress.XtraLayout;
using System;
using System.Windows.Forms;

namespace SolutionWithCustomWindow.Module.Win.Controllers {
    public partial class LinkViewController : ViewController {
        public LinkViewController() {
            SimpleAction showLVWindowAction = new SimpleAction(this, "Show Persons ListView", DevExpress.Persistent.Base.PredefinedCategory.View);
            showLVWindowAction.ImageName = "ModelEditor_Views";
            showLVWindowAction.Execute += new SimpleActionExecuteEventHandler(showLVWindowAction_Execute);


            SimpleAction showDVWindowAction = new SimpleAction(this, "Show Current DetailView", DevExpress.Persistent.Base.PredefinedCategory.View);
            showDVWindowAction.ImageName = "ModelEditor_Views";
            showDVWindowAction.Execute += new SimpleActionExecuteEventHandler(showDVWindowAction_Execute);

        }

        private void showDVWindowAction_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            NonXAFForm form = new NonXAFForm();
            form.Text = "Form with the XAF DetailView";

            LayoutControl layoutControl = new LayoutControl();
            layoutControl.Dock = System.Windows.Forms.DockStyle.Fill;
            form.Controls.Add(layoutControl);


            IObjectSpace os = Application.CreateObjectSpace();
            var record = os.GetObject(View.CurrentObject);

            if (record == null)
            {
                record = os.CreateObject(View.ObjectTypeInfo.Type);
            }



            //   DevExpress.ExpressApp.DetailView view = Application.CreateDetailView(os, "Person_DetailView", true, record);
            DevExpress.ExpressApp.DetailView view = Application.CreateDetailView(os,  record);
            view.ViewEditMode = DevExpress.ExpressApp.Editors.ViewEditMode.Edit;
            //view.CreateControls();
            //LayoutControlItem item2 = layoutControl.Root.AddItem();
            //item2.Text = "Persons";
            //item2.Control = (Control)view.Control;

            //form.ShowDialog();

            Frame frame = Application.CreateFrame(TemplateContext.NestedFrame);
            frame.CreateTemplate();
            frame.SetView(view);
            LayoutControlItem item2 = new LayoutControlItem();
            item2.Parent = layoutControl.Root;
            item2.Text = "Persons";
            item2.Control = (Control)frame.Template;
            form.ShowDialog();

        }

        void showLVWindowAction_Execute(object sender, SimpleActionExecuteEventArgs e) {
            NonXAFForm form = new NonXAFForm();
            form.Text = "Form with the XAF ListView";
  
            LayoutControl layoutControl = new LayoutControl();
            layoutControl.Dock = System.Windows.Forms.DockStyle.Fill;
            form.Controls.Add(layoutControl);

            LayoutControlItem item1 = layoutControl.Root.AddItem();
            TextBox textBox1 = new TextBox();
            item1.Text = "Company";
            item1.Control = textBox1;

            DevExpress.ExpressApp.View listView = Application.CreateListView(Application.CreateObjectSpace(), typeof(Person), false);
            listView.CreateControls();
            LayoutControlItem item2 = layoutControl.Root.AddItem();
            item2.Text = "Persons";
            item2.Control = (Control)listView.Control;

            form.ShowDialog();
        }
    }
}
