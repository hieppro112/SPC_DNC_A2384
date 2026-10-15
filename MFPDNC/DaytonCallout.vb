Option Strict On
Option Explicit On

Imports System
Imports System.Collections.Generic
Imports System.Globalization
Imports System.Text
Imports System.Text.RegularExpressions

' Parser callout Dayton Progress theo tài liệu "1-7 Product Nomenclature (Rev 09012019)".
' Mọi chiều dài trả về đơn vị mm (hàng inch được đổi 1" = 25.4 mm).

Public Enum CalloutCategory
    Unknown
    Punch
    DieButton
    Guide
    Retainer
End Enum

Public Class CalloutAlteration
    Public Property Code As String = ""
    Public Property Value As Nullable(Of Double)
    Public Property RawValue As String = ""
    Public Property Params As New List(Of String)
    Public Property Description As String = ""

    Public Overrides Function ToString() As String
        Dim s As String = Code & RawValue
        If Params.Count > 0 Then s &= " " & String.Join(" ", Params.ToArray())
        If Description <> "" Then s &= " : " & Description
        Return s
    End Function
End Class

Public Class CalloutResult
    Public Property Raw As String = ""
    Public Property TypeCode As String = ""
    Public Property ProductLine As String = ""
    Public Property Kind As String = ""
    Public Property Shape As String = ""
    Public Property Specialty As String = ""
    Public Property Modifier As String = ""
    Public Property IsMetric As Boolean
    Public Property Category As CalloutCategory
    Public Property DCode As String = ""
    Public Property LengthCode As String = ""
    Public Property Steel As String = ""
    Public Property SteelIsDefault As Boolean

    Public Property D As Nullable(Of Double)
    Public Property L As Nullable(Of Double)
    Public Property L1 As Nullable(Of Double)
    Public Property L1FromCode As Nullable(Of Double)
    Public Property B As Nullable(Of Double)
    Public Property Shank As Nullable(Of Double)
    Public Property LandCode As String = ""
    Public Property LandLength As Nullable(Of Double)
    Public Property HeadThickness As Nullable(Of Double)
    Public Property HeadDiameter As Nullable(Of Double)

    Public Property PointDims As New Dictionary(Of String, Double)
    Public Property Alterations As New List(Of CalloutAlteration)
    Public Property Errors As New List(Of String)
    Public Property Warnings As New List(Of String)

    Public ReadOnly Property IsValid As Boolean
        Get
            Return Errors.Count = 0
        End Get
    End Property

    Public ReadOnly Property P As Nullable(Of Double)
        Get
            Return GetDim("P")
        End Get
    End Property

    Public ReadOnly Property W As Nullable(Of Double)
        Get
            Return GetDim("W")
        End Get
    End Property

    Public ReadOnly Property R As Nullable(Of Double)
        Get
            Return GetDim("R")
        End Get
    End Property

    Public Function GetDim(name As String) As Nullable(Of Double)
        Dim v As Double
        If PointDims.TryGetValue(name, v) Then Return v
        Return Nothing
    End Function

    Public Function FindAlteration(code As String) As CalloutAlteration
        For Each a As CalloutAlteration In Alterations
            If a.Code = code Then Return a
        Next
        Return Nothing
    End Function

    Public Function Summary() As String
        Dim sb As New StringBuilder()
        sb.AppendLine("Callout : " & Raw)
        Dim typeDesc As String = TypeCode & " = " & ProductLine & " | " & Kind & " | " & Shape
        If Specialty <> "" Then typeDesc &= " | " & Specialty
        If Modifier <> "" Then typeDesc &= " | " & Modifier
        sb.AppendLine("Loại    : " & typeDesc)
        sb.AppendLine("Nhóm    : " & Category.ToString() & If(IsMetric, " (Metric)", " (Inch)"))
        sb.AppendLine("D       : " & Fmt(D) & "   (mã D " & DCode & ")")
        sb.AppendLine("Mã dài  : " & If(LengthCode = "", "-", LengthCode))
        sb.AppendLine("L       : " & Fmt(L))
        If Shank.HasValue Then sb.AppendLine("Shank   : " & Fmt(Shank))
        If L1.HasValue Then
            Dim note As String = ""
            If L1FromCode.HasValue AndAlso Math.Abs(L1FromCode.Value - L1.Value) > 0.0001 Then note = "   (mã gốc " & Fmt(L1FromCode) & ", đã đổi bằng XBR)"
            sb.AppendLine("L1 (SBR): " & Fmt(L1) & note)
        End If
        sb.AppendLine("B       : " & If(B.HasValue, Fmt(B), "- (không có trong mã, tra catalog)"))
        If LandCode <> "" Then sb.AppendLine("Land    : " & LandCode & " (tra catalog theo D)")
        If LandLength.HasValue Then sb.AppendLine("Land dài: " & Fmt(LandLength))
        For Each kv As KeyValuePair(Of String, Double) In PointDims
            sb.AppendLine(kv.Key.PadRight(8) & ": " & Fmt(kv.Value))
        Next
        If HeadThickness.HasValue Then sb.AppendLine("Dày đầu : " & Fmt(HeadThickness))
        If HeadDiameter.HasValue Then sb.AppendLine("ĐK đầu  : " & Fmt(HeadDiameter))
        sb.AppendLine("Thép    : " & Steel & If(SteelIsDefault, " (mặc định)", ""))
        For Each a As CalloutAlteration In Alterations
            sb.AppendLine("Gia công: " & a.ToString())
        Next
        For Each e As String In Errors
            sb.AppendLine("LỖI     : " & e)
        Next
        For Each wn As String In Warnings
            sb.AppendLine("Lưu ý   : " & wn)
        Next
        Return sb.ToString()
    End Function

    Private Function Fmt(v As Nullable(Of Double)) As String
        If Not v.HasValue Then Return "-"
        Dim mm As String = v.Value.ToString("0.####", CultureInfo.InvariantCulture) & " mm"
        If IsMetric Then Return mm
        Return (v.Value / 25.4).ToString("0.####", CultureInfo.InvariantCulture) & " in (" & v.Value.ToString("0.###", CultureInfo.InvariantCulture) & " mm)"
    End Function
End Class

Public NotInheritable Class DaytonCallout
    Private Sub New()
    End Sub

    Private Const MmPerInch As Double = 25.4
    Private Const Num As String = "(\d+(?:\.\d*)?|\.\d+)"

    Private Shared ReadOnly InchLines As New Dictionary(Of String, String) From {
        {"H", "Heavy Duty Ball Lock"}, {"K", "Kommercial"}, {"L", "Light Duty Ball Lock"},
        {"M", "Versatile Micro Guides"}, {"P", "Retainer Inch"}, {"R", "Rotary Broach"},
        {"T", "TuffPunch"}, {"V", "Versatile/VersaPlus"}, {"W", "WedgeLock"}, {"Y", "Core Pins"}}

    Private Shared ReadOnly MetricLines As New Dictionary(Of String, String) From {
        {"A", "Press Fit/TuffPunch"}, {"B", "Heavy Duty Ball Lock"}, {"C", "Light Duty Ball Lock"},
        {"D", "Versatile"}, {"N", "Retainer Metric"}, {"S", "Press Fit w/dowel"}, {"U", "Retainer Metric"}}

    Private Shared ReadOnly Kinds As New Dictionary(Of String, String) From {
        {"A", "Headless Split Die Button"}, {"B", "Head Split Die Button"}, {"C", "Clospace Punch/Core Pin"},
        {"D", "Headless Die Button"}, {"E", "Head Up Guide"}, {"F", "Head Down Guide"},
        {"G", "Headless Guide"}, {"H", "Head Type Die Button"}, {"J", "Jektole Punch"},
        {"K", "Regular Point Larger than Shank"}, {"L", "Spring Pilot/Extra Heavy Die Button"},
        {"M", "Misfeed Detector"}, {"N", "Headless Die Button w/Dowel"}, {"P", "Regular Punch/Pilot/Broach"},
        {"Q", "Quill Bushing"}, {"R", "Head Type Die Button/Retainer"}, {"U", "Regular Straight Punch"},
        {"V", "Pointed Wire Punch"}, {"W", "Straight Wire Punch"}, {"X", "Straight Clospace Punch"},
        {"Y", "Jektole Str Punch"}, {"Z", "Jektole Point Larger Than Shank"}}

    Private Shared ReadOnly Shapes As New Dictionary(Of String, String) From {
        {"A", "Positive Pickup Pilot/Change Retainer"}, {"B", "Straight Blank (No Point)"},
        {"C", "Classified Shape/Change Retainer"}, {"E", "Co'bore EDM Die Button Blank"},
        {"F", "RotaryBroach Double Hex"}, {"G", "Countersink"}, {"H", "Flatted Round"},
        {"I", "EZ Fit Retainer Insert"}, {"J", "'D' shape (C28)"}, {"K", "Radius Rectangle (C11)"},
        {"L", "Rectangle with .02 radius"}, {"N", "6-sided shape (C12)"}, {"O", "Oblong"},
        {"P", "Multi-Location/Multi-Position Retainer"}, {"Q", "Quill Guide Bushing"},
        {"R", "Rectangle"}, {"S", "RotaryBroach Square/Special"}, {"T", "Regular Pilot/True Position"},
        {"U", "Straight Hole EDM Die Button Blank"}, {"V", "Round with a flat (C33)"},
        {"W", "Form Punches/Dies"}, {"X", "Round"}, {"Y", "Rectangle with flat corners (C52)"},
        {"Z", "6 sided shape with radius on corners (C85)"}}

    Private Shared ReadOnly Specialties As New Dictionary(Of String, String) From {
        {"A", "Shape Positive Pickup Pilot"}, {"B", "Retainer w/ backing plate"},
        {"C", "Compact Pilot/Change Retainer"}, {"F", "TuffPunch"}, {"L", "PosiLoc Retainer"},
        {"P", "VersaPlus"}, {"R", "Reversible Change Retainer"},
        {"S", "Special/Retainer for Shape Punches"}, {"T", "Shape Regular Pilot"}}

    Private Shared ReadOnly Modifiers As New Dictionary(Of String, String) From {
        {"F", "TUFF Punch Shape Pilot"}, {"L", "PosiLoc Retainer w/ backing plate"}, {"P", "VersaPlus Shape Pilot"}}

    Private Shared ReadOnly InchDCodes As New Dictionary(Of Integer, Double) From {
        {12, 0.125}, {18, 0.1875}, {25, 0.25}, {31, 0.3125}, {37, 0.375}, {43, 0.4375},
        {50, 0.5}, {62, 0.625}, {75, 0.75}, {87, 0.875}, {100, 1.0}, {125, 1.25},
        {150, 1.5}, {175, 1.75}, {200, 2.0}, {225, 2.25}, {250, 2.5}, {275, 2.75}}

    Private Shared ReadOnly MetricDCodes As New List(Of Integer) From {
        5, 6, 8, 10, 13, 16, 20, 22, 25, 32, 38, 40, 45, 50, 56, 63, 71, 76, 85, 90, 100}

    Private Shared ReadOnly MetricL1Codes As New List(Of Integer) From {8, 10, 13, 15, 19, 21, 25, 27, 30, 32}

    '3 loai cua ky tu dung thu 2 trong chuoi
    Private Shared ReadOnly PunchKinds As String = "CJKPUVWXYZ"
    Private Shared ReadOnly DieButtonKinds As String = "ABDHNR"
    Private Shared ReadOnly GuideKinds As String = "EFGQ"
    'danh sách các loại thep slide 50
    Private Shared ReadOnly Steels As New List(Of String) From {"A2", "M2", "PS", "PS4"}

    ' Tên dài đặt trước để "RL" không bị đọc thành "R".
    Private Shared ReadOnly DimNames As String() = {"PP", "WW", "LA", "RL", "RS", "RP", "AP", "RF", "AN", "R1", "R2", "P", "W", "R", "A", "B", "S"}

    Private Shared ReadOnly CoatingsNeedM2 As New List(Of String) From {
        "CRN", "XAN", "XCD", "XCN", "XNA", "XNAD", "XNAP", "XNM", "XNP", "XNT"}

    Private Shared ReadOnly AltDescriptions As Dictionary(Of String, String) = BuildAltDescriptions()

    Private Shared Function BuildAltDescriptions() As Dictionary(Of String, String)
        Dim d As New Dictionary(Of String, String) From {
            {"XL", "Rút ngắn chiều dài tổng L"},
            {"XLL", "L chính xác (bằng hoặc ngắn hơn chuẩn)"},
            {"LL", "L chính xác (dạng viết tắt của XLL, không có trong tài liệu)"},
            {"XB", "Chiều dài mũi B khác chuẩn (<= 1.625"") / chiều dài land của cối"},
            {"XBR", "Đổi chiều dài đoạn thẳng trước bán kính L1 (SBR)"},
            {"XBB", "B/SBR dài hơn (1.626"" - 2.000"")"},
            {"X3B", "B/SBR dài hơn (2.001"" - 3.000"")"},
            {"XLB", "Rút ngắn L nhưng giữ nguyên B"},
            {"XBL", "Land của cối dài bằng L"},
            {"XT", "Đầu mỏng hơn"},
            {"TT", "Độ dày đầu chính xác"},
            {"XTT", "Độ dày đầu chính xác"},
            {"XH", "Giảm đường kính đầu"},
            {"XD", "Giảm đường kính thân"},
            {"XTK", "Độ dày đầu kiểu Kommercial cho chày Versatile"},
            {"X0", "Rãnh dowel 1/8"" hoặc 3.0 mm (F=.5D) tại 0/90/180/270°"},
            {"X1", "Rãnh dowel 1/8"" hoặc 3.0 mm (F=.5D) tại góc khác"},
            {"X4", "Rãnh dowel 1/8"" hoặc 3.0 mm tại 0/90/180/270°"},
            {"X7", "Rãnh dowel 1/8"" hoặc 3.0 mm tại góc khác"},
            {"X41", "Rãnh dowel 3/16"" hoặc 4.0 mm tại 0/90/180/270°"},
            {"X71", "Rãnh dowel 3/16"" hoặc 4.0 mm tại góc khác"},
            {"X43", "Rãnh dowel 1/4"" hoặc 6.0 mm tại 0/90/180/270°"},
            {"X73", "Rãnh dowel 1/4"" hoặc 6.0 mm tại góc khác"},
            {"X2", "Mặt phẳng khóa chuẩn tại 0/90/180/270°"},
            {"X3", "Hai mặt phẳng khóa chuẩn tại 0/90/180/270°"},
            {"X20", "Mặt phẳng khóa chuẩn (F khác X2) tại 0/90/180/270°"},
            {"X8", "Mặt phẳng khóa ở đầu land tại 0/90/180/270°"},
            {"X80", "Mặt phẳng khóa ở đầu land (F khác X8) tại 0/90/180/270°"},
            {"X81", "Mặt phẳng thân sâu .060""/1.5 mm dài .500""/13 mm tại 0/90/180/270°"},
            {"X82", "Mặt phẳng thân sâu .060""/1.5 mm dài .625""/16 mm tại 0/90/180/270°"},
            {"X83", "Mặt phẳng thân sâu .060""/1.5 mm dài .750""/20 mm tại 0/90/180/270°"},
            {"X84", "Mặt phẳng thân sâu .060""/1.5 mm hết chiều dài tại 0/90/180/270°"},
            {"X85", "Mặt phẳng thân sâu .093""/2.5 mm dài .500""/13 mm tại 0/90/180/270°"},
            {"X86", "Mặt phẳng thân sâu .093""/2.5 mm dài .625""/16 mm tại 0/90/180/270°"},
            {"X87", "Mặt phẳng thân sâu .093""/2.5 mm dài .750""/20 mm tại 0/90/180/270°"},
            {"X88", "Mặt phẳng thân sâu .093""/2.5 mm hết chiều dài tại 0/90/180/270°"},
            {"X89", "Mặt phẳng thân, chỉ định sâu (D) và dài (L) tại 0/90/180/270°"},
            {"X5", "Mặt phẳng khóa chuẩn tại góc khác"},
            {"X6", "Hai mặt phẳng khóa chuẩn tại góc khác"},
            {"X50", "Mặt phẳng khóa chuẩn (F khác X5) tại góc khác"},
            {"X9", "Mặt phẳng khóa ở đầu land tại góc khác"},
            {"X90", "Mặt phẳng khóa ở đầu land (F khác X9) tại góc khác"},
            {"XS19", "Góc cắt - mũi đinh (nail point)"},
            {"XS20", "Góc cắt - vát (chamfer)"},
            {"XS21", "Góc cắt - côn (conical)"},
            {"XS22", "Góc cắt - cắt kép (double shear)"},
            {"XS23", "Góc cắt - cắt đơn (single shear)"},
            {"XS24", "Góc cắt - cắt đơn có mặt phẳng ở đầu"},
            {"BS", "Ball seat (có vị trí hướng kính)"},
            {"SB", "Ball seat thứ hai"},
            {"XWS", "Whistle stop"},
            {"XK", "Chày Jektole không có lỗ bên"},
            {"XJ", "Kích thước Jektole không chuẩn"},
            {"XNL", "Không có lead"},
            {"XSC", "Rãnh giữ phôi (slug control)"},
            {"XAR", "Côn ngược ở mũi chày / lỗ thoát"},
            {"CRN", "Phủ Chromium Nitride"},
            {"XAN", "Phủ TiAlN (DayTAN)"},
            {"XCD", "Phủ DLC (Diamond-like Carbon)"},
            {"XCN", "Phủ TiCN"},
            {"XCR", "DayKool (xử lý nhiệt lạnh sâu)"},
            {"XN", "Xử lý Daytride"},
            {"XNA", "Balinit Alcrona Pro (AlCrN)"},
            {"XNAD", "Balinit Alcrona Pro Duplex"},
            {"XNAP", "Balinit Alcrona Pro Advanced"},
            {"XNM", "Xử lý MoST (cho M2/PS/PS4)"},
            {"XNP", "Nitride + TiCN"},
            {"XNT", "Phủ Daytin (TiN)"}}
        For i As Integer = 1 To 9
            d("X9" & i) = d("X8" & i).Replace("tại 0/90/180/270°", "tại góc khác")
        Next
        Return d
    End Function


    Public Shared Function Parse(text As String) As CalloutResult
        Dim r As New CalloutResult()
        r.Raw = If(text, "").Trim().Trim(""""c).Trim()
        Dim upper As String = r.Raw.ToUpperInvariant()
        Dim normalized As String = Regex.Replace(upper, "(\d),(\d)", "$1.$2")
        If normalized <> upper Then r.Warnings.Add("Dấu phẩy thập phân đã được đổi thành dấu chấm")
        Dim tokens As New List(Of String)(Regex.Split(normalized, "\s+"))
        tokens.RemoveAll(Function(t) t = "")
        'kiem tra chuoi va tach tach chuoi thanh cac element con 
        If tokens.Count = 0 Then
            r.Errors.Add("Chuỗi rỗng")
            Return r
        End If

        Dim first As Match = Regex.Match(tokens(0), "^([A-Z]{3,5})(\d{2,3})?$")
        If Not first.Success Then
            If tokens(0).Contains("-") Then
                r.Errors.Add("Không phải callout Dayton (có dấu '-'), có thể là mã MISUMI")
            Else
                r.Errors.Add("Nhóm chữ loại phải gồm 3-5 chữ cái: '" & tokens(0) & "'")
            End If
            Return r
        End If
        tokens.RemoveAt(0)
        If first.Groups(2).Success Then tokens.Insert(0, first.Groups(2).Value)

        If Not DecodeType(r, first.Groups(1).Value) Then Return r

        If tokens.Count = 0 OrElse Not Regex.IsMatch(tokens(0), "^\d{2,3}$") Then
            r.Errors.Add("Thiếu mã D (2-3 chữ số) sau nhóm chữ loại")
            Return r
        End If
        DecodeD(r, tokens(0))
        tokens.RemoveAt(0)

        If tokens.Count > 0 AndAlso Not Steels.Contains(tokens(0)) AndAlso
           Regex.IsMatch(tokens(0), "^(\d{2,6}|[SABCDE]\d{2,3})$") Then
            r.LengthCode = tokens(0)
            tokens.RemoveAt(0)
        End If

        ParseRest(r, tokens)
        If r.LengthCode <> "" Then
            DecodeLength(r)
        ElseIf r.Category = CalloutCategory.Punch OrElse r.Category = CalloutCategory.DieButton Then
            r.Warnings.Add("Không có mã chiều dài")
        End If
        ApplyAlterations(r)
        FinalChecks(r)
        Return r
    End Function

    Private Shared Function DecodeType(r As CalloutResult, letters As String) As Boolean
        r.TypeCode = letters
        Dim c1 As String = letters.Substring(0, 1)
        'so sanh phan loai hang metrix và hàng hệ inch
        If InchLines.ContainsKey(c1) Then
            r.IsMetric = False
            r.ProductLine = InchLines(c1) & " (Inch)"
        ElseIf MetricLines.ContainsKey(c1) Then
            r.IsMetric = True
            r.ProductLine = MetricLines(c1) & " (Metric)"
        Else
            r.Errors.Add("Chữ thứ 1 '" & c1 & "' không phải dòng catalog nào")
            Return False
        End If

        'so sanh va gan gia tri cho ky tu thu 2 va 3
        Dim c2 As String = letters.Substring(1, 1)
        Dim c3 As String = letters.Substring(2, 1)
        If Kinds.ContainsKey(c2) Then r.Kind = Kinds(c2) Else r.Errors.Add("Chữ thứ 2 '" & c2 & "' không phải loại sản phẩm nào")
        If Shapes.ContainsKey(c3) Then r.Shape = Shapes(c3) Else r.Errors.Add("Chữ thứ 3 '" & c3 & "' không phải hình dạng nào")
        'kiem tra con ky tu dac biet nao o sau nua khong
        If letters.Length >= 4 Then
            Dim c4 As String = letters.Substring(3, 1)
            If Specialties.ContainsKey(c4) Then r.Specialty = Specialties(c4) Else r.Errors.Add("Chữ thứ 4 '" & c4 & "' không phải sản phẩm đặc biệt nào")
        End If
        If letters.Length = 5 Then
            Dim c5 As String = letters.Substring(4, 1)
            If Modifiers.ContainsKey(c5) Then r.Modifier = Modifiers(c5) Else r.Errors.Add("Chữ thứ 5 '" & c5 & "' không phải bộ điều chỉnh nào")
        End If

        Dim c4Code As String = If(letters.Length >= 4, letters.Substring(3, 1), "")
        If "PNU".Contains(c1) OrElse (c2 = "R" AndAlso "BLRS".Contains(c4Code) AndAlso c4Code <> "") Then
            r.Category = CalloutCategory.Retainer
        ElseIf PunchKinds.Contains(c2) Then
            r.Category = CalloutCategory.Punch
        ElseIf DieButtonKinds.Contains(c2) Then
            r.Category = CalloutCategory.DieButton
        ElseIf GuideKinds.Contains(c2) Then
            r.Category = CalloutCategory.Guide
        Else
            r.Category = CalloutCategory.Unknown
        End If
        Return r.Errors.Count = 0
    End Function

    'tinh kich thuoc D
    Private Shared Sub DecodeD(r As CalloutResult, code As String)
        r.DCode = code
        Dim n As Integer = Integer.Parse(code, CultureInfo.InvariantCulture)
        If r.IsMetric Then
            r.D = n
            If Not MetricDCodes.Contains(n) Then r.Warnings.Add("D = " & n & " mm không có trong bảng mã D metric của tài liệu")
        ElseIf InchDCodes.ContainsKey(n) Then
            r.D = InchDCodes(n) * MmPerInch
        Else
            r.D = Nearest16(n / 100.0) * MmPerInch
            r.Warnings.Add("Mã D inch " & code & " không có trong bảng, đã làm tròn tới 1/16""")
        End If
    End Sub

    Private Shared Sub ParseRest(r As CalloutResult, tokens As List(Of String))
        Dim lastAlt As CalloutAlteration = Nothing
        Dim i As Integer = 0
        While i < tokens.Count
            Dim t As String = tokens(i)
            Dim m As Match

            If Steels.Contains(t) Then
                If r.Steel <> "" Then r.Warnings.Add("Có nhiều mã thép: " & r.Steel & ", " & t)
                r.Steel = t
                lastAlt = Nothing
                i += 1
                Continue While
            End If

            m = Regex.Match(t, "^ANG" & Num & "?$")
            If m.Success Then
                Dim ang As String = m.Groups(1).Value
                If ang = "" AndAlso i + 1 < tokens.Count AndAlso Regex.IsMatch(tokens(i + 1), "^" & Num & "$") Then
                    ang = tokens(i + 1)
                    i += 1
                End If
                If lastAlt IsNot Nothing Then
                    lastAlt.Params.Add("ANG " & ang)
                Else
                    r.Warnings.Add("'ANG " & ang & "' không đi sau mã khóa nào")
                End If
                i += 1
                Continue While
            End If

            If lastAlt IsNot Nothing AndAlso (lastAlt.Code = "X89" OrElse lastAlt.Code = "X99") Then
                m = Regex.Match(t, "^([DL]" & Num & "|FL)$")
                If m.Success Then
                    lastAlt.Params.Add(t)
                    i += 1
                    Continue While
                End If
            End If

            ' Dạng viết liền: X2-ANG0.000-F5.004
            m = Regex.Match(t, "^(XS?\d+)-(.+)$")
            If m.Success Then
                Dim joined As CalloutAlteration = TryParseAlteration(m.Groups(1).Value)
                joined.Params.AddRange(m.Groups(2).Value.Split("-"c))
                r.Alterations.Add(joined)
                lastAlt = joined
                i += 1
                Continue While
            End If

            Dim dimName As String = ""
            Dim dimValue As Double
            If TryParseDim(t, r.IsMetric, dimName, dimValue) Then
                If r.PointDims.ContainsKey(dimName) Then r.Warnings.Add("Kích thước " & dimName & " bị lặp")
                r.PointDims(dimName) = ToMm(r, dimValue)
                lastAlt = Nothing
                i += 1
                Continue While
            End If

            Dim alt As CalloutAlteration = TryParseAlteration(t)
            If alt IsNot Nothing Then
                r.Alterations.Add(alt)
                lastAlt = alt
                i += 1
                Continue While
            End If

            r.Warnings.Add("Không nhận diện được '" & t & "'")
            i += 1
        End While

        If r.Steel = "" Then
            r.Steel = "A2"
            r.SteelIsDefault = True
        End If
    End Sub

    Private Shared Function TryParseDim(t As String, isMetric As Boolean, ByRef name As String, ByRef value As Double) As Boolean
        For Each n As String In DimNames
            If Not t.StartsWith(n, StringComparison.Ordinal) Then Continue For
            Dim rest As String = t.Substring(n.Length)
            If Not Regex.IsMatch(rest, "^" & Num & "$") Then Continue For
            Dim v As Double = ParseNum(rest)
            ' "R1.000" là R = 1.000; chỉ coi là R1/R2 khi giá trị inch dạng ".xxx" khác 0.
            If (n = "R1" OrElse n = "R2") AndAlso (isMetric OrElse Not rest.StartsWith(".") OrElse v = 0) Then Continue For
            name = n
            value = v
            Return True
        Next
        Return False
    End Function

    Private Shared Function TryParseAlteration(t As String) As CalloutAlteration
        Dim m As Match = Regex.Match(t, "^(XS?\d+)$")
        Dim code As String
        Dim raw As String = ""
        If m.Success AndAlso Not t.StartsWith("X3B") Then
            code = m.Groups(1).Value
        Else
            m = Regex.Match(t, "^(X3B|TT|LL|BS|SB|CRN|X[A-Z]+)=?" & Num & "?$")
            If Not m.Success Then Return Nothing
            code = m.Groups(1).Value
            raw = m.Groups(2).Value
        End If
        Dim a As New CalloutAlteration()
        a.Code = code
        a.RawValue = raw
        If raw <> "" Then a.Value = ParseNum(raw)
        If AltDescriptions.ContainsKey(code) Then
            a.Description = AltDescriptions(code)
        Else
            a.Description = "(không có trong tài liệu)"
        End If
        Return a
    End Function

    Private Shared Sub DecodeLength(r As CalloutResult)
        Dim m As Match = Regex.Match(r.LengthCode, "^([A-Z]?)(\d+)$")
        Dim letter As String = m.Groups(1).Value
        Dim digits As String = m.Groups(2).Value
        Dim c1 As String = r.TypeCode.Substring(0, 1)
        Dim c4 As String = If(r.TypeCode.Length >= 4, r.TypeCode.Substring(3, 1), "")

        Select Case r.Category
            Case CalloutCategory.Punch
                If r.IsMetric Then
                    DecodeMetricPunchLength(r, letter, digits)
                ElseIf c1 = "V" AndAlso c4 = "P" Then
                    DecodeVersaPlusLength(r, letter, digits)
                ElseIf c1 = "T" OrElse c4 = "F" Then
                    DecodeTuffInchLength(r, letter, digits)
                ElseIf c1 = "K" OrElse c1 = "H" OrElse c1 = "L" Then
                    DecodeKommercialLength(r, letter, digits)
                ElseIf c1 = "V" Then
                    DecodeVersatileInchLength(r, letter, digits)
                Else
                    r.Warnings.Add("Tài liệu không có quy tắc đọc mã chiều dài cho dòng " & r.ProductLine)
                End If
            Case CalloutCategory.DieButton
                DecodeDieButtonLength(r, letter, digits)
            Case Else
                r.Warnings.Add("Tài liệu không có quy tắc đọc mã chiều dài cho nhóm " & r.Category.ToString() & ", giữ nguyên mã " & r.LengthCode)
        End Select
    End Sub

    ' Inch Versatile: 4 số = [inch][phần tư] shank + [inch][phần tư] L, B = L - shank.
    Private Shared Sub DecodeVersatileInchLength(r As CalloutResult, letter As String, digits As String)
        If letter <> "" OrElse digits.Length <> 4 Then
            r.Errors.Add("Mã chiều dài Versatile inch phải gồm 4 chữ số: '" & r.LengthCode & "'")
            Return
        End If
        Dim q1 As Integer = CInt(digits.Substring(1, 1))
        Dim q2 As Integer = CInt(digits.Substring(3, 1))
        If q1 > 3 OrElse q2 > 3 Then
            r.Errors.Add("Mã " & digits & " không hợp lệ: chữ số phần tư inch chỉ được 0-3. Không tự đổi số, cần hỏi lại khách hàng")
            Return
        End If
        Dim shank As Double = CInt(digits.Substring(0, 1)) + q1 / 4.0
        Dim oal As Double = CInt(digits.Substring(2, 1)) + q2 / 4.0
        If shank >= oal Then
            r.Errors.Add("Mã " & digits & " không hợp lệ: shank >= L")
            Return
        End If
        r.Shank = shank * MmPerInch
        r.L = oal * MmPerInch
        r.B = (oal - shank) * MmPerInch
    End Sub

    ' Kommercial & Ball Lock inch: chữ = B (S chuẩn/B .750/C 1.00/D 1.25/E 1.50) + 3 số = L.
    ' Lấy kích thước L
    Private Shared Sub DecodeKommercialLength(r As CalloutResult, letter As String, digits As String)
        If letter = "" Then
            r.Warnings.Add("Tài liệu không mô tả mã chiều dài chỉ gồm số cho " & r.ProductLine & " (ví dụ Compact Pilot), giữ nguyên mã " & r.LengthCode)
            Return
        End If
        If digits.Length <> 3 Then
            r.Errors.Add("Mã chiều dài Kommercial/Ball Lock phải là 1 chữ + 3 số: '" & r.LengthCode & "'")
            Return
        End If
        Dim oal As Double = CInt(digits) / 100.0
        r.L = oal * MmPerInch
        Dim b As Double
        Select Case letter
            Case "S"
                r.LandCode = "S"
                r.Warnings.Add("B = S (chuẩn): chiều dài mũi tra catalog theo D")
                Return
            Case "B" : b = 0.75
            Case "C" : b = 1.0
            Case "D" : b = 1.25
            Case "E" : b = 1.5
            Case Else
                r.Errors.Add("Chữ chiều dài mũi '" & letter & "' không hợp lệ (chỉ S, B, C, D, E)")
                Return
        End Select
        r.B = b * MmPerInch
        r.Shank = (oal - b) * MmPerInch
        If oal - b < 0.75 Then
            r.Errors.Add("Mã " & r.LengthCode & " không hợp lệ: shank = " & (oal - b).ToString("0.###", CultureInfo.InvariantCulture) & """ < 0.75"" (tối thiểu của hàng catalog chuẩn)")
        End If
    End Sub

    ' VersaPlus inch: 3 số L1 + 3 số L, đơn vị 1/100 inch.
    Private Shared Sub DecodeVersaPlusLength(r As CalloutResult, letter As String, digits As String)
        If letter <> "" OrElse digits.Length <> 6 Then
            r.Errors.Add("Mã chiều dài VersaPlus phải gồm 6 chữ số: '" & r.LengthCode & "'")
            Return
        End If
        Dim l1 As Double = CInt(digits.Substring(0, 3)) / 100.0
        Dim oal As Double = CInt(digits.Substring(3, 3)) / 100.0
        If l1 >= oal Then
            r.Errors.Add("Mã " & digits & " không hợp lệ: L1 >= L. Không tự đảo số, cần hỏi lại khách hàng")
            Return
        End If
        r.L1 = l1 * MmPerInch
        r.L1FromCode = r.L1
        r.L = oal * MmPerInch
    End Sub

    ' TuffPunch inch: chữ = L1 (A .500/B .750/C 1.00) + 3 số L.
    Private Shared Sub DecodeTuffInchLength(r As CalloutResult, letter As String, digits As String)
        If letter = "" OrElse digits.Length <> 3 Then
            r.Errors.Add("Mã chiều dài TuffPunch inch phải là 1 chữ + 3 số: '" & r.LengthCode & "'")
            Return
        End If
        Dim l1 As Double
        Select Case letter
            Case "A" : l1 = 0.5
            Case "B" : l1 = 0.75
            Case "C" : l1 = 1.0
            Case Else
                r.Errors.Add("TuffPunch chỉ có chữ A, B, C cho L1. Muốn L1 khác phải dùng gia công XBR (ví dụ XBR1.250)")
                Return
        End Select
        r.L1 = l1 * MmPerInch
        r.L1FromCode = r.L1
        r.L = CInt(digits) / 100.0 * MmPerInch
    End Sub

    ' Metric: 2 số đầu = L1 (mm), phần còn lại = L (mm). Mã 3 số như 840 = L1 8 + L 40.
    Private Shared Sub DecodeMetricPunchLength(r As CalloutResult, letter As String, digits As String)
        If letter <> "" Then
            r.Warnings.Add("Chày metric thường có mã chiều dài chỉ gồm số, giữ nguyên mã " & r.LengthCode)
            Return
        End If
        If digits.Length <= 2 Then
            r.L = CInt(digits)
            Return
        End If
        Dim chosen As Integer = 0
        For Each cut As Integer In New Integer() {2, 1}
            If digits.Length - cut < 2 Then Continue For
            Dim l1 As Integer = CInt(digits.Substring(0, cut))
            Dim oal As Integer = CInt(digits.Substring(cut))
            If MetricL1Codes.Contains(l1) AndAlso oal > l1 Then
                chosen = cut
                Exit For
            End If
        Next
        If chosen = 0 Then
            chosen = If(digits.Length >= 4, 2, 1)
            r.Warnings.Add("L1 trong mã " & digits & " không thuộc danh sách chuẩn (8, 10, 13, 15, 19, 21, 25, 27, 30, 32)")
        End If
        r.L1 = CInt(digits.Substring(0, chosen))
        r.L1FromCode = r.L1
        r.L = CInt(digits.Substring(chosen))
        If r.L.Value <= r.L1.Value Then r.Errors.Add("Mã " & digits & " không hợp lệ: L <= L1")
    End Sub

    ' Cối: inch = 1/100 inch làm tròn 1/16 (62 = .625); metric = mm. Chữ đứng trước = land B.
    Private Shared Sub DecodeDieButtonLength(r As CalloutResult, letter As String, digits As String)
        If r.IsMetric Then
            r.L = CInt(digits)
        Else
            r.L = Nearest16(CInt(digits) / 100.0) * MmPerInch
        End If
        If letter <> "" Then
            r.LandCode = letter
            If r.IsMetric AndAlso "DBC".Contains(r.TypeCode.Substring(0, 1)) Then
                r.Warnings.Add("Theo tài liệu cối Versatile/Ball Lock metric chỉ có số, chữ '" & letter & "' là bất thường")
            End If
        End If
    End Sub

    Private Shared Sub ApplyAlterations(r As CalloutResult)
        Dim explicitB As Boolean = False
        For Each a As CalloutAlteration In r.Alterations
            If Not a.Value.HasValue Then Continue For
            Dim v As Double = ToMm(r, a.Value.Value)
            Select Case a.Code
                Case "XBR"
                    r.L1 = v
                Case "XB", "XBB", "X3B"
                    If r.Category = CalloutCategory.DieButton Then
                        r.LandLength = v
                    Else
                        r.B = v
                        explicitB = True
                        If r.Shank.HasValue AndAlso r.L.HasValue Then r.Shank = r.L.Value - v
                    End If
            End Select
        Next

        For Each a As CalloutAlteration In r.Alterations
            If Not a.Value.HasValue Then Continue For
            Dim v As Double = ToMm(r, a.Value.Value)
            Select Case a.Code
                Case "XL", "XLL", "LL"
                    ' XLB mới giữ B, nên XL/XLL rút ngắn từ phía mũi (trừ khi B được ghi rõ bằng XB).
                    r.L = v
                    If explicitB AndAlso r.B.HasValue Then
                        r.Shank = v - r.B.Value
                    ElseIf r.Shank.HasValue AndAlso r.B.HasValue Then
                        r.B = v - r.Shank.Value
                    End If
                    If r.L1.HasValue Then r.Warnings.Add(a.Code & " rút ngắn L từ phía mũi: cần kiểm tra lại L1")
                Case "XLB"
                    r.L = v
                    If r.B.HasValue Then r.Shank = v - r.B.Value
                Case "XD"
                    r.D = v
                    r.Warnings.Add("XD: đường kính thân đã giảm còn " & v.ToString("0.###", CultureInfo.InvariantCulture) & " mm")
                Case "XT", "XTT", "TT"
                    r.HeadThickness = v
                Case "XH"
                    r.HeadDiameter = v
                Case "XP", "XXP"
                    If Not r.PointDims.ContainsKey("P") Then
                        r.PointDims("P") = v
                        r.Warnings.Add(a.Code & " không có trong tài liệu, tạm lấy làm P")
                    End If
                Case "XW", "XXW"
                    If Not r.PointDims.ContainsKey("W") Then
                        r.PointDims("W") = v
                        r.Warnings.Add(a.Code & " không có trong tài liệu, tạm lấy làm W")
                    End If
            End Select
        Next
    End Sub

    Private Shared Sub FinalChecks(r As CalloutResult)
        For Each a As CalloutAlteration In r.Alterations
            If CoatingsNeedM2.Contains(a.Code) AndAlso r.Steel = "A2" Then
                r.Warnings.Add(a.Code & ": lớp phủ yêu cầu thép M2 trở lên, nhưng thép là A2")
            End If
        Next
        If (r.Category = CalloutCategory.Punch OrElse r.Category = CalloutCategory.DieButton) AndAlso
           r.TypeCode.Substring(2, 1) <> "B" AndAlso r.PointDims.Count = 0 Then
            r.Warnings.Add("Không có kích thước mũi/lỗ (P, W, ...)")
        End If
        If "ORKLY".Contains(r.TypeCode.Substring(2, 1)) AndAlso r.PointDims.ContainsKey("P") AndAlso Not r.PointDims.ContainsKey("W") Then
            r.Warnings.Add("Hình '" & r.Shape & "' thường cần cả P và W")
        End If
        Dim kind As String = r.TypeCode.Substring(1, 1)
        If r.Category = CalloutCategory.Punch AndAlso r.D.HasValue AndAlso r.P.HasValue Then
            If r.P.Value > r.D.Value AndAlso kind <> "K" AndAlso kind <> "Z" Then
                r.Warnings.Add("P > D nhưng loại không phải 'mũi lớn hơn thân' (K/Z), cần kiểm tra")
            End If
            If "UYW".Contains(kind) AndAlso r.LengthCode = "" AndAlso r.D.Value > 2 * r.P.Value Then
                r.Warnings.Add("Chày thẳng: số " & r.DCode & " lớn hơn nhiều so với P, có thể là L chứ không phải D, cần xác nhận")
            End If
        End If
    End Sub

    Private Shared Function ToMm(r As CalloutResult, v As Double) As Double
        If r.IsMetric Then Return v
        Return v * MmPerInch
    End Function

    Private Shared Function Nearest16(inch As Double) As Double
        Return Math.Round(inch * 16.0, MidpointRounding.AwayFromZero) / 16.0
    End Function

    Private Shared Function ParseNum(s As String) As Double
        Return Double.Parse(s, NumberStyles.Float, CultureInfo.InvariantCulture)
    End Function
End Class
