Option Strict On
Imports System.Collections.Generic

Public Module CalloutMapper
    Public Function Parse(productName As String) As CalloutModel
        Dim res As CalloutResult = DaytonCallout.Parse(productName)
        Dim model As New CalloutModel()

        model.ProductName = res.Raw
        model.IsValid = res.IsValid
        model.Errors.AddRange(res.Errors)
        model.Warnings.AddRange(res.Warnings)

        model.TypeCode = res.TypeCode
        model.ProductLine = res.ProductLine
        model.Kind = res.Kind
        model.Shape = res.Shape
        model.Specialty = res.Specialty
        model.Modifier = res.Modifier
        model.IsMetric = res.IsMetric
        model.Category = res.Category.ToString()

        model.DCode = res.DCode
        model.LengthCode = res.LengthCode
        model.Steel = res.Steel
        model.SteelIsDefault = res.SteelIsDefault

        model.D = res.D
        model.L = res.L
        model.L1 = res.L1
        model.L1FromCode = res.L1FromCode
        model.B = res.B
        model.Shank = res.Shank
        model.P = res.P
        model.W = res.W
        model.R = res.R
        For Each kv As KeyValuePair(Of String, Double) In res.PointDims
            If kv.Key <> "P" AndAlso kv.Key <> "W" AndAlso kv.Key <> "R" Then model.OtherDims(kv.Key) = kv.Value
        Next

        model.LandCode = res.LandCode
        model.LandLength = res.LandLength
        model.HeadThickness = res.HeadThickness
        model.HeadDiameter = res.HeadDiameter

        For Each a As CalloutAlteration In res.Alterations
            model.AlterationCodes.Add(a.Code)
            model.Alterations.Add(a.ToString())
        Next
        Return model
    End Function
End Module
