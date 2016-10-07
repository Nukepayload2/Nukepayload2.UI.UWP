' “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍
Imports Nukepayload2.UI.UWP
Imports Windows.UI

''' <summary>
''' 颜色拾取器。捎带笔粗细修改功能。
''' </summary>
Public NotInheritable Class ColorPickerDialog
    Public ReadOnly Property StrokeThickness As Double
        Get
            Dim width = 1
            Integer.TryParse(txtWid?.Text, width)
            Return width
        End Get
    End Property
    Public ReadOnly Property PickedColor As Color
    Dim Canceled As Boolean
    Public Overloads Async Function ShowAsync() As Task(Of Color?)
        Await MyBase.ShowAsync()
        Return If(Canceled, New Color?, New Color?(PickedColor))
    End Function

    Private Sub RectDrag_PointerPressed(sender As Object, e As PointerRoutedEventArgs) Handles RectDrag.PointerPressed
        DragMove()
    End Sub
    Sub New()

        ' 此调用是设计器所必需的。
        InitializeComponent()

        ' 在 InitializeComponent() 调用之后添加任何初始化。

    End Sub
    Sub New(defaultColor As Color)
        MyClass.New
        txtTrans.Text = defaultColor.A
        txtRed.Text = defaultColor.R
        txtGreen.Text = defaultColor.G
        txtBlue.Text = defaultColor.B
        SetCurrentColor()
    End Sub
    Private Function SetCurrentColor() As Boolean
        Dim col As New Color
        Try
            col.A = txtTrans.Text
            col.R = txtRed.Text
            col.G = txtGreen.Text
            col.B = txtBlue.Text
        Catch ex As InvalidCastException
            Return False
        End Try
        _PickedColor = col
        Dim attrib = InkPrev.InkPresenter.CopyDefaultDrawingAttributes
        attrib.Color = PickedColor
        InkPrev.InkPresenter.UpdateDefaultDrawingAttributes(attrib)
        Return True
    End Function

    Private Sub btnClose_Click(sender As Object, e As RoutedEventArgs) Handles btnClose.Click
        If SetCurrentColor() Then Hide()
    End Sub

    Private Sub ColorPicker_PickedColorChanged(sender As Object, e As RoutedEventArgs) Handles ColorPicker.PickedColorChanged
        Dim col = ColorPicker.PickedColor
        txtTrans.Text = col.A
        txtRed.Text = col.R
        txtGreen.Text = col.G
        txtBlue.Text = col.B
        RecPrev.InputColor = col
        RecPrev.Refresh()
        SetCurrentColor()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As RoutedEventArgs)
        Canceled = True
        Hide()
    End Sub

    Private Sub InkPrev_Loaded(sender As Object, e As RoutedEventArgs) Handles InkPrev.Loaded
        InkPrev.InkPresenter.InputDeviceTypes = 7
    End Sub

    Private Sub txtWid_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txtWid.TextChanged
        Dim attrib = InkPrev.InkPresenter.CopyDefaultDrawingAttributes
        attrib.Size = New Size(StrokeThickness, StrokeThickness)
        InkPrev.InkPresenter.UpdateDefaultDrawingAttributes(attrib)
    End Sub

    Private Sub RecPrev_PickedColorChanged(sender As Object, e As RoutedEventArgs) Handles RecPrev.PickedColorChanged
        Dim col = RecPrev.PickedColor
        txtTrans.Text = col.A
        txtRed.Text = col.R
        txtGreen.Text = col.G
        txtBlue.Text = col.B
        SetCurrentColor()
    End Sub

    Private Sub TblClear_Tapped(sender As Object, e As TappedRoutedEventArgs)
        InkPrev.InkPresenter.StrokeContainer.Clear()
    End Sub
End Class