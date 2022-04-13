Imports System.Data.SqlClient
Imports System.Runtime.InteropServices
Imports System.Text
'Imports SAPFunctionsOCX


Module Module1

    Public IPNUM As String
    Public CLIEN As String
    Public SYSNM As String
    Public SYSTM As String
    Public USNAM As String
    Public PASSW As String
    Public DBNAME As String

    Public Conn As SqlConnection
    Public HostConn As SqlConnection
    Public DA As SqlDataAdapter
    Public DS As DataSet
    Public CMD As SqlCommand
    Public DT As DataTable
    Public Query As String
    Public CARI As String
    Public CUSTNUM As String
    Public DS2 As DataSet
    Public DA2 As SqlDataAdapter

    Public COCODE As String
    Public CONAME As String
    Public COADDR As String
    Public PLANT As String
    Public SLOC As String
    Public PGROUP As String
    Public DOCTYPE As String

    Public LogonControl As Object
    Public SAPConn As Object
    Public BAPI As Object
    Public SAPConStatus As Boolean
    Public SaveStatus As Boolean
    Public ConnectStatus As Boolean
    Public GetPO As String
    Public NoOrder As Long = 0
    Public CurMonPeriode As String
    Public CurYearPeriode As String
    Public PrevMonPeriode As String
    Public PrevYearPeriode As String
    Public SAPCon As Object

    Public BATCH As String
    Public MATNUM As String
    Public SAPDOC As String
    Public DOC_NO As String
    Public FilterOkay As Boolean

    Public TAXRATE As Double = 0



    <DllImport("kernel32")>
    Private Function GetPrivateProfileString(ByVal section As String, ByVal key As String, ByVal def As String, ByVal retVal As StringBuilder, ByVal size As Integer, ByVal filePath As String) As Integer
    End Function

    Public Function GetIniValue(section As String, key As String, filename As String, Optional defaultValue As String = "") As String
        Dim sb As New StringBuilder(500)
        If GetPrivateProfileString(section, key, defaultValue, sb, sb.Capacity, filename) > 0 Then
            Return sb.ToString
        Else
            Return defaultValue
        End If
    End Function

    Public Sub LogToSQLServer()

        Dim IPNUM As String = GetIniValue("SQL", "IPN", My.Application.Info.DirectoryPath & "\SysInfo.ini")
        Dim DBNAME As String = GetIniValue("SQL", "DBN", My.Application.Info.DirectoryPath & "\SysInfo.ini")

        Conn = New SqlConnection

        Conn.ConnectionString = "Data Source=" & IPNUM & "\SQLEXPRESS;" & _
                                "Initial Catalog=" & DBNAME & ";" & _
                                "User=sa;Password=tki3c134"

        Try
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
                'MsgBox("Koneksi Berhasil !")
            End If
        Catch ex As Exception
            MsgBox("Koneksi Gagal !" & Err.Description)
        End Try

    End Sub

    Public Sub AutoNumberRowsForGridView(ByVal dataGridView As DataGridView)

        If dataGridView IsNot Nothing Then

            dataGridView.RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            dataGridView.RowHeadersWidth = 50

            Dim count As Integer = 0

            While (count <= (dataGridView.Rows.Count - 1))

                'If Trim(dataGridView.Rows(count).Cells(0).Value.ToString) <> "" Then

                dataGridView.Rows(count).HeaderCell.Value = String.Format((count + 1).ToString(), "0")
                count += 1

                'End If

            End While

        End If

    End Sub

    Public Function TAX_RATE(ByRef EffDate As Date) As Decimal

        Dim DS_TAX As DataSet
        Dim DA_TAX As SqlDataAdapter

        TAXRATE = 0

        Query = "SELECT TOP 1 * FROM [TAXRATE] WHERE [EffDate] <= '" & Format(EffDate, "yyyy-MM-dd") & "' ORDER BY [EffDate] DESC "

        DS_TAX = New DataSet
        DA_TAX = New SqlDataAdapter(Query, Conn)
        DA_TAX.Fill(DS_TAX, "TAX")

        If DS_TAX.Tables("TAX").Rows.Count = 1 Then
            TAXRATE = DS_TAX.Tables("TAX").Rows(0)("RATE")
        End If

        TAX_RATE = TAXRATE

    End Function

    Public Sub LOCAL_CONNECT()

        Dim IPNUM As String = GetIniValue("LOCAL", "IPN", My.Application.Info.DirectoryPath & "\SysInfo.ini")

        If InStr(IPNUM, "192.168.3.17") > 0 Then
            PASSW = "tki3c134"
        ElseIf InStr(IPNUM, "192.168.3.143") > 0 Then
            PASSW = "TTIadmin777!"
        Else
            PASSW = ""
        End If

        ConnectStatus = False

        Conn = New SqlConnection

        Conn.ConnectionString = "Data Source=" & IPNUM & ";" & _
                                "Initial Catalog=SIASAT_" & DBNAME & ";" & _
                                "User=sa;Password=" & PASSW

        Try
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
                ConnectStatus = True
            End If
        Catch ex As Exception
            MsgBox("Connection fail !" & Err.Description)
            ConnectStatus = False
        End Try

        MDIMain.statuslbl4.Text = IPNUM

    End Sub

    Public Sub HOST_CONNECT()

        Dim IPNUM As String = GetIniValue("HOST", "IPN", My.Application.Info.DirectoryPath & "\SysInfo.ini")
        Dim DBNAME As String = GetIniValue("HOST", "DBN", My.Application.Info.DirectoryPath & "\SysInfo.ini")


        ConnectStatus = False

        HostConn = New SqlConnection

        HostConn.ConnectionString = "Data Source=" & IPNUM & ";" & _
                                    "Initial Catalog=" & DBNAME & ";" & _
                                    "User=sa;Password=tki3c134"

        Try
            If HostConn.State = ConnectionState.Closed Then
                HostConn.Open()
                ConnectStatus = True
            End If
        Catch ex As Exception
            MsgBox("Koneksi Gagal !" & Err.Description)
        End Try

    End Sub

    Public Sub LogToSAP(ByRef LogonViewOff As Boolean)

        Dim retcd As Boolean = False

        IPNUM = GetIniValue("SAP", "IPN", My.Application.Info.DirectoryPath & "\SysInfo.ini")
        CLIEN = GetIniValue("SAP", "CLN", My.Application.Info.DirectoryPath & "\SysInfo.ini")
        USNAM = GetIniValue("SAP", "USN", My.Application.Info.DirectoryPath & "\SysInfo.ini")
        SYSTM = GetIniValue("SAP", "SYS", My.Application.Info.DirectoryPath & "\SysInfo.ini")
        SYSNM = GetIniValue("SAP", "NUM", My.Application.Info.DirectoryPath & "\SysInfo.ini")
        PASSW = GetIniValue("SAP", "PWD", My.Application.Info.DirectoryPath & "\SysInfo.ini")


        LogonControl = CreateObject("SAP.LogonControl.1")

        SAPConn = LogonControl.NewConnection

        SAPConn.Language = "EN"
        SAPConn.ApplicationServer = IPNUM
        SAPConn.Client = CLIEN
        SAPConn.SystemNumber = SYSNM
        SAPConn.System = SYSTM
        SAPConn.User = USNAM
        SAPConn.Password = PASSW


        retcd = SAPConn.Logon(0, LogonViewOff)

        If retcd = True Then
            USNAM = SAPConn.User
            'PASSW = SAPConn.Password

            SAPConStatus = True
        Else
            'MessageBox.Show(retcd)
            SAPConStatus = False
        End If

    End Sub


    Public Sub Koneksi_SAP_BAPI()

        BAPI = CreateObject("SAP.BAPI.1")
        SAPConn = BAPI.Connection

        SAPConn.ApplicationServer = "192.168.7.86"
        SAPConn.Client = "777"
        SAPConn.SystemNumber = "00"
        SAPConn.User = "OPS.TEAM.001"
        SAPConn.Password = "HELPDESK"
        SAPConn.Language = "EN"

        If SAPConn.Logon(0, True) = True Then
            MessageBox.Show("Connection successful !")
        Else
            MessageBox.Show("Connection fail !")
        End If


    End Sub

    Public Sub Koneksi_SAP_RFC()

        SAPCon = CreateObject("SAP.Functions")
        SAPCon.Connection.ApplicationServer = "192.168.3.157"
        SAPCon.Connection.client = "777"
        SAPCon.Connection.SystemNumber = "00"
        SAPCon.Connection.User = "DUMMY.USER"
        SAPCon.Connection.Password = "KERAKERA"
        SAPCon.Connection.language = "EN"

        If SAPCon.Connection.Logon(0, True) = True Then
            'MessageBox.Show("Connection to SAP successful !", "Info")
            SAPConStatus = True
        Else
            ' MessageBox.Show("Connection to SAP fail !")
            SAPConStatus = False
        End If

    End Sub

    Public Sub GET_PERIOD()

        Dim SAP_RFC_Func, SAPRFC_Func As Object
        Dim oCOMP_CODE As Object
        Dim oZPERIODE As Object
        Dim result As Boolean

        'Call LogToSAP(True)

        SAP_RFC_Func = CreateObject("SAP.Functions")
        SAP_RFC_Func.Connection = SAPConn

        SAPRFC_Func = SAP_RFC_Func.Add("ZS4TR_GET_FI_PERIODE")

        oCOMP_CODE = SAPRFC_Func.EXPORTS("COMP_CODE")
        oCOMP_CODE.VALUE = PLANT

        oZPERIODE = SAPRFC_Func.Tables("ZPERIODE")

        ' Bersihkan dulu
        oZPERIODE.Rows.RemoveAll()

        result = SAPRFC_Func.Call

        If result = True Then

            If oZPERIODE.RowCount > 0 Then

                CurMonPeriode = Right(oZPERIODE.Rows(1)("TOPE1"), 2)
                CurYearPeriode = oZPERIODE.Rows(1)("TOYE1")
                PrevMonPeriode = Right(oZPERIODE.Rows(1)("FRPE1"), 2)
                PrevYearPeriode = oZPERIODE.Rows(1)("FRYE1")

            End If

        Else
            MsgBox("Call to ZCM_GET_PERIODE !", , "Error")
        End If

        SAPRFC_Func = Nothing
        SAP_RFC_Func = Nothing
        oCOMP_CODE = Nothing
        oZPERIODE = Nothing

    End Sub

    Public Sub GET_MM_PERIOD()

        Dim SAP_RFC_Func, SAPRFC_Func As Object
        Dim oCOMP_CODE As Object
        Dim oZPERIODE As Object
        Dim result As Boolean

        'Call LogToSAP(True)

        SAP_RFC_Func = CreateObject("SAP.Functions")
        SAP_RFC_Func.Connection = SAPConn

        SAPRFC_Func = SAP_RFC_Func.Add("ZCM_GET_PERIODE")

        oCOMP_CODE = SAPRFC_Func.EXPORTS("COMP_CODE")
        oCOMP_CODE.VALUE = PLANT

        oZPERIODE = SAPRFC_Func.Tables("ZPERIODE")

        ' Bersihkan dulu
        oZPERIODE.Rows.RemoveAll()

        result = SAPRFC_Func.Call

        If result = True Then

            If oZPERIODE.rowcount > 0 Then

                CurMonPeriode = oZPERIODE.Rows(1)("LFMON")
                CurYearPeriode = oZPERIODE.Rows(1)("LFGJA")
                PrevMonPeriode = oZPERIODE.Rows(1)("VMMON")
                PrevYearPeriode = oZPERIODE.Rows(1)("VMGJA")

            End If

        Else
            MsgBox("Call to ZCM_GET_PERIODE !", , "Error")
        End If

        SAPRFC_Func = Nothing
        SAP_RFC_Func = Nothing
        oCOMP_CODE = Nothing
        oZPERIODE = Nothing

    End Sub

    Function GetReleasedStatusPO(ByRef ProdOrderNo As String) As Boolean

        Dim SAP_RFC_Func, SAPRFC_Func As Object
        Dim oPRD_ORD As Object
        Dim oHASIL As Object
        Dim Result As Boolean

        'Call LogToSAP(True)

        SAP_RFC_Func = CreateObject("SAP.Functions")
        SAP_RFC_Func.Connection = SAPConn

        SAPRFC_Func = SAP_RFC_Func.Add("ZCM_GET_PRDORD_STAT")

        oPRD_ORD = SAPRFC_Func.EXPORTS("PRD_ORD")
        oHASIL = SAPRFC_Func.IMPORTS("HASIL")

        oPRD_ORD.VALUE = ProdOrderNo

        Result = SAPRFC_Func.Call

        If Result = True Then
            If oHASIL.VALUE = "I0002" Then
                'MsgBox("Order Released", MsgBoxStyle.Information)
                GetReleasedStatusPO = True
            Else
                'MsgBox("Order Not Released", MsgBoxStyle.Exclamation)
                GetReleasedStatusPO = False
            End If
        Else
            MsgBox("Call to ZCM_GET_PRDORD_STAT !", , "Error")
        End If

        SAP_RFC_Func = Nothing
        SAPRFC_Func = Nothing
        oPRD_ORD = Nothing
        oHASIL = Nothing

        Return GetReleasedStatusPO

    End Function

    Function VALNUM(ByRef CharNum As String) As Single
        VALNUM = Val(Replace(Replace(CharNum, ",", ""), ".", ""))
    End Function

    Function VALCHAR(ByRef CharNum As String) As String
        VALCHAR = Val(Replace(CharNum, ",", "."))
    End Function

    Function PETIK(ByRef Kata As String) As String
        PETIK = Replace(Kata, "'", "''")
    End Function

    Function DESIMAL(ByRef Nilai As String) As String
        If InStr(Nilai, ",") = 0 And InStr(Nilai, ".") = 0 Then
            DESIMAL = Nilai & ".00"
        Else
            DESIMAL = Replace(Replace(Nilai, ".", ""), ",", ".")
        End If
    End Function

    Function DATECHAR(ByRef TrxDate As Date) As String
        DATECHAR = Format(TrxDate, "yyyy-MM-dd")
    End Function

    Function CHARDATE(ByRef TrxDate As String) As String
        CHARDATE = Mid(TrxDate, 7, 4) & "-" & Mid(TrxDate, 4, 2) & "-" & Mid(TrxDate, 1, 2)
    End Function

    Public Function SLOC_INFO(ByRef StorLoc As String) As String

        'Call LogToSAP(True)

        Dim objRfcFunc, objQueryTab As Object
        Dim objOptTab, objFldTab, objDatTab As Object
        Dim objDatRec As Object
        Dim SAP_RFC As Object

        Dim Lokasi As String = ""

        SAP_RFC = CreateObject("SAP.Functions")
        SAP_RFC.Connection = SAPConn

        objRfcFunc = SAP_RFC.Add("RFC_READ_TABLE")

        objQueryTab = objRfcFunc.Exports("QUERY_TABLE")
        objQueryTab.Value = "T001L"  ' Storage locations

        objOptTab = objRfcFunc.Tables("OPTIONS")
        objFldTab = objRfcFunc.Tables("FIELDS")
        objDatTab = objRfcFunc.Tables("DATA")

        objOptTab.FreeTable()
        objOptTab.Rows.Add()
        objOptTab(objOptTab.rowcount, "TEXT") = "WERKS = '" + PLANT + "' AND "
        objOptTab.Rows.Add()
        objOptTab(objOptTab.rowcount, "TEXT") = "LGORT = '" + StorLoc + "' "

        objFldTab.FreeTable()
        objFldTab.Rows.Add()
        objFldTab(objFldTab.rowcount, "FIELDNAME") = "LGOBE"    ' Desc

        If objRfcFunc.Call = True Then
            If objDatTab.RowCount > 0 Then
                For Each objDatRec In objDatTab.Rows
                    Lokasi = Mid(objDatTab.Rows(1)("WA"), objFldTab(1, "OFFSET") + 1, objFldTab(1, "LENGTH"))
                Next
            End If
        End If

        SLOC_INFO = Lokasi

        objRfcFunc = Nothing
        objQueryTab = Nothing
        objOptTab = Nothing
        objFldTab = Nothing
        objDatTab = Nothing
        SAP_RFC = Nothing

        Return SLOC_INFO

    End Function


    Public Function TglAkhirBulan(ByVal Tanggal1 As Date) As Date

        Dim Tanggal2 As New Date(Tanggal1.Year, Tanggal1.Month, 1)

        Tanggal2 = Tanggal2.AddMonths(1)
        Tanggal2 = Tanggal2.AddDays(-(Tanggal2.Day))
        Return Tanggal2

    End Function


End Module

