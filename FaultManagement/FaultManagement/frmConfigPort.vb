Public Class frmConfigPort

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        If txtHostIP.Text.Trim = "" Then
            MessageBox.Show("กรุณาระบุ Host IP")
            Exit Sub
        End If
        If txtHostName.Text.Trim = "" Then
            MessageBox.Show("กรุณาระบุ Host Name")
            Exit Sub
        End If
        If txtPortNumber.Text.Trim = "" Then
            MessageBox.Show("กรุณาระบุ Port Number")
            Exit Sub
        End If

        Try
            If chkSun.Checked = False And chkMon.Checked = False And chkTue.Checked = False And chkWed.Checked = False And chkThu.Checked = False And chkFri.Checked = False And chkSat.Checked = False Then
                MessageBox.Show("กรุณาระบุ Alarm Date")
                Exit Sub
            End If

            Dim TimeFrom As String = ""
            Dim TimeTo As String = ""
            If chkAllDayEvent.Checked = False Then
                TimeFrom = dpAlarmTimeFrom.Value.ToString("HH:mm")
                TimeTo = dpAlarmTimeTo.Value.ToString("HH:mm")

                If TimeFrom.Trim = "" Then
                    MessageBox.Show("กรุณาระบุ Alarm Time From")
                    Exit Sub
                End If

                If TimeTo.Trim = "" Then
                    MessageBox.Show("กรุณาระบุ Alarm Time To")
                    Exit Sub
                End If

                If TimeFrom > TimeTo Then
                    MessageBox.Show("Alarm Time From จะต้องมีค่าน้อยกว่า Alarm Time To")
                    Exit Sub
                End If
            End If

            Dim ini As New FaultManagement.Org.Mentalis.Files.IniReader(Application.StartupPath & "\Config.ini")
            ini.Section = "Setting"
            Dim ws As New FaultManagementService.FaultManagementService
            ws.Timeout = 20000
            ws.Url = ini.ReadString("DCWebserviceURL")
            If ws.AddConfigPort(txtHostName.Text.Trim, txtHostIP.Text.Trim, txtPortNumber.Text.Trim, GetCheckBox(chkSun), GetCheckBox(chkMon), GetCheckBox(chkTue), GetCheckBox(chkWed), GetCheckBox(chkThu), GetCheckBox(chkFri), GetCheckBox(chkSat), GetCheckBox(chkAllDayEvent), TimeFrom, TimeTo) = True Then
                MessageBox.Show("บันทึกข้อมูลเรียบร้อย")
                SetPortList(ws, ini.ReadString("IsShopInstall"))
            Else
                MessageBox.Show("บันทึกข้อมูลผิดพลาด !")
            End If
            ws = Nothing
            ini = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Function GetCheckBox(ByVal chk As CheckBox) As String
        Dim ret As String = ""
        If chk.Checked = True Then
            ret = "Y"
        Else
            ret = "N"
        End If
        Return ret
    End Function

    Private Sub SetPortList(ByVal ws As FaultManagementService.FaultManagementService, ByVal IsShopInstall As String)
        Dim dt As New DataTable
        If IsShopInstall = "Y" Then
            dt = ws.GetConfigPortList(" and HostName='" & Environment.MachineName & "'")
        Else
            dt = ws.GetConfigPortList("")
        End If

        If dt.Rows.Count > 0 Then
            DGVPortList.AutoGenerateColumns = False
            DGVPortList.DataSource = dt
        Else
            DGVPortList.AutoGenerateColumns = False
            DGVPortList.DataSource = Nothing
        End If
    End Sub

    Private Sub frmConfigPort_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Dim IsShopInstall As String = "Y"
        Try
            Dim ini As New FaultManagement.Org.Mentalis.Files.IniReader(Application.StartupPath & "\Config.ini")
            ini.Section = "Setting"
            Dim ws As New FaultManagementService.FaultManagementService
            ws.Timeout = 20000
            ws.Url = ini.ReadString("DCWebserviceURL")
            IsShopInstall = ini.ReadString("IsShopInstall")

            SetPortList(ws, IsShopInstall)
            ws = Nothing
            ini = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        If IsShopInstall = "Y" Then
            txtHostName.Text = Environment.MachineName
            txtHostIP.Text = Engine.Common.FunctionEng.GetIPAddress

            txtHostName.Enabled = False
            txtHostIP.Enabled = False
        End If

        dpAlarmTimeFrom.Format = DateTimePickerFormat.Custom
        dpAlarmTimeFrom.CustomFormat = "HH:mm"
    End Sub

    Private Sub DGVPortList_CellContentClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGVPortList.CellContentClick
        If e.RowIndex < 0 Then
            Exit Sub
        End If
        Dim grv As DataGridViewRow = DGVPortList.Rows(e.RowIndex)
        If grv.Cells(e.ColumnIndex).Value = "Delete" Then
            Dim Yes As Integer = MessageBox.Show("ยืนยันการลบข้อมูล", "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)
            If Yes = 1 Then
                Try
                    Dim ini As New FaultManagement.Org.Mentalis.Files.IniReader(Application.StartupPath & "\Config.ini")
                    ini.Section = "Setting"
                    Dim ws As New FaultManagementService.FaultManagementService
                    ws.Timeout = 20000
                    ws.Url = ini.ReadString("DCWebserviceURL")

                    If ws.DeleteConfigPortList(grv.Cells("id").Value) = True Then
                        SetPortList(ws, ini.ReadString("IsShopInstall"))
                    End If
                    ws = Nothing
                    ini = Nothing
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                End Try
            End If
        End If
    End Sub

    Private Sub chkAllDayEvent_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAllDayEvent.CheckedChanged
        If chkAllDayEvent.Checked = True Then
            dpAlarmTimeFrom.Enabled = False
            dpAlarmTimeTo.Enabled = False
        Else
            dpAlarmTimeFrom.Enabled = True
            dpAlarmTimeTo.Enabled = True
        End If
    End Sub
End Class