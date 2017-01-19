Namespace Linq

    Public Module SequenceExecutionExtensions

        <Extension>
        Public Function OnFirst(Of T)(target As IEnumerable(Of T), action As Action(Of T)) As ISequenceExecution(Of T)
            Dim exec As New SequenceExecution(Of T)(target.GetEnumerator)
            Return exec.AndNext(action)
        End Function
    End Module

    Friend Class SequenceExecution(Of T)
        Implements ISequenceExecution(Of T)

        Protected _enumerator As IEnumerator(Of T)

        Friend Sub New(enumerator As IEnumerator(Of T))
            _enumerator = enumerator
        End Sub

        Public Function AndNext(action As Action(Of T)) As ISequenceExecution(Of T) Implements ISequenceExecution(Of T).AndNext
            If _enumerator.MoveNext() Then
                action(_enumerator.Current)
                Return Me
            Else
                Return Nothing
            End If
        End Function

        Public Function GetEnumerator() As IEnumerator(Of T) Implements ISequenceExecution(Of T).GetEnumerator
            Return _enumerator
        End Function
    End Class
End Namespace
