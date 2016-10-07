#Const FEATURE_RESIZE = False

Public MustInherit Class DragableContentDialog
    Inherits ContentDialog
    Dim ncHandler As New NonClientAeraHandler(Me)

    Public Sub DragMove()
        ncHandler.DragMove()
    End Sub

End Class