Imports System
Imports System.ComponentModel
Imports System.Threading
Imports System.IO.Ports
Imports System.Text
Imports System.IO

Public Class frmMain
    Dim myPort As Array
    Delegate Sub SetTextCallback(ByVal [text] As String)
    Dim FILE_NAME As String = "C:\Users\Public\text_timbang.txt"

    Private Sub frmMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SerialPort1.PortName = "COM1"
        SerialPort1.BaudRate = 9600
        SerialPort1.Parity = IO.Ports.Parity.None
        SerialPort1.StopBits = IO.Ports.StopBits.One
        SerialPort1.DataBits = 8
        SerialPort1.Open()
        Me.ShowInTaskbar = False
        Me.Hide()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

    End Sub

    Private Sub SerialPort1_DataReceived(ByVal sender As Object, ByVal e As System.IO.Ports.SerialDataReceivedEventArgs) Handles SerialPort1.DataReceived
        ReceivedText(SerialPort1.ReadExisting())
    End Sub

    Private Sub ReceivedText(ByVal [text] As String)
        If Me.RichTextBox1.InvokeRequired Then
            Dim x As New SetTextCallback(AddressOf ReceivedText)
            Me.Invoke(x, New Object() {(text)})
        Else
            Me.RichTextBox1.Text &= [text]
        End If
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        If System.IO.File.Exists(FILE_NAME) = True Then
            Dim objWriter As New System.IO.StreamWriter(FILE_NAME, False)
            objWriter.Write(RichTextBox1.Text)
            objWriter.Close()
        Else
            Dim path As String = "C:\Users\Public\text_timbang.txt"
            Dim fs As FileStream = File.Create(path)
            fs.Close()

            Dim objWriter As New System.IO.StreamWriter(FILE_NAME, False)
            objWriter.Write(RichTextBox1.Text)
            objWriter.Close()
        End If
    End Sub

    Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick
        RichTextBox1.Text = ""
        System.IO.File.WriteAllText(FILE_NAME, "")
    End Sub
End Class
