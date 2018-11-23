using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.XtraLayout;
using SolutionWithCustomWindow.Win;
using System;
using System.Windows.Forms;


namespace WindowsNonXafApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            XpoTypesInfoHelper.GetXpoTypeInfoSource();
            XafTypesInfo.Instance.RegisterEntity(typeof(Person));


            SolutionWithCustomWindowWindowsFormsApplication app = new SolutionWithCustomWindowWindowsFormsApplication();
            IObjectSpace os = app.CreateObjectSpace();
            //  NonXAFForm form = new NonXAFForm();
            this.Text = "Form with the XAF ListView";

            LayoutControl layoutControl = new LayoutControl();
            layoutControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Controls.Add(layoutControl);

            LayoutControlItem item1 = layoutControl.Root.AddItem();
            TextBox textBox1 = new TextBox();
            item1.Text = "Company";
            item1.Control = textBox1;

            DevExpress.ExpressApp.View listView = app.CreateListView(os, typeof(Person), false);
            listView.CreateControls();
            LayoutControlItem item2 = layoutControl.Root.AddItem();
            item2.Text = "Persons";
            item2.Control = (Control)listView.Control;

            this.ShowDialog();

        }
    }
}
