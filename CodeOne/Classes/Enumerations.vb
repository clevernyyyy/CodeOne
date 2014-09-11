<Serializable()>
Public Class Enumerations
    Enum enmType
        _String = 1
        _Decimal = 2
        _Integer = 3
        _Double = 4
        _Boolean = 5
        _DateTime = 6
    End Enum
    Enum enmAuthorization
        'this is the "User Level" Enumration that is set on login for each user as the "UserPackage.UserInformation.cAuthorization"
        Administrator = 1
        InHouseUser = 2
        GeneralAgent = 3
        Producer = 4
        Insured = 5
    End Enum
    Enum enmTimeZone
        Central = 6
    End Enum
    Public Enum enmServer
        Develop = 1
        Test = 2
        Live = 3
    End Enum
End Class
