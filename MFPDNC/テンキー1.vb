
Public Class tenkeyF
    Public Property MySQL As Object
    'tenky.Textが0の時、ボタンに対応した数値を上書きし、0以外が入力されていた場合、ボタンに対応した数値を右側に付け足す

    Private Sub tenkey0_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tenkey0.Click
        If (tenkey.Text = "0") Then
            tenkey.Text = "0"
        ElseIf (tenkey.Text = "-0") Then
            tenkey.Text = "-0"
        Else
            tenkey.Text = tenkey.Text & "0"
        End If
    End Sub

    Private Sub tenkey1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles テンキー1.Click
        If (tenkey.Text = "0") Then
            tenkey.Text = "1"
        Else
            tenkey.Text = tenkey.Text & "1"
        End If
    End Sub

    Private Sub tenkeyF_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

    End Sub

    Private Sub tenkey_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        tenkey.Text = "0"
    End Sub

    Private Sub tenkey12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tenkey12.Click
        tenkey.Text = "0"
    End Sub

    Private Sub tenkey2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tenkey2.Click
        If (tenkey.Text = "0") Then
            tenkey.Text = "2"
        Else
            tenkey.Text = tenkey.Text & "2"
        End If
    End Sub

    Private Sub tenkey3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tenkey3.Click
        If (tenkey.Text = "0") Then
            tenkey.Text = "3"
        Else
            tenkey.Text = tenkey.Text & "3"
        End If
    End Sub

    Private Sub tenkey4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tenkey4.Click
        If (tenkey.Text = "0") Then
            tenkey.Text = "4"
        Else
            tenkey.Text = tenkey.Text & "4"
        End If
    End Sub

    Private Sub tenkey5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tenkey5.Click
        If (tenkey.Text = "0") Then
            tenkey.Text = "5"
        Else
            tenkey.Text = tenkey.Text & "5"
        End If
    End Sub

    Private Sub tenkey6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tenkey6.Click
        If (tenkey.Text = "0") Then
            tenkey.Text = "6"
        Else
            tenkey.Text = tenkey.Text & "6"
        End If
    End Sub

    Private Sub tenkey7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tenkey7.Click
        If (tenkey.Text = "0") Then
            tenkey.Text = "7"
        Else
            tenkey.Text = tenkey.Text & "7"
        End If
    End Sub

    Private Sub tenkey8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tenkey8.Click
        If (tenkey.Text = "0") Then
            tenkey.Text = "8"
        Else
            tenkey.Text = tenkey.Text & "8"
        End If
    End Sub

    Private Sub tenkey9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tenkey9.Click
        If (tenkey.Text = "0") Then
            tenkey.Text = "9"
        Else
            tenkey.Text = tenkey.Text & "9"
        End If
    End Sub

    Private Sub tenkey11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tenkey11.Click
        Dim ss As String
        ss = InStr(tenkey.Text, ".")
        If (tenkey.Text = "0") Then
            tenkey.Text = "0."
        ElseIf (tenkey.Text = "") Then
            tenkey.Text = "0."
        ElseIf (ss <> "0") Then
            tenkey.Text = tenkey.Text
        Else
            tenkey.Text = tenkey.Text & "."
        End If
    End Sub

    Private Sub tenkey13_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tenkey13.Click
        'メインフォームにあるLabelNo.Textにtenky.Textの数値を入力し、現フォームを閉じる
        If (Main.intSS = 1) Then
            Main.LabelD.Text = tenkey.Text
        ElseIf (Main.intSS = 2) Then
            Main.LabelL.Text = tenkey.Text
        ElseIf (Main.intSS = 3) Then
            Main.LabelP.Text = tenkey.Text
        ElseIf (Main.intSS = 4) Then
            Main.LabelT.Text = tenkey.Text
        ElseIf (Main.intSS = 5) Then
            Main.LabelB.Text = tenkey.Text
        ElseIf (Main.intSS = 6) Then
            Main.LabelA.Text = tenkey.Text
        ElseIf (Main.intSS = 7) Then
            Main.LabelY.Text = tenkey.Text
        ElseIf (Main.intSS = 8) Then
            Main.LabelOrN.Text = tenkey.Text
        ElseIf (Main.intSS = 9) Then
            Main.LabelNo.Text = tenkey.Text
        ElseIf (Main.intSS = 10) Then
            Main.LabelBL.Text = tenkey.Text
        End If
        Close()
    End Sub

    Private Sub Tenkey14_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Tenkey14.Click
        Dim ss As String
        If (tenkey.Text = "0") Then
            tenkey.Text = "0"
        Else
            ss = tenkey.Text.Length
            If (ss - 1 >= 1) Then
                tenkey.Text = tenkey.Text.Substring(0, ss - 1)
            ElseIf (ss - 1 = 0) Then
                tenkey.Text = "0"
            End If
        End If
    End Sub

    Private Sub tenkey_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tenkey.Click

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim pm As String
        Dim txtl As Integer
        pm = Microsoft.VisualBasic.Left(tenkey.Text, 1)
        If (pm = "-") Then
            txtl = tenkey.Text.Length
            tenkey.Text = Microsoft.VisualBasic.Right(tenkey.Text, txtl - 1)
        Else
            tenkey.Text = "-" & tenkey.Text
        End If
    End Sub
End Class
