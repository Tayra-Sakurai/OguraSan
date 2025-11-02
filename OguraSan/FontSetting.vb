Public Class FontSetting
    ''' <summary>
    ''' The target element.
    ''' Must be a TextBlock.
    ''' </summary>
    Private TargetElm As TextBlock

    ''' <summary>
    ''' Font family variable.
    ''' </summary>
    Private _Family As FontFamily
    Private _Size As Double
    Private _Stretch As FontStretch

    ''' <summary>
    ''' Gets or sets the font family applied to the selected element.
    ''' </summary>
    ''' <returns>
    ''' (FontFamily)
    ''' </returns>
    Public Property Family As FontFamily
        Get
            Return _Family
        End Get
        Set(value As FontFamily)
            _Family = value
            TargetElm.FontFamily = value
        End Set
    End Property

    Public Property FamilyName As String
        Get
            Return _Family.Source
        End Get
        Set(value As String)
            _Family = New FontFamily(value)
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the font size.
    ''' </summary>
    ''' <returns>
    ''' (Double)
    ''' The font size.
    ''' </returns>
    Public Property Size As Double
        Get
            Return _Size
        End Get
        Set(value As Double)
            _Size = value
            TargetElm.FontSize = value
        End Set
    End Property

    ''' <summary>
    ''' Sets or gets the font stretch.
    ''' </summary>
    ''' <returns>
    ''' (FontStretch)
    ''' The instance
    ''' </returns>
    Public Property Stretch As FontStretch
        Get
            Return _Stretch
        End Get
        Set(value As FontStretch)
            _Stretch = value
            TargetElm.FontStretch = value
        End Set
    End Property

    ''' <summary>
    ''' Initializes the instance
    ''' </summary>
    ''' <param name="target">
    ''' (target)
    ''' The textblock to be edited.
    ''' </param>
    Public Sub New(target As TextBlock)
        TargetElm = target
        _Family = target.FontFamily
        _Size = target.FontSize
        _Stretch = target.FontStretch
    End Sub
End Class
