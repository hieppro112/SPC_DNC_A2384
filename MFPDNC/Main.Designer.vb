<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Main
    Inherits System.Windows.Forms.Form

    'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows フォーム デザイナーで必要です。
    Private components As System.ComponentModel.IContainer

    'メモ: 以下のプロシージャは Windows フォーム デザイナーで必要です。
    'Windows フォーム デザイナーを使用して変更できます。  
    'コード エディターを使って変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.LabelNo = New System.Windows.Forms.Label()
        Me.LabelStyle = New System.Windows.Forms.Label()
        Me.LabelType = New System.Windows.Forms.Label()
        Me.LabelD = New System.Windows.Forms.Label()
        Me.LabelL = New System.Windows.Forms.Label()
        Me.LabelP = New System.Windows.Forms.Label()
        Me.LabelT = New System.Windows.Forms.Label()
        Me.LabelB = New System.Windows.Forms.Label()
        Me.LabelA = New System.Windows.Forms.Label()
        Me.LabelY = New System.Windows.Forms.Label()
        Me.LabelARA = New System.Windows.Forms.Label()
        Me.LabelMat = New System.Windows.Forms.Label()
        Me.LabelBL = New System.Windows.Forms.Label()
        Me.LabelOrN = New System.Windows.Forms.Label()
        Me.LabelGN = New System.Windows.Forms.Label()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.SerialPort1 = New System.IO.Ports.SerialPort(Me.components)
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.LabelNC = New System.Windows.Forms.Label()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.lbl_sizeD = New System.Windows.Forms.Label()
        Me.lbl_sizeL = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'LabelNo
        '
        Me.LabelNo.BackColor = System.Drawing.Color.White
        Me.LabelNo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LabelNo.Font = New System.Drawing.Font("MS UI Gothic", 24.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LabelNo.Location = New System.Drawing.Point(20, 40)
        Me.LabelNo.Name = "LabelNo"
        Me.LabelNo.Size = New System.Drawing.Size(210, 45)
        Me.LabelNo.TabIndex = 0
        Me.LabelNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LabelStyle
        '
        Me.LabelStyle.BackColor = System.Drawing.Color.White
        Me.LabelStyle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LabelStyle.Font = New System.Drawing.Font("MS UI Gothic", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LabelStyle.Location = New System.Drawing.Point(21, 120)
        Me.LabelStyle.Name = "LabelStyle"
        Me.LabelStyle.Size = New System.Drawing.Size(700, 45)
        Me.LabelStyle.TabIndex = 1
        Me.LabelStyle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LabelType
        '
        Me.LabelType.BackColor = System.Drawing.Color.White
        Me.LabelType.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LabelType.Font = New System.Drawing.Font("MS UI Gothic", 24.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LabelType.Location = New System.Drawing.Point(20, 200)
        Me.LabelType.Name = "LabelType"
        Me.LabelType.Size = New System.Drawing.Size(335, 45)
        Me.LabelType.TabIndex = 2
        Me.LabelType.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LabelD
        '
        Me.LabelD.BackColor = System.Drawing.Color.White
        Me.LabelD.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LabelD.Font = New System.Drawing.Font("MS UI Gothic", 24.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LabelD.Location = New System.Drawing.Point(20, 280)
        Me.LabelD.Name = "LabelD"
        Me.LabelD.Size = New System.Drawing.Size(150, 45)
        Me.LabelD.TabIndex = 3
        Me.LabelD.Text = "0"
        Me.LabelD.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LabelL
        '
        Me.LabelL.BackColor = System.Drawing.Color.White
        Me.LabelL.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LabelL.Font = New System.Drawing.Font("MS UI Gothic", 24.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LabelL.Location = New System.Drawing.Point(205, 280)
        Me.LabelL.Name = "LabelL"
        Me.LabelL.Size = New System.Drawing.Size(150, 45)
        Me.LabelL.TabIndex = 4
        Me.LabelL.Text = "0"
        Me.LabelL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LabelP
        '
        Me.LabelP.BackColor = System.Drawing.Color.White
        Me.LabelP.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LabelP.Font = New System.Drawing.Font("MS UI Gothic", 24.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LabelP.Location = New System.Drawing.Point(390, 280)
        Me.LabelP.Name = "LabelP"
        Me.LabelP.Size = New System.Drawing.Size(150, 45)
        Me.LabelP.TabIndex = 5
        Me.LabelP.Text = "0"
        Me.LabelP.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LabelT
        '
        Me.LabelT.BackColor = System.Drawing.Color.White
        Me.LabelT.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LabelT.Font = New System.Drawing.Font("MS UI Gothic", 24.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LabelT.Location = New System.Drawing.Point(20, 440)
        Me.LabelT.Name = "LabelT"
        Me.LabelT.Size = New System.Drawing.Size(150, 45)
        Me.LabelT.TabIndex = 6
        Me.LabelT.Text = "0"
        Me.LabelT.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LabelB
        '
        Me.LabelB.BackColor = System.Drawing.Color.White
        Me.LabelB.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LabelB.Font = New System.Drawing.Font("MS UI Gothic", 24.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LabelB.Location = New System.Drawing.Point(20, 360)
        Me.LabelB.Name = "LabelB"
        Me.LabelB.Size = New System.Drawing.Size(150, 45)
        Me.LabelB.TabIndex = 7
        Me.LabelB.Text = "0"
        Me.LabelB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LabelA
        '
        Me.LabelA.BackColor = System.Drawing.Color.White
        Me.LabelA.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LabelA.Font = New System.Drawing.Font("MS UI Gothic", 24.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LabelA.Location = New System.Drawing.Point(205, 360)
        Me.LabelA.Name = "LabelA"
        Me.LabelA.Size = New System.Drawing.Size(150, 45)
        Me.LabelA.TabIndex = 8
        Me.LabelA.Text = "0"
        Me.LabelA.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LabelY
        '
        Me.LabelY.BackColor = System.Drawing.Color.White
        Me.LabelY.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LabelY.Font = New System.Drawing.Font("MS UI Gothic", 24.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LabelY.Location = New System.Drawing.Point(205, 440)
        Me.LabelY.Name = "LabelY"
        Me.LabelY.Size = New System.Drawing.Size(150, 45)
        Me.LabelY.TabIndex = 9
        Me.LabelY.Text = "0"
        Me.LabelY.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LabelARA
        '
        Me.LabelARA.BackColor = System.Drawing.Color.White
        Me.LabelARA.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LabelARA.Font = New System.Drawing.Font("MS UI Gothic", 24.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LabelARA.Location = New System.Drawing.Point(575, 280)
        Me.LabelARA.Name = "LabelARA"
        Me.LabelARA.Size = New System.Drawing.Size(150, 45)
        Me.LabelARA.TabIndex = 10
        Me.LabelARA.Text = "0"
        Me.LabelARA.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LabelMat
        '
        Me.LabelMat.BackColor = System.Drawing.Color.White
        Me.LabelMat.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LabelMat.Font = New System.Drawing.Font("MS UI Gothic", 24.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LabelMat.Location = New System.Drawing.Point(390, 360)
        Me.LabelMat.Name = "LabelMat"
        Me.LabelMat.Size = New System.Drawing.Size(150, 45)
        Me.LabelMat.TabIndex = 12
        Me.LabelMat.Text = "0"
        Me.LabelMat.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LabelBL
        '
        Me.LabelBL.BackColor = System.Drawing.Color.White
        Me.LabelBL.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LabelBL.Font = New System.Drawing.Font("MS UI Gothic", 24.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LabelBL.Location = New System.Drawing.Point(575, 360)
        Me.LabelBL.Name = "LabelBL"
        Me.LabelBL.Size = New System.Drawing.Size(150, 45)
        Me.LabelBL.TabIndex = 16
        Me.LabelBL.Text = "0"
        Me.LabelBL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LabelOrN
        '
        Me.LabelOrN.BackColor = System.Drawing.Color.White
        Me.LabelOrN.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LabelOrN.Font = New System.Drawing.Font("MS UI Gothic", 24.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LabelOrN.Location = New System.Drawing.Point(760, 280)
        Me.LabelOrN.Name = "LabelOrN"
        Me.LabelOrN.Size = New System.Drawing.Size(150, 45)
        Me.LabelOrN.TabIndex = 17
        Me.LabelOrN.Text = "0"
        Me.LabelOrN.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LabelGN
        '
        Me.LabelGN.BackColor = System.Drawing.Color.White
        Me.LabelGN.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LabelGN.Font = New System.Drawing.Font("MS UI Gothic", 24.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LabelGN.Location = New System.Drawing.Point(760, 360)
        Me.LabelGN.Name = "LabelGN"
        Me.LabelGN.Size = New System.Drawing.Size(150, 45)
        Me.LabelGN.TabIndex = 18
        Me.LabelGN.Text = "0"
        Me.LabelGN.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Timer1
        '
        Me.Timer1.Interval = 500
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.Location = New System.Drawing.Point(17, 19)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(102, 21)
        Me.Label1.TabIndex = 20
        Me.Label1.Text = "SPCPONo"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label2.Location = New System.Drawing.Point(17, 99)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(84, 21)
        Me.Label2.TabIndex = 21
        Me.Label2.Text = "TypeFull"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label3.Location = New System.Drawing.Point(17, 179)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(53, 21)
        Me.Label3.TabIndex = 22
        Me.Label3.Text = "Type"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label4.Location = New System.Drawing.Point(17, 259)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(24, 21)
        Me.Label4.TabIndex = 23
        Me.Label4.Text = "D"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label5.Location = New System.Drawing.Point(202, 259)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(21, 21)
        Me.Label5.TabIndex = 24
        Me.Label5.Text = "L"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label6.Location = New System.Drawing.Point(387, 259)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(23, 21)
        Me.Label6.TabIndex = 25
        Me.Label6.Text = "P"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label7.Location = New System.Drawing.Point(17, 419)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(22, 21)
        Me.Label7.TabIndex = 26
        Me.Label7.Text = "T"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label8.Location = New System.Drawing.Point(17, 339)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(23, 21)
        Me.Label8.TabIndex = 27
        Me.Label8.Text = "B"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label9.Location = New System.Drawing.Point(201, 339)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(23, 21)
        Me.Label9.TabIndex = 28
        Me.Label9.Text = "A"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label10.Location = New System.Drawing.Point(202, 419)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(22, 21)
        Me.Label10.TabIndex = 29
        Me.Label10.Text = "Y"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label11.Location = New System.Drawing.Point(571, 259)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(108, 21)
        Me.Label11.TabIndex = 30
        Me.Label11.Text = "Rough only"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label12.Location = New System.Drawing.Point(386, 339)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(79, 21)
        Me.Label12.TabIndex = 31
        Me.Label12.Text = "Material"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label17.Location = New System.Drawing.Point(571, 339)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(69, 21)
        Me.Label17.TabIndex = 36
        Me.Label17.Text = "BlankL"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label18.Location = New System.Drawing.Point(756, 259)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(135, 21)
        Me.Label18.TabIndex = 37
        Me.Label18.Text = "OrderQuantity"
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label19.Location = New System.Drawing.Point(756, 339)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(131, 21)
        Me.Label19.TabIndex = 38
        Me.Label19.Text = "GoodQuantity"
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.White
        Me.Button1.Font = New System.Drawing.Font("MS UI Gothic", 24.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Button1.Location = New System.Drawing.Point(575, 430)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(150, 60)
        Me.Button1.TabIndex = 39
        Me.Button1.Text = "Rework"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'Button2
        '
        Me.Button2.BackColor = System.Drawing.Color.White
        Me.Button2.Font = New System.Drawing.Font("MS UI Gothic", 24.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Button2.Location = New System.Drawing.Point(760, 430)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(150, 60)
        Me.Button2.TabIndex = 40
        Me.Button2.Text = "Transfer"
        Me.Button2.UseVisualStyleBackColor = False
        '
        'LabelNC
        '
        Me.LabelNC.BackColor = System.Drawing.Color.White
        Me.LabelNC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LabelNC.Font = New System.Drawing.Font("MS UI Gothic", 24.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LabelNC.Location = New System.Drawing.Point(670, 40)
        Me.LabelNC.Name = "LabelNC"
        Me.LabelNC.Size = New System.Drawing.Size(240, 45)
        Me.LabelNC.TabIndex = 42
        Me.LabelNC.Text = "NC Not Ready"
        Me.LabelNC.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TextBox1
        '
        Me.TextBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.TextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBox1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.TextBox1.Location = New System.Drawing.Point(810, 12)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(100, 13)
        Me.TextBox1.TabIndex = 43
        '
        'lbl_sizeD
        '
        Me.lbl_sizeD.AutoSize = True
        Me.lbl_sizeD.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lbl_sizeD.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lbl_sizeD.Location = New System.Drawing.Point(67, 259)
        Me.lbl_sizeD.Name = "lbl_sizeD"
        Me.lbl_sizeD.Size = New System.Drawing.Size(67, 21)
        Me.lbl_sizeD.TabIndex = 23
        Me.lbl_sizeD.Text = "size d"
        '
        'lbl_sizeL
        '
        Me.lbl_sizeL.AutoSize = True
        Me.lbl_sizeL.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lbl_sizeL.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lbl_sizeL.Location = New System.Drawing.Point(249, 259)
        Me.lbl_sizeL.Name = "lbl_sizeL"
        Me.lbl_sizeL.Size = New System.Drawing.Size(68, 21)
        Me.lbl_sizeL.TabIndex = 23
        Me.lbl_sizeL.Text = "size L"
        '
        'Main
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(929, 511)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.LabelNC)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Label19)
        Me.Controls.Add(Me.Label18)
        Me.Controls.Add(Me.Label17)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.lbl_sizeL)
        Me.Controls.Add(Me.lbl_sizeD)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.LabelGN)
        Me.Controls.Add(Me.LabelOrN)
        Me.Controls.Add(Me.LabelBL)
        Me.Controls.Add(Me.LabelMat)
        Me.Controls.Add(Me.LabelARA)
        Me.Controls.Add(Me.LabelY)
        Me.Controls.Add(Me.LabelA)
        Me.Controls.Add(Me.LabelB)
        Me.Controls.Add(Me.LabelT)
        Me.Controls.Add(Me.LabelP)
        Me.Controls.Add(Me.LabelL)
        Me.Controls.Add(Me.LabelD)
        Me.Controls.Add(Me.LabelType)
        Me.Controls.Add(Me.LabelStyle)
        Me.Controls.Add(Me.LabelNo)
        Me.DataBindings.Add(New System.Windows.Forms.Binding("Location", Global.複合機DNC.My.MySettings.Default, "Main", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Location = Global.複合機DNC.My.MySettings.Default.Main
        Me.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Name = "Main"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "MFPDNC"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LabelNo As System.Windows.Forms.Label
    Friend WithEvents LabelStyle As System.Windows.Forms.Label
    Friend WithEvents LabelType As System.Windows.Forms.Label
    Friend WithEvents LabelD As System.Windows.Forms.Label
    Friend WithEvents LabelL As System.Windows.Forms.Label
    Friend WithEvents LabelP As System.Windows.Forms.Label
    Friend WithEvents LabelT As System.Windows.Forms.Label
    Friend WithEvents LabelB As System.Windows.Forms.Label
    Friend WithEvents LabelA As System.Windows.Forms.Label
    Friend WithEvents LabelY As System.Windows.Forms.Label
    Friend WithEvents LabelARA As System.Windows.Forms.Label
    Friend WithEvents LabelMat As System.Windows.Forms.Label
    Friend WithEvents LabelBL As System.Windows.Forms.Label
    Friend WithEvents LabelOrN As System.Windows.Forms.Label
    Friend WithEvents LabelGN As System.Windows.Forms.Label
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents SerialPort1 As System.IO.Ports.SerialPort
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents LabelNC As System.Windows.Forms.Label
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents lbl_sizeD As Label
    Friend WithEvents lbl_sizeL As Label
End Class
