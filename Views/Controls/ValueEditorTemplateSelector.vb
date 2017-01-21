Imports System.Numerics
Imports System.Reflection
Imports Windows.UI

Public Class ValueEditorTemplateSelector
    Inherits DataTemplateSelector

    Shared ResourceDic As New ResourceDictionary With {
        .Source = New Uri("ms-appx:///Nukepayload2.UI.UWP/Themes/DynamicDataTemplates.xaml")
    }

    Shared TypeTable As New Dictionary(Of Type, DataTemplate) From {
        {GetType(Long), ResourceDic!Int64EditorDataTemplate},
        {GetType(ULong), ResourceDic!UInt64EditorDataTemplate},
        {GetType(Integer), ResourceDic!Int32EditorDataTemplate},
        {GetType(UInteger), ResourceDic!UInt32EditorDataTemplate},
        {GetType(Short), ResourceDic!Int16EditorDataTemplate},
        {GetType(UShort), ResourceDic!UInt16EditorDataTemplate},
        {GetType(SByte), ResourceDic!SByteEditorDataTemplate},
        {GetType(Byte), ResourceDic!ByteEditorDataTemplate},
        {GetType(Single), ResourceDic!SingleEditorDataTemplate}，
        {GetType(Double), ResourceDic!DoubleEditorDataTemplate}，
        {GetType(Decimal), ResourceDic!DecimalEditorDataTemplate},
        {GetType(TimeSpan), ResourceDic!TimeSpanEditorDataTemplate},
        {GetType(Date), ResourceDic!DateTimeEditorDataTemplate},
        {GetType(DateTimeOffset), ResourceDic!DateTimeOffsetEditorDataTemplate},
        {GetType(String), ResourceDic!StringEditorDataTemplate},
        {GetType(Char), ResourceDic!CharEditorDataTemplate},
        {GetType(Vector2), ResourceDic!Vector2EditorDataTemplate},
        {GetType(Vector3), ResourceDic!Vector3EditorDataTemplate},
        {GetType(Vector4), ResourceDic!Vector4EditorDataTemplate},
        {GetType(Color), ResourceDic!ColorEditorDataTemplate},
        {GetType(Matrix3x2), ResourceDic!Matrix3x2EditorDataTemplate},
        {GetType(Matrix4x4), ResourceDic!Matrix4x4EditorDataTemplate},
        {GetType(Boolean), ResourceDic!BooleanEditorDataTemplate},
        {GetType(Boolean?), ResourceDic!BooleanEditorDataTemplate}
    }

    ''' <summary>
    ''' 将指定类型适合的数据模板添加注册
    ''' </summary>
    ''' <param name="key">类型</param>
    ''' <param name="value">编辑指定类型的对象的模板</param>
    Public Shared Sub RegisterDataTemplate(key As Type, value As DataTemplate)
        If TypeTable.ContainsKey(key) Then
            Return
        End If
        TypeTable.Add(key, value)
    End Sub

    ''' <summary>
    ''' 指定的类型是否已经有数据模板
    ''' </summary>
    ''' <param name="key">要检索的类型</param>
    ''' <returns>已经有数据模板 ?</returns>
    Public Shared Function IsDataTemplateDefined(key As Type) As Boolean
        Return TypeTable.ContainsKey(key)
    End Function

    ''' <summary>
    ''' 将指定类型适合的数据模板添加注册，并替换已经存在的模板设定。
    ''' </summary>
    ''' <param name="key">类型</param>
    ''' <param name="value">编辑指定类型的对象的模板</param>
    Public Shared Sub RegisterReplaceDataTemplate(key As Type, value As DataTemplate)
        If TypeTable.ContainsKey(key) Then
            TypeTable.Remove(key)
        End If
        TypeTable.Add(key, value)
    End Sub

    ''' <summary>
    ''' 尝试获取指定类型的数据模板。找不到则返回空。
    ''' </summary>
    ''' <param name="itemType">要检索的类型</param>
    ''' <returns>指定类型的数据模板。找不到则返回空。</returns>
    Public Function TryGetDefinedTemplate(itemType As Type) As DataTemplate
        Dim typeInf = itemType.GetTypeInfo
        If TypeTable.ContainsKey(itemType) Then
            Return TypeTable(itemType)
        ElseIf GetType(IEnumerable(Of)).GetTypeInfo.IsAssignableFrom(typeInf) Then
            Return ResourceDic!PropertySetListDataTemplate
        ElseIf Nullable.GetUnderlyingType(itemType) IsNot Nothing Then
            Return ResourceDic!NullableValueEditorDataTemplate
        End If
        Return Nothing
    End Function

    Protected Overrides Function SelectTemplateCore(item As Object) As DataTemplate
        Return If(TryGetDefinedTemplate(item.GetType), MyBase.SelectTemplateCore(item))
    End Function
End Class
