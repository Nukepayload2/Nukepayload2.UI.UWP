Imports Microsoft.VisualStudio.TestPlatform.UnitTestFramework
Imports Nukepayload2.UI.UWP

<TestClass>
Public Class TestTypeValidations

    <TestMethod>
    Public Sub TestIsConvertableInt32()
        Dim number As Integer = 1234
        Assert.IsTrue(TypeValidations.IsConvertable(number.ToString, number.GetType))
    End Sub
    <TestMethod>
    Public Sub TestIsConvertableString()
        Dim str = "呵呵哒"
        Assert.IsTrue(TypeValidations.IsConvertable(str.ToString, str.GetType))
    End Sub
    <TestMethod>
    Public Sub TestIsConvertableDate()
        Dim date1 = #2000-01-01#
        Assert.IsTrue(TypeValidations.IsConvertable(date1.ToString, date1.GetType))
    End Sub
    <TestMethod>
    Public Sub TestIsConvertableTime()
        Dim time1 = TimeSpan.FromDays(2)
        Assert.IsTrue(TypeValidations.IsConvertable(time1.ToString, time1.GetType))
    End Sub
    <TestMethod>
    Public Sub TestIsConvertableNullableTime()
        Dim time2 As New TimeSpan?(TimeSpan.FromDays(3))
        Assert.IsTrue(TypeValidations.IsConvertable(time2.ToString, GetType(TimeSpan?)))
    End Sub
End Class
