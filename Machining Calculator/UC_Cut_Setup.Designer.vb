<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UC_Cut_Setup
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
        Me.Panel_header = New System.Windows.Forms.Panel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.CheckBox_FullWOC = New System.Windows.Forms.CheckBox()
        Me.CheckBox_Finishing = New System.Windows.Forms.CheckBox()
        Me.Panel_header.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel_header
        '
        Me.Panel_header.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel_header.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.Panel_header.Controls.Add(Me.Label3)
        Me.Panel_header.Location = New System.Drawing.Point(-1, -1)
        Me.Panel_header.Name = "Panel_header"
        Me.Panel_header.Size = New System.Drawing.Size(241, 30)
        Me.Panel_header.TabIndex = 9
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(12, 7)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(114, 16)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Cut Parameters"
        '
        'CheckBox_FullWOC
        '
        Me.CheckBox_FullWOC.AutoSize = True
        Me.CheckBox_FullWOC.Location = New System.Drawing.Point(151, 35)
        Me.CheckBox_FullWOC.Name = "CheckBox_FullWOC"
        Me.CheckBox_FullWOC.Size = New System.Drawing.Size(83, 17)
        Me.CheckBox_FullWOC.TabIndex = 10
        Me.CheckBox_FullWOC.Text = "Slot/Pocket"
        Me.CheckBox_FullWOC.UseVisualStyleBackColor = True
        '
        'CheckBox_Finishing
        '
        Me.CheckBox_Finishing.AutoSize = True
        Me.CheckBox_Finishing.Location = New System.Drawing.Point(151, 61)
        Me.CheckBox_Finishing.Name = "CheckBox_Finishing"
        Me.CheckBox_Finishing.Size = New System.Drawing.Size(67, 17)
        Me.CheckBox_Finishing.TabIndex = 11
        Me.CheckBox_Finishing.Text = "Finishing"
        Me.CheckBox_Finishing.UseVisualStyleBackColor = True
        '
        'UC_Cut_Setup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Controls.Add(Me.CheckBox_Finishing)
        Me.Controls.Add(Me.CheckBox_FullWOC)
        Me.Controls.Add(Me.Panel_header)
        Me.Name = "UC_Cut_Setup"
        Me.Size = New System.Drawing.Size(239, 85)
        Me.Panel_header.ResumeLayout(False)
        Me.Panel_header.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel_header As System.Windows.Forms.Panel
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents CheckBox_FullWOC As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox_Finishing As System.Windows.Forms.CheckBox

End Class
