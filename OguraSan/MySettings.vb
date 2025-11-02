Imports System.Configuration

Friend NotInheritable Class MySettings
    Inherits ApplicationSettingsBase

    <ApplicationScopedSetting()>
    Public ReadOnly Property OguraConnectionString As String
        Get
            Return CType(Me("OguraConnectionString"), String)
        End Get
    End Property

    <UserScopedSetting()>
    Public Property ProgressDataCurrent As String
        Get
            Dim JsonVal As String = CType(Me("ProgressDataCurrent"), String)
            Return JsonVal
        End Get
        Set(value As String)
            Me("ProgressDataCurrent") = value
        End Set
    End Property

    <UserScopedSetting(), DefaultSettingValue("False")>
    Public Property DataSaved As Boolean
        Get
            Return CType(Me("DataSaved"), Boolean)
        End Get
        Set(value As Boolean)
            Me("DataSaved") = value
        End Set
    End Property

    <UserScopedSetting(), DefaultSettingValue("0")>
    Public Property LastRecord As Long
        Get
            Return CType(Me("LastRecord"), Long)
        End Get
        Set(value As Long)
            Me("LastRecord") = value
        End Set
    End Property

    <UserScopedSetting(), DefaultSettingValue("0")>
    Public Property MaxRecord As Long
        Get
            Return CType(Me("MaxRecord"), Long)
        End Get
        Set(value As Long)
            Me("MaxRecord") = value
        End Set
    End Property

    <UserScopedSetting(), DefaultSettingValue("False")>
    Public Property Kimariji As Boolean
        Get
            Return CBool(Me("Kimariji"))
        End Get
        Set(value As Boolean)
            Me("Kimariji") = value
        End Set
    End Property
End Class
