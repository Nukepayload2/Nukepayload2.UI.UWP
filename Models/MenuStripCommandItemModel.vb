Public Class MenuStripCommandItemModel
    Public Sub New(icon As Uri, text As String, onClick As ICommand)
        Me.Icon = icon
        Me.Text = text
        Me.OnClick = onClick
    End Sub

    Public Property Icon As Uri
    Public Property Text As String
    Public Property OnClick As ICommand
End Class