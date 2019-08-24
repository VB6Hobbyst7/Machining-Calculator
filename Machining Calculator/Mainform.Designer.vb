<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Mainform
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.UC_Outputs1 = New Machining_Calculator.UC_Outputs()
        Me.UC_Cut_Setup1 = New Machining_Calculator.UC_Cut_Setup()
        Me.UC_SpeedsFeeds1 = New Machining_Calculator.UC_SpeedsFeeds()
        Me.UC_Tool_Setup1 = New Machining_Calculator.UC_Tool_Setup()
        Me.UC_Machine_Matl_Setup1 = New Machining_Calculator.UC_Machine_Matl_Setup()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(871, 377)
        Me.TabControl1.TabIndex = 0
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.Button2)
        Me.TabPage1.Controls.Add(Me.Button1)
        Me.TabPage1.Controls.Add(Me.UC_Outputs1)
        Me.TabPage1.Controls.Add(Me.UC_Cut_Setup1)
        Me.TabPage1.Controls.Add(Me.UC_SpeedsFeeds1)
        Me.TabPage1.Controls.Add(Me.UC_Tool_Setup1)
        Me.TabPage1.Controls.Add(Me.UC_Machine_Matl_Setup1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(863, 351)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "TabPage1"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(223, 263)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 5
        Me.Button1.Text = "Button1"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'UC_Outputs1
        '
        Me.UC_Outputs1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.UC_Outputs1.Location = New System.Drawing.Point(575, 0)
        Me.UC_Outputs1.Name = "UC_Outputs1"
        Me.UC_Outputs1.Size = New System.Drawing.Size(247, 286)
        Me.UC_Outputs1.TabIndex = 4
        '
        'UC_Cut_Setup1
        '
        Me.UC_Cut_Setup1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.UC_Cut_Setup1.Location = New System.Drawing.Point(0, 113)
        Me.UC_Cut_Setup1.Name = "UC_Cut_Setup1"
        Me.UC_Cut_Setup1.Size = New System.Drawing.Size(267, 85)
        Me.UC_Cut_Setup1.TabIndex = 3
        '
        'UC_SpeedsFeeds1
        '
        Me.UC_SpeedsFeeds1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.UC_SpeedsFeeds1.Location = New System.Drawing.Point(273, 113)
        Me.UC_SpeedsFeeds1.Name = "UC_SpeedsFeeds1"
        Me.UC_SpeedsFeeds1.Size = New System.Drawing.Size(302, 85)
        Me.UC_SpeedsFeeds1.TabIndex = 2
        '
        'UC_Tool_Setup1
        '
        Me.UC_Tool_Setup1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.UC_Tool_Setup1.Location = New System.Drawing.Point(0, 74)
        Me.UC_Tool_Setup1.Name = "UC_Tool_Setup1"
        Me.UC_Tool_Setup1.Size = New System.Drawing.Size(575, 39)
        Me.UC_Tool_Setup1.TabIndex = 1
        '
        'UC_Machine_Matl_Setup1
        '
        Me.UC_Machine_Matl_Setup1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.UC_Machine_Matl_Setup1.Location = New System.Drawing.Point(0, 0)
        Me.UC_Machine_Matl_Setup1.Name = "UC_Machine_Matl_Setup1"
        Me.UC_Machine_Matl_Setup1.Size = New System.Drawing.Size(575, 74)
        Me.UC_Machine_Matl_Setup1.TabIndex = 0
        '
        'TabPage2
        '
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(863, 351)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "TabPage2"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(317, 263)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(75, 23)
        Me.Button2.TabIndex = 6
        Me.Button2.Text = "Button2"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Mainform
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(871, 377)
        Me.Controls.Add(Me.TabControl1)
        Me.Name = "Mainform"
        Me.Text = "Form1"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents UC_Machine_Matl_Setup1 As Machining_Calculator.UC_Machine_Matl_Setup
    Friend WithEvents UC_Cut_Setup1 As Machining_Calculator.UC_Cut_Setup
    Friend WithEvents UC_SpeedsFeeds1 As Machining_Calculator.UC_SpeedsFeeds
    Friend WithEvents UC_Tool_Setup1 As Machining_Calculator.UC_Tool_Setup
    Friend WithEvents UC_Outputs1 As Machining_Calculator.UC_Outputs
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button

End Class
