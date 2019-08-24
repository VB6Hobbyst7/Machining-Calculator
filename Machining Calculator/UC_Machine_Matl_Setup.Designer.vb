<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UC_Machine_Matl_Setup
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ComboBox_Machine = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.ComboBox_Material = New System.Windows.Forms.ComboBox()
        Me.Btn_Machine_Add = New System.Windows.Forms.Button()
        Me.Btn_Machine_Edit = New System.Windows.Forms.Button()
        Me.Btn_Matl_Edit = New System.Windows.Forms.Button()
        Me.Btn_Matl_Add = New System.Windows.Forms.Button()
        Me.Panel_header = New System.Windows.Forms.Panel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Panel_header.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(132, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(66, 16)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Machine"
        '
        'ComboBox_Machine
        '
        Me.ComboBox_Machine.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBox_Machine.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_Machine.FormattingEnabled = True
        Me.ComboBox_Machine.Location = New System.Drawing.Point(202, 9)
        Me.ComboBox_Machine.Name = "ComboBox_Machine"
        Me.ComboBox_Machine.Size = New System.Drawing.Size(274, 21)
        Me.ComboBox_Machine.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(132, 45)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(64, 16)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Material"
        '
        'ComboBox_Material
        '
        Me.ComboBox_Material.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBox_Material.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_Material.FormattingEnabled = True
        Me.ComboBox_Material.Location = New System.Drawing.Point(202, 44)
        Me.ComboBox_Material.Name = "ComboBox_Material"
        Me.ComboBox_Material.Size = New System.Drawing.Size(274, 21)
        Me.ComboBox_Material.TabIndex = 3
        '
        'Btn_Machine_Add
        '
        Me.Btn_Machine_Add.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Btn_Machine_Add.Location = New System.Drawing.Point(482, 7)
        Me.Btn_Machine_Add.Name = "Btn_Machine_Add"
        Me.Btn_Machine_Add.Size = New System.Drawing.Size(25, 23)
        Me.Btn_Machine_Add.TabIndex = 4
        Me.Btn_Machine_Add.Text = "+"
        Me.Btn_Machine_Add.UseVisualStyleBackColor = True
        '
        'Btn_Machine_Edit
        '
        Me.Btn_Machine_Edit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Btn_Machine_Edit.Location = New System.Drawing.Point(513, 7)
        Me.Btn_Machine_Edit.Name = "Btn_Machine_Edit"
        Me.Btn_Machine_Edit.Size = New System.Drawing.Size(45, 23)
        Me.Btn_Machine_Edit.TabIndex = 5
        Me.Btn_Machine_Edit.Text = "Edit"
        Me.Btn_Machine_Edit.UseVisualStyleBackColor = True
        '
        'Btn_Matl_Edit
        '
        Me.Btn_Matl_Edit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Btn_Matl_Edit.Location = New System.Drawing.Point(513, 42)
        Me.Btn_Matl_Edit.Name = "Btn_Matl_Edit"
        Me.Btn_Matl_Edit.Size = New System.Drawing.Size(45, 23)
        Me.Btn_Matl_Edit.TabIndex = 7
        Me.Btn_Matl_Edit.Text = "Edit"
        Me.Btn_Matl_Edit.UseVisualStyleBackColor = True
        '
        'Btn_Matl_Add
        '
        Me.Btn_Matl_Add.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Btn_Matl_Add.Location = New System.Drawing.Point(482, 42)
        Me.Btn_Matl_Add.Name = "Btn_Matl_Add"
        Me.Btn_Matl_Add.Size = New System.Drawing.Size(25, 23)
        Me.Btn_Matl_Add.TabIndex = 6
        Me.Btn_Matl_Add.Text = "+"
        Me.Btn_Matl_Add.UseVisualStyleBackColor = True
        '
        'Panel_header
        '
        Me.Panel_header.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Panel_header.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.Panel_header.Controls.Add(Me.Label3)
        Me.Panel_header.Location = New System.Drawing.Point(0, 0)
        Me.Panel_header.Name = "Panel_header"
        Me.Panel_header.Size = New System.Drawing.Size(126, 74)
        Me.Panel_header.TabIndex = 8
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(8, 7)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(110, 16)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Machine/Stock"
        '
        'UC_Machine_Matl_Setup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Controls.Add(Me.Panel_header)
        Me.Controls.Add(Me.Btn_Matl_Edit)
        Me.Controls.Add(Me.Btn_Matl_Add)
        Me.Controls.Add(Me.Btn_Machine_Edit)
        Me.Controls.Add(Me.Btn_Machine_Add)
        Me.Controls.Add(Me.ComboBox_Material)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.ComboBox_Machine)
        Me.Controls.Add(Me.Label1)
        Me.Name = "UC_Machine_Matl_Setup"
        Me.Size = New System.Drawing.Size(575, 74)
        Me.Panel_header.ResumeLayout(False)
        Me.Panel_header.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ComboBox_Machine As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents ComboBox_Material As System.Windows.Forms.ComboBox
    Friend WithEvents Btn_Machine_Add As System.Windows.Forms.Button
    Friend WithEvents Btn_Machine_Edit As System.Windows.Forms.Button
    Friend WithEvents Btn_Matl_Edit As System.Windows.Forms.Button
    Friend WithEvents Btn_Matl_Add As System.Windows.Forms.Button
    Friend WithEvents Panel_header As System.Windows.Forms.Panel
    Friend WithEvents Label3 As System.Windows.Forms.Label

End Class
