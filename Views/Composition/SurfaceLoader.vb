'*********************************************************
'
' Copyright (c) Microsoft. All rights reserved.
' This code is licensed under the MIT License (MIT).
' THE SOFTWARE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, 
' INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
' FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. 
' IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, 
' DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, 
' TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH 
' THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
'
'*********************************************************

Imports Microsoft.Graphics.Canvas
Imports Microsoft.Graphics.Canvas.Text
Imports Microsoft.Graphics.Canvas.UI.Composition
Imports Windows.Graphics.DirectX
Imports Windows.UI
Imports Windows.UI.Composition

Public Delegate Function LoadTimeEffectHandler(bitmap As CanvasBitmap, device As CompositionGraphicsDevice, sizeTarget As Size) As CompositionDrawingSurface

Public Class SurfaceLoader
    Private Shared _intialized As Boolean
    Private Shared _compositor As Compositor
    Private Shared _canvasDevice As CanvasDevice
    Private Shared _compositionDevice As CompositionGraphicsDevice

    Public Shared Sub Initialize(compositor As Compositor)
        Debug.Assert(Not _intialized OrElse compositor Is _compositor)

        If Not _intialized Then
            _compositor = compositor
            _canvasDevice = New CanvasDevice()
            _compositionDevice = CanvasComposition.CreateCompositionGraphicsDevice(_compositor, _canvasDevice)

            _intialized = True
        End If
    End Sub

    Public Shared Sub Uninitialize()
        _compositor = Nothing

        If _compositionDevice IsNot Nothing Then
            _compositionDevice.Dispose()
            _compositionDevice = Nothing
        End If

        If _canvasDevice IsNot Nothing Then
            _canvasDevice.Dispose()
            _canvasDevice = Nothing
        End If

        _intialized = False
    End Sub

    Public Shared ReadOnly Property IsInitialized() As Boolean
        Get
            Return _intialized
        End Get
    End Property

    Public Shared Async Function LoadFromUri(uri As Uri) As Task(Of CompositionDrawingSurface)
        Return Await LoadFromUri(uri, Size.Empty)
    End Function

    Public Shared Async Function LoadFromUri(uri As Uri, sizeTarget As Size) As Task(Of CompositionDrawingSurface)
        Debug.Assert(_intialized)

        Dim bitmap As CanvasBitmap = Await CanvasBitmap.LoadAsync(_canvasDevice, uri)
        Dim sizeSource As Size = bitmap.Size

        If sizeTarget.IsEmpty Then
            sizeTarget = sizeSource
        End If

        Dim surface As CompositionDrawingSurface = _compositionDevice.CreateDrawingSurface(sizeTarget, DirectXPixelFormat.B8G8R8A8UIntNormalized, DirectXAlphaMode.Premultiplied)
        Using ds = CanvasComposition.CreateDrawingSession(surface)
            ds.Clear(Color.FromArgb(0, 0, 0, 0))
            ds.DrawImage(bitmap, New Rect(0, 0, sizeTarget.Width, sizeTarget.Height), New Rect(0, 0, sizeSource.Width, sizeSource.Height))
        End Using

        Return surface
    End Function

    Public Shared Function LoadText(text As String, sizeTarget As Size, textFormat As CanvasTextFormat, textColor As Color, bgColor As Color) As CompositionDrawingSurface
        Debug.Assert(_intialized)

        Dim surface As CompositionDrawingSurface = _compositionDevice.CreateDrawingSurface(sizeTarget, DirectXPixelFormat.B8G8R8A8UIntNormalized, DirectXAlphaMode.Premultiplied)
        Using ds = CanvasComposition.CreateDrawingSession(surface)
            ds.Clear(bgColor)
            ds.DrawText(text, New Rect(0, 0, sizeTarget.Width, sizeTarget.Height), textColor, textFormat)
        End Using

        Return surface
    End Function

    Public Shared Async Function LoadFromUri(uri As Uri, sizeTarget As Size, loadEffectHandler As LoadTimeEffectHandler) As Task(Of CompositionDrawingSurface)
        Debug.Assert(_intialized)
        If loadEffectHandler IsNot Nothing Then
            Dim bitmap As CanvasBitmap = Await CanvasBitmap.LoadAsync(_canvasDevice, uri)
            Return loadEffectHandler(bitmap, _compositionDevice, sizeTarget)
        Else
            Return Await LoadFromUri(uri, sizeTarget)
        End If
    End Function
End Class
