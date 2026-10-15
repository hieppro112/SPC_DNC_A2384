Option Strict On
Imports System
Imports System.Globalization
Imports System.IO
Imports System.Text

Module Tester
    Sub Main(args As String())
        Console.OutputEncoding = Encoding.UTF8
        Console.InputEncoding = Encoding.UTF8

        If args.Length > 0 Then
            RunFile(args(0))
            Console.WriteLine("Nhấn Enter để đóng...")
            Console.ReadLine()
            Return
        End If

        Console.WriteLine("=== TEST PARSER CALLOUT DAYTON ===")
        Console.WriteLine("Nhập product name rồi nhấn Enter. Để trống + Enter để thoát.")
        Console.WriteLine("Ví dụ: AJX 08 1950 M2 P6.300 XBR13.000 XCN")
        While True
            Console.WriteLine()
            Console.Write("> ")
            Dim line As String = Console.ReadLine()
            If line Is Nothing OrElse line.Trim() = "" Then Exit While

            Dim model As CalloutModel = CalloutMapper.Parse(line)
            Console.Write(Describe(model))
        End While
    End Sub

    ' Mỗi dòng trong file là 1 product name; kết quả ghi ra file *_ket_qua.txt cạnh file gốc.
    Private Sub RunFile(path As String)
        Dim outPath As String = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(System.IO.Path.GetFullPath(path)),
                                                      System.IO.Path.GetFileNameWithoutExtension(path) & "_ket_qua.txt")
        Dim n, errCount As Integer
        Using w As New StreamWriter(outPath, False, New UTF8Encoding(True))
            For Each line As String In File.ReadAllLines(path, Encoding.Default)
                If line.Trim() = "" Then Continue For
                Dim model As CalloutModel = CalloutMapper.Parse(line)
                n += 1
                If Not model.IsValid Then errCount += 1
                w.WriteLine(Describe(model))
            Next
        End Using
        Console.WriteLine("Đã phân tích " & n & " dòng (" & errCount & " dòng có LỖI).")
        Console.WriteLine("Kết quả: " & outPath)
    End Sub

    Private Function Describe(model As CalloutModel) As String
        Dim sb As New StringBuilder()
        sb.AppendLine("model.ProductName   = " & model.ProductName)
        sb.AppendLine("model.IsValid       = " & model.IsValid.ToString())
        sb.AppendLine("model.TypeCode      = " & model.TypeCode)
        sb.AppendLine("model.ProductLine   = " & model.ProductLine)
        sb.AppendLine("model.Kind          = " & model.Kind)
        sb.AppendLine("model.Shape         = " & model.Shape)
        If model.Specialty <> "" Then sb.AppendLine("model.Specialty     = " & model.Specialty)
        If model.Modifier <> "" Then sb.AppendLine("model.Modifier      = " & model.Modifier)
        sb.AppendLine("model.Category      = " & model.Category)
        sb.AppendLine("model.IsMetric      = " & model.IsMetric.ToString())
        sb.AppendLine("model.DCode         = " & model.DCode)
        sb.AppendLine("model.LengthCode    = " & model.LengthCode)
        sb.AppendLine("model.D             = " & Mm(model.D))
        sb.AppendLine("model.L             = " & Mm(model.L))
        sb.AppendLine("model.L1            = " & Mm(model.L1))
        sb.AppendLine("model.L1FromCode    = " & Mm(model.L1FromCode))
        sb.AppendLine("model.B             = " & Mm(model.B))
        sb.AppendLine("model.Shank         = " & Mm(model.Shank))
        sb.AppendLine("model.P             = " & Mm(model.P))
        sb.AppendLine("model.W             = " & Mm(model.W))
        sb.AppendLine("model.R             = " & Mm(model.R))
        For Each kv In model.OtherDims
            sb.AppendLine(("model.OtherDims(""" & kv.Key & """)").PadRight(20) & "= " & Mm(kv.Value))
        Next
        If model.LandCode <> "" Then sb.AppendLine("model.LandCode      = " & model.LandCode)
        If model.LandLength.HasValue Then sb.AppendLine("model.LandLength    = " & Mm(model.LandLength))
        If model.HeadThickness.HasValue Then sb.AppendLine("model.HeadThickness = " & Mm(model.HeadThickness))
        If model.HeadDiameter.HasValue Then sb.AppendLine("model.HeadDiameter  = " & Mm(model.HeadDiameter))
        sb.AppendLine("model.Steel         = " & model.Steel & If(model.SteelIsDefault, " (mặc định)", ""))
        For Each a As String In model.Alterations
            sb.AppendLine("model.Alterations   : " & a)
        Next
        For Each e As String In model.Errors
            sb.AppendLine("model.Errors        : " & e)
        Next
        For Each wn As String In model.Warnings
            sb.AppendLine("model.Warnings      : " & wn)
        Next
        Return sb.ToString()
    End Function

    Private Function Mm(v As Double?) As String
        If Not v.HasValue Then Return "Nothing"
        Return v.Value.ToString("0.####", CultureInfo.InvariantCulture)
    End Function
End Module
