Imports Windows.UI

Module ScreenCapture
    <Extension>
    Async Function PickPremultipledColor(Source As FrameworkElement, X As Integer, Y As Integer) As Task(Of Color)
        Dim rt As New RenderTargetBitmap()
        Await rt.RenderAsync(Source)
        Dim buf = Await rt.GetPixelsAsync
        X *= rt.PixelWidth / Source.ActualWidth
        Y *= rt.PixelHeight / Source.ActualHeight
        Dim startIndex = (X + Y * rt.PixelWidth) * 4
        Return New Color With {.B = buf.GetByte(startIndex),
                               .G = buf.GetByte(startIndex + 1),
                               .R = buf.GetByte(startIndex + 2),
                               .A = buf.GetByte(startIndex + 3)}
    End Function
End Module