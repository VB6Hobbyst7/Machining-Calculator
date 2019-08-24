Public Class UC_Machine_Matl_Setup
    Implements IUC

    Public Event ControlUpdated(ByVal Sender As Object, ByVal e As ControlValUpdatedEventArgs) Implements IUC.ControlUpdated
    Public Event CalcValueRequest(ByVal Sender As Object, ByVal e As ControlValUpdatedEventArgs) Implements IUC.CalcValueRequest

    Private _Cboxes As New List(Of ComboBox)

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        SetupUI()
    End Sub 'for designer  
    Private Sub SetupUI()
        ComboBox_Machine.Tag = ValMgr.ValIDs.ActiveMachineID
        ComboBox_Material.Tag = ValMgr.ValIDs.ActiveMaterialID

        '--------- Collect all comboboxes -------------

        For Each c As Control In Me.Controls
            If c.GetType Is GetType(ComboBox) Then
                _Cboxes.Add(c)
            End If
        Next
    End Sub


    Public Sub UpdateData(ByVal MachineData As BindingSource, ByVal MatlData As BindingSource)

        Btn_Machine_Add.Tag = MachineData.DataSource 'allows all of the new buttons to be differentiated by class type
        Btn_Machine_Edit.Tag = MachineData.DataSource

        Btn_Matl_Add.Tag = MatlData.DataSource 'allows all of the new buttons to be differentiated by class type
        Btn_Matl_Edit.Tag = MatlData.DataSource


        UpdateComboBox(ComboBox_Machine, MachineData, "Name")
        UpdateComboBox(ComboBox_Material, MatlData, "Name")

    End Sub
    Private Sub UpdateComboBox(ByVal Cbox As ComboBox, ByVal BS As BindingSource, ByVal DispMember As String)
        '--------- Stop event firing when updating
        RemoveHandler Cbox.SelectedIndexChanged, AddressOf ComboBox_SelectedIndexChanged


        Dim ID1 As Integer = Cbox.SelectedValue
        Cbox.DataSource = BS
        Cbox.DisplayMember = DispMember
        Cbox.ValueMember = "ID"
        If ID1 <> Nothing Then
            Cbox.SelectedValue = ID1 'try to update to previous selected value if possible
        End If

        're-enable events
        AddHandler Cbox.SelectedIndexChanged, AddressOf ComboBox_SelectedIndexChanged

    End Sub

    Private Sub ComboBox_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            Dim cBox As ComboBox = sender
            Dim ID As ValMgr.ValIDs = CType(cBox.Tag, ValMgr.ValIDs) 'get type of object for combobox

            RaiseEvent ControlUpdated(Me, New ControlValUpdatedEventArgs(ID, cBox.SelectedValue))
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try      
    End Sub 'raises event


    '-------------- THESE ARE DISABLED FOR NOW -------------------
    Private Sub Btn_Add_Click(sender As Object, e As EventArgs) Handles Btn_Machine_Add.Click, Btn_Matl_Add.Click
        Try
            Dim btn As Button = sender
            Dim ItemType As Type = btn.Tag
            'RaiseEvent ControlUpdated(Me, New ControlValUpdatedEventArgs(IUC.ControlIDs.Btn_New, 1, ItemType))
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try   
    End Sub
    Private Sub Btn_Edit_Click(sender As Object, e As EventArgs) Handles Btn_Matl_Edit.Click, Btn_Machine_Edit.Click
        Try
            Dim btn As Button = sender

            For Each Cbox As ComboBox In _Cboxes
                If Cbox.Tag = btn.Tag Then 'check if they have the same types

                    Dim ItemType As Type = btn.Tag
                    'RaiseEvent ControlUpdated(Me, New ControlValUpdatedEventArgs(IUC.ControlIDs.Btn_Edit, Cbox.SelectedValue, ItemType))
                End If
            Next

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub


End Class

