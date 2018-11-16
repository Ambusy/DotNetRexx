Imports System.Runtime.InteropServices
Public Class mmioFormat
    Private Declare Ansi Function mmioOpen Lib "winmm.dll" Alias "mmioOpenA" (ByVal szFileName As String, ByRef lpmmioinfo As MMIOINFO, ByVal dwOpenFlags As Integer) As IntPtr
    Private Declare Ansi Function mmioStringToFOURCC Lib "winmm.dll" Alias "mmioStringToFOURCCA" (ByVal sz As String, ByVal uFlags As Integer) As Integer
    Private Declare Function mmioDescendParent Lib "winmm.dll" Alias "mmioDescend" (ByVal hmmio As IntPtr, ByRef lpck As MMCKINFO, ByVal x As Integer, ByVal uFlags As Integer) As Integer
    Private Declare Function mmioDescend Lib "winmm.dll" (ByVal hmmio As IntPtr, ByRef lpck As MMCKINFO, ByRef lpckParent As MMCKINFO, ByVal uFlags As Integer) As Integer
    Private Declare Function mmioReadString Lib "winmm.dll" Alias "mmioRead" (ByVal hmmio As IntPtr, ByVal pch() As Byte, ByVal cch As Integer) As Integer
    Private Declare Function mmioAscend Lib "winmm.dll" (ByVal hmmio As IntPtr, ByRef lpck As MMCKINFO, ByVal uFlags As Integer) As Integer
    Private Declare Sub CopyWaveFormatFromBytes Lib "kernel32" Alias "RtlMoveMemory" (ByRef dest As WAVEFORMAT, ByVal source() As Byte, ByVal cb As Integer)
    Private Declare Function mmioClose Lib "winmm.dll" (ByVal hmmio As IntPtr, ByVal uFlags As Integer) As Integer
    <StructLayout(LayoutKind.Sequential)> _
    Friend Structure MMIOINFO
        Friend dwFlags As Integer
        Friend fccIOProc As Integer
        Friend pIOProc As Integer
        Friend wErrorRet As Integer
        Friend htask As Integer
        Friend cchBuffer As Integer
        Friend pchBuffer As String
        Friend pchNext As String
        Friend pchEndRead As String
        Friend pchEndWrite As String
        Friend lBufOffset As Integer
        Friend lDiskOffset As Integer
        Friend adwInfo1 As Integer
        Friend adwInfo2 As Integer
        Friend adwInfo3 As Integer
        Friend adwInfo4 As Integer
        Friend dwReserved1 As Integer
        Friend dwReserved2 As Integer
        Friend hmmio As Integer
    End Structure
    <StructLayout(LayoutKind.Sequential)> _
    Friend Structure MMCKINFO
        Friend ckid As Integer
        Friend ckSize As Integer
        Friend fccType As Integer
        Friend dwDataOffset As Integer
        Friend dwFlags As Integer
    End Structure
    <StructLayout(LayoutKind.Sequential)> _
    Friend Structure WAVEFORMAT
        Friend wFormatTag As Short
        Friend nChannels As Short
        Friend nSamplesPerSec As Integer
        Friend nAvgBytesPerSec As Integer
        Friend nBlockAlign As Short
        Friend wBitsPerSample As Short
        Friend cbSize As Short
    End Structure


    Private Const MMIO_READ As Integer = &H0
    Private Const MMIO_FINDRIFF As Integer = &H20
    Private Const MMIO_FINDCHUNK As Integer = &H10


    Dim mmckinfoParentIn As New MMCKINFO
    Dim mmckinfoSubchunkIn As New MMCKINFO
    Dim mmioinf As New MMIOINFO
    Friend ErrorMsg As String = ""

    Friend fileName As String
    Friend InputHandle As IntPtr = IntPtr.Zero
    Private m_FormatBuffer(49) As Byte
    Private mF As WAVEFORMAT ' waveformat structure

    Sub New(fn As String)
        fileName = fn
    End Sub
    Friend Function Do_mmioFormat() As String
        Dim rc As Integer
        InputHandle = mmioOpen(fileName, mmioinf, MMIO_READ)
        If InputHandle.ToInt64 = 0 Then
            ErrorMsg = "Error while opening the input file."
            Return "-1"
        End If
        'Check if this is a wave file
        mmckinfoParentIn.fccType = mmioStringToFOURCC("WAVE", 0)
        rc = mmioDescendParent(InputHandle, mmckinfoParentIn, 0, MMIO_FINDRIFF)
        If rc <> 0 Then
            CloseFile()
            ErrorMsg = "Invalid file type."
            Return "-1"
        End If
        'Get format info
        mmckinfoSubchunkIn.ckid = mmioStringToFOURCC("fmt", 0)
        rc = mmioDescend(InputHandle, mmckinfoSubchunkIn, mmckinfoParentIn, MMIO_FINDCHUNK)
        If rc <> 0 Then
            CloseFile()
            ErrorMsg = "Couldn't find format chunk."
            Return "-1"
        End If
        rc = mmioReadString(InputHandle, m_FormatBuffer, mmckinfoSubchunkIn.ckSize)
        If rc = -1 Then
            CloseFile()
            ErrorMsg = "Couldn't read from WAVE file."
            Return "-1"
        End If
        rc = mmioAscend(InputHandle, mmckinfoSubchunkIn, 0)
        CopyWaveFormatFromBytes(mF, m_FormatBuffer, Marshal.SizeOf(mF))
        CloseFile()
        Return CStr(mF.wFormatTag) & " " & CStr(mF.nChannels) & " " & CStr(mF.nSamplesPerSec) & " " & CStr(mF.nAvgBytesPerSec) & " " & CStr(mF.nBlockAlign) & " " & CStr(mF.wBitsPerSample) & " " & CStr(mF.cbSize)
    End Function
    Friend Sub CloseFile()
        mmioClose(InputHandle, 0)
    End Sub
End Class
