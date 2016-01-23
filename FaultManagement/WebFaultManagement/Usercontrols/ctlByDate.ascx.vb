
Partial Class FormControls_ctlByDate
    Inherits System.Web.UI.UserControl

    Public ReadOnly Property DateFrom() As Date
        Get
            Return txtDateFrom.DateValue
        End Get
    End Property
    Public ReadOnly Property DateTo() As Date
        Get
            Return txtDateTo.DateValue
        End Get
    End Property
End Class
