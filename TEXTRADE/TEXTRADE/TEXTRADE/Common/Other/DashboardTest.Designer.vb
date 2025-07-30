<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class DashboardTEst
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.DashboardDesigner1 = New DevExpress.DashboardWin.DashboardDesigner()
        Me.DashboardViewer1 = New DevExpress.DashboardWin.DashboardViewer(Me.components)
        CType(Me.DashboardDesigner1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DashboardViewer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DashboardDesigner1
        '
        Me.DashboardDesigner1.AsyncMode = True
        Me.DashboardDesigner1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DashboardDesigner1.Location = New System.Drawing.Point(0, 0)
        Me.DashboardDesigner1.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.DashboardDesigner1.Name = "DashboardDesigner1"
        Me.DashboardDesigner1.Size = New System.Drawing.Size(1229, 664)
        Me.DashboardDesigner1.TabIndex = 0
        '
        'DashboardViewer1
        '
        Me.DashboardViewer1.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(210, Byte), Integer), CType(CType(210, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.DashboardViewer1.Appearance.Options.UseBackColor = True
        Me.DashboardViewer1.AsyncMode = True
        Me.DashboardViewer1.Location = New System.Drawing.Point(468, 0)
        Me.DashboardViewer1.Name = "DashboardViewer1"
        Me.DashboardViewer1.Size = New System.Drawing.Size(761, 664)
        Me.DashboardViewer1.TabIndex = 1
        '
        'DashboardTEst
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1229, 664)
        Me.Controls.Add(Me.DashboardViewer1)
        Me.Controls.Add(Me.DashboardDesigner1)
        Me.Name = "DashboardTEst"
        Me.Text = "Dashboard"
        CType(Me.DashboardDesigner1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DashboardViewer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents DashboardDesigner1 As DevExpress.DashboardWin.DashboardDesigner
    Friend WithEvents DashboardViewer1 As DevExpress.DashboardWin.DashboardViewer
End Class
