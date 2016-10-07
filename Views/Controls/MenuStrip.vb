Imports System.Runtime.InteropServices.WindowsRuntime

''' <summary>
''' 内容只有分组的按钮的菜单。
''' </summary>
Public NotInheritable Class MenuStrip
    Inherits Pivot

    Public Sub New()
        Me.DefaultStyleKey = GetType(MenuStrip)
    End Sub

End Class