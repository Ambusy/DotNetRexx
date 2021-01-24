Imports RxLib.Rexx
Public Class SampleForm
    Dim WithEvents Rx As New Rexx(New RexxCompData)
    Dim DrRc As Integer
    Private Sub SelFileName_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SelFileName.Click
        Dim Files As String = ""
        CompExecRexx.Visible = False
        OpenFileDialog1.Title = "Select a Rexx program"
        OpenFileDialog1.Filter = "Rexx programs (*.rex)|*.rex|All files (*.*)|*.*"
        OpenFileDialog1.FileName = RexxFileName.Text
        OpenFileDialog1.CheckFileExists = True
        OpenFileDialog1.Multiselect = False
        OpenFileDialog1.ShowHelp = False
        OpenFileDialog1.ShowDialog()
        Files = OpenFileDialog1.FileName.Trim()
        If Files <> "" Then
            RexxFileName.Text = Files
            CompExecRexx.Visible = True
        End If
    End Sub
    Private Sub SampleRxLib_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        ' cancel executing Rexx 
        Rexx.CancRexx = True
    End Sub
    Private Sub Form1_KeyDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        ' intercept break-key while rexx is executing (KeyPreview = true!)
        If eventArgs.KeyCode = 19 Then ' User pressed BREAK-key
            If Me.Cursor = System.Windows.Forms.Cursors.WaitCursor Then
                If MsgBox("Cancel?", MsgBoxStyle.OkCancel, "B R E A K") = MsgBoxResult.Ok Then
                    Rexx.CancRexx = True
                End If
            End If
            Exit Sub
        End If
    End Sub
    Private Sub CompExecRexx_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CompExecRexx.Click
        ' compile and run Rexx
        RexxRc.Text = "Rc=" & CStr(CompRunRexx(RexxFileName.Text, RexxParm.Text))
        RexxRc.Visible = True
    End Sub
    Private Sub ExecRexx_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExecRexx.Click
        ' run previously compiled rexx
        RexxRc.Text = "Rc=" & CStr(RunRexx(RexxFileName.Text, RexxParm.Text))
        RexxRc.Visible = True
    End Sub
    Private Function CompRunRexx(ByVal f As String, ByVal p As String) As Integer
        RexxRc.Visible = False
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        Rexx.RexxTrace = CDebug.Checked ' if true, the Rexx script will halt before the first statement in interactive debug
        DrRc = Rx.CompileRexxScript(f) ' compile
        If DrRc = 0 Then
            Rexx.RexxHandlesSay = True 'False ' I provide for SAY and PULL handlers
            DrRc = Rx.ExecuteRexxScript(p) ' execute
            ExecRexx.Visible = True
        End If
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Return DrRc
    End Function
    Private Function RunRexx(ByVal f As String, ByVal p As String) As Integer
        Rexx.RexxHandlesSay = False ' I don't use SAY and PULL handlers anymore
        RexxRc.Visible = False
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        DrRc = Rx.ExecuteRexxScript(p) ' execute
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Return DrRc
    End Function

    Friend Sub RexxCmd(ByVal ExecLayer As String, ByVal RexxCommand As String, ByVal e As RexxEvent, ByRef RexxEnv As Rexx) Handles Rx.doCmd
        ' execute a command in Rexx 
        ' ExecLayer is the name in the ADDRESS environment in Rexx, any string is a valid name
        ' RexxCommand gives the Rexx command string
        ' e points to the Rexx event, containing the returncode rc for Rexx
        ' RexxEnv points to the public rexx-environment-variables
        Dim cvr As DefVariable = Nothing
        Dim execName As String = "", srcName As String = "", n As String = "", k As Integer = 0 ' needed for the interface
        Dim rc As Integer = 0
        If ExecLayer = "VAR" Then ' shows how to read or write a variable and it's value in the ReXX pool
            ' Samplecode to retrieve the value of variable "MyVar"
            ' GetVar returns the value if the variable specified by it's index 
            ' the second parameter returns the execution-time name of the variable, 
            '  which is different only if composite names are used.
            '  Eg. getting the value of "a.i" when "i" in Rexx has value 2, 
            '  would return "a.2"
            ' the third parameter returns the source name of the variable, always equal to the Position-name
            Dim rso As String = RexxEnv.GetVar(RexxEnv.SourceNameIndexPosition("MYVAR", Rexx.tpSymbol.tpVariable, cvr), execName, srcName)
            ' Samplecode to store variable "MyVar" and it's value "MyValue". note variable name always in capitals
            ' Position locates the variable name in the Rexx pool and returns the index
            ' StoreVar stores the given value for the variable with specified index
            RexxEnv.StoreVar(RexxEnv.SourceNameIndexPosition("MYVAR", Rexx.tpSymbol.tpVariable, cvr), "MyValue:" & RexxCommand, k, execName, n)
            ' Samplecode to retrieve the value of variable "MyVar" once again
            Dim rs As String = RexxEnv.GetVar(RexxEnv.SourceNameIndexPosition("MYVAR", Rexx.tpSymbol.tpVariable, cvr), execName, srcName)
        End If
        If ExecLayer = "DO" Then ' shows how to start a rexx program: it's name is passed by the first Rexx. This program need NOT exist when the first rexx is compiled.
            Dim rxc As New DoRexx(RexxCommand, "")
            e.rc = rxc.DrRc ' exit code is returned to the original rexx program
        End If
        If ExecLayer = "EXTERN" Then ' execute a command without an ADDRESS indication of a RXFUNCCALL program
            If RexxCommand.StartsWith("RXFUNCCALL,") Then
                Dim wrds() As String = RexxCommand.Split(","c)
                Dim pars(10) As String
                For i As Integer = 2 To wrds.Length - 1 ' get the parameters
                    pars(i - 2) = RexxEnv.GetVar(RexxEnv.SourceNameIndexPosition(wrds(i).Trim, Rexx.tpSymbol.tpVariable, cvr), execName, srcName)
                Next
                'You do the interface to the dll/modules:
                Select Case wrds(1).ToLower.Trim
                    Case "winmm.dll/mmioformat"
                        Dim mmio As mmioFormat = New mmioFormat(pars(0))
                        Dim format As String = mmio.Do_mmioFormat()
                        If format = "-1" Then format = format & " " & mmio.ErrorMsg
                        Rexx.QStack.Add("this item is on pull stack")
                        ' variable RESULT contains the return value from an external function to a rexx call.
                        RexxEnv.StoreVar(RexxEnv.SourceNameIndexPosition("RESULT", Rexx.tpSymbol.tpVariable, cvr), format, k, execName, n)
                        mmio = Nothing
                    Case "rxsysfn/sysloadfuncs"
                        Dim res As String = pars(2) + CStr(CInt(pars(0)) + CInt(pars(1)))
                        ' variable RESULT contains the return value from an external function to a rexx call.
                        RexxEnv.StoreVar(RexxEnv.SourceNameIndexPosition("RESULT", Rexx.tpSymbol.tpVariable, cvr), res, k, execName, n)
                End Select
                ' CALL wrds(1) (param, param, ..., param) 
                ' and set the returnvalue(s) for REXX: 
            End If
            e.rc = 0 ' exit code is returned
        End If
        If ExecLayer = "WINDOWS" Then ' to call a windows application and wait for it to finish
            Dim myProcess As Process = Nothing
            Dim ExeName, ExeParams As String, SplitterChar As Char, i As Integer
            If RexxCommand.Substring(0, 1) = """" Then ' take care or commands between quotes
                SplitterChar = """"c
                ExeName = RexxCommand.Substring(1)
            Else
                SplitterChar = " "c
                ExeName = RexxCommand
            End If
            i = ExeName.IndexOf(SplitterChar)
            If i = 0 Then
                ExeParams = ""
            Else
                ExeParams = ExeName.Substring(i + 1)
                ExeName = ExeName.Substring(0, i)
            End If
            Try
                myProcess = Process.Start(ExeName, ExeParams)
                myProcess.WaitForExit()
                rc = myProcess.ExitCode
            Catch ex As Exception
                rc = -1 ' return as mainframe VM/CMS does when a command is not found
            End Try
        End If
        e.rc = rc
    End Sub
    Friend Sub RexxCancel() Handles Rx.doCancel
        'handle "User cancelled Rexxprogram"
    End Sub
    Friend Sub RexxStep() Handles Rx.doStep
        ' Handle "Rexx program executed 30.000 steps. 
        ' Eg. allow user to interrupt:
        '
        System.Windows.Forms.Application.DoEvents()
        '
        ' add to the form "Private Sub Form1_KeyDown" as shown above, to handle BREAK-key
        ' note: form.keypreview is set to intercept keys at form-level.
        '
    End Sub
    Friend Sub RexxSay(ByVal s As String) Handles Rx.doSay
        ' give a message to the user
        If MsgBox(s, MsgBoxStyle.OkCancel) = MsgBoxResult.Cancel Then
            Rexx.CancRexx = True
        End If
    End Sub
    Friend Sub RexxPull(ByRef s As String) Handles Rx.doPull
        ' ask a reply from the user
        s = InputBox("Enter value")
    End Sub
End Class
Public Class DoRexx
    ' if your Rexx program invokes another (or recursively the same) Rexx, you need to create this structure in order to be able to handle it's events
    Public WithEvents Rxn As New Rexx(New RexxCompData)
    Public DrRc As Integer
    Public Sub New(ByVal RexxFilename As String, ByVal parms As String)
        If Rxn.CompileRexxScript(RexxFilename) = 0 Then
            DrRc = Rxn.ExecuteRexxScript(parms)
        End If
    End Sub
    ' events are handled by the same routines you already wrote above, I presume
    Private Sub RexxCmd(ByVal env As String, ByVal s As String, ByVal e As RexxEvent, ByRef RexxEnv As Rexx) Handles Rxn.doCmd
        SampleForm.RexxCmd(env, s, e, RexxEnv)
    End Sub
    Private Sub RexxCancel() Handles Rxn.doCancel
        SampleForm.RexxCancel()
    End Sub
    Private Sub RexxStep() Handles Rxn.doStep
        SampleForm.RexxStep()
    End Sub
    Private Sub RexxSay(ByVal s As String) Handles Rxn.doSay
        SampleForm.RexxSay(s)
    End Sub
    Private Sub RexxPull(ByRef s As String) Handles Rxn.doPull
        SampleForm.RexxPull(s)
    End Sub
End Class