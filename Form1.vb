Imports System.Text
Imports System.Net
Imports System.IO
Public Class Form1
    Dim img As Boolean = True
    Dim at As Boolean
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        gurl(Nothing)
    End Sub
    Public Function GenerateRandomString(ByRef iLength As Integer) As String
        Dim rdm As New Random()
        If img = False Then
            Dim allowChrs() As Char = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz-_"
            Dim sResult As String = ""
            For i As Integer = 0 To iLength - 1
                sResult += allowChrs(rdm.Next(0, allowChrs.Length))
            Next
            Return sResult
        End If
        If img = True Then
            Dim allowChrs() As Char = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz"
            Dim sResult As String = ""
            For i As Integer = 0 To iLength - 1
                sResult += allowChrs(rdm.Next(0, allowChrs.Length))
            Next
            Return sResult
        End If
    End Function

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Clipboard.SetText(Label1.Text)
    End Sub


    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Label1.Text = "Generating..."
        PictureBox1.Visible = False
        Me.Size = New Size(570, 208)
        Timer1.Enabled = True
        File.WriteAllText("C:\mas\t.log", "")
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Label2.Text = "Checking..."
        If at = False Then
            Button1.PerformClick()
        ElseIf at = True Then
            If img = True Then
                Dim a As Boolean = AreSameImage(PictureBox1.BackgroundImage, PictureBox2.BackgroundImage)
                If a = True Then
                    Label2.Text = "Image may be removed or not available"
                    Label2.ForeColor = Color.Red
                    PictureBox1.Visible = False
                    Button1.PerformClick()
                    Exit Sub
                End If
            End If
            Label2.Text = "Success"
            PictureBox1.Visible = True
            Label2.ForeColor = Color.Green
            Timer1.Enabled = False
            End If
    End Sub

    Private Sub TrackBar1_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TrackBar1.Scroll
        If TrackBar1.Value = 0 Then TrackBar1.Value = 1
        Timer1.Interval = TrackBar1.Value
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Timer1.Enabled = False
        PictureBox1.Visible = True
        Label2.Text = "Ready"
        ListBox1.Items.Clear()
        TextBox1.Text = ""
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Dim d As String
        d = InputBox("Enter the video ID (after watch?=)", "URL preview", "-_-_-_-_-_-")
        PictureBox1.Visible = True
        gurl(d)
    End Sub
    Sub gurl(ByVal ref As String)
        Try
            Label2.ForeColor = Color.Black
ingen:
            Label2.Text = ""
            Dim vak As Integer
            If img = False Then vak = 11
            If img = True Then vak = 7
            Dim sast As String = GenerateRandomString(vak)
            If Not ref = Nothing Then
                sast = ref
                GoTo sc
            End If
            If TextBox1.Text.Contains(sast) = True Then GoTo ingen
sc:
            Dim r As New Random()
            Dim i As Integer = r.Next(2)
            Dim s As String
            If i = 0 Then s = ".png"
            If i = 1 Then s = ".jpg"
            If i = 2 Then s = ".gif"
            If img = False Then Label1.Text = "http://www.youtube.com/watch?v=" & sast
            ListBox1.Items.Add(Label1.Text)
            TextBox1.Text = TextBox1.Text & ";" & sast
            If img = False Then
                Dim url As String = "http://i.ytimg.com/vi/" & sast & "/0.jpg"
                ListBox1.Items.Add(url)
                Dim tClient As WebClient = New WebClient
                Dim tImage As Bitmap = Bitmap.FromStream(New MemoryStream(tClient.DownloadData(url)))
                PictureBox1.BackgroundImage = tImage
            End If
            If img = True Then
                Dim url As String = "http://i.imgur.com/" & sast & s
                ListBox1.Items.Add(url)
                Dim tClient As WebClient = New WebClient
                Dim tImage As Bitmap = Bitmap.FromStream(New MemoryStream(tClient.DownloadData(url)))
                PictureBox1.BackgroundImage = tImage
            End If
            at = True

            If img = True Then
                Dim a As Boolean = AreSameImage(PictureBox1.BackgroundImage, PictureBox2.BackgroundImage)
                If a = False Then
                    If img = True Then Label1.Text = "http://i.imgur.com/" & sast & s
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            Label2.Text = "Error details: " & ex.Message()
            PictureBox1.BackgroundImage = Nothing
            at = False
            Exit Sub
        End Try
    End Sub

    Private Sub PictureBox1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox1.Click
        PictureBox1.BackgroundImage = Nothing
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim tClient As WebClient = New WebClient
        Dim tImage As Bitmap = Bitmap.FromStream(New MemoryStream(tClient.DownloadData("http://i.imgur.com/ICNOWnE.jpg")))
        PictureBox2.BackgroundImage = tImage
        If img = True Then Label3.Text = "Switch modes (imgur mode)"
        If File.Exists("C:\mas\t.log") Then
            Label1.Text = "Generating..."
            Me.Size = New Size(570, 208)
            Timer1.Enabled = True
            Try
                File.Delete("C:\mas\t.log")
            Catch ex As Exception
            End Try
        End If
    End Sub

    Private Function AreSameImage(ByVal image As Image, ByVal image1 As Image) As Boolean
        Dim BM1 As Bitmap = image
        Dim BM2 As Bitmap = image1
        For X = 0 To BM1.Width - 1
            For y = 0 To BM2.Height - 1
                If BM1.GetPixel(X, y) <> BM2.GetPixel(X, y) Then
                    Return False
                End If
            Next
        Next
        Return True
    End Function

    Private Sub Label3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label3.Click
        If img = False Then
            img = True
            Label3.Text = "Switch modes (imgur mode)"
            Exit Sub
        ElseIf img = True Then
            img = False
            Label3.Text = "Switch modes (YouTube mode)"
            Exit Sub
        End If
    End Sub
End Class
