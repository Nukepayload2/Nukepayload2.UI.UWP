Imports System.Reflection
Imports Windows.UI.Text

Public Class FontDefaults

    Public Shared ReadOnly Property FontWeights As Dictionary(Of String, FontWeight)
    Public Shared ReadOnly Property FontWeightsInv As Dictionary(Of FontWeight, String)
    Shared Sub New()
        Dim fwData = (From fw In GetType(FontWeights).GetRuntimeProperties
                      Where fw.GetMethod.IsStatic
                      Select New NamedProperty(Of FontWeight)(fw.Name, DirectCast(fw.GetValue(Nothing), FontWeight)))
        FontWeights = fwData.ToDictionary(Function(v) v.Name, Function(v) v.Data)
        FontWeightsInv = fwData.ToDictionary(Function(v) v.Data, Function(v) v.Name)
    End Sub
End Class