Imports System.Windows.Forms
Imports KioskPayment.Engine

Public Class frmMDIParent

    Private Sub frmMDIParent_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        
        Me.BackgroundImage = _frmMDIParent

        Dim frm As New frmChangeLanguage
        'frm.MdiParent = Me
        frm.ShowDialog()
    End Sub
End Class
