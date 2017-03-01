Imports System.Reflection

Public Class ConstructorSelection
    Implements INotifyPropertyChanged

    Dim _ParameterValues As Object()
    ''' <summary>
    ''' 用于构造对象的参数
    ''' </summary>
    Public Property ParameterValues As Object()
        Get
            Return _ParameterValues
        End Get
        Set(value As Object())
            _ParameterValues = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(ParameterValues)))
        End Set
    End Property

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
            ReDim ParameterValues(params.Length - 1)
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(SelectedConstructor)))
            _ParameterInfoes = params
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(ParameterInfoes)))
        End Set
    End Property

    Public ReadOnly Property ParameterInfoes As ParameterInfo()

    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
End Class
