Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Text

Public Class DummyTable
    Public Sub New()

    End Sub

#Region "Properties"
    Private m_entryID As Integer
    Public Property EntryID As Integer
        Get
            Return m_entryID
        End Get
        Set(value As Integer)
            m_entryID = value
        End Set
    End Property

    Private m_stringValue As String
    Public Property StringValue As String
        Get
            Return m_stringValue
        End Get
        Set(value As String)
            m_stringValue = value
        End Set
    End Property

    Private m_intValue As Integer
    Public Property IntValue As Integer
        Get
            Return m_intValue
        End Get
        Set(value As Integer)
            m_intValue = value
        End Set
    End Property

    Private m_decimalValue As Decimal
    Public Property DecimalValue As Decimal
        Get
            Return m_decimalValue
        End Get
        Set(value As Decimal)
            m_decimalValue = value
        End Set
    End Property

    Private m_doubleValue As Decimal
    Public Property DoubleValue As Decimal
        Get
            Return m_doubleValue
        End Get
        Set(value As Decimal)
            m_doubleValue = value
        End Set
    End Property

    Private m_booleanValue As Boolean
    Public Property BooleanValue As Boolean
        Get
            Return m_booleanValue
        End Get
        Set(value As Boolean)
            m_booleanValue = value
        End Set
    End Property

    Private m_intByteValue As Integer
    Public Property IntByteValue As Integer
        Get
            Return m_intByteValue
        End Get
        Set(value As Integer)
            m_intByteValue = value
        End Set
    End Property

    Private m_creationDate As DateTime
    Public Property CreationDate As DateTime
        Get
            Return m_creationDate
        End Get
        Set(value As DateTime)
            m_creationDate = value
        End Set
    End Property

#End Region

    Public Shared Function ObjectGeneration(dt As DataTable) As List(Of DummyTable)
        Dim obj As DummyTable = Nothing
        Dim lst As List(Of DummyTable) = New List(Of DummyTable)()

        For Each dr As DataRow In dt.Rows
            obj = New DummyTable()
            obj.EntryID = Convert.ToInt32(dr("EntryID"))
            obj.StringValue = Convert.ToString(dr("StringValue"))
            obj.IntValue = Convert.ToInt32(dr("IntValue"))
            obj.DecimalValue = Convert.ToDecimal(dr("DecimalValue"))
            obj.DoubleValue = Convert.ToDecimal(dr("DoubleValue"))
            obj.BooleanValue = Convert.ToBoolean(dr("BooleanValue"))
            obj.IntByteValue = Convert.ToInt32(dr("IntByteValue"))
            obj.CreationDate = Convert.ToDateTime(dr("CreationDate"))

            lst.Add(obj)
        Next

        Return lst
    End Function

    Public Sub UpdateDummyTable(obj As DummyTable)
        Dim sb As StringBuilder = New StringBuilder()

        sb.Append("UPDATE DummyTable SET ")
        sb.Append("StringValue").Append(" = ").Append(AddString(m_stringValue)).Append(",")
        sb.Append("IntValue").Append(" = ").Append(m_intValue).Append(",")
        sb.Append("DecimalValue").Append(" = ").Append(m_decimalValue).Append(",")
        sb.Append("DoubleValue").Append(" = ").Append(m_doubleValue).Append(",")
        sb.Append("BooleanValue").Append(" = ").Append(m_booleanValue).Append(",")
        sb.Append("IntByteValue").Append(" = ").Append(m_intByteValue).Append(",")
        sb.Append("CreationDate").Append(" = ").Append(AddString(m_creationDate))
        sb.Append("WHERE EntryID = ").Append(obj.EntryID).Append(";")
    End Sub

    Public Sub InsertDummyTable(obj As DummyTable)
        Dim sb As StringBuilder = New StringBuilder()

        sb.Append("INSERT INTO DummyTable(StringValue, IntValue, DecimalValue, DoubleValue, BooleanValue, IntByteValue, CreationDa) VALUES ( ")
        sb.Append(AddString(m_stringValue)).Append(",")
        sb.Append(m_intValue).Append(",")
        sb.Append(m_decimalValue).Append(",")
        sb.Append(m_doubleValue).Append(",")
        sb.Append(m_booleanValue).Append(",")
        sb.Append(m_intByteValue).Append(",")
        sb.Append(AddString(m_creationDate))
        sb.Append(");")
    End Sub

    Public Function AddString(target As String) As String
        Return "'" + target + "'"
    End Function

    Public Function AddString(target As DateTime) As String
        Return "'" + AddString(target.ToString("u")) + "'"
    End Function

End Class
