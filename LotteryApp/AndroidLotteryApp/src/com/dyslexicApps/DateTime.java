package com.dyslexicApps;

import java.util.Calendar;
import java.util.Date;

/* Some ideas to keep in mind. This class is a reproduction of the C# DateTime
 * class. Since java is retarded, January is 0, February is 1 etc... so this
 * class compensates for the illogical aspect of java.
 * Months:
 * -When returned to the user it is (_month + 1) to reflect a real world date month.
 * -When the month is being set by the user, it is set as _month = (value - 1) to 
 *  reflect java's perception of a logical date class.
 * */
public class DateTime 
{
	//This sets todays date by default
	public DateTime()
	{
		_baseCalendar = Calendar.getInstance();
		_baseDate = _baseCalendar.getTime();
		
		SetDateTimeComponents();
	}
	
	public DateTime(Date date)
	{
		_baseCalendar = Calendar.getInstance();
		_baseDate = date;
		_baseCalendar.setTime(_baseDate);
		
		SetDateTimeComponents();
	}
	
	public DateTime(int year, int month, int day)
	{
		_year  = year;
		_month = (month - 1);
		_day   = day;
        _hour    = 0;
        _minute  = 0;
        _second  = 0;
        _AMPM = 0;

        _baseCalendar = Calendar.getInstance();
		_baseCalendar.set(_year, _month, _day);
		_baseDate = _baseCalendar.getTime();
	}
	
	public static DateTime Today()
	{
		return new DateTime();
	}
	
	public String toString()
	{
		String strAMPM = "AM";

        if(_AMPM == 1)
            strAMPM = "PM";

        return pad2(_month + 1) + "/" + pad2(_day) + "/" + _year + " " + pad2(_hour) + ":" + pad2(_minute) + ":" + pad2(_second) + " " + strAMPM;
	}
	
	//This takes the base calendar object and gets the mm/dd/yyyy components
	private void SetDateTimeComponents()
	{
		_year  = _baseCalendar.get(Calendar.YEAR);
		_month = _baseCalendar.get(Calendar.MONTH);
		_day   = _baseCalendar.get(Calendar.DAY_OF_MONTH);
        _hour    = _baseCalendar.get(Calendar.HOUR);
        _minute  = _baseCalendar.get(Calendar.MINUTE);
        _second  = _baseCalendar.get(Calendar.SECOND);
        _AMPM = _baseCalendar.get(Calendar.AM_PM);
    }

    private String pad2(int number)
    {
        return String.format("%02d", number);
    }

	//Subtracting two DateTime objects returns a long in milliseconds since the epoch.
	public static long Subtract(DateTime thisDate, DateTime minus_thisDate)
	{
		return thisDate._baseCalendar.getTimeInMillis() - minus_thisDate._baseCalendar.getTimeInMillis();
	}
	
	private Date _baseDate;
	private Calendar _baseCalendar;
	
	private int _year;
	public void set_year(int _year) {
		this._year = _year;
	}

	public int get_year() {
		return _year;
	}

	private int _month;
	public void set_month(int _month) {
		this._month = _month - 1;
	}

	public int get_month() {
		return _month + 1;
	}
	
	private int _day;
	public void set_day(int _day) {
		this._day = _day;
	}
	
	public int get_day() {
		return _day;
	}

    private int _hour;
    private int _minute;
    private int _second;
    private int _AMPM;
}
