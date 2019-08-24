<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UC_Outputs
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
        Me.ProgressBar_MRR = New System.Windows.Forms.ProgressBar()
        Me.Label_MRR = New System.Windows.Forms.Label()
        Me.Label_Life = New System.Windows.Forms.Label()
        Me.ProgressBar_Life = New System.Windows.Forms.ProgressBar()
        Me.Label_Power = New System.Windows.Forms.Label()
        Me.ProgressBar_Power = New System.Windows.Forms.ProgressBar()
        Me.Label_Torque = New System.Windows.Forms.Label()
        Me.ProgressBar_Torque = New System.Windows.Forms.ProgressBar()
        Me.Label_ToolForce = New System.Windows.Forms.Label()
        Me.ProgressBar_ToolForce = New System.Windows.Forms.ProgressBar()
        Me.Label_ToolDefl = New System.Windows.Forms.Label()
        Me.ProgressBar_ToolDefl = New System.Windows.Forms.ProgressBar()
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
        Me.Panel_header.Size = New System.Drawing.Size(249, 30)
        Me.Panel_header.TabIndex = 9
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(12, 7)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(60, 16)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Outputs"
        '
        'ProgressBar_MRR
        '
        Me.ProgressBar_MRR.Location = New System.Drawing.Point(26, 58)
        Me.ProgressBar_MRR.Maximum = 150
        Me.ProgressBar_MRR.Name = "ProgressBar_MRR"
        Me.ProgressBar_MRR.Size = New System.Drawing.Size(190, 11)
        Me.ProgressBar_MRR.Step = 1
        Me.ProgressBar_MRR.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        Me.ProgressBar_MRR.TabIndex = 11
        Me.ProgressBar_MRR.Value = 20
        '
        'Label_MRR
        '
        Me.Label_MRR.AutoSize = True
        Me.Label_MRR.Location = New System.Drawing.Point(23, 42)
        Me.Label_MRR.Name = "Label_MRR"
        Me.Label_MRR.Size = New System.Drawing.Size(35, 13)
        Me.Label_MRR.TabIndex = 12
        Me.Label_MRR.Text = "MRR:"
        '
        'Label_Life
        '
        Me.Label_Life.AutoSize = True
        Me.Label_Life.Location = New System.Drawing.Point(23, 152)
        Me.Label_Life.Name = "Label_Life"
        Me.Label_Life.Size = New System.Drawing.Size(51, 13)
        Me.Label_Life.TabIndex = 14
        Me.Label_Life.Text = "Tool Life:"
        '
        'ProgressBar_Life
        '
        Me.ProgressBar_Life.Location = New System.Drawing.Point(26, 168)
        Me.ProgressBar_Life.Maximum = 150
        Me.ProgressBar_Life.Name = "ProgressBar_Life"
        Me.ProgressBar_Life.Size = New System.Drawing.Size(190, 11)
        Me.ProgressBar_Life.Step = 1
        Me.ProgressBar_Life.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        Me.ProgressBar_Life.TabIndex = 13
        Me.ProgressBar_Life.Value = 20
        '
        'Label_Power
        '
        Me.Label_Power.AutoSize = True
        Me.Label_Power.Location = New System.Drawing.Point(23, 76)
        Me.Label_Power.Name = "Label_Power"
        Me.Label_Power.Size = New System.Drawing.Size(40, 13)
        Me.Label_Power.TabIndex = 16
        Me.Label_Power.Text = "Power:"
        '
        'ProgressBar_Power
        '
        Me.ProgressBar_Power.Location = New System.Drawing.Point(26, 92)
        Me.ProgressBar_Power.Maximum = 150
        Me.ProgressBar_Power.Name = "ProgressBar_Power"
        Me.ProgressBar_Power.Size = New System.Drawing.Size(190, 11)
        Me.ProgressBar_Power.Step = 1
        Me.ProgressBar_Power.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        Me.ProgressBar_Power.TabIndex = 15
        Me.ProgressBar_Power.Value = 20
        '
        'Label_Torque
        '
        Me.Label_Torque.AutoSize = True
        Me.Label_Torque.Location = New System.Drawing.Point(23, 113)
        Me.Label_Torque.Name = "Label_Torque"
        Me.Label_Torque.Size = New System.Drawing.Size(44, 13)
        Me.Label_Torque.TabIndex = 18
        Me.Label_Torque.Text = "Torque:"
        '
        'ProgressBar_Torque
        '
        Me.ProgressBar_Torque.Location = New System.Drawing.Point(26, 129)
        Me.ProgressBar_Torque.Maximum = 150
        Me.ProgressBar_Torque.Name = "ProgressBar_Torque"
        Me.ProgressBar_Torque.Size = New System.Drawing.Size(190, 11)
        Me.ProgressBar_Torque.Step = 1
        Me.ProgressBar_Torque.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        Me.ProgressBar_Torque.TabIndex = 17
        Me.ProgressBar_Torque.Value = 20
        '
        'Label_ToolForce
        '
        Me.Label_ToolForce.AutoSize = True
        Me.Label_ToolForce.Location = New System.Drawing.Point(23, 192)
        Me.Label_ToolForce.Name = "Label_ToolForce"
        Me.Label_ToolForce.Size = New System.Drawing.Size(61, 13)
        Me.Label_ToolForce.TabIndex = 20
        Me.Label_ToolForce.Text = "Tool Force:"
        '
        'ProgressBar_ToolForce
        '
        Me.ProgressBar_ToolForce.Location = New System.Drawing.Point(26, 208)
        Me.ProgressBar_ToolForce.Maximum = 150
        Me.ProgressBar_ToolForce.Name = "ProgressBar_ToolForce"
        Me.ProgressBar_ToolForce.Size = New System.Drawing.Size(190, 11)
        Me.ProgressBar_ToolForce.Step = 1
        Me.ProgressBar_ToolForce.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        Me.ProgressBar_ToolForce.TabIndex = 19
        Me.ProgressBar_ToolForce.Value = 20
        '
        'Label_ToolDefl
        '
        Me.Label_ToolDefl.AutoSize = True
        Me.Label_ToolDefl.Location = New System.Drawing.Point(23, 229)
        Me.Label_ToolDefl.Name = "Label_ToolDefl"
        Me.Label_ToolDefl.Size = New System.Drawing.Size(82, 13)
        Me.Label_ToolDefl.TabIndex = 22
        Me.Label_ToolDefl.Text = "Tool Deflection:"
        '
        'ProgressBar_ToolDefl
        '
        Me.ProgressBar_ToolDefl.Location = New System.Drawing.Point(26, 245)
        Me.ProgressBar_ToolDefl.Maximum = 150
        Me.ProgressBar_ToolDefl.Name = "ProgressBar_ToolDefl"
        Me.ProgressBar_ToolDefl.Size = New System.Drawing.Size(190, 11)
        Me.ProgressBar_ToolDefl.Step = 1
        Me.ProgressBar_ToolDefl.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        Me.ProgressBar_ToolDefl.TabIndex = 21
        Me.ProgressBar_ToolDefl.Value = 20
        '
        'UC_Outputs
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Controls.Add(Me.Label_ToolDefl)
        Me.Controls.Add(Me.ProgressBar_ToolDefl)
        Me.Controls.Add(Me.Label_ToolForce)
        Me.Controls.Add(Me.ProgressBar_ToolForce)
        Me.Controls.Add(Me.Label_Torque)
        Me.Controls.Add(Me.ProgressBar_Torque)
        Me.Controls.Add(Me.Label_Power)
        Me.Controls.Add(Me.ProgressBar_Power)
        Me.Controls.Add(Me.Label_Life)
        Me.Controls.Add(Me.ProgressBar_Life)
        Me.Controls.Add(Me.Label_MRR)
        Me.Controls.Add(Me.ProgressBar_MRR)
        Me.Controls.Add(Me.Panel_header)
        Me.Name = "UC_Outputs"
        Me.Size = New System.Drawing.Size(247, 286)
        Me.Panel_header.ResumeLayout(False)
        Me.Panel_header.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel_header As System.Windows.Forms.Panel
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents ProgressBar_MRR As System.Windows.Forms.ProgressBar
    Friend WithEvents Label_MRR As System.Windows.Forms.Label
    Friend WithEvents Label_Life As System.Windows.Forms.Label
    Friend WithEvents ProgressBar_Life As System.Windows.Forms.ProgressBar
    Friend WithEvents Label_Power As System.Windows.Forms.Label
    Friend WithEvents ProgressBar_Power As System.Windows.Forms.ProgressBar
    Friend WithEvents Label_Torque As System.Windows.Forms.Label
    Friend WithEvents ProgressBar_Torque As System.Windows.Forms.ProgressBar
    Friend WithEvents Label_ToolForce As System.Windows.Forms.Label
    Friend WithEvents ProgressBar_ToolForce As System.Windows.Forms.ProgressBar
    Friend WithEvents Label_ToolDefl As System.Windows.Forms.Label
    Friend WithEvents ProgressBar_ToolDefl As System.Windows.Forms.ProgressBar

End Class
