Public Class FormMat

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        Main.LabelMat.Text = "SKD11"
        Main.strMatCode = "1"
        Close()
    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        Main.LabelMat.Text = "SKH51"
        Main.strMatCode = "2"
        Close()
    End Sub

    Private Sub Button3_Click(sender As System.Object, e As System.EventArgs) Handles Button3.Click
        Main.LabelMat.Text = "HAP40"
        Main.strMatCode = "3"
        Close()
    End Sub
End Class