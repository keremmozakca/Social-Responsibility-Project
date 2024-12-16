Public Class TeamPlayer

    Public Property team_name As String
    Public Property image As Image
    Public Property right_motion As Integer
    Public Property right_discovery As Integer
    Public Property right_collectable As Integer
    Public Property right_condition As Integer
    Public Property money As Integer

    Public Property GameObject As PictureBox

    Public Property locationX As Integer
    Public Property locationY As Integer
    Public Property mapPositionRow As Integer
    Public Property mapPositionCol As Integer
    Public Property treasureLock As Integer

    Public Sub New(team_name As String, right_m As Integer, right_d As Integer, right_col As Integer,
                       right_con As Integer, row As Integer, col As Integer)
        Me.right_motion = right_m
        Me.right_discovery = right_d
        Me.right_collectable = right_col
        Me.right_condition = right_con
        Me.money = 0
        Me.GameObject = New PictureBox()
        Me.mapPositionRow = row
        Me.mapPositionCol = col
        Me.money = 3
        Me.treasureLock = 3
    End Sub

    Public Sub Initialize(location As Point, size As Size)
        Me.GameObject.Location = location
        Me.GameObject.Size = size
        Me.GameObject.SizeMode = PictureBoxSizeMode.StretchImage
        Me.GameObject.Image = Me.image
        Me.locationX = Me.GameObject.Location.X
        Me.locationY = Me.GameObject.Location.Y
    End Sub
End Class