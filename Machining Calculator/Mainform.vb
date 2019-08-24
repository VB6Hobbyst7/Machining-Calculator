Public Class Mainform

    Private Sub Mainform_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try

            LoadedForm = Me 'allows me to be controlled from main module
            LoadEventHandlers()
            LoadDefaultEntities()


        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Public Sub ControlValueUpdated(Sender As Object, e As ControlValUpdatedEventArgs)

        Dim allValIDs As Integer() = CType([Enum].GetValues(GetType(ValMgr.ValIDs)), Integer()) 'get all ids in the enum

        'we don't want this to run when the computer updates values, that is handled internally
        'only fire if a recognized function was updated
        If e.UserEdit = True And allValIDs.Contains(e.ControlID) Then

            If e.ControlID = ValMgr.ValIDs.NullPlaceHolder Then
                Throw New Exception("Control tried to update linked value with no linked value set in the tag")
            End If

            Mgr_Vals.UpdateValue_NoUI(e.ControlID, e.ControlValue, True)


            'Select Case e.ControlID

            'Case IUC.ControlIDs.Combobox
            'Select Case Sender.GetType 'Look between different user controls which contain comboboxes

            'Case GetType(UC_Tool_Setup)
            'updatevalue = ValMgr.ValIDs.ActiveToolID

            'Case GetType(UC_Machine_Matl_Setup)
            'Select Case e.ItemType 'can tell which combobox was edited because its tag holds the object type
            'Case GetType(Milling_Machine)

            'Case GetType(Material)
            'updatevalue = ValMgr.ValIDs.ActiveMaterialID
            'End Select



            'End Select


            'End Select
        End If
    End Sub




    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        LoadDefaultEntities()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Mgr_Mill_Machine.Delete(1)
    End Sub
End Class




Public Class LabelledNumTxtBox
    Inherits NumericalInputTextBox

    Private _Label As New Label

    Public ReadOnly Property LabelObj As Label
        Get
            Return _Label
        End Get
    End Property

    Public Sub New()
        MyBase.New()
        _Label.Show()
    End Sub 'for designer

    Public Sub New(ByVal Name As String, ByVal Location As Point, ByVal unitType As Units.DataUnitType, ByVal DefaultInputUnit As Units.AllUnits, ByRef sender As Control, Optional ByVal ID As Integer = -1)
        MyBase.New(65, New Point(Location.X + 60, Location.Y), unitType, DefaultInputUnit, sender, ID)

        _Label.Text = Name
        _Label.Location = New Point(Location.X, Location.Y + 2)
        _Label.Width = 60
        _Label.Height = 12

        sender.Controls.Add(_Label)
        _Label.Show()

    End Sub

End Class 'numerical input box with an attached naming label

Public Class NumericalInputTextBox
    Inherits TextBox

    Private _unitType As Units.DataUnitType
    Private _DefaultInputUnit As Units.AllUnits
    Private _ID As Integer = -1

    Public Event RequestCalcValue(ByVal sender As Object, ByVal e As EventArgs)
    Public Event ValueChanged(ByVal sender As Object, ByVal val As Double, ByVal UserEdit As Boolean) 'fires when value changes (if correct)
    Public Event ValueConfirmed(ByVal sender As Object, ByVal val As Double) 'fires on enter key (if correct value)

    Public ReadOnly Property ID As Integer
        Get
            Return _ID
        End Get
    End Property
    Public Property Value As Double
        Get
            If CheckInput() = False Then
                Me.SelectAll()
                Throw New Exception("Incorrect value <" & Me.Text & "> entered into textbox.")
            End If

            Return ConvertInput()
        End Get
        Set(value As Double)
            RemoveHandler Me.TextChanged, AddressOf Textbox_TextChanged 'stop from raising user event

            '-------- If the default value for the txt is not the program default we need to convert the value -----

            If Units.DefaultUnit(_unitType) <> _DefaultInputUnit Then
                value = Units.Convert(Units.DefaultUnit(_unitType), value, _DefaultInputUnit) 'convert from program default to txt default
            End If

            If value Mod 1 <> 0 Then

                value = Math.Round(value, 3)

                Me.Text = CStr(value) & Units.UnitStrings(_DefaultInputUnit)(0)
            Else
                Me.Text = CStr(value) & ".000" & Units.UnitStrings(_DefaultInputUnit)(0) 'add decimals if not provided
            End If

            ValidateTextField(False) 'set color/raise events

            AddHandler Me.TextChanged, AddressOf Textbox_TextChanged 're-allow event
        End Set
    End Property

    Public Sub New()
        MyBase.New()
        Me.Text = "0.000"
    End Sub 'for designer
    Public Sub New(ByVal Width As Integer, ByVal Location As Point, ByVal unitType As Units.DataUnitType, ByVal DefaultInputUnit As Units.AllUnits, ByRef sender As Control, Optional ByVal ID As Integer = -1)
        MyBase.New()
        _unitType = unitType
        _DefaultInputUnit = DefaultInputUnit
        _ID = ID

        If DefaultInputUnit = -1 Or unitType = -1 Then
            Me.Text = "0.000"
        Else
            Me.Text = "0.000" & Units.UnitStrings(DefaultInputUnit)(0)
        End If

        Me.Width = Width
        Me.Anchor = AnchorStyles.Left Or AnchorStyles.Top 'Or AnchorStyles.Right
        Me.Location = Location

        sender.Controls.Add(Me)
        Me.Show()
    End Sub

    Private Sub TextBox_MouseClick(sender As Object, e As EventArgs) Handles MyBase.Click
        Try
            Dim sendtxt As TextBox = sender
            sendtxt.SelectAll()
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try    
    End Sub
    Private Sub TextBox_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Me.KeyPress
        Try
            If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Enter) And CheckInput() = True Then
                RaiseEvent ValueConfirmed(Me, ConvertInput())
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub
    Private Sub Textbox_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Me.TextChanged
        Try
            ValidateTextField(True)
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub

    Private Sub ValidateTextField(ByVal UserEdit As Boolean)
        If Me.Text <> "" Then
            If CheckInput() = True Then 'input is correct form

                If UserEdit Then 'edit was made by a user
                    Me.BackColor = Color.White
                Else 'edit was made by code
                    Me.BackColor = Color.Lime
                End If

                RaiseEvent ValueChanged(Me, ConvertInput(), UserEdit)
                
            Else 'wrong input format
                Me.BackColor = Color.IndianRed
            End If

        Else 'if text is "" user wants a calculated value
            RaiseEvent RequestCalcValue(Me, Nothing)
        End If
    End Sub

    Private Function CheckInput() As Boolean
        Try
            Dim mytext As String = Me.Text
            Dim unitIdentifier As String = ""

            If _unitType <> -1 And _DefaultInputUnit <> -1 Then
                Dim allowedUnitStrings As List(Of String) = Units.TypeUnitStrings(_unitType)

                For i As Integer = 0 To allowedUnitStrings.Count - 1
                    If mytext.Contains(allowedUnitStrings(i)) Then
                        unitIdentifier = allowedUnitStrings(i)
                        Exit For
                    End If
                Next

                If unitIdentifier <> "" Then
                    mytext = mytext.Replace(unitIdentifier, "")
                End If
            End If

            Dim inputData_dbl As Double = CDbl(mytext)

            'Me.Text = CStr(Math.Round(Units.Convert(Units.UnitEnums(unitIdentifier), inputData_dbl, _DefaultInputUnit), 5)) & Units.UnitStrings(_DefaultInputUnit)(0)

            Return True

        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Function ConvertInput() As Double
        Dim mytext As String = Me.Text
        Dim unitIdentifier As String = ""
        Dim output As Double

        If _unitType = -1 Or _DefaultInputUnit = -1 Then
            output = CDbl(Me.Text)
        Else

            Dim allowedUnitStrings As List(Of String) = Units.TypeUnitStrings(_unitType)

            For i As Integer = 0 To allowedUnitStrings.Count - 1
                If mytext.Contains(allowedUnitStrings(i)) Then
                    unitIdentifier = allowedUnitStrings(i)
                    Exit For
                End If
            Next

            If unitIdentifier <> "" Then
                mytext = mytext.Replace(unitIdentifier, "")
                output = Units.Convert(Units.UnitEnums(unitIdentifier), CDbl(mytext), Units.DefaultUnit(_unitType)) 'convert to the default type
            Else
                output = Units.Convert(_DefaultInputUnit, CDbl(mytext), Units.DefaultUnit(_unitType)) 'if no specific type specified need to convert from the default input type
            End If
        End If

        Return output
    End Function

End Class 'textbox which converts user input based on units




Public Interface IUC

    Event ControlUpdated(ByVal Sender As Object, ByVal e As ControlValUpdatedEventArgs)
    Event CalcValueRequest(ByVal Sender As Object, ByVal e As ControlValUpdatedEventArgs)

    '--------- OLD

    'Enum ControlID
    'Combobox
    'Btn_New
    'Btn_Edit
    'Txt_WOC
    'Txt_DOC
    'Txt_Feed
    'Txt_FeedPerTooth
    'Txt_RPM
    'Txt_Vc
    'Chk_Finishing
    'Chk_SlotPocket
    'End Enum

End Interface 'interface for user controls to implement - standardizes events

Public Class ControlValUpdatedEventArgs
    Inherits EventArgs

    Private _ItemType As Type
    Private _ControlID As ValMgr.ValIDs
    Private _ControlValue As Double
    Private _UserEdit As Boolean = True

    Public ReadOnly Property ControlType As Type
        Get
            Return _ItemType
        End Get
    End Property
    Public ReadOnly Property ControlID As ValMgr.ValIDs
        Get
            Return _ControlID
        End Get
    End Property
    Public ReadOnly Property ControlValue As Integer
        Get
            Return _ControlValue
        End Get
    End Property
    Public ReadOnly Property UserEdit As Boolean
        Get
            Return _UserEdit
        End Get
    End Property

    Public Sub New(ByVal ControlID As ValMgr.ValIDs, ByVal ControlValue As Double, Optional ByVal ControlType As Type = Nothing, Optional ByVal UserEdit As Boolean = True)
        _ControlID = ControlID
        _ControlValue = ControlValue
        _ItemType = ControlType
        _UserEdit = UserEdit
    End Sub

End Class 'event parameter for when a user control UI item is changed by the user

