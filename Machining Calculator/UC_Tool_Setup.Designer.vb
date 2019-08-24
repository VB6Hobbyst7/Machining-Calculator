<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UC_Tool_Setup
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
        Me.ComboBox_Tool = New System.Windows.Forms.ComboBox()
        Me.Btn_Tool_Add = New System.Windows.Forms.Button()
        Me.Btn_Tool_Edit = New System.Windows.Forms.Button()
        Me.Panel_header = New System.Windows.Forms.Panel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Panel_header.SuspendLayout()
        Me.SuspendLayout()
        '
        'ComboBox_Tool
        '
        Me.ComboBox_Tool.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBox_Tool.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_Tool.FormattingEnabled = True
        Me.ComboBox_Tool.Location = New System.Drawing.Point(74, 9)
        Me.ComboBox_Tool.Name = "ComboBox_Tool"
        Me.ComboBox_Tool.Size = New System.Drawing.Size(402, 21)
        Me.ComboBox_Tool.TabIndex = 1
        '
        'Btn_Tool_Add
        '
        Me.Btn_Tool_Add.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Btn_Tool_Add.Location = New System.Drawing.Point(482, 7)
        Me.Btn_Tool_Add.Name = "Btn_Tool_Add"
        Me.Btn_Tool_Add.Size = New System.Drawing.Size(25, 23)
        Me.Btn_Tool_Add.TabIndex = 4
        Me.Btn_Tool_Add.Text = "+"
        Me.Btn_Tool_Add.UseVisualStyleBackColor = True
        '
        'Btn_Tool_Edit
        '
        Me.Btn_Tool_Edit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Btn_Tool_Edit.Location = New System.Drawing.Point(513, 7)
        Me.Btn_Tool_Edit.Name = "Btn_Tool_Edit"
        Me.Btn_Tool_Edit.Size = New System.Drawing.Size(45, 23)
        Me.Btn_Tool_Edit.TabIndex = 5
        Me.Btn_Tool_Edit.Text = "Edit"
        Me.Btn_Tool_Edit.UseVisualStyleBackColor = True
        '
        'Panel_header
        '
        Me.Panel_header.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Panel_header.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.Panel_header.Controls.Add(Me.Label3)
        Me.Panel_header.Location = New System.Drawing.Point(0, 0)
        Me.Panel_header.Name = "Panel_header"
        Me.Panel_header.Size = New System.Drawing.Size(68, 39)
        Me.Panel_header.TabIndex = 8
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(12, 7)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(40, 16)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Tool"
        '
        'UC_Machine_Matl_Setup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Controls.Add(Me.Panel_header)
        Me.Controls.Add(Me.Btn_Tool_Edit)
        Me.Controls.Add(Me.Btn_Tool_Add)
        Me.Controls.Add(Me.ComboBox_Tool)
        Me.Name = "UC_Machine_Matl_Setup"
        Me.Size = New System.Drawing.Size(575, 39)
        Me.Panel_header.ResumeLayout(False)
        Me.Panel_header.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ComboBox_Tool As System.Windows.Forms.ComboBox
    Friend WithEvents Btn_Tool_Add As System.Windows.Forms.Button
    Friend WithEvents Btn_Tool_Edit As System.Windows.Forms.Button
    Friend WithEvents Panel_header As System.Windows.Forms.Panel
    Friend WithEvents Label3 As System.Windows.Forms.Label

End Class
