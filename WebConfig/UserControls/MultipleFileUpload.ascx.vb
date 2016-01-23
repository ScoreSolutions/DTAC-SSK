Imports System.ComponentModel
Imports System.Text
Imports System.Data
Imports System.Configuration
Imports System.Collections
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls

''' <summary>
''' This control is to upload multiple files.
''' </summary>
Partial Public Class MultipleFileUpload
    Inherits System.Web.UI.UserControl
    'This is Click event defenition for MultipleFileUpload control.
    Public Event Click As MultipleFileUploadClick

    ''' <summary>
    ''' The no of visible rows to display.
    ''' </summary>
    Private _Rows As Integer = 6
    Public Property Rows() As Integer
        Get
            Return _Rows
        End Get
        Set(ByVal value As Integer)
            _Rows = If(value < 6, 6, value)
        End Set
    End Property

    ''' <summary>
    ''' The no of maximukm files to upload.
    ''' </summary>
    Private _UpperLimit As Integer = 0
    Public Property UpperLimit() As Integer
        Get
            Return _UpperLimit
        End Get
        Set(ByVal value As Integer)
            _UpperLimit = value
        End Set
    End Property

    ''' <summary>
    ''' Methos for page load event.
    ''' </summary>
    ''' <param name="sender">Reference of the object that raises this event.</param>
    ''' <param name="e">Contains information regarding page load click event data.</param>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
        lblCaption.Text = If(_UpperLimit = 0, "Maximum Files: No Limit", String.Format("Maximum Files: {0}", _UpperLimit))
        pnlListBox.Attributes("style") = "overflow:auto;"
        pnlListBox.Height = Unit.Pixel(20 * _Rows - 1)
        Page.ClientScript.RegisterStartupScript(GetType(Page), "MyScript", GetJavaScript())
    End Sub

    ''' <summary>
    ''' Methods for btnUpload Click event. 
    ''' </summary>
    ''' <param name="sender">Reference of the object that raises this event.</param>
    ''' <param name="e">Contains information regarding button click event data.</param>
    Protected Sub btnUpload_Click(ByVal sender As Object, ByVal e As EventArgs)
        ' Fire the event.
        RaiseEvent Click(Me, New FileCollectionEventArgs(Me.Request))
    End Sub

    ''' <summary>
    ''' This method is used to generate javascript code for MultipleFileUpload control that execute at client side.
    ''' </summary>
    ''' <returns>Javascript as a string object.</returns>
    Private Function GetJavaScript() As String
        Dim JavaScript As New StringBuilder()

        JavaScript.Append("<script type='text/javascript'>")
        JavaScript.Append("var Id = 0;" & vbLf)
        JavaScript.AppendFormat("var MAX = {0};" & vbLf, _UpperLimit)
        JavaScript.AppendFormat("var DivFiles = document.getElementById('{0}');" & vbLf, pnlFiles.ClientID)
        JavaScript.AppendFormat("var DivListBox = document.getElementById('{0}');" & vbLf, pnlListBox.ClientID)
        JavaScript.AppendFormat("var BtnAdd = document.getElementById('{0}');" & vbLf, btnAdd.ClientID)
        JavaScript.Append("function Add()")
        JavaScript.Append("{" & vbLf)
        JavaScript.Append("var IpFile = GetTopFile();" & vbLf)
        JavaScript.Append("if(IpFile == null || IpFile.value == null || IpFile.value.length == 0)" & vbLf)
        JavaScript.Append("{" & vbLf)
        JavaScript.Append("alert('Please select a file to add.');" & vbLf)
        JavaScript.Append("return;" & vbLf)
        JavaScript.Append("}" & vbLf)
        JavaScript.Append("var NewIpFile = CreateFile();" & vbLf)
        JavaScript.Append("DivFiles.insertBefore(NewIpFile,IpFile);" & vbLf)
        JavaScript.Append("if(MAX != 0 && GetTotalFiles() - 1 == MAX)" & vbLf)
        JavaScript.Append("{" & vbLf)
        JavaScript.Append("NewIpFile.disabled = true;" & vbLf)
        JavaScript.Append("BtnAdd.disabled = true;" & vbLf)
        JavaScript.Append("}" & vbLf)
        JavaScript.Append("IpFile.style.display = 'none';" & vbLf)
        JavaScript.Append("DivListBox.appendChild(CreateItem(IpFile));" & vbLf)
        JavaScript.Append("}" & vbLf)
        JavaScript.Append("function CreateFile()")
        JavaScript.Append("{" & vbLf)
        JavaScript.Append("var IpFile = document.createElement('input');" & vbLf)
        JavaScript.Append("IpFile.id = IpFile.name = 'IpFile_' + Id++;" & vbLf)
        JavaScript.Append("IpFile.type = 'file';" & vbLf)
        JavaScript.Append("return IpFile;" & vbLf)
        JavaScript.Append("}" & vbLf)
        JavaScript.Append("function CreateItem(IpFile)" & vbLf)
        JavaScript.Append("{" & vbLf)
        JavaScript.Append("var Item = document.createElement('div');" & vbLf)
        JavaScript.Append("Item.style.backgroundColor = '#ffffff';" & vbLf)
        JavaScript.Append("Item.style.fontWeight = 'normal';" & vbLf)
        JavaScript.Append("Item.style.textAlign = 'left';" & vbLf)
        JavaScript.Append("Item.style.verticalAlign = 'middle'; " & vbLf)
        JavaScript.Append("Item.style.cursor = 'default';" & vbLf)
        JavaScript.Append("Item.style.height = 20 + 'px';" & vbLf)
        JavaScript.Append("var Splits = IpFile.value.split('\\');" & vbLf)
        JavaScript.Append("Item.innerHTML = Splits[Splits.length - 1] + '&nbsp;';" & vbLf)
        JavaScript.Append("Item.value = IpFile.id;" & vbLf)
        JavaScript.Append("Item.title = IpFile.value;" & vbLf)
        JavaScript.Append("var A = document.createElement('a');" & vbLf)
        JavaScript.Append("A.innerHTML = 'Delete';" & vbLf)
        JavaScript.Append("A.id = 'A_' + Id++;" & vbLf)
        JavaScript.Append("A.href = '#';" & vbLf)
        JavaScript.Append("A.style.color = 'blue';" & vbLf)
        JavaScript.Append("A.onclick = function()" & vbLf)
        JavaScript.Append("{" & vbLf)
        JavaScript.Append("DivFiles.removeChild(document.getElementById(this.parentNode.value));" & vbLf)
        JavaScript.Append("DivListBox.removeChild(this.parentNode);" & vbLf)
        JavaScript.Append("if(MAX != 0 && GetTotalFiles() - 1 < MAX)" & vbLf)
        JavaScript.Append("{" & vbLf)
        JavaScript.Append("GetTopFile().disabled = false;" & vbLf)
        JavaScript.Append("BtnAdd.disabled = false;" & vbLf)
        JavaScript.Append("}" & vbLf)
        JavaScript.Append("}" & vbLf)
        JavaScript.Append("Item.appendChild(A);" & vbLf)
        JavaScript.Append("Item.onmouseover = function()" & vbLf)
        JavaScript.Append("{" & vbLf)
        JavaScript.Append("Item.bgColor = Item.style.backgroundColor;" & vbLf)
        JavaScript.Append("Item.fColor = Item.style.color;" & vbLf)
        JavaScript.Append("Item.style.backgroundColor = '#C6790B';" & vbLf)
        JavaScript.Append("Item.style.color = '#ffffff';" & vbLf)
        JavaScript.Append("Item.style.fontWeight = 'bold';" & vbLf)
        JavaScript.Append("}" & vbLf)
        JavaScript.Append("Item.onmouseout = function()" & vbLf)
        JavaScript.Append("{" & vbLf)
        JavaScript.Append("Item.style.backgroundColor = Item.bgColor;" & vbLf)
        JavaScript.Append("Item.style.color = Item.fColor;" & vbLf)
        JavaScript.Append("Item.style.fontWeight = 'normal';" & vbLf)
        JavaScript.Append("}" & vbLf)
        JavaScript.Append("return Item;" & vbLf)
        JavaScript.Append("}" & vbLf)
        JavaScript.Append("function Clear()" & vbLf)
        JavaScript.Append("{" & vbLf)
        JavaScript.Append("DivListBox.innerHTML = '';" & vbLf)
        JavaScript.Append("DivFiles.innerHTML = '';" & vbLf)
        JavaScript.Append("DivFiles.appendChild(CreateFile());" & vbLf)
        JavaScript.Append("BtnAdd.disabled = false;" & vbLf)
        JavaScript.Append("}" & vbLf)
        JavaScript.Append("function GetTopFile()" & vbLf)
        JavaScript.Append("{" & vbLf)
        JavaScript.Append("var Inputs = DivFiles.getElementsByTagName('input');" & vbLf)
        JavaScript.Append("var IpFile = null;" & vbLf)
        JavaScript.Append("for(var n = 0; n < Inputs.length && Inputs[n].type == 'file'; ++n)" & vbLf)
        JavaScript.Append("{" & vbLf)
        JavaScript.Append("IpFile = Inputs[n];" & vbLf)
        JavaScript.Append("break;" & vbLf)
        JavaScript.Append("}" & vbLf)
        JavaScript.Append("return IpFile;" & vbLf)
        JavaScript.Append("}" & vbLf)
        JavaScript.Append("function GetTotalFiles()" & vbLf)
        JavaScript.Append("{" & vbLf)
        JavaScript.Append("var Inputs = DivFiles.getElementsByTagName('input');" & vbLf)
        JavaScript.Append("var Counter = 0;" & vbLf)
        JavaScript.Append("for(var n = 0; n < Inputs.length && Inputs[n].type == 'file'; ++n)" & vbLf)
        JavaScript.Append("Counter++;" & vbLf)
        JavaScript.Append("return Counter;" & vbLf)
        JavaScript.Append("}" & vbLf)
        JavaScript.Append("function GetTotalItems()" & vbLf)
        JavaScript.Append("{" & vbLf)
        JavaScript.Append("var Items = DivListBox.getElementsByTagName('div');" & vbLf)
        JavaScript.Append("return Items.length;" & vbLf)
        JavaScript.Append("}" & vbLf)
        JavaScript.Append("function DisableTop()" & vbLf)
        JavaScript.Append("{" & vbLf)
        JavaScript.Append("if(GetTotalItems() == 0)" & vbLf)
        JavaScript.Append("{" & vbLf)
        JavaScript.Append("alert('Please browse at least one file to upload.');" & vbLf)
        JavaScript.Append("return false;" & vbLf)
        JavaScript.Append("}" & vbLf)
        JavaScript.Append("GetTopFile().disabled = true;" & vbLf)
        JavaScript.Append("return true;" & vbLf)
        JavaScript.Append("}" & vbLf)
        JavaScript.Append("</script>")

        Return JavaScript.ToString()
    End Function
End Class

''' <summary>
''' EventArgs class that has some readonly properties regarding posted files corresponding to MultipleFileUpload control. 
''' </summary>
Public Class FileCollectionEventArgs
    Inherits EventArgs
    Private _HttpRequest As HttpRequest

    Public ReadOnly Property PostedFiles() As HttpFileCollection
        Get
            Return _HttpRequest.Files
        End Get
    End Property

    Public ReadOnly Property Count() As Integer
        Get
            Return _HttpRequest.Files.Count
        End Get
    End Property

    Public ReadOnly Property HasFiles() As Boolean
        Get
            Return If(_HttpRequest.Files.Count > 0, True, False)
        End Get
    End Property

    Public ReadOnly Property TotalSize() As Double
        Get
            Dim Size As Double = 0.0
            For n As Integer = 0 To _HttpRequest.Files.Count - 1
                If _HttpRequest.Files(n).ContentLength < 0 Then
                    Continue For
                Else
                    Size += _HttpRequest.Files(n).ContentLength
                End If
            Next

            Return Math.Round(Size / 1024.0, 2)
        End Get
    End Property

    Public Sub New(ByVal oHttpRequest As HttpRequest)
        _HttpRequest = oHttpRequest
    End Sub
End Class

'Delegate that represents the Click event signature for MultipleFileUpload control.
Public Delegate Sub MultipleFileUploadClick(ByVal sender As Object, ByVal e As FileCollectionEventArgs)