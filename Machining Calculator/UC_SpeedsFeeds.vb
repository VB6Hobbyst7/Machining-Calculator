Public Class UC_SpeedsFeeds
    Implements IUC

    Private _TxtList As New Dictionary(Of Integer, LabelledNumTxtBox)

    Public Event CalcValueRequest(Sender As Object, e As ControlValUpdatedEventArgs) Implements IUC.CalcValueRequest
    Public Event ControlUpdated(Sender As Object, e As ControlValUpdatedEventArgs) Implements IUC.ControlUpdated

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.

        SetupUI()
    End Sub
    Public Sub SetTxtValue(ByVal ValID As Integer, ByVal Value As Double)
        _TxtList(ValID).Value = Value 'the control's tag will be the value id
    End Sub

    Private Sub SetupUI()
        Dim _TXT_RPM As New LabelledNumTxtBox("RPM", New Point(10, Panel_header.Height + 5), Units.DataUnitType.Rot_Speed, Units.AllUnits.RPM, Me, ValMgr.ValIDs.RPM)
        Dim _TXT_Vc As New LabelledNumTxtBox("Surf-Speed", New Point(10, Panel_header.Height + _TXT_RPM.Height + 10), Units.DataUnitType.Speed, Units.AllUnits.m_min, Me, ValMgr.ValIDs.Vc)
        Dim _TXT_Feed As New LabelledNumTxtBox("Feed Rate", New Point(150, Panel_header.Height + 5), Units.DataUnitType.Speed, Units.AllUnits.mm_min, Me, ValMgr.ValIDs.Vf)
        Dim _TXT_FPT As New LabelledNumTxtBox("Feed/Tooth", New Point(150, Panel_header.Height + _TXT_RPM.Height + 10), Units.DataUnitType.Length, Units.AllUnits.mm, Me, ValMgr.ValIDs.Fz)

        _TXT_RPM.Tag = ValMgr.ValIDs.RPM
        _TXT_Vc.Tag = ValMgr.ValIDs.Vc
        _TXT_Feed.Tag = ValMgr.ValIDs.Vf
        _TXT_FPT.Tag = ValMgr.ValIDs.Fz

        _TxtList.Add(ValMgr.ValIDs.RPM, _TXT_RPM)
        _TxtList.Add(ValMgr.ValIDs.Vc, _TXT_Vc)
        _TxtList.Add(ValMgr.ValIDs.Vf, _TXT_Feed)
        _TxtList.Add(ValMgr.ValIDs.Fz, _TXT_FPT)

        For Each Txt As LabelledNumTxtBox In _TxtList.Values
            AddHandler Txt.ValueChanged, AddressOf Txt_ValueChanged

            'AddHandler Txt.RequestCalcValue, AddressOf Txt_RequestCalculation
        Next

    End Sub

    Private Sub Txt_ValueChanged(ByVal sender As Object, ByVal val As Double, ByVal UserEdit As Boolean)
        Dim txt As LabelledNumTxtBox = sender
        Dim ControlID As ValMgr.ValIDs = txt.Tag
        RaiseEvent ControlUpdated(Me, New ControlValUpdatedEventArgs(ControlID, val, Nothing, UserEdit))
    End Sub

    'Private Sub Txt_RequestCalculation(ByVal sender As Object, ByVal e As EventArgs)
    'Dim txt As LabelledNumTxtBox = sender
    'Dim ControlID As IUC.ControlIDs = txt.Tag
    'RaiseEvent CalcValueRequest(Me, New ControlValUpdatedEventArgs(ControlID, 1))
    'End Sub


End Class





