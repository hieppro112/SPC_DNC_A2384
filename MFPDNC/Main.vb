
'Imports CalloutTester

Public Class Main
    'マスターデータ定義
    Public mDst As New DataSet
    Public mDst2 As New DataSet
    '転送PRG定義用
    Public ncpro(0 To 50) As String
    Public tenso(0 To 50) As String
    '連番テキスト読み取り
    Public time As String
    Public time2 As String
    Public dtUpdate As DateTime
    'inifile取り込み用
    Public IniPath As String
    Public IniName As String
    Dim IniFileName As String
    Public IniGet11 As Integer
    Public IniGet12 As Integer
    Public IniGet13 As Integer
    Public IniGet14 As Integer
    Public IniGet31 As Integer
    Public IniGet32 As Integer
    Public IniGet33 As Integer
    Public IniGet41 As Integer
    Public IniGet42 As Integer
    Public IniGet51 As Integer
    Public IniGet52 As Integer
    Public IniGetString11 As String
    Public IniGetString12 As String
    Public IniGetString13 As String
    Public IniGetString14 As String
    Public IniGetString31 As String
    Public IniGetString32 As String
    Public IniGetString33 As String
    Public IniGetString41 As String
    Public IniGetString42 As String
    Public IniGetString51 As String
    Public IniGetString52 As String

    '機器接続パラ
    Public TesM As Integer
    'エラーコード
    Public intErc As Integer
    'DIO
    Public Data As Byte
    Public ret As Integer
    Public id As Short
    Public IData(7) As Byte
    Public BitNo(7) As Short
    '転送シーケンスNo
    Public seqn As Double

    'プログラムコード
    Public strPGMCode As String

    '材質コード
    Public strMatCode As String

    Public intAraOnly As Integer

    'ラベルをダブル型に変換
    Public dblD As Double
    Public dblL As Double
    Public dblP As Double

    'テンキー用変数
    Public intSS As Integer

    '転送シーケンス番号
    Public intSeq As Integer
    'シーケンスタイムアウトエラー代わり
    Public intTimeOut As Integer
    'カウントアップし続けないための処理
    Public intCountUp As Integer
    'NCPRG行数
    Public intNCPRGN As Integer

    '前回の駿河連番
    Public strSrgNo As String

    'khia bao doi tuong
    Private MySql As New MySQL()
    Private _productDayton As DaytonItem
    Private _calloutModel As CalloutModel


    Class IniFileHandler
        '-----------------
        ' Ｉｎｉ ファイルクラス
        '-----------------
        '指定のIniファイルの指定キーの値を取得(文字列)
        Declare Function GetPrivateProfileString Lib "KERNEL32.DLL" Alias "GetPrivateProfileStringA" (
        ByVal lpAppName As String,
        ByVal lpKeyName As String, ByVal lpDefault As String,
        ByVal lpReturnedString As String, ByVal nSize As Integer,
        ByVal lpFileName As String) As Integer

        '指定のIniファイルの指定キーの値を取得(整数値)
        Declare Function GetPrivateProfileInt Lib "KERNEL32.DLL" Alias "GetPrivateProfileIntA" (
        ByVal lpAppName As String,
        ByVal lpKeyName As String, ByVal nDefault As Integer,
        ByVal lpFileName As String) As Integer

        '指定のIniファイルの指定キーの値を変更(文字列)
        Declare Function WritePrivateProfileString Lib "KERNEL32.DLL" Alias "WritePrivateProfileStringA" (
        ByVal lpAppName As String, ByVal lpKeyName As String,
        ByVal lpString As String, ByVal lpFileName As String) As Integer

        '指定のIniファイルの指定キーの値を変更(整数値)
        Declare Function WritePrivateProfileInt Lib "KERNEL32.DLL" Alias "WritePrivateProfileIntA" (
        ByVal lpAppName As String, ByVal lpKeyName As String,
        ByVal lpInt As Integer, ByVal lpFileName As String) As Integer


    End Class

    Private Sub ParaSet()
        Dim strFileNamePath As String
        strFileNamePath = "C:\Dnc\filename.CSV"
        'strFileNamePath = "C:\Users\02099\Desktop\栗原\プログラムetc\SPCMTO\MarueiDNC\filename.CSV"
        Dim tr As System.IO.TextReader
        Dim ss As String
        tr = My.Computer.FileSystem.OpenTextFileReader(strFileNamePath, System.Text.Encoding.Default)
        ss = tr.ReadLine
        Dim j As Integer
        j = 0
        Do While ss IsNot Nothing
            Dim strLeft As String
            strLeft = Microsoft.VisualBasic.Left(ss, 1)
            If (strLeft <> "'") Then ' nêu gap dau " ' " thì bỏ qua 
                Select Case j
                    Case 0
                        IniGetString11 = ss
                    Case 1
                        IniGetString12 = ss
                    Case 2
                        IniGetString13 = ss
                    Case 3
                        IniGetString14 = ss
                    Case 4
                        IniGetString31 = ss
                    Case 5
                        IniGetString32 = ss
                    Case 6
                        IniGetString33 = ss
                    Case 7
                        IniGetString41 = ss
                    Case 8
                        IniGetString42 = ss
                    Case 9
                        IniGetString51 = ss
                    Case 10
                        IniGetString52 = ss
                End Select
                j += 1
            End If
            ss = tr.ReadLine
        Loop
        tr.Close()

        TesM = CInt(IniGetString32) ' kiem tra xem che do test mode co duoc kich hoat khong 
    End Sub

    Public Sub ReadMaster(
        ByVal pStrFolderName As String,
        ByVal pStrFileName As String,
        ByVal pBolHeader As Boolean,
        ByVal pStrSQL As String,
        ByRef pDst As DataSet
    )
        'マスターファイル読取定型
        Dim pDcn As New System.Data.OleDb.OleDbConnection()
        Dim pDcm As New System.Data.OleDb.OleDbCommand()
        Dim pDad As New System.Data.OleDb.OleDbDataAdapter()

        If System.IO.File.Exists(pStrFolderName & "\" & pStrFileName) = False Then
            'ファイルが無い場合
            Exit Sub
        End If

        'ヘッダー有無
        Dim pStrHeader As String
        pStrHeader = "NO"
        If pBolHeader = True Then
            pStrHeader = "YES"
        End If

        pDcn.ConnectionString =
                "Provider=Microsoft.Jet.OLEDB.4.0;" &
                "Data Source=" & pStrFolderName & ";" &
                "Extended Properties=""Text;" &
                "HDR=" & pStrHeader & ";" &
                "FMT=Delimited"""

        pDcm.Connection = pDcn

        pDcm.CommandText = pStrSQL

        pDad.SelectCommand = pDcm
        pDst.Clear()
        pDad.Fill(pDst)


        pDcn.Close()

        pDad = Nothing
        pDcm = Nothing
        pDcn = Nothing

    End Sub

    Private Sub GetCSV()
        '受注残ファイル中身全部取得
        If (System.IO.File.Exists(IniGetString13 & IniGetString14) = False) Then
            If (TesM <> 0) Then
                MsgBox("Not found Orderdata")
                Exit Sub
            Else
                If (System.IO.File.Exists(IniGetString11 & IniGetString12) = False) Then
                    MsgBox("Disconected network")
                    Exit Sub
                End If
                System.IO.File.Copy(IniGetString11 & IniGetString12, IniGetString13 & IniGetString14, True)
            End If
        End If

        ReadMaster(
            IniGetString13,
            IniGetString14,
            True,
            "SELECT * FROM [" & IniGetString12 & "]",
            mDst)

    End Sub

    Private Sub CSVREAD()
        '受注残データから連番と一致するデータを取得
        'Dim pDrw() As DataRow = mDst.Tables(0).Select("[SPCPONumber] = '" & CInt(LabelNo.Text) & "'")
        Dim pDrw() As DataRow = mDst.Tables(0).Select("[SPCPONumber] = '" & CLng(LabelNo.Text) & "'")
        If (pDrw.Count = 0) Then
            intErc = 1
            pDrw = Nothing
            Exit Sub
        Else
            Try
                LabelType.Text = pDrw(0).Item("Type")
                Dim strPGMMat As String
                strPGMMat = MasterRead()
                If strPGMMat = "0" Then
                    intErc = 3
                    pDrw = Nothing
                    Exit Sub
                End If
                strPGMCode = Microsoft.VisualBasic.Left(strPGMMat, 4)
                strMatCode = Microsoft.VisualBasic.Right(strPGMMat, 1)
                LabelOrN.Text = pDrw(0).Item("OrderQuantity") & ""
                LabelStyle.Text = pDrw(0).Item("ProductNameFull") & ""
                LabelBL.Text = pDrw(0).Item("BlankL") & ""
                LabelD.Text = pDrw(0).Item("D") & ""
                'LabelD.Text = "5"
                LabelL.Text = pDrw(0).Item("L") & ""
                'LabelL.Text = "38"
                LabelP.Text = pDrw(0).Item("P") & ""
                LabelT.Text = pDrw(0).Item("TC") & ""
                LabelB.Text = pDrw(0).Item("B") & ""
                LabelA.Text = pDrw(0).Item("G") & ""
                LabelY.Text = pDrw(0).Item("Y") & ""
                If (CInt(strMatCode) = 1) Then
                    LabelMat.Text = "SKD11"
                ElseIf (CInt(strMatCode) = 2) Then
                    LabelMat.Text = "SKH51"
                ElseIf (CInt(strMatCode) = 3) Then
                    LabelMat.Text = "HAP40"
                End If
                intAraOnly = 1
                LabelARA.Text = CStr(intAraOnly)
                LabelGN.Text = "0"
                If TesM = 0 Then
                    ret = DioOutBit(id, 4, 0)
                    intSeq = 6
                End If
            Catch ex As Exception
                intErc = 2
                pDrw = Nothing
                Exit Sub
            End Try
        End If
        pDrw = Nothing
    End Sub

    Private Sub GetMaster()
        ReadMaster(
            IniGetString51,
            IniGetString52,
            True,
            "SELECT * FROM [" & IniGetString52 & "]",
            mDst2)
    End Sub

    'doc file master de lay ra PGMCode tuong ung voi Type \ edit
    Private Function MasterRead() As String
        Dim pDrw() As DataRow = mDst2.Tables(0).Select("[Type]= '" & LabelType.Text & "'")
        If (pDrw.Count = 0) Then
            'Return "0"
            Return "11101" 'gan cho 1 master chạy hàng mẫu của SPAS
        Else
            Dim strCode As String
            strCode = pDrw(0).Item("PGMCode")
            Return strCode
        End If
    End Function

    Private Sub Main_FormClosed(sender As Object, e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        ' Tắt lúc đang thu nhỏ/phóng to thì lưu vị trí bình thường, không lưu tọa độ -32000
        If Me.WindowState <> FormWindowState.Normal Then
            My.Settings.Main = Me.RestoreBounds.Location
        End If
        Timer1.Enabled = False
        If (TesM = 0) Then
            SerialPort1.Close()
            Dim n As Integer
            n = 0
            Do
                ret = DioOutBit(id, n, 0)
                n = n + 1
            Loop Until n = 8

            ret = DioExit(id)

        End If
    End Sub

    Private Sub Main_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        ' Vị trí đã lưu nằm ngoài mọi màn hình (tắt lúc thu nhỏ, hoặc màn hình phụ đã rút) thì đưa về màn hình chính
        If Not Screen.AllScreens.Any(Function(s) s.WorkingArea.IntersectsWith(New Rectangle(Me.Location, Me.Size))) Then
            Me.Location = Screen.PrimaryScreen.WorkingArea.Location
        End If
        _productDayton = New DaytonItem()
        Call ParaSet()
        Call GetCSV()
        Call GetMaster()

        If (TesM = 0) Then
            Call SerialOpen()
            Call DIOAct()
        End If
        intSeq = 0
        intCountUp = 0
        intNCPRGN = 19
        LabelMat.Text = "SKD11"
        strMatCode = "1"
        intAraOnly = 1
        LabelARA.Text = CStr(intAraOnly)
        If (TesM = 1) Then
            LabelNC.Text = "NC Not Ready"
        End If
        Button2.BackColor = Color.Yellow
        Button2.Enabled = True
        Timer1.Enabled = True
        lbl_sizeD.Text = ""
        lbl_sizeL.Text = ""


        Dim MySql As New MySQL()
    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        'If (LabelNC.Text = "NC Ready") Then
        If (LabelNC.Text = "NC Ready") Or (TesM <> 0) Then
            'エラーチェック
            If (CDbl(LabelD.Text) < CDbl(LabelP.Text)) Then
                MsgBox("001 P Error P>D")
                Exit Sub
            End If
            If (CDbl(LabelB.Text) <> 0) Then
                If (CDbl(LabelL.Text) - CDbl(LabelB.Text) < 28) Then
                    MsgBox("002 B Error L-B<28mm")
                    Exit Sub
                End If
            End If
            If (CDbl(LabelBL.Text) <> 0) Then
                If (CDbl(LabelL.Text) > CDbl(LabelBL.Text)) Then
                    MsgBox("003 BlankError BlankL<L")
                    Exit Sub
                End If
                If (CDbl(LabelBL.Text) > CDbl(LabelL.Text) + 40) Then
                    MsgBox("004 BlankError BlankL>L+40mm")
                    Exit Sub
                End If
            End If
            'If (CDbl(LabelT.Text) <> 0) Then
            '    If (CDbl(LabelT.Text) >= 5) Then
            '        MsgBox("005 T寸エラー 5<=T")
            '        Exit Sub
            '    End If
            'End If
            If (CDbl(LabelOrN.Text) = 0) Then
                MsgBox("006 Don't input OrderQuantity")
                Exit Sub
            End If
            If (LabelType.Text = "") Then
                MsgBox("007 Don't input Type")
                Exit Sub
            End If
            If (CDbl(LabelD.Text) = 0) Then
                MsgBox("008 Don't input D")
                Exit Sub
            End If
            If (CDbl(LabelL.Text) = 0) Then
                MsgBox("009 Don't input L")
                Exit Sub
            End If
            If (CDbl(LabelBL.Text) = 0) Then
                MsgBox("010 Don't input BlankL")
                Exit Sub
            End If
            If (CInt(strPGMCode) >= 1120 And CInt(strPGMCode) <= 1122) Then
                If (CInt(strPGMCode) >= 1600 And CInt(strPGMCode) <= 1631) Then
                    If (CDbl(LabelL.Text) <> CDbl(LabelBL.Text)) Then
                        FormPin.ShowDialog()

                    End If
                End If
            End If
            Button2.BackColor = Color.Silver
            Button2.Enabled = False
            Call PRGCRI()
            Call PRGOUT()
            If (TesM = 0) Then
                intSeq = 1
            End If
        End If
    End Sub

    Private Sub RS232C()
        For i = 0 To intNCPRGN
            ncpro(i) = tenso(i)
        Next i

        For i = 0 To intNCPRGN
            SerialPort1.Write(ncpro(i) & Chr(&HA))
        Next i
    End Sub

    Private Sub PRGCRI()
        tenso(0) = "%"
        tenso(1) = "O0001"
        tenso(2) = "M73"
        tenso(3) = "N1"
        tenso(4) = "#500=" & strPGMCode
        tenso(5) = "#501=" & LabelD.Text
        tenso(6) = "#502=" & LabelL.Text
        tenso(7) = "#503=" & LabelP.Text
        tenso(8) = "#504=" & LabelB.Text
        tenso(9) = "#507=" & LabelA.Text
        tenso(10) = "#512=" & LabelY.Text
        tenso(11) = "#518=" & LabelOrN.Text
        tenso(12) = "#519=" & strMatCode
        tenso(13) = "#545=" & LabelBL.Text
        tenso(14) = "#514=" & LabelT.Text
        tenso(15) = "#540=" & CStr(intAraOnly)
        tenso(16) = "M98P4010"
        tenso(17) = "M26"
        tenso(18) = "GOTO1"
        tenso(19) = "%"
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        Dim intActN As Integer
        intActN = CInt(LabelGN.Text)
        If (intActN > 0) Then
            intActN -= 1
            LabelGN.Text = CStr(intActN)
        End If
        Dim intOrN As Integer
        intOrN = CInt(LabelOrN.Text)
        If (intOrN > intActN) Then
            'ret = DioOutBit(id, 4, 0)
            If (TesM = 0) Then
                ret = DioOutBit(id, 4, 0)
            End If
        End If
        Button2.BackColor = Color.Yellow
        Button2.Enabled = True
    End Sub

    Private Sub LabelType_Click(sender As System.Object, e As System.EventArgs) Handles LabelType.Click
        intSeq = 0
        Style.ShowDialog()
        Button2.BackColor = Color.Yellow
        Button2.Enabled = True
    End Sub

    Private Sub LabelD_Click(sender As System.Object, e As System.EventArgs) Handles LabelD.Click
        intSeq = 0
        intSS = 1
        tenkeyF.ShowDialog()
        Button2.BackColor = Color.Yellow
        Button2.Enabled = True
    End Sub

    Private Sub LabelL_Click(sender As System.Object, e As System.EventArgs) Handles LabelL.Click
        intSeq = 0
        intSS = 2
        tenkeyF.ShowDialog()
        Button2.BackColor = Color.Yellow
        Button2.Enabled = True
    End Sub

    Private Sub LabelP_Click(sender As System.Object, e As System.EventArgs) Handles LabelP.Click
        intSeq = 0
        intSS = 3
        tenkeyF.ShowDialog()
        Button2.BackColor = Color.Yellow
        Button2.Enabled = True
    End Sub

    Private Sub LabelT_Click(sender As System.Object, e As System.EventArgs) Handles LabelT.Click
        intSeq = 0
        intSS = 4
        tenkeyF.ShowDialog()
        Button2.BackColor = Color.Yellow
        Button2.Enabled = True
    End Sub

    Private Sub LabelB_Click(sender As System.Object, e As System.EventArgs) Handles LabelB.Click
        intSeq = 0
        intSS = 5
        tenkeyF.ShowDialog()
        Button2.BackColor = Color.Yellow
        Button2.Enabled = True
    End Sub

    Private Sub LabelA_Click(sender As System.Object, e As System.EventArgs) Handles LabelA.Click
        intSeq = 0
        intSS = 6
        tenkeyF.ShowDialog()
        Button2.BackColor = Color.Yellow
        Button2.Enabled = True
    End Sub

    Private Sub LabelY_Click(sender As System.Object, e As System.EventArgs) Handles LabelY.Click
        intSeq = 0
        intSS = 7
        tenkeyF.ShowDialog()
        Button2.BackColor = Color.Yellow
        Button2.Enabled = True
    End Sub

    Private Sub LabelARA_Click(sender As System.Object, e As System.EventArgs) Handles LabelARA.Click
        intSeq = 0
        If (intAraOnly = 0) Then
            intAraOnly = 1
            LabelARA.Text = CStr(intAraOnly)
        Else
            intAraOnly = 0
            LabelARA.Text = CStr(intAraOnly)
        End If
        Button2.BackColor = Color.Yellow
        Button2.Enabled = True
    End Sub

    Private Sub LabelMat_Click(sender As System.Object, e As System.EventArgs) Handles LabelMat.Click
        intSeq = 0
        FormMat.ShowDialog()
        Button2.BackColor = Color.Yellow
        Button2.Enabled = True
    End Sub

    Private Sub LabelBL_Click(sender As System.Object, e As System.EventArgs) Handles LabelBL.Click
        intSeq = 0
        intSS = 10
        tenkeyF.ShowDialog()
        Button2.BackColor = Color.Yellow
        Button2.Enabled = True
    End Sub

    Private Sub LabelOrN_Click(sender As System.Object, e As System.EventArgs) Handles LabelOrN.Click
        intSeq = 0
        intSS = 8
        tenkeyF.ShowDialog()
        Button2.BackColor = Color.Yellow
        Button2.Enabled = True
    End Sub

    Private Sub LabelNo_Click(sender As System.Object, e As System.EventArgs) Handles LabelNo.Click
        intSeq = 0
        intSS = 9
        tenkeyF.ShowDialog()
        Button2.BackColor = Color.Yellow
        Button2.Enabled = True
    End Sub

    Private Sub DIOAct()
        ret = DioInit(IniGetString31, id)
        Dim n As Integer = 0
        Do
            IData(n) = 0
            n = n + 1
        Loop Until n = 8
        n = 0
        Do
            ret = DioOutBit(id, n, 0)
            n = n + 1
        Loop Until n = 8
        BitNo = {0, 1, 2, 3, 4, 5, 6, 7}

    End Sub

    Private Sub SerialOpen()
        ' シリアルポートのオープン
        SerialPort1.PortName = IniGetString33
        ' シリアルポートの通信速度指定
        SerialPort1.BaudRate = 4800
        ' シリアルポートのパリティ指定
        SerialPort1.Parity = IO.Ports.Parity.Even
        ' シリアルポートのビット数指定
        SerialPort1.DataBits = 7
        ' シリアルポートのストップビット指定
        SerialPort1.StopBits = IO.Ports.StopBits.Two
        ' シリアルポートのオープン
        SerialPort1.Open()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As System.EventArgs) Handles Timer1.Tick
        Timer1.Enabled = False
        TextBox1.Focus()
        If (TesM = 0) Then
            Call SRead()
            Call TranceSeq()
            Call Count()
            Call honsuu()
        End If
        Timer1.Enabled = True
    End Sub

    Private Sub SRead()
        ret = DioInpMultiBit(id, BitNo, 8, IData)

        If (IData(4) = 0) Then
            If (IData(0) = 0) Then
                LabelNC.Text = "NC Preparation"
            Else
                LabelNC.Text = "NC Ready"
            End If
        Else
            LabelNC.Text = "NC Working"
        End If
        If (IData(3) = 1) Then
            LabelNC.Text = "NC Alarm"
            Dim n As Integer
            n = 0
            Do
                ret = DioOutBit(id, n, 0)
                n = n + 1
            Loop Until n = 8
            intSeq = 0
        End If

    End Sub

    Private Sub TranceSeq()
        If (intSeq = 2) Then
            intTimeOut += 1
        End If
        If (intSeq = 3) Then
            intTimeOut += 1
        End If
        If (intSeq = 4) Then
            intTimeOut += 1
        End If
        If (intSeq = 5) Then
            intTimeOut += 1
        End If
        If (intSeq = 7) Then
            intTimeOut += 1
        End If
        If (intSeq = 8) Then
            intTimeOut += 1
        End If

        If (intTimeOut > 20) Then
            intSeq = 0
            intTimeOut = 0
            'エラー処理
            MsgBox("Time Out")
            Dim n As Integer
            n = 0
            Do
                ret = DioOutBit(id, n, 0)
                n = n + 1
            Loop Until n = 8
            intSeq = 0
            Exit Sub
        End If

        If (intSeq = 5) Then
            If (IData(4) = 1) Then
                ret = DioOutBit(id, 2, 0)
                intSeq = 0
                intTimeOut = 0
            End If
        End If

        If (intSeq = 4) Then
            If (IData(2) = 0) Then
                ret = DioOutBit(id, 2, 1)
                intSeq = 5
                intTimeOut = 0
            End If
        End If

        If (intSeq = 3) Then
            If (IData(1) = 1) Then
                ret = DioOutBit(id, 1, 0)
                Call RS232C()
                intSeq = 4
                intTimeOut = 0
            End If
        End If

        If (intSeq = 2) Then
            If (IData(4) = 0) Then
                ret = DioOutBit(id, 3, 0)
                ret = DioOutBit(id, 1, 1)
                intSeq = 3
                intTimeOut = 0
            End If
        End If

        If (intSeq = 1) Then
            ret = DioOutBit(id, 3, 1)
            intSeq = 2
            intTimeOut = 0
        End If

        If (intSeq = 7) Then
            If (IData(4) = 0) Then
                ret = DioOutBit(id, 3, 0)
                intSeq = 0
                intTimeOut = 0
            End If
        End If

        If (intSeq = 6) Then
            ret = DioOutBit(id, 3, 1)
            intSeq = 7
            intTimeOut = 0
        End If

        If (intSeq = 8) Then
            If (IData(6) = 0) Then
                ret = DioOutBit(id, 5, 0)
                ret = DioOutBit(id, 6, 0)
                intCountUp = 0
                intSeq = 0
                intTimeOut = 0
            End If
        End If
    End Sub

    Private Sub Count()
        If (IData(6) = 1) Then
            If (intCountUp = 0) Then
                intCountUp = 1
                Dim intActN As Integer
                Dim intOrN As Integer
                intActN = CInt(LabelGN.Text)
                intOrN = CInt(LabelOrN.Text)
                intActN += 1
                LabelGN.Text = CStr(intActN)
                If (intActN >= intOrN) Then
                    ret = DioOutBit(id, 6, 1)
                Else
                    ret = DioOutBit(id, 5, 1)
                End If
                intSeq = 8
            End If
        End If
    End Sub

    Private Sub honsuu()
        Dim intActN As Integer
        Dim intOrN As Integer
        intActN = CInt(LabelGN.Text)
        intOrN = CInt(LabelOrN.Text)
        If (intActN >= intOrN) Then
            ret = DioOutBit(id, 4, 1)
        End If
    End Sub

    Private Async Sub LabelNo_TextChanged(sender As Object, e As System.EventArgs) Handles LabelNo.TextChanged
        lbl_sizeD.Text=""
        lbl_sizeL.Text = ""
        If (LabelNo.Text <> "0") Then
            If (LabelNo.Text <> "") Then
                If (LabelNo.Text <> strSrgNo) Then
                    Dim check As Boolean = Await Check_Dayton(LabelNo.Text)
                    strSrgNo = LabelNo.Text
                    intSeq = 0
                    If check = False Then
                        'MsgBox("Không tìm thấy type master tại đường dẫn: " + IniGet51 + IniGet52)
                        CSVREAD()
                        Exit Sub
                    Else
                        DayTonREAD()
                    End If
                    If (intErc = 1) Then 'neu intErc = 1 thi khong loi
                        intErc = 0
                        If System.IO.File.Exists(IniGetString11 & IniGetString12) = True Then
                            System.IO.File.Copy(IniGetString11 & IniGetString12, IniGetString13 & IniGetString14, True)
                        Else
                            MsgBox("Network Disconnected")
                            Exit Sub
                        End If
                        Call GetCSV()
                        Call CSVREAD()
                        If (intErc = 1) Then
                            intErc = 0
                            MsgBox("There is not SRGNo in Orderdata")
                            Exit Sub
                        ElseIf (intErc = 2) Then
                            intErc = 0
                            MsgBox("Search Error")
                            Exit Sub
                        ElseIf intErc = 3 Then
                            intErc = 0
                            'MsgBox("There is not Type in Master")
                            MsgBox("Không tìm thấy type master tại đường dẫn: " + IniGet51 + IniGet52)

                            Exit Sub
                        End If
                    ElseIf (intErc = 2) Then
                        intErc = 0
                        MsgBox("Search Error")
                        Exit Sub
                    ElseIf intErc = 3 Then
                        intErc = 0
                        'MsgBox("There is not Type in Master")
                        MsgBox("Không tìm thấy type master  ")
                        Exit Sub
                    End If
                    Button2.BackColor = Color.Yellow
                    Button2.Enabled = True
                End If
            End If
        End If
    End Sub



    Private Sub PRGOUT()
        For i = 0 To intNCPRGN
            ncpro(i) = tenso(i)
        Next i

        Try
            FileOpen(2, IniGetString41 & IniGetString42, OpenMode.Output)
            For i = 0 To intNCPRGN 'PRGの行数だけ
                PrintLine(2, ncpro(i))
            Next i
        Catch
            MsgBox("Write Error")
        Finally
            FileClose(2)
        End Try
    End Sub

    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress
        If e.KeyChar = Chr(Keys.Enter) Then
            Dim renban As String
            renban = Microsoft.VisualBasic.Left(TextBox1.Text, 12)
            If LabelNo.Text <> renban Then
                LabelNo.Text = renban
            End If
            renban = ""
            TextBox1.Text = ""
        End If
    End Sub

    ' EDIT
    Private Async Function Check_Dayton(ByVal s As String) As Threading.Tasks.Task(Of Boolean)
        'Dim result As String = Await MySql.GetTypeProductAsync(s)
        _productDayton = Await MySql.GetTypeProductAsync(s)
        Dim result = _productDayton.ORD_TYPE
        If result.Contains("DP") Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub DayTonREAD()
        Dim pDrw() As DataRow = mDst.Tables(0).Select("[SPCPONumber] = '" & CLng(LabelNo.Text) & "'")
        If (pDrw.Count = 0) Then
            intErc = 1
            pDrw = Nothing
            Exit Sub
        Else
            Try
                LabelType.Text = pDrw(0).Item("Type")
                Dim strPGMMat As String
                strPGMMat = MasterRead()
                If strPGMMat = "0" Then
                    intErc = 3
                    pDrw = Nothing
                    Exit Sub
                End If
                strPGMCode = Microsoft.VisualBasic.Left(strPGMMat, 4)
                strMatCode = Microsoft.VisualBasic.Right(strPGMMat, 1)


                'lay cac du lieu dua vao
                Console.WriteLine(DaytonCallout.Parse(_productDayton.ProductName).Summary())
                _calloutModel = CalloutMapper.Parse(_productDayton.ProductName)
                Dim prt As String = GetBlankL(_productDayton.PRT_AĐCMT1)

                LabelOrN.Text = pDrw(0).Item("OrderQuantity") & ""
                LabelStyle.Text = pDrw(0).Item("ProductNameFull") & ""
                'LabelBL.Text = pDrw(0).Item("BlankL") & ""
                LabelBL.Text = prt & ""   ' gan gia tri vao blankL
                LabelD.Text = Math.Ceiling(CDbl(_calloutModel.D)).ToString() & ""
                LabelL.Text = Math.Ceiling(CDbl(_calloutModel.L)).ToString() & ""
                LabelP.Text = _calloutModel.P & ""
                LabelT.Text = pDrw(0).Item("TC") & ""
                'LabelB.Text = pDrw(0).Item("B") & _calloutModel.B
                'If Val(pDrw(0).Item("B") & "") = 0 Then
                '    LabelB.Text = _calloutModel.B & ""
                'Else
                '    LabelB.Text = pDrw(0).Item("B") & ""
                'End If
                LabelB.Text = _calloutModel.B & ""
                LabelA.Text = pDrw(0).Item("G") & ""
                LabelY.Text = pDrw(0).Item("Y") & ""
                lbl_sizeL.Text = _calloutModel.L
                lbl_sizeD.Text = _calloutModel.D

                If (CInt(strMatCode) = 1) Then
                    LabelMat.Text = "SKD11"
                ElseIf (CInt(strMatCode) = 2) Then
                    LabelMat.Text = "SKH51"
                ElseIf (CInt(strMatCode) = 3) Then
                    LabelMat.Text = "HAP40"
                End If
                intAraOnly = 1
                LabelARA.Text = CStr(intAraOnly)
                LabelGN.Text = "0"
                If TesM = 0 Then
                    ret = DioOutBit(id, 4, 0)
                    intSeq = 6
                End If
            Catch ex As Exception
                intErc = 2
                pDrw = Nothing
                Exit Sub
            End Try
        End If
        pDrw = Nothing
    End Sub

    'Ham để lấy blankL

    Public Function GetBlankL(blankName As String) As Double?
        If String.IsNullOrWhiteSpace(blankName) Then Return Nothing
        Dim parts As String() = blankName.Trim().Split("-"c)
        If parts.Length < 3 Then Return 0
        Dim v As Double
        If Double.TryParse(parts(2).Trim(), Globalization.NumberStyles.Float,
                           Globalization.CultureInfo.InvariantCulture, v) Then Return v
        Return 0
    End Function
End Class
