Imports System
Imports System.Data.SqlClient
Imports System.Runtime.Remoting.Messaging
Imports System.Threading.Tasks


Public Class MySQL
    Private sqlConnect As String = "Server=192.168.122.2;Database=MTO;Uid=admin;Pwd=Buitanphat0201@;Max Pool Size=200;Min Pool Size=5;"
    Private queryGetData As String = "  select orderL.SPCNo,orderL.ORD_TYPE,orderL.ProductName,orderL.BlankNameFull,orderL.ProductNameFull2, CMT.PRT_ADDCMT1
  from [MTO].[dbo].[Manufa_OrderList] orderL
  LEFT JOIN [MANUFASPCPD].[dbo].[MANUFA_F_PD_DT_REQ_CMT] CMT
	ON orderL.SPCNo = CMT.AUFNR COLLATE DATABASE_DEFAULT
  where orderL.SPCNo = @id"
    Public Async Function GetTypeProductAsync(ByVal po As String) As Task(Of DaytonItem)
        Using conn As New SqlConnection(sqlConnect)
            Try
                Await conn.OpenAsync()
                Using cmd As New SqlCommand(queryGetData, conn)
                    cmd.Parameters.AddWithValue("@id", po)
                    Using reader As SqlDataReader = Await cmd.ExecuteReaderAsync()
                        If Await reader.ReadAsync() Then
                            ' In log kiểm tra
                            Console.WriteLine("reader: " & reader("ORD_TYPE").ToString())

                            Dim dayton As New DaytonItem With {
                            .Po = reader("SPCNo").ToString(),
                            .ORD_TYPE = reader("ORD_TYPE").ToString(),
                            .ProductName = reader("ProductName").ToString(),
                            .PRT_AĐCMT1 = reader("PRT_ADDCMT1").ToString()
                            }

                            Return dayton
                        End If
                    End Using
                End Using
                Return Nothing
            Catch ex As Exception
                Console.WriteLine("Error: " & ex.Message)
            End Try

        End Using
    End Function


End Class
