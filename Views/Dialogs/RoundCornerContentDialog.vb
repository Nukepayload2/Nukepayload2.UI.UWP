' The Templated Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=23423
#Const FEATURE_RESIZE = False
Public Class RoundCornerContentDialog
    Inherits DragableContentDialog

    Public Sub New()
        Me.DefaultStyleKey = GetType(RoundCornerContentDialog)
    End Sub

End Class