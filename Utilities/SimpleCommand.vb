Public Class SimpleCommand
    Implements ICommand

    Sub New(exec As Action)
        Me.Exec = exec
    End Sub

    Public ReadOnly Property Exec As Action

    Public Event CanExecuteChanged As EventHandler Implements ICommand.CanExecuteChanged

    Public Sub ExecuteAsync(parameter As Object) Implements ICommand.Execute
        Exec.Invoke
    End Sub

    Public Function CanExecute(parameter As Object) As Boolean Implements ICommand.CanExecute
        Return True
    End Function
End Class
