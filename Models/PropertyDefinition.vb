Imports System.ComponentModel.DataAnnotations
Imports System.Reflection

''' <summary>
''' 表示属性的定义
''' </summary>
Public Class PropertyDefinition
    ''' <summary>
    ''' 获取这个属性的原名
    ''' </summary>
    Public ReadOnly Property Name As String
        Get
            Return PropertyInfo.Name
        End Get
    End Property
    ''' <summary>
    ''' 获取或设置属性值
    ''' </summary>
    Public Property Value As Object
        Get
            Return PropertyInfo.GetValue(Owner)
        End Get
        Set(value As Object)
            PropertyInfo.SetValue(Owner, value)
        End Set
    End Property
    ''' <summary>
    ''' 这个属性是不是只读的？
    ''' </summary>
    Public ReadOnly Property IsReadOnly As Boolean
        Get
            Return PropertyInfo.CanRead AndAlso Not PropertyInfo.CanWrite
        End Get
    End Property

    ''' <summary>
    ''' 属性使用 <see cref="DisplayAttribute.Description"/> 标记的描述。
    ''' </summary>
    Public Property Description As String
    ''' <summary>
    ''' 属性使用 <see cref="DisplayAttribute.GroupName"/> 标记的组名。使用 SemanticZoom 模式显示组名时将使用这个属性。
    ''' </summary>
    Public Property GroupName As String
    ''' <summary>
    ''' 属性使用 <see cref="DisplayAttribute.Name"/> 标记的别名。可以在显示时将属性翻译成其它语言。
    ''' </summary>
    Public Property [Alias] As String
    ''' <summary>
    ''' 拥有这个属性的对象
    ''' </summary>
    Public Property Owner As Object
    ''' <summary>
    ''' 属性的反射信息
    ''' </summary>
    Public Property PropertyInfo As PropertyInfo
End Class