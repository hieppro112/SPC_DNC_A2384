Option Strict On
Imports System.Collections.Generic

' Dữ liệu phân tích từ 1 product name Dayton. Chiều dài đơn vị mm; Nothing = callout không có giá trị đó.
Public Class CalloutModel
    Public ProductName As String = ""
    Public IsValid As Boolean
    Public Errors As New List(Of String)
    Public Warnings As New List(Of String)

    Public TypeCode As String = ""
    Public ProductLine As String = ""
    Public Kind As String = ""
    Public Shape As String = ""
    Public Specialty As String = ""
    Public Modifier As String = ""
    Public IsMetric As Boolean
    Public Category As String = ""

    Public DCode As String = ""
    Public LengthCode As String = ""
    Public Steel As String = ""
    Public SteelIsDefault As Boolean

    Public D As Double?
    Public L As Double?
    Public L1 As Double?
    Public L1FromCode As Double?
    Public B As Double?
    Public Shank As Double?
    Public P As Double?
    Public W As Double?
    Public R As Double?
    Public OtherDims As New Dictionary(Of String, Double)

    Public LandCode As String = ""
    Public LandLength As Double?
    Public HeadThickness As Double?
    Public HeadDiameter As Double?

    Public AlterationCodes As New List(Of String)
    Public Alterations As New List(Of String)
End Class
