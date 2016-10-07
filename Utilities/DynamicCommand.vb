Public Class DynamicCommand
    Implements ICommand
    Sub New(exec As Action)
        Me.exec = Sub(o) exec()
    End Sub
    Sub New(exec As Action(Of Object))
        Me.exec = exec
    End Sub
    Sub New(exec As Action(Of Object), canExec As Func(Of Object, Boolean))
        MyClass.New(exec)
        Me.canExec = canExec
    End Sub

    Dim canExec As Func(Of Object, Boolean)
    Dim exec As Action(Of Object)

    Public Event CanExecuteChanged As EventHandler Implements ICommand.CanExecuteChanged

    Public Sub Execute(parameter As Object) Implements ICommand.Execute
        exec?.Invoke(parameter)
    End Sub

    Public Function CanExecute(parameter As Object) As Boolean Implements ICommand.CanExecute
        Return If(canExec?.Invoke(parameter), True)
    End Function
End Class
