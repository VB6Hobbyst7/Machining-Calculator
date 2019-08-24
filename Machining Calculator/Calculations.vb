Imports System.Math
Public Class Calculations

    'http://www.sandvik.coromant.com/en-us/knowledge/milling/formulas_and_definitions/formulas
    'http://www.sandvik.coromant.com/en-us/knowledge/milling/getting_started/general_guidelines/max_chip_thickness/pages/default.aspx
    'http://www.tysontool.com/tech-mill-formulas.pdf

    Public Shared Function Vc_2(ByVal N As Double, ByVal D_cap As Double) As Double

        'Dcap = effective cutting diameter [mm]
        'N = tool rotation speed [rpm]
        Return N * PI * D_cap

    End Function 'surface speed [mm/min]
    Public Shared Function Vc(ByVal TMatType As Tool_Coating, ByVal MType As Material.MaterialCategory) As Double
        Select Case TMatType

            Case Tool_Coating.Uncoated_Carbide
                Select Case MType
                    Case Material.MaterialCategory.N1
                        Return 2000 * 1000

                End Select
        End Select

        Return -1
    End Function 'returns surface speed [mm/min]
    Public Function fz(ByVal Ttype As Milling_Tool.Milling_Tool_Type, ByVal MType As Material.MaterialCategory, ByVal ToolDiam As Double, Optional ByVal Slotting As Boolean = False) As Double

        'tooldiam [mm]

        Dim output As Double = -1

        Select Case Ttype

            Case Milling_Tool.Milling_Tool_Type.Flat

                Select Case MType
                    Case Material.MaterialCategory.N1
                        output = ToolDiam * 0.009 + 0.0002 'Widia Alusurf 5102

                End Select


                If Slotting Then
                    output *= 0.8 'reduce by 20% for slotting
                End If

        End Select

        Return output
    End Function 'feed per tooth [mm/tooth]
    Public Shared Function fz_2(ByVal Vf As Double, ByVal N As Double, ByVal Zc As Double) As Double

        'Vf = feed rate [mm/min], [in/min]
        'N = spindle speed [rpm]
        'Zc = # of effective cutter teeth

        Return Vf / (N * Zc)

    End Function 'feed per tooth [mm], [in]

    Public Shared Function N(ByVal Vc As Double, ByVal Dcap As Double) As Double

        'Vc = surface speed [mm/min],
        'Dcap = effective cutting diameter [mm]

        Return Vc / (PI * Dcap)

    End Function ' spindle speed [rpm]
    
    Public Shared Function Vf(ByVal fz As Double, ByVal N As Double, ByVal Zc As Double) As Double

        'fz = feed per tooth [mm], [in]
        'N = spindle speed [rpm]
        'Zc = # of effective cutter teeth

        Return fz * N * Zc

    End Function 'feed rate [mm/min], [in/min]




    Public Shared Function DOC(ByVal FluteLength As Double) As Double
        Return FluteLength * 0.9
    End Function


    Public Shared Function fz_comp_ChipThinning(ByVal Hex As Double, ByVal Ap As Double, ByVal Ae As Double, ByVal Dcap As Double, ByVal Rn As Double, ByVal Taper_angle As Double) As Double
        'Hex = Max chip thickness (uncomped feed per tooth) [mm], [in]
        'Taper angle [deg]
        'Ap = axial depth of cut [mm], [in]
        'Ae = radial depth of cut [mm], [in]
        'Rn = nose radius [mm], [in]
        'Dcap = effective tool diameter at cutting depth [mm], [in]

        Dim output As Double = Hex

        '---------------- First adjust for radial depth --------------------

        If Ae / Dcap < 0.5 Then 'only adjust if cutting less than 50% of tool diam
            output = output * Dcap / (2 * Sqrt(Ae * Dcap - (Ae * Ae)))
        End If


        '------------- adjust further square/tapered tools---------------

        If Taper_angle > 0 Then

            output = output / Cos(Taper_angle * PI / 180.0)

        Else 'must be mutually exclusive

            '-------------adjust further for radiused tools ----------------- 

            If Rn > 0 And Ap < Rn Then 'make sure depth of cut is lower than max radius
                output = output * Rn / Sqrt((Ap * Rn * 2) - (Ap * Ap))
            End If

        End If

        'Best performance is achieved when the lead angle, ψr, remains over 30° when using round insert cutters or ball nose end mills at limited depths of cut. This means that the depth of cut should not exceed 25% x insert diameter, iC.

        Return output
    End Function 'chip thinning compensated feed per tooth [mm], [in]

    'NEED A CALCULATION FOR HM
    Public Shared Function Kc(ByVal Kc1 As Double, ByVal Hm As Double, ByVal Mc As Double, Optional ByVal Gamma_0 As Double = 0)
        'kc1 = material property [N/mm^2]
        'Mc = material property []
        'Hm = avg chip thickness [mm], [in]
        'Gamma_0 = chip rake angle []

        Return Kc1 * Pow(Hm, -1 * Mc) * (1 - Gamma_0 / 100)

    End Function 'Specific cutting force [N/mm^2]

    Public Shared Function Hex(ByVal Ap As Double, ByVal Taper_angle As Double)
        'Ap = axial depth of cut [mm]
        'Taper angle [deg]

        Return Ap * Cos(Taper_angle * PI / 180.0)
    End Function 'Max chip thickness [mm]


    '--------- DOC Limits -------------

    Public Shared Function DOC_PC(ByVal Pc As Double, ByVal Ae As Double, ByVal Vf As Double, ByVal Kc As Double) As Double

        'Ae = radial depth of cut [mm]
        'Vf = feed rate [mm/min]
        'kc = Specific cutting force [N/mm²]
        'Pc = Gross cut power requirement [W]

        Return (Pc * 60 * 1000) / (Ae * Vf * Kc)

    End Function 'DOC limited by power [mm]
    Public Shared Function DOC_Td(ByVal Td As Double, ByVal E As Double, ByVal D As Double, ByVal L As Double, ByVal WOC As Double, ByVal Kc As Double) As Double

        'Td = allowable deflection [mm]
        'WOC = cut width [mm]
        'kc = Specific cutting force [N/mm²]
        'E = tools youngs modulus [Pa]
        'D = tool diameter [mm]
        'L = tool extension length [mm]

        '----------- Iteration Parameters ----------------

        Dim Tolerance As Double = 0.001
        Dim DOC_Increment As Double = 0.001


        Dim DOC As Double = 1  'start with guess for DOC

        Dim deflection As Double = -1000000 'start with large negative number so loop enters

        '-------------- Iterate -----------------------

        While (deflection - Td) > Tolerance

            Dim Force As Double = Fc(DOC, WOC, Kc)
            deflection = Tool_Deflection(Force, E, D, L, DOC)

            If deflection > Td Then 'if deflection is too high lower the DOC
                DOC -= DOC_Increment
            Else
                DOC += DOC_Increment
            End If
        End While


        Return DOC

    End Function 'DOC for given tool deflection [mm]
    Public Shared Function DOC_UTS()

    End Function

    '-------- Outputs -----------

    Public Function Mrr(ByVal Ap As Double, ByVal Ae As Double, ByVal Vf As Double) As Double

        'Ap = axial depth of cut [mm]
        'Ae = radial depth of cut [mm]
        'Vf = feed rate [mm/min]

        Return Ap * Ae * Vf / 1000

    End Function 'Metal removal rate [cm³/min]
    Public Function Pc(ByVal Ap As Double, ByVal Ae As Double, ByVal Vf As Double, ByVal Kc As Double, ByVal Em As Double) As Double

        'Ap = axial depth of cut [mm]
        'Ae = radial depth of cut [mm]
        'Vf = feed rate [mm/min]
        'kc = Specific cutting force [N/mm²]
        'Em = Machine efficiency []

        Dim output As Double = Ap * Ae * Vf * Kc / (60 * 1000 * Em)

        Return output

    End Function 'Gross cut power requirement [W]
    Public Function Tc(ByVal Pc As Double, ByVal RPM As Double) As Double
        'Pc = Gross cut power requirement [W]
        'RPM = tool rotation speed [RPM]

        Dim omega As Double = RPM * 2 * PI / 60.0 'convert to [rad/s]

        Return Pc / omega
    End Function 'Gross torque requirement [Nm]
    Public Shared Function Tool_Deflection(ByVal Fc As Double, ByVal E As Double, ByVal D As Double, ByVal L As Double, ByVal DOC As Double) As Double
        'Fc = tool cutting force [N]
        'E = tools youngs modulus [Pa]
        'D = tool diameter [mm]
        'L = tool extension length [mm]
        'DOC = depth of cut [mm]

        '----------------- Convert all dimensions to meters ------------
        D /= 1000
        L /= 1000
        DOC /= 1000

        '----------- Estimate Moment of Inertia -------------------

        Dim I As Double = (PI * Pow(D, 4)) / 64 '[m^4]

        '----------- Get Cutting Force Per Unit Length -------------

        Dim w As Double = Fc / DOC '[N/m]

        '----------- Calculate Stop Distance of Force ----------------

        Dim a As Double = L - DOC '[m]

        '----------- Estimate Deflection ------------------
        'http://www.engineersedge.com/beam_bending/beam_bending24.htm

        Dim Deflection As Double = w * (4 * L * (Pow(L, 3) - Pow(a, 3)) - (Pow(L, 4) - Pow(a, 4))) / (24 * E * I) '[m]

        Return Deflection * 1000 'convert to mm

    End Function 'Estimate tool deflection [mm]
    Public Shared Function Fc(ByVal DOC As Double, ByVal WOC As Double, ByVal Kc As Double) As Double
        'DOC = cut depth [mm]
        'WOC = cut width [mm]
        'kc = Specific cutting force [N/mm²]

        Return DOC * WOC * Kc
    End Function 'cutting force [N]

    'ADD TOOL LIFE - Taylor Eqn

End Class

Public Class ValMgr

    Private _Data As New Dictionary(Of ValIDs, Val)

    Public Event ValueUpdated(ByVal Val As Val, ByVal ID As ValIDs, ByVal UserInput As Boolean) 'send this to inform UI to update
    Public Event CalculationRequired(ByVal ParentVal As ValMgr.Val, ByVal ParentID As ValMgr.ValIDs, ByVal UserInput As Boolean) 'send this to inform other values they need calculation

    Public ReadOnly Property Value(ByVal ID As ValIDs) As Double
        Get
            Return _Data(ID).Val
        End Get
    End Property

    Public Sub New()
        '------------- Top level (no parents) ------------------------
        _Data.Add(ValIDs.ActiveMachineID, New Val({ValIDs.RPM}, {}))
        _Data.Add(ValIDs.ActiveToolID, New Val({ValIDs.Vc, ValIDs.Fz}, {}))
        _Data.Add(ValIDs.ActiveMaterialID, New Val({ValIDs.Vc}, {}))
        _Data.Add(ValIDs.Slotting, New Val({ValIDs.DOC}, {}))
        _Data.Add(ValIDs.Finishing, New Val({}, {}))


        _Data.Add(ValIDs.DOC, New Val({}, {}, 2))
        _Data.Add(ValIDs.WOC, New Val({}, {}, 2))
        _Data.Add(ValIDs.Vc, New Val({ValIDs.RPM}, {}))
        _Data.Add(ValIDs.RPM, New Val({ValIDs.Vf}, {ValIDs.Vc}))
        _Data.Add(ValIDs.Fz, New Val({ValIDs.Vf}, {}))

        _Data.Add(ValIDs.Vf, New Val({}, {ValIDs.Fz}))
    End Sub 'intializes all numbers and defines their children

    Public Sub UpdateValue(ByVal ID As ValIDs, ByVal Data As Double, Optional ByVal UserInput As Boolean = False)
        UpdateValue_Silent(ID, Data)

        RaiseEvent CalculationRequired(_Data(ID), ID, UserInput)
        RaiseEvent ValueUpdated(_Data(ID), ID, UserInput)

    End Sub
    Public Sub UpdateValue_UIonly(ByVal ID As ValIDs, ByVal Data As Double, Optional ByVal UserInput As Boolean = False)
        UpdateValue_Silent(ID, Data)

        RaiseEvent ValueUpdated(_Data(ID), ID, UserInput)
    End Sub 'raises the UI updating event only
    Public Sub UpdateValue_NoUI(ByVal ID As ValIDs, ByVal Data As Double, Optional ByVal UserInput As Boolean = False)
        UpdateValue_Silent(ID, Data)

        RaiseEvent CalculationRequired(_Data(ID), ID, UserInput)
    End Sub 'raises only the sub-calculation event
    Public Sub UpdateValue_Silent(ByVal ID As ValIDs, ByVal Data As Double)

        If _Data.ContainsKey(ID) = False Then
            Throw New Exception("Tried to update as non-initialized value")
        End If

        _Data(ID).Val = Data
    End Sub 'use sparingly when you don't want the events to fire (Ie. potentially recursive child loop)




    Public Class Val

        Private _Val As Double
        Private _Children As New List(Of ValIDs) 'only include first level children... process will iterate
        Private _Feedback As New List(Of ValIDs)

        Public Property Val As Double
            Get
                Return _Val
            End Get
            Set(value As Double)
                _Val = value
            End Set
        End Property
        Public ReadOnly Property Children As List(Of ValIDs)
            Get
                Return _Children
            End Get
        End Property

        Public ReadOnly Property Feedback As List(Of ValIDs)
            Get
                Return _Feedback
            End Get
        End Property

        Public Sub New(ByVal Children As ValIDs(), ByVal Feedback As ValIDs(), Optional Val As Double = 0.0)
            _Val = Val
            _Children = Children.ToList
            _Feedback = Feedback.ToList
        End Sub

    End Class

    Public Enum ValIDs
        NullPlaceHolder = 0 'controls with no tag set will return 0. This value is just for picking up that case and throwing an error

        ActiveToolID 'int
        ActiveMachineID 'int
        ActiveMaterialID 'int

        Slotting '[bool] whether slotting function is turned on
        Finishing '[bool] whether finishing function is turned on

        DOC
        WOC

        Vc
        RPM

        Fz
        Vf


    End Enum 'these values are stored in the tags of controls that display/link to them

End Class


Public Class Milling_Tool

    Inherits BaseManagedClass

    Private _Type As Milling_Tool_Type
    Private _Geometry As New Dictionary(Of GF, Double) 'holds all geometry, [mm] or [deg]

    Private _Name As String
    Private _Material As Integer 'tool material class ID
    Private _Coating As Integer 'coating material class ID

    'Public Event Milling_Tool_Updated(ByVal sender As Milling_Tool, ByVal e As Integer)

#Region "Standard Properties"


    Public ReadOnly Property Tool_Type As Milling_Tool_Type
        Get
            Return _Type      
        End Get
    End Property
    Public ReadOnly Property Geometry(ByVal Field As GF) As Double
        Get
            If _Geometry.ContainsKey(Field) Then
                Return _Geometry(Field)
            Else
                Return 0 'return 0 for the geometry if it is not specified for the tool (taper, nose radius, etc.)
            End If
        End Get 
    End Property
    Public Property Geometry_All As Dictionary(Of GF, Double)
        Get
            Return _Geometry
        End Get
        Set(value As Dictionary(Of GF, Double))
            _Geometry = value

            If CheckMandatoryGeometry(_Type, _Geometry) = False Then
                Throw New Exception("Mandatory geometry not given for new tool")
            End If

            CalculateOptionalGeometry(_Type, _Geometry)

            If CheckOptionalGeometry(_Type, _Geometry) = False Then
                Throw New Exception("Optional geometry not calculated properly for new tool")
            End If

            RaiseEditEvent(UpdateFields.Geometry)
        End Set
    End Property
    Public Property Name As String
        Get
            Return _Name
        End Get
        Set(value As String)
            _Name = value
            RaiseEditEvent(UpdateFields.Name)
        End Set
    End Property
    Public Property Material As Integer
        Get
            Return _Material
        End Get
        Set(value As Integer)
            _Material = value
            RaiseEditEvent(UpdateFields.Material)
        End Set
    End Property
    Public Property Coating As Integer
        Get
            Return _Coating
        End Get
        Set(value As Integer)
            _Coating = value
            RaiseEditEvent(UpdateFields.Coating)
        End Set
    End Property

#End Region

    Public ReadOnly Property D_Cap(ByVal Ap As Double) As Double
        Get
            Dim output As Double = _Geometry(GF.Diameter) 'get stock diameter

            Select Case _Type

                Case Milling_Tool_Type.Ball_Nose, Milling_Tool_Type.Bull_Nose

                    Dim Rn As Double = _Geometry(GF.Nose_Radius)

                    If Ap < Rn Then
                        output = output - Sqrt(Pow(2 * Rn, 2) - Pow((2 * Rn - (2 * Ap)), 2))
                    End If

                Case Milling_Tool_Type.Chamfer

                    Dim Taper As Double = _Geometry(GF.Taper_Angle) * PI / 180.0

                    If Ap < (0.5 * _Geometry(GF.Diameter) / Tan(Taper)) Then 'check if in tapered region
                        output = output - (2 * Ap / Tan((0.5 * PI) - Taper))
                    End If

                Case Milling_Tool_Type.Taper

                    output = output + (2 * Ap / Tan((0.5 * PI) - (GF.Taper_Angle * PI / 180.0)))

                Case Milling_Tool_Type.Face

                    Dim Rn As Double = _Geometry(GF.Nose_Radius)
                    Dim Taper As Double = _Geometry(GF.Taper_Angle) * PI / 180.0

                    If Rn > 0 And Ap < Rn Then
                        output = output + Sqrt(Pow(2 * Rn, 2) - Pow((2 * Rn - (2 * Ap)), 2))

                    ElseIf Rn > 0 And Ap > Rn Then 'no taper, cutting deeper than radius
                        output = output + 2 * Rn

                    Else 'taper
                        If Taper <> 0 Then
                            output = output + (2 * Ap / Tan((0.5 * PI) - Taper))
                        End If

                    End If

            End Select

            Return output
        End Get
    End Property 'effective cutting diameter [mm]


    Public Sub New(Name As String, Material As Integer, Coating As Integer, Ttype As Milling_Tool_Type, Geometry As Dictionary(Of GF, Double))
        _Name = Name
        _Material = Material
        _Coating = Coating
        _Type = Ttype

        For Each KVP As KeyValuePair(Of GF, Double) In Geometry 'dont overwrite dictionary directly --- this prevents byref functionality from changing input list outside the sub
            _Geometry.Add(KVP.Key, KVP.Value)
        Next

        If CheckMandatoryGeometry(_Type, _Geometry) = False Then
            Throw New Exception("Mandatory geometry not given for new tool")
        End If

        CalculateOptionalGeometry(_Type, _Geometry)

        If CheckOptionalGeometry(_Type, _Geometry) = False Then
            Throw New Exception("Optional geometry not calculated properly for new tool")
        End If

    End Sub



    Private Function CheckMandatoryGeometry(ByVal Ttype As Milling_Tool_Type, ByVal Geometry As Dictionary(Of GF, Double)) As Boolean

        For Each Field As GF In MandatoryGeometryFields(Ttype)
            If Geometry.Keys.Contains(Field) = False Then 'mandatory field not included
                Return False
            End If
        Next

        Return True
    End Function 'check geometry to see if valid for tooltype
    Private Sub CalculateOptionalGeometry(ByVal Ttype As Milling_Tool_Type, ByRef Geometry As Dictionary(Of GF, Double))

        Dim ToCalculate As New List(Of GF)

        For Each Field As GF In OptionalGeometryFields(Ttype)
            If Geometry.Keys.Contains(Field) = False Then 'mandatory field not included
                ToCalculate.Add(Field)
            End If
        Next

        Select Case Ttype
            Case Milling_Tool_Type.Flat, Milling_Tool_Type.Ball_Nose, Milling_Tool_Type.Bull_Nose, Milling_Tool_Type.Taper, Milling_Tool_Type.DoveTail, Milling_Tool_Type.Chamfer

                For Each Geom As GF In ToCalculate
                    If Geom = GF.Extension Then
                        Geometry.Add(GF.Extension, Geometry(GF.Diameter) * 3)
                    ElseIf Geom = GF.Flute_Length Then
                        Geometry.Add(GF.Flute_Length, Geometry(GF.Extension))
                    ElseIf Geom = GF.Rake_Angle Then
                        Geometry.Add(GF.Rake_Angle, 0)
                    ElseIf Geom = GF.Shank_Diam Then
                        Geometry.Add(GF.Shank_Diam, Geometry(GF.Diameter))
                    ElseIf Geom = GF.Nose_Radius Then
                        Geometry.Add(GF.Nose_Radius, 0)
                    ElseIf Geom = GF.Tip_Diam Then
                        Geometry.Add(GF.Tip_Diam, 0)
                    End If
                Next

            Case Milling_Tool_Type.Face

                For Each Geom As GF In ToCalculate
                    If Geom = GF.Extension Then
                        Geometry.Add(GF.Extension, Geometry(GF.Diameter))
                    ElseIf Geom = GF.Shank_Diam Then
                        Geometry.Add(GF.Shank_Diam, Geometry(GF.Diameter) / 2.0)
                    ElseIf Geom = GF.Nose_Radius Then
                        Geometry.Add(GF.Nose_Radius, 0)
                    ElseIf Geom = GF.Taper_Angle Then
                        Geometry.Add(GF.Taper_Angle, 0)
                    End If
                Next

            Case Milling_Tool_Type.Radius

                For Each Geom As GF In ToCalculate
                    If Geom = GF.Extension Then
                        Geometry.Add(GF.Extension, Geometry(GF.Diameter) * 3)
                    ElseIf Geom = GF.Flute_Length Then
                        Geometry.Add(GF.Flute_Length, Geometry(GF.Nose_Radius) / 2.0)
                    ElseIf Geom = GF.Rake_Angle Then
                        Geometry.Add(GF.Rake_Angle, 0)
                    ElseIf Geom = GF.Shank_Diam Then
                        Geometry.Add(GF.Shank_Diam, Geometry(GF.Diameter))
                    End If
                Next

            Case Milling_Tool_Type.Slot

                For Each Geom As GF In ToCalculate
                    If Geom = GF.Extension Then
                        Geometry.Add(GF.Extension, Geometry(GF.Diameter) * 2)
                    ElseIf Geom = GF.Nose_Radius Then
                        Geometry.Add(GF.Nose_Radius, 0)
                    End If
                Next

            Case Milling_Tool_Type.Drill
                'none needed
        End Select


    End Sub
    Private Function CheckOptionalGeometry(ByVal Ttype As Milling_Tool_Type, ByVal Geometry As Dictionary(Of GF, Double)) As Boolean

        For Each Field As GF In OptionalGeometryFields(Ttype)
            If Geometry.Keys.Contains(Field) = False Then 'optional field not included
                Return False
            End If
        Next

        Return True
    End Function 'check geometry to see if valid for tooltype


    Protected Overrides Sub RaiseEditEvent(ByVal e As Integer)
        MyBase.RaiseEditEvent_Base(Me, e)
    End Sub

    Public Shared ReadOnly Property Mill_Cutter_Type_Names(ByVal Cutter_Type As Milling_Tool_Type)
        Get
            Select Case Cutter_Type
                Case Milling_Tool_Type.Flat
                    Return "Flat Mill"
                Case Milling_Tool_Type.Ball_Nose
                    Return "Ball Mill"
                Case Milling_Tool_Type.Bull_Nose
                    Return "Bull-nose Mill"
                Case Milling_Tool_Type.Chamfer
                    Return "Chamfer Mill"
                Case Milling_Tool_Type.DoveTail
                    Return "Dovetail Mill"
                Case Milling_Tool_Type.Drill
                    Return "Drill"
                Case Milling_Tool_Type.Face
                    Return "Face Mill"
                Case Milling_Tool_Type.Radius
                    Return "Radius Mill"
                Case Milling_Tool_Type.Slot
                    Return "Slot Cutter"
                Case Milling_Tool_Type.Taper
                    Return "Taper Mill"
                Case Else
                    Return ""
            End Select
        End Get
    End Property
    Public Shared ReadOnly Property MandatoryGeometryFields(ByVal ToolType As Milling_Tool_Type) As GF()
        Get
            Select Case ToolType
                Case Milling_Tool_Type.Flat, Milling_Tool_Type.Ball_Nose
                    Return {GF.Diameter, GF.Flutes}

                Case Milling_Tool_Type.Bull_Nose
                    Return {GF.Diameter, GF.Flutes, GF.Nose_Radius}

                Case Milling_Tool_Type.Taper, Milling_Tool_Type.DoveTail
                    Return {GF.Diameter, GF.Flutes, GF.Taper_Angle}

                Case Milling_Tool_Type.Chamfer
                    Return {GF.Diameter, GF.Flutes, GF.Taper_Angle, GF.Flute_Length}

                Case Milling_Tool_Type.Radius
                    Return {GF.Diameter, GF.Flutes, GF.Nose_Radius, GF.Flute_Length}

                Case Milling_Tool_Type.Slot
                    Return {GF.Diameter, GF.Flutes, GF.Flute_Length, GF.Shank_Diam}

                Case Milling_Tool_Type.Face
                    Return {GF.Diameter, GF.Flutes, GF.Flute_Length}

                Case Milling_Tool_Type.Drill
                    Return {GF.Diameter, GF.Tip_Angle, GF.Flute_Length}

                Case Else
                    Return {}
            End Select
        End Get
    End Property
    Public Shared ReadOnly Property OptionalGeometryFields(ByVal ToolType As Milling_Tool_Type) As GF()
        Get
            Select Case ToolType
                Case Milling_Tool_Type.Flat, Milling_Tool_Type.Ball_Nose, Milling_Tool_Type.Bull_Nose
                    Return {GF.Extension, GF.Flute_Length, GF.Rake_Angle}

                Case Milling_Tool_Type.Taper, Milling_Tool_Type.DoveTail
                    Return {GF.Extension, GF.Flute_Length, GF.Rake_Angle, GF.Shank_Diam, GF.Nose_Radius}

                Case Milling_Tool_Type.Chamfer
                    Return {GF.Extension, GF.Flute_Length, GF.Rake_Angle, GF.Shank_Diam, GF.Tip_Diam}

                Case Milling_Tool_Type.Radius
                    Return {GF.Extension, GF.Flute_Length, GF.Rake_Angle, GF.Shank_Diam}

                Case Milling_Tool_Type.Slot
                    Return {GF.Extension, GF.Nose_Radius}

                Case Milling_Tool_Type.Face
                    Return {GF.Extension, GF.Nose_Radius, GF.Shank_Diam, GF.Taper_Angle}

                Case Else
                    Return {}
            End Select
        End Get
    End Property

    Public Enum UpdateFields
        Geometry
        Name
        ID
        Material
        Coating
    End Enum
    Public Enum Milling_Tool_Type

        Flat
        Ball_Nose
        Bull_Nose
        Taper
        DoveTail
        Chamfer
        Radius
        Slot
        Drill
        Face


    End Enum

   

    ''' <summary>
    ''' Geometry Fields
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum GF
        Diameter
        Flutes
        Extension
        Flute_Length
        Rake_Angle
        Taper_Angle
        Nose_Radius
        Shank_Diam
        Tip_Diam
        Tip_Angle
    End Enum

    


    


    Public Class Old

        Private _Diameter As Double = -1
        Private _ShankDiameter As Double = -1
        Private _Num_Flutes As Double = -1
        Private _Flute_Length As Double = -1 'can limit max DOC
        Private _NoseRadius As Double = -1
        Private _TaperAngle As Double = -1
        Private _RakeAngle As Double = -1

        Private _Extension As Double = -1

        Private _GeometryType As Milling_Tool_Type

        ''' <summary>
        ''' Flat Mills, Ball Mills
        ''' </summary>
        Public Sub New(D As Double, N As Double, Cutter_Type As Milling_Tool_Type, Optional Ext As Double = -1, Optional Fl As Double = -1)
            _GeometryType = Cutter_Type

            _Diameter = D
            _Num_Flutes = N
            _ShankDiameter = D

            If _Extension = -1 Then 'extension not known
                _Extension = 4 * _Diameter 'approximate
            Else
                _Extension = Ext
            End If


            If Fl = -1 Then 'flute length not known
                _Flute_Length = _Extension
            Else
                _Flute_Length = Fl
            End If


            Select Case Cutter_Type
                Case Milling_Tool_Type.Flat
                    Return
                Case Milling_Tool_Type.Ball_Nose
                    _NoseRadius = _Diameter / 2.0
                    Return
                Case Else
                    Throw New Exception("Wrong initialization sub for given tool")
            End Select

        End Sub


        ''' <summary>
        ''' Bull Nose Mills, Radius Mills
        ''' </summary>
        Public Sub New(D As Double, N As Double, Rn As Double, Cutter_Type As Milling_Tool_Type, Optional Ext As Double = -1, Optional Fl As Double = -1, Optional Ds As Double = -1)
            _GeometryType = Cutter_Type

            _Diameter = D
            _ShankDiameter = D
            _Num_Flutes = N
            _NoseRadius = Rn

            If _Extension = -1 Then 'extension not known
                _Extension = 4 * _Diameter 'approximate
            Else
                _Extension = Ext
            End If

            If Fl = -1 Then 'flute length not known
                If Cutter_Type = Milling_Tool_Type.Radius Then 'flute length is generally short for radius cutters
                    _Flute_Length = _NoseRadius
                Else
                    _Flute_Length = _Extension
                End If
            Else
                _Flute_Length = Fl
            End If

            If Ds = -1 Then 'shank diameter not known
                _ShankDiameter = _Diameter
            Else
                _ShankDiameter = Ds
            End If

            Select Case Cutter_Type
                Case Milling_Tool_Type.Bull_Nose Or Milling_Tool_Type.Radius
                    Return
                Case Else
                    Throw New Exception("Wrong initialization sub for given tool")
            End Select

        End Sub


        ''' <summary>
        ''' Chamfer, DoveTail, Taper
        ''' </summary>
        Public Sub New(D As Double, N As Double, Rn As Double, Taper As Double, Cutter_Type As Milling_Tool_Type, Optional Ext As Double = -1, Optional Fl As Double = -1, Optional Ds As Double = -1)
            _GeometryType = Cutter_Type

            _Diameter = D
            _Num_Flutes = N
            _NoseRadius = Rn

            If Ext = -1 Then 'extension not known
                _Extension = 4 * _Diameter
            Else
                _Extension = Ext
            End If

            If Fl = -1 Then 'flute length not known
                _Flute_Length = _Extension
            Else
                _Flute_Length = Fl
            End If

            If Ds = -1 Then 'shank diameter not known 
                _ShankDiameter = _Diameter
            Else
                _ShankDiameter = Ds
            End If

            Select Case Cutter_Type
                Case Milling_Tool_Type.Taper Or Milling_Tool_Type.DoveTail Or Milling_Tool_Type.Chamfer
                    Return
                Case Else
                    Throw New Exception("Wrong initialization sub for given tool")
            End Select

        End Sub






    End Class 'old

End Class
Public Class Tool_material

    Inherits BaseManagedClass

    Private _Name As String

    Private _E As Double 'young's modulus [Pa]
    Private _Sut As Double 'ultimate tensile strength [Pa]

    Public Property Name As String
        Get
            Return _Name
        End Get
        Set(value As String)
            _Name = value
            RaiseEditEvent(UpdateFields.Name)
        End Set
    End Property
    Public Property E As Double
        Get
            Return _E
        End Get
        Set(value As Double)
            _E = value
            RaiseEditEvent(UpdateFields.E)
        End Set
    End Property
    Public Property Sut As Double
        Get
            Return _Sut
        End Get
        Set(value As Double)
            _Sut = value
            RaiseEditEvent(UpdateFields.Sut)
        End Set
    End Property


    Public Sub New(ByVal Name As String, ByVal E_GPa As Double, ByVal Sut_MPa As Double)
        _Name = Name
        _E = Units.Convert(Units.AllUnits.GPa, E_GPa, Units.AllUnits.Pa)
        _Sut = Units.Convert(Units.AllUnits.MPa, Sut_MPa, Units.AllUnits.Pa)

    End Sub

    Protected Overrides Sub RaiseEditEvent(ByVal e As Integer)
        MyBase.RaiseEditEvent_Base(Me, e)
    End Sub

    Public Enum UpdateFields
        Name
        E
        Sut
    End Enum

End Class
Public Enum Tool_Coating
    Uncoated_HSS
    Uncoated_Carbide
    AlTiN
End Enum


Public Class Milling_Machine

    Inherits BaseManagedClass

    Private _Name As String

    Private _DriveType As DriveType
    Private _Curve As PowerCurve = Nothing

    Private _MaxRPM As Double = -1 'max RPM [rpm] - only need if no power curve available 
    Private _MaxPower As Double = -1 'max Power [hp] - only need if no power curve available 
    Private _MaxFeed As Double = -1 'max feed rate [mm/min]

    Public Shared ReadOnly Property Drive_Efficiency(ByVal Drive As DriveType) As Double
        Get
            Select Case Drive
                Case DriveType.Direct
                    Return 0.9 'LOOK UP ACTUAL VALUE
                Case DriveType.Belt
                    Return 0.7 'LOOK UP ACTUAL VALUE
                Case Else
                    Return 0
            End Select
        End Get
    End Property 'LOOK UP ACTUAL VALUES
    Public ReadOnly Property PowerCurveKnown As Boolean
        Get
            If _Curve Is Nothing Then
                Return False
            Else
                Return True
            End If
        End Get
    End Property
    Public ReadOnly Property Power_Curve As PowerCurve
        Get
            Return _Curve
        End Get
    End Property
    Public ReadOnly Property MaxFeed As Double
        Get
            Return _MaxFeed
        End Get
    End Property '[mm/min]
    Public ReadOnly Property MaxRPM As Double
        Get
            If PowerCurveKnown Then
                Return _Curve.Max_RPM
            Else
                Return _MaxRPM
            End If
        End Get
    End Property '[rpm]
    Public ReadOnly Property MaxPower As Double
        Get
            If PowerCurveKnown Then
                Return _Curve.Max_Power
            Else
                Return _MaxPower
            End If
        End Get
    End Property '[rpm]
    Public ReadOnly Property Name As String
        Get
            Return _Name
        End Get
    End Property


    Public Sub New(ByVal Name As String, ByVal Drive As DriveType, ByVal Curve As PowerCurve, ByVal MaxFeed As Double)

        _Name = Name
        _DriveType = Drive
        _Curve = Curve

    End Sub 'if power curve is known
    Public Sub New(ByVal Name As String, ByVal Drive As DriveType, ByVal MaxPower As Double, ByVal MaxRPM As Double, ByVal MaxFeed As Double)

        _Name = Name
        _DriveType = Drive

        _MaxRPM = MaxRPM
        _MaxPower = MaxPower
        _MaxFeed = MaxFeed

    End Sub 'if power curve is not known

    Public Enum DriveType
        Direct
        Belt
    End Enum


    Public Class PowerCurve

        Private _Rpms As Double() '[rpm] --- max RPM is last
        Private _Powers As Double() '[hp] --- follows same order as RPM
        Private _DriveType As DriveType


        Public ReadOnly Property Num_Points As Integer
            Get
                Return _Rpms.Length
            End Get
        End Property

        Public ReadOnly Property Rpm_ByIndex(ByVal index As Integer) As Double
            Get
                Return _Rpms(index)
            End Get
        End Property
        Public ReadOnly Property Power_ByIndex(ByVal index As Integer) As Double
            Get
                Return CalculatePower(_Powers(index))
            End Get
        End Property
        Public ReadOnly Property Power_ByRPM(ByVal RPM As Double) As Double
            Get
                Return CalculatePower(GetPower(RPM))
            End Get
        End Property
        Public ReadOnly Property Torque_ByRPM(ByVal RPM As Double) As Double
            Get
                Return CalculateTorque(RPM, GetPower(RPM))
            End Get
        End Property 'torque [Nm]
        Public ReadOnly Property Power_ByRPM_LinInterpolate(ByVal RPM As Double) As Double
            Get
                Return Power_ByRPM_LinInterpolate(RPM)
            End Get
        End Property

        Public ReadOnly Property Max_RPM As Double
            Get
                Return _Rpms.Last
            End Get
        End Property
        Public ReadOnly Property Max_Power As Double
            Get
                Return _Powers.Max
            End Get
        End Property


        Public Sub New(ByVal Rpms As Double(), Powers As Double(), ByVal Drive As DriveType)

            _DriveType = Drive

            If Rpms.Length <> Powers.Length Then 'needs to have same length
                Throw New Exception("Power list and rpm list length not matching ")
            End If

            _Rpms = Rpms
            _Powers = Powers
        End Sub


        Private Function GetPower(ByVal RPM As Double) As Double
            Dim output As Double = -1

            For i As Integer = 0 To _Rpms.Length - 1
                If _Rpms(i) = RPM Then
                    output = _Powers(i)
                End If
            Next

            Return output
        End Function
        Private Function GetPower_LinearIterpolate(ByVal RPM As Double) As Double
            Dim output As Double = -1

            '-------- Handle extrapolation ------------
            If RPM > _Rpms.Last Then
                Return _Powers.Last
            ElseIf RPM < _Rpms.First Then
                Return _Powers.First
            End If

            '-------- Determine which points RPMs lie between
            Dim i As Integer = 0

            While (_Rpms(i) < RPM And _Rpms(i + 1) > RPM) = False
                i += 1
            End While

            '-------- interpolate ------------

            output = _Powers(i) + (RPM - _Rpms(i)) * (_Powers(i + 1) - _Powers(i)) / (_Rpms(i + 1) - _Rpms(i))

            Return output
        End Function

        Private Function CalculateTorque(ByVal RPM As Double, ByVal HP As Double) As Double

            Return (HP * Units.Convert(Units.AllUnits.HP, HP, Units.AllUnits.W) / (RPM * 2 * PI / 60) * Milling_Machine.Drive_Efficiency(_DriveType))
        End Function '[Nm]
        Private Function CalculatePower(ByVal Power_W As Double) As Double
            
            Return Power_W * Milling_Machine.Drive_Efficiency(_DriveType)


        End Function ' [W]

    End Class

    Protected Overrides Sub RaiseEditEvent(ByVal e As Integer)
        MyBase.RaiseEditEvent_Base(Me, e)
    End Sub 'NOT LINKED TO ANYTHING

End Class
Public Enum Milling_Operations
    Facing
    Peripheral
    Slotting
    Ramping
End Enum

Public Class Material

    Inherits BaseManagedClass

    Private _Name As String
    Private _Category As MaterialCategory

    Private _KC1 As Double 'specific cutting force [N/mm^2]
    Private _MC As Double 'Sandvic material property

    Private _HB_min As Integer 'min brinell hardness
    Private _HB_max As Integer 'max brinell hardness

    Public Property Name As String
        Get
            Return _Name
        End Get
        Set(value As String)
            _Name = value
            RaiseEditEvent(UpdateFields.Name)
        End Set
    End Property
    Public Property Category As MaterialCategory
        Get
            Return _Category
        End Get
        Set(value As MaterialCategory)
            _Category = value
            RaiseEditEvent(UpdateFields.Category)
        End Set
    End Property
    Public Property KC1 As Double
        Get
            Return _KC1
        End Get
        Set(value As Double)
            _KC1 = value
            RaiseEditEvent(UpdateFields.KC1)
        End Set
    End Property
    Public Property MC As Double
        Get
            Return _MC
        End Get
        Set(value As Double)
            _MC = value
            RaiseEditEvent(UpdateFields.MC)
        End Set
    End Property
    Public Property HB_max As Integer
        Get
            Return _HB_max
        End Get
        Set(value As Integer)
            _HB_max = value
            RaiseEditEvent(UpdateFields.HB_max)
        End Set
    End Property
    Public Property HB_min As Integer
        Get
            Return _HB_min
        End Get
        Set(value As Integer)
            _HB_min = value
            RaiseEditEvent(UpdateFields.HB_min)
        End Set
    End Property
    Public ReadOnly Property MaterialCategoryNames(ByVal Cat As MaterialCategory)
        Get
            Select Case Cat
                Case MaterialCategory.N1
                    Return "Aluminum Alloy"
                Case MaterialCategory.N2
                    Return "Magnesium Alloy"
                Case MaterialCategory.N3
                    Return "Copper Alloy"
                Case MaterialCategory.N4
                    Return "Zinc Alloy"
                Case Else
                    Return ""
            End Select
        End Get
    End Property


    Public Sub New(ByVal Name As String, ByVal Cat As MaterialCategory, ByVal KC1 As Double, ByVal MC As Double, ByVal HB_max As Integer, Optional HB_min As Integer = -1)
        _Name = Name
        _Category = Cat


        _KC1 = KC1
        _MC = MC
        _HB_max = HB_max

        If HB_min = -1 Then 'not given - set both to max
            _HB_min = HB_max
        Else
            _HB_min = HB_min
        End If
    End Sub


    Public Enum MaterialCategory
        'Aluminum
        'Steel
        'Stainless_Steel
        'Cast_Iron

        'Non-ferrous
        N1 'aluminums
        N2 'magnesiums
        N3 'copper
        N4 'zinc
    End Enum


    Protected Overrides Sub RaiseEditEvent(ByVal e As Integer)
        MyBase.RaiseEditEvent_Base(Me, e)
    End Sub
    Public Enum UpdateFields
        Name
        Category
        KC1
        MC
        HB_min
        HB_max
    End Enum

End Class



Public Class ClassManager(Of T As BaseManagedClass)
    Private _Data As New List(Of T)

    Public Event EntityAdded(ByVal Item As Object)
    Public Event EntityDeleted(ByVal Item As Object)
    Public Event EntityEdited(ByVal Item As Object, ByVal e As Integer)

    Public ReadOnly Property AllIDs As List(Of Integer)
        Get
            Dim output As New List(Of Integer)

            For Each ent As T In _Data
                output.Add(ent.ID)
            Next

            Return output
        End Get
    End Property
    Public Function GetObject(ByVal ID As Integer) As T
        Return SearchForID(ID)
    End Function
    Public ReadOnly Property BindSource As BindingSource
        Get
            Dim output As New BindingSource
            output.DataSource = GetType(T)

            For Each item As T In _Data
                output.Add(item)
            Next

            Return output
        End Get
    End Property

    Public Sub Add(ByVal Item As T)

        Item.SetID(CreateUniqueID)
        AddHandler Item.ClassUpdated, AddressOf RedirectUpdateEvents
        _Data.Add(Item)

        RaiseEvent EntityAdded(Item)

    End Sub
    Public Sub Delete(ByVal Index As Integer)
        Dim deleteObj As T = SearchForID(Index)
        _Data.Remove(deleteObj)

        If deleteObj IsNot Nothing Then 'could throw error if trying to delete what doesn't exists
            RemoveHandler deleteObj.ClassUpdated, AddressOf RedirectUpdateEvents
            RaiseEvent EntityDeleted(deleteObj)
        End If
    End Sub


    Private Sub RedirectUpdateEvents(ByVal Sender As Object, ByVal e As Integer)
        RaiseEvent EntityEdited(Sender, e)
    End Sub
    Private Function CreateUniqueID() As Integer

        Dim IDlist As New List(Of Integer) 'contains all ids

        For Each Item As T In _Data
            IDlist.Add(Item.ID)
        Next

        Dim counter As Integer = 1

        While IDlist.Contains(counter) 'search for unused number
            counter += 1
        End While

        Return counter
    End Function
    Private Function SearchForID(ByVal SearchID As Integer) As T

        For Each e As T In _Data
            If e.ID = SearchID Then
                Return e
            End If
        Next
        Return Nothing
    End Function

    '--------------- old
    Private Interface IManagedClass

        Event ClassUpdated(ByVal Sender As Object, ByVal e As Integer)

        ReadOnly Property ID As Integer
        Sub SetID(ByVal ID As Integer)

    End Interface
    Private Class ManagedClassEventArgs(Of T_Arg)
        Inherits EventArgs

        Private _item As T_Arg
        Private _UpdateField As Integer
        Private _eventType As ManagedClassEventTypes

        Public ReadOnly Property Item As T_Arg
            Get
                Return _item
            End Get
        End Property
        Public ReadOnly Property UpdateField As Integer
            Get
                Return _UpdateField
            End Get
        End Property
        Public ReadOnly Property EventType As ManagedClassEventTypes
            Get
                Return _eventType
            End Get
        End Property

        Public Sub New(ByVal item As T_Arg, ByVal UpdateField As Integer, ByVal EType As ManagedClassEventTypes)
            _item = item
            _UpdateField = UpdateField
            _eventType = EType
        End Sub
        Public Sub New(ByVal item As T_Arg, ByVal EType As ManagedClassEventTypes)
            _item = item
            _eventType = EType
        End Sub

    End Class
    Private Enum ManagedClassEventTypes
        Add
        Edit
        Delete
    End Enum

End Class

Public MustInherit Class BaseManagedClass

    Private _ID As Integer = -1

    Public Event ClassUpdated(ByVal Sender As Object, ByVal e As Integer)

    Public ReadOnly Property ID As Integer
        Get
            If _ID = -1 Then
                Throw New Exception("ID was requested from base-class before assignment")
            Else
                Return _ID
            End If
        End Get
    End Property
    Public Sub SetID(ByVal ID As Integer)
        If _ID = -1 Then
            _ID = ID
        Else
            Throw New Exception("Tried to re-assign ID - can only be done once")
        End If
    End Sub

    Protected MustOverride Sub RaiseEditEvent(ByVal e As Integer)
    Protected Overridable Sub RaiseEditEvent_Base(ByVal Sender As Object, ByVal e As Integer)
        RaiseEvent ClassUpdated(Sender, e)
    End Sub

End Class 'allows auto handling of ID and edit event raising




Public Class Units

    '----------------- UPDATE THESE IF NEW UNITS ARE ADDED -------------------
    Private Shared ReadOnly Property ConversionFactor(ByVal Unit As Integer) As Double
        Get
            Select Case Unit
                '---------------- Length ------------------
                Case AllUnits.m
                    Return 1.0
                Case AllUnits.mm
                    Return 0.001
                Case AllUnits.cm
                    Return 0.01
                Case AllUnits.inch
                    Return 0.0254
                Case AllUnits.ft
                    Return 0.3048

                    '---------------------------Area----------------------
                Case AllUnits.m_squared
                    Return ConversionFactor(AllUnits.m) * ConversionFactor(AllUnits.m)
                Case AllUnits.mm_squared
                    Return ConversionFactor(AllUnits.mm) * ConversionFactor(AllUnits.mm)
                Case AllUnits.cm_squared
                    Return ConversionFactor(AllUnits.cm) * ConversionFactor(AllUnits.cm)
                Case AllUnits.in_squared
                    Return ConversionFactor(AllUnits.inch) * ConversionFactor(AllUnits.inch)
                Case AllUnits.ft_squared
                    Return ConversionFactor(AllUnits.ft) * ConversionFactor(AllUnits.ft)

                    '-------------------- Force ---------------------------

                Case AllUnits.N
                    Return 1
                Case AllUnits.lb
                    Return 4.44822

                    '----------------- Pressure ----------------------
                Case AllUnits.Pa
                    Return 1
                Case AllUnits.KPa
                    Return 1000
                Case AllUnits.MPa
                    Return 1000 * 1000
                Case AllUnits.GPa
                    Return 1000 * 1000 * 1000
                Case AllUnits.Psi
                    Return 6894.76
                Case AllUnits.Bar
                    Return 100000

                    '------------------ Power --------------------
                Case AllUnits.W
                    Return 1
                Case AllUnits.KW
                    Return 1000
                Case AllUnits.HP
                    Return 745.7

                    '------------------ Speed --------------------

                Case AllUnits.mm_min
                    Return 1
                Case AllUnits.m_min
                    Return 1000
                Case AllUnits.in_min
                    Return 25.4

                    '------------------ Angluar Speed --------------------

                Case AllUnits.rad_s
                    Return 1
                Case AllUnits.RPM
                    Return 60 / (2.0 * PI)

                Case Else
                    Return Nothing
            End Select
        End Get
    End Property
    Public Shared ReadOnly Property UnitStrings(ByVal Unit As AllUnits) As String()
        Get

            '-------- NOTE: FIRST ARRAY ENTRY IS DEFAULT ------------

            Select Case Unit
                '-------------------- Length -------------------
                Case AllUnits.m
                    Return {"m"}
                Case AllUnits.mm
                    Return {"mm"}
                Case AllUnits.cm
                    Return {"cm"}
                Case AllUnits.inch
                    Return {"in", "inch", """"}
                Case AllUnits.ft
                    Return {"ft", "feet", "'"}

                    '---------------------------Area----------------------

                Case AllUnits.m_squared
                    Return {"m^2"}
                Case AllUnits.mm_squared
                    Return {"mm^2"}
                Case AllUnits.cm_squared
                    Return {"cm^2"}
                Case AllUnits.in_squared
                    Return {"in^2", "sqin"}
                Case AllUnits.ft_squared
                    Return {"ft^2", "sqft"}

                    '-------------------- Force ---------------------------

                Case AllUnits.N
                    Return {"N"}
                Case AllUnits.lb
                    Return {"lb", "lbs"}

                    '----------------- Pressure ----------------------
                Case AllUnits.Pa
                    Return {"Pa", "pa"}
                Case AllUnits.KPa
                    Return {"KPa", "kpa", "Kpa"}
                Case AllUnits.MPa
                    Return {"MPa", "mpa", "Mpa"}
                Case AllUnits.GPa
                    Return {"GPa", "gpa", "Gpa"}
                Case AllUnits.Psi
                    Return {"psi", "Psi"}
                Case AllUnits.Bar
                    Return {"bar", "Bar"}

                    '----------------- Power ----------------------
                Case AllUnits.W
                    Return {"W", "w"}
                Case AllUnits.KW
                    Return {"KW", "kw", "Kw", "kW"}
                Case AllUnits.HP
                    Return {"HP", "hp", "Hp"}

                    '----------------- Speed ----------------------
                Case AllUnits.mm_min
                    Return {"mm/min"}

                Case AllUnits.in_min
                    Return {"in/min", "IPM", "ipm"}

                Case AllUnits.m_min
                    Return {"m/min"}

                    '----------------- Rotation Speed ----------------------
                Case AllUnits.rad_s
                    Return {"rad/s"}
                Case AllUnits.RPM
                    Return {"RPM", "rpm"}


                Case Else
                    Return Nothing
            End Select
        End Get
    End Property
    Public Shared ReadOnly Property DefaultUnit(ByVal UnitType As DataUnitType) As AllUnits
        Get
            Select Case UnitType
                Case DataUnitType.Length
                    Return AllUnits.mm

                Case DataUnitType.Area
                    Return AllUnits.m_squared

                Case DataUnitType.Force
                    Return AllUnits.N

                Case DataUnitType.Pressure
                    Return AllUnits.Pa

                Case DataUnitType.Power
                    Return AllUnits.W

                Case DataUnitType.Speed
                    Return AllUnits.mm_min

                Case DataUnitType.Rot_Speed
                    Return AllUnits.RPM

                Case Else
                    Return Nothing
            End Select
        End Get
    End Property
    Public Shared ReadOnly Property UnitTypeRange(ByVal UnitType As DataUnitType) As Integer()
        Get
            Select Case UnitType
                Case DataUnitType.Length
                    Return {0, 4}

                Case DataUnitType.Area
                    Return {5, 9}

                Case DataUnitType.Force
                    Return {10, 11}

                Case DataUnitType.Pressure
                    Return {12, 17}

                Case DataUnitType.Power
                    Return {18, 20}

                Case DataUnitType.Speed
                    Return {21, 23}

                Case DataUnitType.Rot_Speed
                    Return {24, 25}

                Case Else
                    Return Nothing
            End Select
        End Get
    End Property

    '----------------------------------------------------------------------------
    Public Shared ReadOnly Property TypeUnitStrings(ByVal UnitType As DataUnitType) As List(Of String)
        Get
            Dim range As Integer() = UnitTypeRange(UnitType)

            Dim output As New List(Of String)

            For i As Integer = range(0) To range(1) 'search through the enum range for that type
                For Each S As String In UnitStrings(i) 'get all the strings for each enum and add to output
                    output.Add(S)
                Next
            Next

            Return output
        End Get
    End Property
    Public Shared ReadOnly Property UnitEnums(ByVal UnitString As String) As AllUnits
        Get
            For I As Integer = 0 To [Enum].GetNames(GetType(AllUnits)).Count - 1
                If UnitStrings(I).Contains(UnitString) Then
                    Return I
                End If
            Next
            Return -1 'if could not find
        End Get
    End Property
    Public Shared Function Convert(ByVal InputUnit As AllUnits, ByVal Data As Double, ByVal OutputUnit As AllUnits) As Double
        Return Data * ConversionFactor(InputUnit) / ConversionFactor(OutputUnit)
    End Function



    Public Enum DataUnitType
        Length = 0 'm
        Area = 1 'm^2
        Force = 2 'N
        Pressure = 3 'Pa
        Power = 4 'W
        Speed = 5 'mm/min
        Rot_Speed = 6 'rad/s
    End Enum
    Public Enum AllUnits
        '--------------- Length -------------------
        mm
        cm
        m
        inch
        ft
        '------------- Area -----------------------
        mm_squared
        cm_squared
        m_squared
        in_squared
        ft_squared
        '------------- Force -----------------------
        N
        lb
        '------------- Pressure ---------------
        KPa
        MPa
        GPa
        Pa
        Psi
        Bar

        '------------- Power ---------------
        W
        KW
        HP

        '------------ Speed -----------------
        mm_min
        in_min
        m_min

        '------------ Rotational Speed -----------------
        rad_s
        RPM


    End Enum


End Class
