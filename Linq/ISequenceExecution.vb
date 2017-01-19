Namespace Linq
    ''' <summary>
    ''' 按次序对每一个元素执行不同的动作。用拓展方法 <see cref="OnFirst(Of T)(IEnumerable(Of T), Action(Of T))"/> 创建这个接口对应的实例。
    ''' </summary>
    ''' <typeparam name="T">元素的类型</typeparam>
    Public Interface ISequenceExecution(Of T)
        ''' <summary>
        ''' 在下一个元素上执行下一个动作。如果没有下一个元素，则返回空。
        ''' </summary>
        ''' <param name="action">要执行的动作</param>
        ''' <returns>执行状态</returns>
        Function AndNext(action As Action(Of T)) As ISequenceExecution(Of T)
        ''' <summary>
        ''' 获取表示执行状态的迭代器
        ''' </summary>
        ''' <returns>表示执行状态的迭代器</returns>
        Function GetEnumerator() As IEnumerator(Of T)
    End Interface
End Namespace