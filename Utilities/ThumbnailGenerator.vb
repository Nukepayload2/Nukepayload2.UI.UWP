Imports Windows.Graphics.Imaging
Imports Windows.Storage

Public Class ThumbnailGenerator
    Public Property ThumbnailSize As Integer
    ''' <summary>
    ''' 用于产生 300x300 的缩略图
    ''' </summary>
    Sub New()
        MyClass.New(300)
    End Sub
    Sub New(thumbnailSize As Integer)
        Me.ThumbnailSize = thumbnailSize
    End Sub
    Public Async Function GenerateThumbnailAsync(imageFile As StorageFile) As Task(Of WriteableBitmap)
        Using strm = Await imageFile.OpenReadAsync
            Dim decoder = Await BitmapDecoder.CreateAsync(strm)
            Dim pxSize = New BitmapSize() With {.Width = decoder.PixelWidth, .Height = decoder.PixelHeight}
            Dim scale = Math.Max(pxSize.Width / ThumbnailSize, pxSize.Height / ThumbnailSize)
            If scale > 1 Then
                pxSize.Height /= scale
                pxSize.Width /= scale
            End If
            Dim transform As New BitmapTransform With {
                .ScaledWidth = pxSize.Width,
                .ScaledHeight = pxSize.Height
            }
            Dim bm = Await decoder.GetSoftwareBitmapAsync(BitmapPixelFormat.Bgra8, BitmapAlphaMode.Premultiplied,
                                                          transform, ExifOrientationMode.RespectExifOrientation,
                                                          ColorManagementMode.DoNotColorManage)
            Dim thumbnail As New WriteableBitmap(pxSize.Width, pxSize.Height)
            bm.CopyToBuffer(thumbnail.PixelBuffer)
            Return thumbnail
        End Using
    End Function
End Class
