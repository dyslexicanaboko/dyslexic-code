using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

public class DummyTable
{
    public DummyTable()
    {

    }

    #region Properties
    private int m_entryID;
    public int EntryID
    {
        get { return m_entryID; }
        set { m_entryID = value; }
    }

    private string m_stringValue;
    public string StringValue
    {
        get { return m_stringValue; }
        set { m_stringValue = value; }
    }

    private int m_intValue;
    public int IntValue
    {
        get { return m_intValue; }
        set { m_intValue = value; }
    }

    private decimal m_decimalValue;
    public decimal DecimalValue
    {
        get { return m_decimalValue; }
        set { m_decimalValue = value; }
    }

    private decimal m_doubleValue;
    public decimal DoubleValue
    {
        get { return m_doubleValue; }
        set { m_doubleValue = value; }
    }

    private bool m_booleanValue;
    public bool BooleanValue
    {
        get { return m_booleanValue; }
        set { m_booleanValue = value; }
    }

    private int m_intByteValue;
    public int IntByteValue
    {
        get { return m_intByteValue; }
        set { m_intByteValue = value; }
    }

    private DateTime m_creationDate;
    public DateTime CreationDate
    {
        get { return m_creationDate; }
        set { m_creationDate = value; }
    }

    #endregion

    public static List<DummyTable> ObjectGeneration(DataTable dt)
    {
        DummyTable obj = null;
        List<DummyTable> lst = new List<DummyTable>();

        foreach (DataRow dr in dt.Rows)
        {
            obj = new DummyTable();
            obj.EntryID = Convert.ToInt32(dr["EntryID"]);
            obj.StringValue = Convert.ToString(dr["StringValue"]);
            obj.IntValue = Convert.ToInt32(dr["IntValue"]);
            obj.DecimalValue = Convert.ToDecimal(dr["DecimalValue"]);
            obj.DoubleValue = Convert.ToDecimal(dr["DoubleValue"]);
            obj.BooleanValue = Convert.ToBoolean(dr["BooleanValue"]);
            obj.IntByteValue = Convert.ToInt32(dr["IntByteValue"]);
            obj.CreationDate = Convert.ToDateTime(dr["CreationDate"]);

            lst.Add(obj);
        }

        return lst;
    }

    public void UpdateDummyTable(DummyTable obj)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("UPDATE DummyTable SET ");
        sb.Append("StringValue").Append(" = ").Append(AddString(m_stringValue)).Append(",");
        sb.Append("IntValue").Append(" = ").Append(m_intValue).Append(",");
        sb.Append("DecimalValue").Append(" = ").Append(m_decimalValue).Append(",");
        sb.Append("DoubleValue").Append(" = ").Append(m_doubleValue).Append(",");
        sb.Append("BooleanValue").Append(" = ").Append(m_booleanValue).Append(",");
        sb.Append("IntByteValue").Append(" = ").Append(m_intByteValue).Append(",");
        sb.Append("CreationDate").Append(" = ").Append(AddString(m_creationDate));
        sb.Append("WHERE EntryID = ").Append(obj.EntryID).Append(";");
    }

    public void InsertDummyTable(DummyTable obj)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("INSERT INTO DummyTable(StringValue, IntValue, DecimalValue, DoubleValue, BooleanValue, IntByteValue, CreationDa) VALUES ( ");
        sb.Append(AddString(m_stringValue)).Append(",");
        sb.Append(m_intValue).Append(",");
        sb.Append(m_decimalValue).Append(",");
        sb.Append(m_doubleValue).Append(",");
        sb.Append(m_booleanValue).Append(",");
        sb.Append(m_intByteValue).Append(",");
        sb.Append(AddString(m_creationDate));
        sb.Append(");");
    }

    public string AddString(String target)
    {
        return "'" + target + "'";
    }

    public string AddString(DateTime target)
    {
        return "'" + AddString(target.ToString("u")) + "'";
    }

}
