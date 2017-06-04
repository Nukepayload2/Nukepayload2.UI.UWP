Imports System.Runtime.InteropServices
Imports Windows.System.Profile

Public Class Features
    Public Shared ReadOnly Property IsHostAcrylicAvailable As Boolean
        Get
            Return WindowsVersion.build >= 15063
        End Get
    End Property

    Public Shared ReadOnly Property IsInAppAcrylicAvailable As Boolean
        Get
            Return WindowsVersion.build >= 14393
        End Get
    End Property

    Public Shared ReadOnly Property IsXamlLightAvailable As Boolean
        Get
            Return WindowsVersion.build >= 15063
        End Get
    End Property

    Public Shared ReadOnly Property IsRevealBrushAvailable As Boolean
        Get
            Return WindowsVersion.build >= 16193
        End Get
    End Property

    Public Shared ReadOnly Property IsConnectedAnimationAvailable As Boolean
        Get
            Return WindowsVersion.build >= 10586
        End Get
    End Property

    Friend Shared ReadOnly Property WindowsVersion As DeviceFamilyVersion =
        CULng(AnalyticsInfo.VersionInfo.DeviceFamilyVersion).AsDeviceFamilyVersion

End Class

Structure DeviceFamilyVersion
    Dim revision, build, minor, major As UShort
End Structure

Module DeviceFamilyVersionAndULongMarshal
    <StructLayout(LayoutKind.Explicit)>
    Private Structure DeviceFamilyVersionULongUnion
        <FieldOffset(0)>
        Dim DeviceFamilyVersionValue As DeviceFamilyVersion
        <FieldOffset(0)>
        Dim ULongValue As ULong
    End Structure
    ''' <summary>
    ''' 将 DeviceFamilyVersion 转换成 ULong
    ''' </summary>
    ''' <param name="DeviceFamilyVersionValue">DeviceFamilyVersion</param>
    ''' <returns>ULong</returns>
    <Extension>
    Public Function AsULong(DeviceFamilyVersionValue As DeviceFamilyVersion) As ULong
        Return (New DeviceFamilyVersionULongUnion With {.DeviceFamilyVersionValue = DeviceFamilyVersionValue}).ULongValue
    End Function
    ''' <summary>
    ''' 将 ULong 转换成 DeviceFamilyVersion
    ''' </summary>
    ''' <param name="ULongValue">ULong</param>
    ''' <returns>DeviceFamilyVersion</returns>
    <Extension>
    Public Function AsDeviceFamilyVersion(ULongValue As ULong) As DeviceFamilyVersion
        Return (New DeviceFamilyVersionULongUnion With {.ULongValue = ULongValue}).DeviceFamilyVersionValue
    End Function

End Module