Public Class FormPin

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        Main.LabelARA.Text = "0"
        Main.intAraOnly = 0
        Close()
    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        Main.LabelARA.Text = "1"
        Main.intAraOnly = 1
        Close()
    End Sub
End Class