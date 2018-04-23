Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.Actions
Imports DevExpress.Persistent.BaseImpl
Imports DevExpress.XtraLayout
Imports System.Windows.Forms

Namespace SolutionWithCustomWindow.Module.Win.Controllers
    Partial Public Class LinkViewController
        Inherits ViewController

        Public Sub New()
            Dim showWindowAction As New SimpleAction(Me, "Show Window", DevExpress.Persistent.Base.PredefinedCategory.View)
            showWindowAction.ImageName = "ModelEditor_Views"
            AddHandler showWindowAction.Execute, AddressOf showWindowAction_Execute
        End Sub
        Private Sub showWindowAction_Execute(ByVal sender As Object, ByVal e As SimpleActionExecuteEventArgs)
            Dim form As New NonXAFForm()
            form.Text = "Form with the XAF ListView"

            Dim layoutControl As New LayoutControl()
            layoutControl.Dock = System.Windows.Forms.DockStyle.Fill
            form.Controls.Add(layoutControl)

            Dim item1 As LayoutControlItem = layoutControl.Root.AddItem()
            Dim textBox1 As New TextBox()
            item1.Text = "Company"
            item1.Control = textBox1

            Dim listView As DevExpress.ExpressApp.View = Application.CreateListView(Application.CreateObjectSpace(), GetType(Person), False)
            listView.CreateControls()
            Dim item2 As LayoutControlItem = layoutControl.Root.AddItem()
            item2.Text = "Persons"
            item2.Control = DirectCast(listView.Control, Control)

            form.ShowDialog()
        End Sub
    End Class
End Namespace
