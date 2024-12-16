Imports System.Drawing.Design

Public Class Game

    Dim minutes As Integer = 25
    Dim seconds As Integer = 0
    Dim direction As Integer = -1
    Dim actions As Integer = 0
    Dim coefficient As Integer = 1
    Dim count As Integer = 0

    Dim map(14, 11) As PictureBox
    Dim setP = New Point(5, 5)

    Dim currentPlayer As TeamPlayer = Nothing
    Dim playerTime As Integer = -1
    Dim rand As New Random()

    Dim team1 As New TeamPlayer("Takım 1", 10, 4, 5, 3, 2, 0)
    Dim team2 As New TeamPlayer("Takım 2", 10, 4, 5, 3, 0, 10)
    Dim team3 As New TeamPlayer("Takım 3", 10, 4, 5, 3, 10, 11)

    Dim tuzak_indices As Integer
    Dim hint_indices As Integer

    Dim treasure_row As Integer
    Dim treasure_col As Integer

    Dim collect_b As Boolean = False
    Dim control_b As Boolean = False
    Private Sub Game_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        team1.image = My.Resources.gezgin1_nobg
        team2.image = My.Resources.gezgin2_nobg
        team3.image = My.Resources.gezgin3_nobg

        map = {
            {s00, s01, s02, s03, s04, s05, s06, s07, s08, s09, s0_10, s0_11, s0_12, s0_13},
            {s10, s11, s12, s13, s14, s15, s16, s17, s18, s19, s1_10, s1_11, s1_12, s1_13},
            {s20, s21, s22, s23, s24, s25, s26, s27, s28, s29, s2_10, s2_11, s2_12, s2_13},
            {s30, s31, s32, s33, s34, s35, s36, s37, s38, s39, s3_10, s3_11, s3_12, s3_13},
            {s40, s41, s42, s43, s44, s45, s46, s47, s48, s49, s4_10, s4_11, s4_12, s4_13},
            {s50, s51, s52, s53, s54, s55, s56, s57, s58, s59, s5_10, s5_11, s5_12, s5_13},
            {s60, s61, s62, s63, s64, s65, s66, s67, s68, s69, s6_10, s6_11, s6_12, s6_13},
            {s70, s71, s72, s73, s74, s75, s76, s77, s78, s79, s7_10, s7_11, s7_12, s7_13},
            {s80, s81, s82, s83, s84, s85, s86, s87, s88, s89, s8_10, s8_11, s8_12, s8_13},
            {s90, s91, s92, s93, s94, s95, s96, s97, s98, s99, s9_10, s9_11, s9_12, s9_13},
            {s10_0, s10_1, s10_2, s10_3, s10_4, s10_5, s10_6, s10_7, s10_8, s10_9, s10_10, s10_11, s10_12, s10_13}
        }

        treasure_assign()
        tuzak_assign()
        hint_assign()
        bonus_assign()

        team1.team_name = "Takım 1"
        team2.team_name = "Takım 2"
        team3.team_name = "Takım 3"
        'Dim startPosition As Point = Canvas.PointToClient(New Point(0, 0))
        '8, 121

        team1.Initialize(createPoint(map(2, 0)), New Drawing.Size(40, 40))
        team2.Initialize(createPoint(map(0, 10)), New Drawing.Size(40, 40))
        team3.Initialize(createPoint(map(10, 11)), New Drawing.Size(40, 40))

        'team1.GameObject.BackColor = Color.FromArgb(128, 255, 128)
        'team2.GameObject.BackColor = Color.FromArgb(128, 255, 128)
        'team3.GameObject.BackColor = Color.FromArgb(128, 255, 128)

        team1.GameObject.BackColor = Color.DarkGreen
        team2.GameObject.BackColor = Color.DarkGreen
        team3.GameObject.BackColor = Color.DarkGreen

        Canvas.Controls.Add(team1.GameObject)
        Canvas.Controls.Add(team2.GameObject)
        Canvas.Controls.Add(team3.GameObject)

        team1.GameObject.BringToFront()
        team2.GameObject.BringToFront()
        team3.GameObject.BringToFront()

        currentPlayer = team1
        playerTime = 0
        sira.Text = "Takım 1"

        motion.Text = currentPlayer.right_motion
        discovery.Text = currentPlayer.right_discovery
        collectable.Text = currentPlayer.right_collectable
        condition.Text = currentPlayer.right_condition
        para.Text = currentPlayer.money

        Zaman.Start()
        'Coordinate_Sys.Start()
    End Sub

    Function createPoint(pb As PictureBox)
        Return New Point(pb.Location.X + setP.X, pb.Location.Y + setP.Y)
    End Function
    Function tuzak_assign()
        Dim new_trap_row As Integer
        Dim new_trap_col As Integer
        Dim i As Integer = 1
        While (i < 11)
            new_trap_row = rand.Next(0, 11)
            new_trap_col = rand.Next(0, 14)
            If map(new_trap_row, new_trap_col).Tag = "" Then
                map(new_trap_row, new_trap_col).Tag = "Trap"
                'map(new_trap_row, new_trap_col).BackColor = Color.IndianRed
                'MsgBox(new_trap_row & " " & new_trap_col)
            Else
                Continue While
            End If
            i += 1
        End While
    End Function
    Function hint_assign()
        Dim new_hint_row As Integer
        Dim new_hint_col As Integer
        Dim i As Integer = 1
        While (i < 6)
            new_hint_row = rand.Next(0, 11)
            new_hint_col = rand.Next(0, 14)
            If map(new_hint_row, new_hint_col).Tag = "" Then
                map(new_hint_row, new_hint_col).Tag = "Hint"
                'map(new_hint_row, new_hint_col).BackColor = Color.Turquoise
                'MsgBox(new_hint_row & " " & new_hint_col)
            Else
                Continue While
            End If
            i += 1
        End While
    End Function

    Function treasure_assign()
        treasure_row = rand.Next(0, 11)
        treasure_col = rand.Next(0, 14)
        If map(treasure_row, treasure_col).Tag = "" Then
            map(treasure_row, treasure_col).Tag = "Treasure"
            'map(treasure_row, treasure_col).BackColor = Color.Gold
        Else
            treasure_assign()
        End If
    End Function

    Function bonus_assign()
        Dim new_bonus_row As Integer
        Dim new_bonus_col As Integer
        Dim i As Integer = 1
        While (i < 11)
            new_bonus_row = rand.Next(0, 11)
            new_bonus_col = rand.Next(0, 14)
            If map(new_bonus_row, new_bonus_col).Tag = "" Then
                map(new_bonus_row, new_bonus_col).Tag = "Bonus"
                'map(new_bonus_row, new_bonus_col).BackColor = Color.RebeccaPurple
                'MsgBox(new_bonus_row & " " & new_bonus_col)
            Else
                Continue While
            End If
            i += 1
        End While
    End Function
    Private Sub Zaman_Tick(sender As Object, e As EventArgs) Handles Zaman.Tick
        Zaman.Interval = 1000
        Application.DoEvents()
        If (minutes <> 0) Or (seconds > 0) Then
            If (seconds = 60) Then
                'System.Threading.Thread.Sleep(1000)
                seconds -= 1
            ElseIf (seconds < 60) And (seconds > 0) Then
                'System.Threading.Thread.Sleep(1000)
                time.Text = minutes & ":" & seconds
                seconds -= 1
            Else
                time.Text = minutes & ":" & seconds
                minutes -= 1
                seconds = 60
                'System.Threading.Thread.Sleep(1000)
            End If
        Else
            Zaman.Stop()
            MessageBox.Show("Game Over!", "Time's Up")
            Me.Hide()
        End If
    End Sub
    Function color_look(r As Integer, c As Integer)
        If map(r, c).Tag = "Trap" Then
            map(r, c).BackColor = Color.IndianRed
        ElseIf map(r, c).Tag = "Bonus" Then
            map(r, c).BackColor = Color.RebeccaPurple
        ElseIf map(r, c).Tag = "Hint" Then
            map(r, c).BackColor = Color.Turquoise
        ElseIf map(r, c).Tag = "Treasure" Then
            map(r, c).BackColor = Color.Gold
        End If
    End Function
    Function color_restart(r As Integer, c As Integer)
        map(r, c).BackColor = Color.FromArgb(128, 255, 128)
    End Function
    Private Sub forward_Click(sender As Object, e As EventArgs) Handles forward.Click
        If direction = 0 Then
            direction = -1
            forward.BackColor = Color.White
        Else
            forward.BackColor = Color.Aqua
            direction = 0
            back.BackColor = Color.White
            right.BackColor = Color.White
            left.BackColor = Color.White
        End If
    End Sub

    Private Sub right_Click(sender As Object, e As EventArgs) Handles right.Click
        If direction = 1 Then
            direction = -1
            right.BackColor = Color.White
        Else
            right.BackColor = Color.Aqua
            direction = 1
            back.BackColor = Color.White
            forward.BackColor = Color.White
            left.BackColor = Color.White
        End If
    End Sub

    Private Sub left_Click(sender As Object, e As EventArgs) Handles left.Click
        If direction = 2 Then
            direction = -1
            left.BackColor = Color.White
        Else
            left.BackColor = Color.Aqua
            direction = 2
            back.BackColor = Color.White
            right.BackColor = Color.White
            forward.BackColor = Color.White
        End If
    End Sub

    Private Sub back_Click(sender As Object, e As EventArgs) Handles back.Click
        If direction = 3 Then
            direction = -1
            back.BackColor = Color.White
        Else
            back.BackColor = Color.Aqua
            direction = 3
            forward.BackColor = Color.White
            right.BackColor = Color.White
            left.BackColor = Color.White
        End If
    End Sub

    Private Sub tekrar_2_Click(sender As Object, e As EventArgs) Handles tekrar_2.Click
        If coefficient = 2 Then
            coefficient = 1
            tekrar_2.BackColor = Color.White
        Else
            tekrar_2.BackColor = Color.OrangeRed
            coefficient = 2
            tekrar_3.BackColor = Color.White
            tekrar_4.BackColor = Color.White
        End If
    End Sub

    Private Sub tekrar_3_Click(sender As Object, e As EventArgs) Handles tekrar_3.Click
        If coefficient = 3 Then
            coefficient = 1
            tekrar_3.BackColor = Color.White
        Else
            tekrar_3.BackColor = Color.OrangeRed
            coefficient = 3
            tekrar_2.BackColor = Color.White
            tekrar_4.BackColor = Color.White
        End If
    End Sub

    Private Sub tekrar_4_Click(sender As Object, e As EventArgs) Handles tekrar_4.Click
        If coefficient = 4 Then
            coefficient = 1
            tekrar_4.BackColor = Color.White
        Else
            tekrar_4.BackColor = Color.OrangeRed
            coefficient = 4
            tekrar_2.BackColor = Color.White
            tekrar_3.BackColor = Color.White
        End If
    End Sub

    Private Sub go_Click(sender As Object, e As EventArgs) Handles go.Click
        If actions Mod 2 <> 0 Then
            actions -= 1
            go.BackColor = Color.White
        Else
            go.BackColor = Color.GreenYellow
            actions += 1
        End If
    End Sub

    Private Sub control_Click(sender As Object, e As EventArgs) Handles control.Click
        Select Case actions
            Case 2, 3, 6, 7
                actions -= 2
                control.BackColor = Color.White
            Case 0, 1, 4, 5
                control.BackColor = Color.GreenYellow
                actions += 2
        End Select
    End Sub

    Private Sub collect_Click(sender As Object, e As EventArgs) Handles collect.Click
        If actions > 3 Then
            actions -= 4
            collect.BackColor = Color.White
        Else
            collect.BackColor = Color.GreenYellow
            actions += 4
        End If
    End Sub
    Private Sub Coordinate_Sys_Tick(sender As Object, e As EventArgs) Handles Coordinate_Sys.Tick
        Coordinate_Sys.Interval = 500
        team1.locationY += 2
        team1.GameObject.Location = New Point(team1.locationX, team1.locationY)
    End Sub

    Private Sub Canvas_MouseMove(sender As Object, e As MouseEventArgs) Handles Canvas.MouseMove
        xcor.Text = e.X
        ycor.Text = e.Y
    End Sub

    Private Sub run_Click(sender As Object, e As EventArgs) Handles run.Click
        'Coordinate_Sys.Stop()
        'select_player()
        'sira.Text = currentPlayer.team_name
        play()
    End Sub
    Function next_player()
        playerTime = (playerTime + 1) Mod 3
        If playerTime = 0 Then
            currentPlayer = team1
        ElseIf playerTime = 1 Then
            currentPlayer = team2
        Else
            currentPlayer = team3
        End If
        sira.Text = currentPlayer.team_name
        printPlayerStats()
    End Function
    Function printPlayerStats()
        motion.Text = currentPlayer.right_motion
        discovery.Text = currentPlayer.right_discovery
        collectable.Text = currentPlayer.right_collectable
        condition.Text = currentPlayer.right_condition
        para.Text = currentPlayer.money
    End Function
    Function discover()
        Dim r = currentPlayer.mapPositionRow
        Dim c = currentPlayer.mapPositionCol
        Dim r_old As Integer = r
        Dim c_old As Integer = c
        If currentPlayer.right_discovery > 0 Then
            If r + 1 < 11 Then
                color_look(r + 1, c)
            End If
            If c + 1 < 14 Then
                color_look(r, c + 1)
            End If
            If r - 1 > 0 Then
                color_look(r - 1, c)
            End If
            If c - 1 > 0 Then
                color_look(r, c - 1)
            End If
        End If
        currentPlayer.right_discovery -= 1
    End Function
    Function res()
        Dim r = currentPlayer.mapPositionRow
        Dim c = currentPlayer.mapPositionCol
        If r + 1 < 11 Then
            color_restart(r + 1, c)
        End If
        If c + 1 < 14 Then
            color_restart(r, c + 1)
        End If
        If r - 1 > 0 Then
            color_restart(r - 1, c)
        End If
        If c - 1 > 0 Then
            color_restart(r, c - 1)
        End If
    End Function
    Function play()
        Select Case actions
            Case 1
                collect_b = False
                control_b = False
                If currentPlayer.right_motion >= coefficient Then
                    walk()
                Else
                    MsgBox("Bu Hareket İçin Enerjiniz Yok")
                End If
            Case 2
                collect_b = False
                control_b = True
                If currentPlayer.right_discovery > 0 Then
                    discover()
                    next_player()
                End If
            Case 3
                collect_b = False
                control_b = True
                If currentPlayer.right_motion >= coefficient And
                   currentPlayer.right_discovery >= coefficient Then
                    walk()
                Else
                    MsgBox("Hareket için Gerekli Koşullar Sağlanmıyor")
                End If
            Case 4
                collect_b = True
                control_b = False
                If currentPlayer.right_collectable > 0 Then
                    collect_()
                    next_player()
                Else
                    MsgBox("Toplama Hakkınız Yok.")
                End If
            Case 5
                collect_b = True
                control_b = False
                If currentPlayer.right_motion >= coefficient And
                   currentPlayer.right_collectable >= coefficient Then
                    walk()
                Else
                    MsgBox("Hareket için Gerekli Koşullar Sağlanmıyor")
                End If
            Case 6
                collect_b = True
                control_b = True
                If currentPlayer.right_collectable > 0 And
                   currentPlayer.right_discovery > 0 Then
                    collect_()
                    discover()
                    next_player()
                Else
                    MsgBox("Aynı anda hem toplayıp hem keşfedemezsiniz.")
                End If
            Case 7
                collect_b = True
                control_b = True
                If currentPlayer.right_motion >= coefficient And
                   currentPlayer.right_collectable >= coefficient And
                   currentPlayer.right_discovery >= coefficient Then
                    walk()
                Else
                    MsgBox("Hareket için Gerekli Koşullar Sağlanmıyor")
                End If
            Case 0
                collect_b = False
                control_b = False
        End Select
    End Function
    Function walk()
        If direction = 0 Then
            motionUP.Start()
        ElseIf direction = 1 Then
            motionRIGHT.Start()
        ElseIf direction = 2 Then
            motionLEFT.Start()
        ElseIf direction = 3 Then
            motionDOWN.Start()
        Else

        End If
    End Function
    Private Sub Game_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Application.Exit()
    End Sub
    Function collect_()
        Dim tag_ = map(currentPlayer.mapPositionRow, currentPlayer.mapPositionCol).Tag
        Dim r = currentPlayer.mapPositionRow
        Dim c = currentPlayer.mapPositionCol
        If currentPlayer.right_collectable > 0 Then
            If tag_ = "Hint" Then
                give_hint()
                map(currentPlayer.mapPositionRow, currentPlayer.mapPositionCol).Tag = ""
            ElseIf tag_ = "Bonus" Then
                currentPlayer.money += 1
                map(currentPlayer.mapPositionRow, currentPlayer.mapPositionCol).Tag = ""
            ElseIf tag_ = "Treasure" Then
                If currentPlayer.treasureLock = 0 Then
                    MsgBox(currentPlayer.team_name & " bu oyunu kazandı. TEBRİKLER!")
                    'Me.Hide()
                    Application.Exit()
                Else
                    currentPlayer.treasureLock -= 1
                End If
            End If
        End If
        currentPlayer.right_collectable -= 1
    End Function
    Function give_hint()
        Dim r = currentPlayer.mapPositionRow
        Dim c = currentPlayer.mapPositionCol
        If treasure_row > r And treasure_col > c Then
            MsgBox("Hazine Güneydoğu Yönünde..")
        ElseIf treasure_row > r And treasure_col < c Then
            MsgBox("Hazine Güneybatı Yönünde..")
        ElseIf treasure_row < r And treasure_col > c Then
            MsgBox("Hazine Kuzeydoğu Yönünde..")
        ElseIf treasure_row < r And treasure_col < c Then
            MsgBox("Hazine Kuzeybatı Yönünde..")
        Else
            If treasure_row = r Then
                If treasure_col > c Then
                    MsgBox("Hazine Doğu Yönünde..")
                Else
                    MsgBox("Hazine Batı Yönünde..")
                End If
            Else
                If treasure_row < r Then
                    MsgBox("Hazine Kuzey Yönünde..")
                Else
                    MsgBox("Hazine Güney Yönünde..")
                End If
            End If
        End If
    End Function
    Function map_control()
        Dim tag = map(currentPlayer.mapPositionRow, currentPlayer.mapPositionCol).Tag
        Dim loc = map(currentPlayer.mapPositionRow, currentPlayer.mapPositionCol)
        loc.BackColor = Color.DarkGreen
        If tag = "Trap" Then
            motionUP.Stop()
            motionRIGHT.Stop()
            motionLEFT.Stop()
            motionDOWN.Stop()
            Dim s1 As Integer = rand.Next(0, 100)
            Dim s2 As Integer = rand.Next(1, 100)
            Dim result = s1 * s2
            Dim data As String
            data = InputBox(s1 & " X " & s2 & " = " & vbNewLine & "Bu işlemin sonucu kaçtır?")
            If result.ToString() = data Then
                currentPlayer.money += 1
                MsgBox("Tuzaktan Kurtuldun :)")
            Else
                currentPlayer.right_motion -= 5 'CEZA
            End If
            map(currentPlayer.mapPositionRow, currentPlayer.mapPositionCol).Tag = ""
            next_player()
        Else
        End If
    End Function
    Private Sub motionUP_Tick(sender As Object, e As EventArgs) Handles motionUP.Tick
        motionUP.Interval = 500
        'printPlayerStats()
        res()
        If currentPlayer.mapPositionRow > 0 Then
            currentPlayer.mapPositionRow -= 1
            currentPlayer.right_motion -= 1
        End If
        currentPlayer.GameObject.Location = createPoint(map(currentPlayer.mapPositionRow,
                                                            currentPlayer.mapPositionCol))
        count += 1
        If count = coefficient Then
            count = 0
            motionUP.Stop()
            If collect_b = True Then
                collect_()
            End If
            If control_b = True Then
                discover()
            End If
            map_control()
            next_player()
            printPlayerStats()
        Else
            If collect_b = True Then
                collect_()
            End If
            If control_b = True Then
                discover()
            End If
        End If
        map_control()
    End Sub

    Private Sub motionRIGHT_Tick(sender As Object, e As EventArgs) Handles motionRIGHT.Tick
        motionRIGHT.Interval = 500
        res()
        'printPlayerStats()
        If currentPlayer.mapPositionCol < 13 Then
            currentPlayer.mapPositionCol += 1
            currentPlayer.right_motion -= 1
        End If
        currentPlayer.GameObject.Location = createPoint(map(currentPlayer.mapPositionRow,
                                                            currentPlayer.mapPositionCol))
        count += 1
        If count = coefficient Then
            count = 0
            motionRIGHT.Stop()
            If collect_b = True Then
                collect_()
            End If
            If control_b = True Then
                discover()
            End If
            map_control()
            next_player()
            printPlayerStats()
        Else
            If collect_b = True Then
                collect_()
            End If
            If control_b = True Then
                discover()
            End If
        End If
        map_control()
    End Sub

    Private Sub motionLEFT_Tick(sender As Object, e As EventArgs) Handles motionLEFT.Tick
        motionLEFT.Interval = 500
        res()
        'printPlayerStats()
        If currentPlayer.mapPositionCol > 0 Then
            currentPlayer.mapPositionCol -= 1
            currentPlayer.right_motion -= 1
        End If
        currentPlayer.GameObject.Location = createPoint(map(currentPlayer.mapPositionRow,
                                                            currentPlayer.mapPositionCol))
        count += 1
        If count = coefficient Then
            count = 0
            motionLEFT.Stop()
            If collect_b = True Then
                collect_()
            End If
            If control_b = True Then
                discover()
            End If
            map_control()
            next_player()
            printPlayerStats()
        Else
            If collect_b = True Then
                collect_()
            End If
            If control_b = True Then
                discover()
            End If
        End If
        map_control()
    End Sub

    Private Sub motionDOWN_Tick(sender As Object, e As EventArgs) Handles motionDOWN.Tick
        motionDOWN.Interval = 500
        res()
        'printPlayerStats()
        If currentPlayer.mapPositionRow < 10 Then
            currentPlayer.mapPositionRow += 1
            currentPlayer.right_motion -= 1
        End If
        currentPlayer.GameObject.Location = createPoint(map(currentPlayer.mapPositionRow,
                                                            currentPlayer.mapPositionCol))
        count += 1
        If count = coefficient Then
            count = 0
            motionDOWN.Stop()
            If collect_b = True Then
                collect_()
            End If
            If control_b = True Then
                discover()
            End If
            map_control()
            next_player()
            printPlayerStats()
        Else
            If collect_b = True Then
                collect_()
            End If
            If control_b = True Then
                discover()
            End If
        End If
        map_control()
    End Sub
    Private Sub rest_Click(sender As Object, e As EventArgs) Handles rest.Click
        currentPlayer.right_motion += 2
        currentPlayer.right_collectable += 1
        currentPlayer.right_discovery += 1
        next_player()
    End Sub
    Private Sub store_Click(sender As Object, e As EventArgs) Handles store.Click
        If currentPlayer.money > 2 Then
            currentPlayer.right_motion += 3
            currentPlayer.right_collectable += 2
            currentPlayer.right_discovery += 2

            currentPlayer.money -= 2
            printPlayerStats()
        End If
    End Sub
End Class
