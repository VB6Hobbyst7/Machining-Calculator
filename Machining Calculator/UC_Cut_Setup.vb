Public Class UC_Cut_Setup
    Implements IUC

    Private WithEvents _TXT_WOC As LabelledNumTxtBox
    Private WithEvents _TXT_DOC As LabelledNumTxtBox
    Private _TxtList As New List(Of LabelledNumTxtBox)

    Public Event CalcValueRequest(Sender As Object, e As ControlValUpdatedEventArgs) Implements IUC.CalcValueRequest
    Public Event ControlUpdated(Sender As Object, e As ControlValUpdatedEventArgs) Implements IUC.ControlUpdated

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.

        SetupUI()
    End Sub
    Public Sub SetTxtValue(ByVal ValID As Integer, ByVal Value As Double)
        For Each txt As LabelledNumTxtBox In _TxtList
            Dim txt_ID As Integer = txt.Tag

            If txt_ID = ValID Then
                txt.Value = Value
            End If
        Next
    End Sub

    Private Sub SetupUI()
        _TXT_DOC = New LabelledNumTxtBox("DOC", New Point(10, Panel_header.Height + 5), Units.DataUnitType.Length, Units.AllUnits.mm, Me, ValMgr.ValIDs.DOC)
        _TXT_WOC = New LabelledNumTxtBox("WOC", New Point(10, Panel_header.Height + _TXT_DOC.Height + 10), Units.DataUnitType.Length, Units.AllUnits.mm, Me, ValMgr.ValIDs.WOC)


        _TXT_DOC.Tag = ValMgr.ValIDs.DOC
        _TXT_WOC.Tag = ValMgr.ValIDs.WOC
        CheckBox_Finishing.Tag = ValMgr.ValIDs.Finishing
        CheckBox_FullWOC.Tag = ValMgr.ValIDs.Slotting

        _TxtList.Add(_TXT_DOC)
        _TxtList.Add(_TXT_WOC)
    End Sub


    Private Sub CheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox_Finishing.CheckedChanged, CheckBox_FullWOC.CheckedChanged
        Try
            Dim ChkBox As CheckBox = sender
            Dim ControlID As ValMgr.ValIDs = ChkBox.Tag
            RaiseEvent ControlUpdated(Me, New ControlValUpdatedEventArgs(ControlID, ChkBox.CheckState))

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try      
    End Sub
    Private Sub Txt_ValueChanged(ByVal sender As Object, ByVal val As Double, ByVal UserEdit As Boolean) Handles _TXT_DOC.ValueChanged, _TXT_WOC.ValueChanged
        Dim txt As LabelledNumTxtBox = sender
        Dim ControlID As ValMgr.ValIDs = txt.Tag
        RaiseEvent ControlUpdated(Me, New ControlValUpdatedEventArgs(ControlID, val, Nothing, UserEdit))
    End Sub


    'Private Sub Txt_RequestCalculation(ByVal sender As Object, ByVal e As EventArgs) Handles _TXT_DOC.RequestCalcValue, _TXT_WOC.RequestCalcValue
    'Dim txt As LabelledNumTxtBox = sender
    'Dim ControlID As IUC.ControlIDs = txt.Tag
    'RaiseEvent CalcValueRequest(Me, New ControlValUpdatedEventArgs(ControlID, 1))
    'End Sub

End Class





