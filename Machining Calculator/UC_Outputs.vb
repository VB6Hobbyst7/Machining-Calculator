Public Class UC_Outputs

    Private _Plist As New Dictionary(Of Values, ProgressBar)
    Private _MaxLimitVals As New Dictionary(Of Values, Double)
    Private _Vals As New Dictionary(Of Values, Double)


    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.

        SetupUI()
    End Sub
    Public Sub UpdateLimitVal(ByVal ValID As Values, ByVal Val As Double)
        _MaxLimitVals(ValID) = Val
        UpdateUI(ValID)
    End Sub
    Public Sub UpdateVal(ByVal ValID As Values, ByVal Val As Double)
        _Vals(ValID) = Val
        UpdateUI(ValID)
    End Sub


    Private Sub UpdateUI(ByVal ValID As Values)
        UpdateProgressBar(_MaxLimitVals(ValID), _Vals(ValID), _Plist(ValID))
        UpdateLabelText(ValID, _Vals(ValID))
    End Sub
    Private Sub UpdateLabelText(ByVal ValID As Values, ByVal Val As Double)
        Select Case ValID
            Case Values.MRR
                Label_MRR.Text = "MRR: " & CStr(Val) & "mm^3"

            Case Values.Power
                Label_Power.Text = "Power: " & CStr(Val) & "HP"

            Case Values.ToolDeflection
                Label_ToolDefl.Text = "Tool Deflection: " & CStr(Val) & "um"

            Case Values.ToolForce
                Label_ToolForce.Text = "Tool Force: " & CStr(Val) & "%"

            Case Values.ToolLife
                Label_Life.Text = "Tool Life: " & CStr(Val) & "%"

            Case Values.Torque
                Label_Life.Text = "Torque: " & CStr(Val) & "Nm"

        End Select
    End Sub


    Private Sub SetupUI()
        _Plist.Add(Values.MRR, ProgressBar_MRR)
        _Plist.Add(Values.Power, ProgressBar_Power)
        _Plist.Add(Values.Torque, ProgressBar_Torque)
        _Plist.Add(Values.ToolLife, ProgressBar_Life)
        _Plist.Add(Values.ToolForce, ProgressBar_ToolForce)
        _Plist.Add(Values.ToolDeflection, ProgressBar_ToolDefl)


        _MaxLimitVals.Add(Values.MRR, 300)
        _MaxLimitVals.Add(Values.Power, 5)
        _MaxLimitVals.Add(Values.Torque, 5)
        _MaxLimitVals.Add(Values.ToolLife, 150)
        _MaxLimitVals.Add(Values.ToolForce, 150)
        _MaxLimitVals.Add(Values.ToolDeflection, 10)

        _Vals.Add(Values.MRR, 0)
        _Vals.Add(Values.Power, 0)
        _Vals.Add(Values.Torque, 0)
        _Vals.Add(Values.ToolLife, 0)
        _Vals.Add(Values.ToolForce, 0)
        _Vals.Add(Values.ToolDeflection, 0)
    End Sub
    Private Sub UpdateProgressBar(ByVal MaxVal As Double, ByVal Val As Double, ByVal P As ProgressBar)
        P.Maximum = CInt(MaxVal * 1000)
        P.Value = CInt(Val * 1000)
        P.Step = CInt(MaxVal * 10)
    End Sub


    Public Enum Values
        MRR
        Power
        Torque
        ToolLife
        ToolForce
        ToolDeflection
    End Enum

End Class





