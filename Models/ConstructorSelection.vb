Imports System.Reflection

Public Class ConstructorSelection
    Implements INotifyPropertyChanged

    Dim _SelectedConstructor As ConstructorInfo
    ''' <summary>
    ''' 当前选择的构造函数
    ''' </summary>
    Public Property SelectedConstructor As ConstructorInfo
        Get
            Return _SelectedConstructor
        End Get
        Set(value As ConstructorInfo)
            _SelectedConstructor = value
            Dim params = value.GetParameters
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(SelectedConstructor)))
            _ParameterInfoes = Aggregate p In params Select New Pair(Of ParameterInfo, String)(p, String.Empty) Into ToArray
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(ParameterInfoes)))
        End Set
    End Property
    ''' <summary>
    ''' 每个参数的详细信息
    ''' </summary>
    Public ReadOnly Property ParameterInfoes As Pair(Of ParameterInfo, String)()

    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
End Class
