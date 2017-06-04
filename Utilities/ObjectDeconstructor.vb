Imports System.Reflection

Public Class ObjectDeconstructor
    ''' <summary>
    ''' 从对象获取属性定义。
    ''' </summary>
    ''' <param name="obj"></param>
    ''' <returns></returns>
    Public Shared Function GetPropertyDefinitions(obj As Object) As IEnumerable(Of PropertyDefinition)
        Return From prop In obj.GetType.GetRuntimeProperties
               Let getMethod = prop.GetMethod
               Where getMethod IsNot Nothing
               Where getMethod.IsPublic AndAlso Not getMethod.IsStatic
               Select New PropertyDefinition(obj, prop)
    End Function
End Class
