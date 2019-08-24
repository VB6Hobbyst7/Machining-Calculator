Imports HsmSwLib
Module Module_Main

    '---------- manager objects for various databases needed in the program

    Public WithEvents Mgr_Mill_Tool As New ClassManager(Of Milling_Tool)
    Public WithEvents Mgr_Mill_Machine As New ClassManager(Of Milling_Machine)
    Public WithEvents Mgr_Matl_Tool As New ClassManager(Of Tool_material)
    Public WithEvents Mgr_Matl As New ClassManager(Of Material)

    Public WithEvents Mgr_Vals As New ValMgr 'manages current active numbers in the program


    Public LoadedForm As Mainform = Nothing 'allows the mainform to be called from this module - is set onload

    Public Sub LoadDefaultEntities()

        '---------- Load Tool Materials -------------
        Mgr_Matl_Tool.Add(New Tool_material("Carbide", 700, 344))
        Mgr_Matl_Tool.Add(New Tool_material("HSS", 200, 3250))


        '------------- Load milling Machines ---------------------
        Mgr_Mill_Machine.Add(New Milling_Machine("Mill1", Milling_Machine.DriveType.Belt, 100, 1000, 1000))
        Mgr_Mill_Machine.Add(New Milling_Machine("Mill2", Milling_Machine.DriveType.Belt, 100, 1000, 1000))

        '--------------- Load Materials --------------------------

        Mgr_Matl.Add(New Material("Aluminum 6061", Material.MaterialCategory.N1, 650, 0.25, 100, 60))

        '---------------- Load Tools ---------------------------

        Dim Geo As New Dictionary(Of Milling_Tool.GF, Double)
        Geo.Add(Milling_Tool.GF.Diameter, 12.7)
        Geo.Add(Milling_Tool.GF.Flutes, 4)
        Geo.Add(Milling_Tool.GF.Extension, 25)

        Mgr_Mill_Tool.Add(New Milling_Tool("TestTool", 1, Tool_Coating.Uncoated_Carbide, Milling_Tool.Milling_Tool_Type.Flat, Geo))

        Geo(Milling_Tool.GF.Extension) = 12
        Geo(Milling_Tool.GF.Diameter) = 5
        Mgr_Mill_Tool.Add(New Milling_Tool("TestTool2", 1, Tool_Coating.Uncoated_Carbide, Milling_Tool.Milling_Tool_Type.Flat, Geo))


        '---------------- SetDefaultActiveParams ---------------------------

        'SOMETHING SHOULD BE DONE TO ENSURE THIS IS REFLECTED IN THE UI
        Mgr_Vals.UpdateValue_Silent(ValMgr.ValIDs.ActiveMachineID, 1) 'these values need to be initialized or they will error
        Mgr_Vals.UpdateValue_Silent(ValMgr.ValIDs.ActiveToolID, 1) 'these values need to be initialized or they will error
        Mgr_Vals.UpdateValue_Silent(ValMgr.ValIDs.ActiveMaterialID, 1) 'these values need to be initialized or they will error


    End Sub 'loads default tools, machines, matls, etc for startup
    Public Sub LoadEventHandlers()

        AddHandler Mgr_Mill_Tool.EntityAdded, AddressOf Mgr_EntityCountChanged
        AddHandler Mgr_Mill_Tool.EntityDeleted, AddressOf Mgr_EntityCountChanged

        AddHandler Mgr_Mill_Machine.EntityAdded, AddressOf Mgr_EntityCountChanged
        AddHandler Mgr_Mill_Machine.EntityDeleted, AddressOf Mgr_EntityCountChanged

        AddHandler Mgr_Matl_Tool.EntityAdded, AddressOf Mgr_EntityCountChanged
        AddHandler Mgr_Matl_Tool.EntityDeleted, AddressOf Mgr_EntityCountChanged

        AddHandler Mgr_Matl.EntityAdded, AddressOf Mgr_EntityCountChanged
        AddHandler Mgr_Matl.EntityDeleted, AddressOf Mgr_EntityCountChanged


        AddHandler LoadedForm.UC_Cut_Setup1.ControlUpdated, AddressOf LoadedForm.ControlValueUpdated
        AddHandler LoadedForm.UC_Machine_Matl_Setup1.ControlUpdated, AddressOf LoadedForm.ControlValueUpdated
        AddHandler LoadedForm.UC_SpeedsFeeds1.ControlUpdated, AddressOf LoadedForm.ControlValueUpdated
        AddHandler LoadedForm.UC_Tool_Setup1.ControlUpdated, AddressOf LoadedForm.ControlValueUpdated

    End Sub 'adds event handlers
    

    Private Sub Mgr_EntityCountChanged(ByVal Item As Object)

        Select Case Item.GetType
            Case GetType(Milling_Machine)
                LoadedForm.UC_Machine_Matl_Setup1.UpdateData(Mgr_Mill_Machine.BindSource, Mgr_Matl.BindSource)

            Case GetType(Material)
                LoadedForm.UC_Machine_Matl_Setup1.UpdateData(Mgr_Mill_Machine.BindSource, Mgr_Matl.BindSource)

            Case GetType(Milling_Tool)
                LoadedForm.UC_Tool_Setup1.UpdateData(Mgr_Mill_Tool.BindSource)

            Case GetType(Tool_material)

        End Select
    End Sub 'fires when a managed entity is added/deleted

    Private Sub ValueUpdated(ByVal Val As ValMgr.Val, ByVal ID As ValMgr.ValIDs, ByVal UserInput As Boolean) Handles Mgr_Vals.ValueUpdated
        Select Case ID

            Case ValMgr.ValIDs.Vc, ValMgr.ValIDs.RPM, ValMgr.ValIDs.Fz, ValMgr.ValIDs.Vf
                LoadedForm.UC_SpeedsFeeds1.SetTxtValue(ID, Val.Val)

            Case ValMgr.ValIDs.DOC, ValMgr.ValIDs.WOC
                LoadedForm.UC_Cut_Setup1.SetTxtValue(ID, Val.Val)



        End Select
    End Sub 'fires when an active param in the program is changed. Use this to update UI items.
    Private Sub CalculationRequired_Old(ByVal ParentVal As ValMgr.Val, ByVal ParentID As ValMgr.ValIDs, ByVal UserInput As Boolean)

        Dim activeTool As Milling_Tool = Mgr_Mill_Tool.GetObject(Mgr_Vals.Value(ValMgr.ValIDs.ActiveToolID))
        Dim activeMatl As Material = Mgr_Matl.GetObject(Mgr_Vals.Value(ValMgr.ValIDs.ActiveMaterialID))
        Dim activeMachine As Milling_Machine = Mgr_Mill_Machine.GetObject(Mgr_Vals.Value(ValMgr.ValIDs.ActiveMachineID))


        For Each ChildID As ValMgr.ValIDs In ParentVal.Children

            Select Case ChildID

                Case ValMgr.ValIDs.Vc
                    Mgr_Vals.UpdateValue(ChildID, Calculations.Vc(activeTool.Coating, activeMatl.Category))

                Case ValMgr.ValIDs.RPM
                    Dim RPM As Double = Calculations.N(Mgr_Vals.Value(ValMgr.ValIDs.Vc), activeTool.D_Cap(Mgr_Vals.Value(ValMgr.ValIDs.DOC)))
                    Mgr_Vals.UpdateValue(ChildID, RPM)

                Case ValMgr.ValIDs.Fz
                    Dim Hex As Double = Calculations.Hex(Mgr_Vals.Value(ValMgr.ValIDs.DOC), 0)
                    Dim Fz_comp As Double = Calculations.fz_comp_ChipThinning(Hex, Mgr_Vals.Value(ValMgr.ValIDs.DOC), Mgr_Vals.Value(ValMgr.ValIDs.WOC), activeTool.D_Cap(Mgr_Vals.Value(ValMgr.ValIDs.DOC)), 0, 0)
                    Mgr_Vals.UpdateValue(ChildID, Fz_comp)

                Case ValMgr.ValIDs.Vf
                    Dim Vf As Double = Calculations.Vf(Mgr_Vals.Value(ValMgr.ValIDs.Fz), Mgr_Vals.Value(ValMgr.ValIDs.RPM), activeTool.Geometry(Milling_Tool.GF.Flutes))
                    Mgr_Vals.UpdateValue(ChildID, Vf)


                Case ValMgr.ValIDs.DOC
                    If CBool(Mgr_Vals.Value(ValMgr.ValIDs.Slotting)) Then

                    Else
                        Dim fluteLength As Double = activeTool.Geometry(Milling_Tool.GF.Flute_Length)
                        Mgr_Vals.UpdateValue(ChildID, Calculations.DOC(fluteLength))
                    End If


            End Select




        Next

        For Each FeedbackID As ValMgr.ValIDs In ParentVal.Feedback
            Select Case FeedbackID

                Case ValMgr.ValIDs.Vc
                    If UserInput Then
                        Mgr_Vals.UpdateValue_UIonly(FeedbackID, Calculations.Vc_2(Mgr_Vals.Value(ValMgr.ValIDs.RPM), activeTool.D_Cap(Mgr_Vals.Value(ValMgr.ValIDs.DOC))))
                    End If

                Case ValMgr.ValIDs.Fz
                    If UserInput Then
                        Mgr_Vals.UpdateValue_UIonly(FeedbackID, Calculations.fz_2(Mgr_Vals.Value(ValMgr.ValIDs.Vf), Mgr_Vals.Value(ValMgr.ValIDs.RPM), activeTool.Geometry(Milling_Tool.GF.Flutes)))
                    End If


            End Select
        Next

    End Sub 'fires when an updated value has children which will need to be calculated. 

    Private Sub CalculationRequired(ByVal ParentVal As ValMgr.Val, ByVal ParentID As ValMgr.ValIDs, ByVal UserInput As Boolean) Handles Mgr_Vals.CalculationRequired

        Dim activeTool As Milling_Tool = Mgr_Mill_Tool.GetObject(Mgr_Vals.Value(ValMgr.ValIDs.ActiveToolID))
        Dim activeToolMatl As Tool_material = Mgr_Matl_Tool.GetObject(activeTool.Material)
        Dim activeMatl As Material = Mgr_Matl.GetObject(Mgr_Vals.Value(ValMgr.ValIDs.ActiveMaterialID))
        Dim activeMachine As Milling_Machine = Mgr_Mill_Machine.GetObject(Mgr_Vals.Value(ValMgr.ValIDs.ActiveMachineID))

        Dim slotting As Boolean = CBool(Mgr_Vals.Value(ValMgr.ValIDs.Slotting))

        '--------- Allowable Deflections

        Dim Td As Double = 0

        If CBool(Mgr_Vals.Value(ValMgr.ValIDs.Finishing)) Then
            Td = 0.004
        Else
            Td = 0.02
        End If


        If slotting Then

            '--------- START WITH A GUESS FOR DOC --------
            Dim DOC As Double = 2

            '-------- Calculate WOC ----------
            Mgr_Vals.UpdateValue_UIonly(ValMgr.ValIDs.WOC, activeTool.Geometry(Milling_Tool.GF.Diameter)) 'WOC = Tool Diameter

            '-------------- Calculate RPM -------------------

            'NOTE: THIS DEPENDS ON DOC, IT NEEDS TO ITERATE - CHANGE THIS
            Dim Vc As Double = Calculations.Vc(activeTool.Coating, activeMatl.Category)
            Dim RPM As Double = Calculations.N(Vc, activeTool.D_Cap(DOC))

            If RPM > activeMachine.MaxRPM Then 'check machine RPM Limit
                RPM = activeMachine.MaxRPM
                Vc = Calculations.Vc_2(RPM, activeTool.D_Cap(DOC))
            End If

            Mgr_Vals.UpdateValue_UIonly(ValMgr.ValIDs.Vc, Vc)
            Mgr_Vals.UpdateValue_UIonly(ValMgr.ValIDs.RPM, RPM)

            '-------- Calculate Feed Rate ---------- (Cut power depends on feed)

            'NOTE: THIS DEPENDS ON DOC, IT NEEDS TO ITERATE - CHANGE THIS
            Dim Hex As Double = Calculations.Hex(Mgr_Vals.Value(ValMgr.ValIDs.DOC), 0)
            Dim Fz_comp As Double = Calculations.fz_comp_ChipThinning(Hex, DOC, Mgr_Vals.Value(ValMgr.ValIDs.WOC), activeTool.D_Cap(DOC), activeTool.Geometry(Milling_Tool.GF.Nose_Radius), activeTool.Geometry(Milling_Tool.GF.Taper_Angle))
            Dim Vf As Double = Calculations.Vf(Fz_comp, RPM, activeTool.Geometry(Milling_Tool.GF.Flutes))

            If Vf > activeMachine.MaxFeed Then 'check machine feed Limit
                Vf = activeMachine.MaxFeed
                Fz_comp = Calculations.fz_2(Vf, RPM, activeTool.Geometry(Milling_Tool.GF.Flutes))
            End If

            Mgr_Vals.UpdateValue_UIonly(ValMgr.ValIDs.Fz, Fz_comp)
            Mgr_Vals.UpdateValue_UIonly(ValMgr.ValIDs.Vf, Vf)

            '-------- Calculate Machine Power Limit ----------

            Dim pc As Double

            If activeMachine.PowerCurveKnown Then
                pc = activeMachine.Power_Curve.Power_ByRPM_LinInterpolate(RPM)
            Else
                pc = activeMachine.MaxPower
            End If


            '-------- Calculate DOC Limits ----------
            Dim DOC_Limits As New List(Of Double)
            Dim Kc As Double = Calculations.Kc(activeMatl.KC1, Hex, activeMatl.MC, activeTool.Geometry(Milling_Tool.GF.Rake_Angle))

            DOC_Limits.Add(Calculations.DOC_PC(pc, Mgr_Vals.Value(ValMgr.ValIDs.WOC), Mgr_Vals.Value(ValMgr.ValIDs.Vf), Kc))

            Dim diam As Double = activeTool.Geometry(Milling_Tool.GF.Diameter)
            Dim ext As Double = activeTool.Geometry(Milling_Tool.GF.Extension)
            Dim WOC As Double = Mgr_Vals.Value(ValMgr.ValIDs.WOC)


            DOC_Limits.Add(Calculations.DOC_Td(Td, activeToolMatl.E, diam, ext, WOC, Kc))
            DOC_Limits.Add(activeTool.Geometry(Milling_Tool.GF.Flute_Length))

            Mgr_Vals.UpdateValue_UIonly(ValMgr.ValIDs.DOC, DOC_Limits.Min)



        Else



        End If


    End Sub 'fires when an updated value has children which will need to be calculated. 


End Module
