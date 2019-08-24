Public Class UC_Tool_Setup

    Implements IUC

    Public Event CalcValueRequest(Sender As Object, e As ControlValUpdatedEventArgs) Implements IUC.CalcValueRequest
    Public Event ControlUpdated(Sender As Object, e As ControlValUpdatedEventArgs) Implements IUC.ControlUpdated


    Private _Cboxes As New List(Of ComboBox)

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        SetupUI()

    End Sub 'for designer
    Private Sub SetupUI()

        ComboBox_Tool.Tag = ValMgr.ValIDs.ActiveToolID 'link it to the value


        '--------- Collect all comboboxes -------------

        For Each c As Control In Me.Controls
            If c.GetType Is GetType(ComboBox) Then
                _Cboxes.Add(c)
            End If
        Next

    End Sub

    Public Sub UpdateData(ByVal ToolData As BindingSource)
        Btn_Tool_Add.Tag = ToolData.DataSource 'allows all of the new buttons to be differentiated by class type
        Btn_Tool_Edit.Tag = ToolData.DataSource

        UpdateComboBox(ComboBox_Tool, ToolData, "Name")
    End Sub
    Private Sub UpdateComboBox(ByVal Cbox As ComboBox, ByVal BS As BindingSource, ByVal DispMember As String)
        '--------- Stop event firing when updating
        RemoveHandler Cbox.SelectedIndexChanged, AddressOf ComboBox_SelectedIndexChanged


        Dim ID1 As Integer = Cbox.SelectedValue 'get id of currently selected
        Cbox.DataSource = BS
        Cbox.DisplayMember = DispMember 'property of class visually display in Cbox
        Cbox.ValueMember = "ID" 'property of class to use for Cbox.SelectedValue
        If ID1 <> Nothing Then
            Cbox.SelectedValue = ID1 'try to update to previous selected value if possible
        End If

        're-enable events
        AddHandler Cbox.SelectedIndexChanged, AddressOf ComboBox_SelectedIndexChanged

    End Sub

    Private Sub ComboBox_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            Dim cBox As ComboBox = sender
            Dim ID As ValMgr.ValIDs = CInt(cBox.Tag) 'get the related value ID (stored in the tag)

            RaiseEvent ControlUpdated(Me, New ControlValUpdatedEventArgs(ID, cBox.SelectedValue))
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub 'raises event
    Private Sub Btn_Add_Click(sender As Object, e As EventArgs) Handles Btn_Tool_Add.Click
        Try
            Dim btn As Button = sender
            Dim ItemType As Type = btn.Tag
            'RaiseEvent ControlUpdated(Me, New ControlValUpdatedEventArgs(IUC.ControlIDs.Btn_New, 1, ItemType))
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub 'DISABLED
    Private Sub Btn_Matl_Edit_Click(sender As Object, e As EventArgs) Handles Btn_Tool_Edit.Click
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
    End Sub 'DISABLED

    
End Class
