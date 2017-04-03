package com.dyslexicApps;

import java.util.*;

/**
 * Created with IntelliJ IDEA.
 * User: eli
 * Date: 7/22/12
 * Time: 5:36 PM
 * To change this template use File | Settings | File Templates.
 */
public class LotteryNumbers
{
    public LotteryNumbers()
    {
        for (int i = 0; i < 6; i++)
            m_numbers.add(0);
    }

    public LotteryNumbers(String lotteryString)
    {
        ParseString(lotteryString);
    }

    private List<Integer> m_numbers = new ArrayList<Integer>();
    protected List<Integer> Numbers()
    {
         return m_numbers;
    }

    public int getNumber1() { return m_numbers.get(0); }
    public void setNumber1(int value) { m_numbers.set(0, Math.abs(value)); }

    public int getNumber2() { return m_numbers.get(1); }
    public void setNumber2(int value) { m_numbers.set(1, Math.abs(value)); }

    public int getNumber3() { return m_numbers.get(2); }
    public void setNumber3(int value) { m_numbers.set(2, Math.abs(value)); }

    public int getNumber4() { return m_numbers.get(3); }
    public void setNumber4(int value) { m_numbers.set(3, Math.abs(value)); }

    public int getNumber5() { return m_numbers.get(4); }
    public void setNumber5(int value) { m_numbers.set(4, Math.abs(value)); }

    public int getNumber6() { return m_numbers.get(5); }
    public void setNumber6(int value) { m_numbers.set(5, Math.abs(value)); }

    public String Jackpot;
    public String Status;

    public void setNumber(int index, String numberValue)
    {
        m_numbers.set(index, ConvertToInt(numberValue));
    }

    public void setNumber(int index, int numberValue)
    {
        m_numbers.set(index, numberValue);
    }

    private void ParseString(String lotteryNumbers)
    {
        String[] arr = lotteryNumbers.split("-");

        if (m_numbers.size() > 0)
            m_numbers.clear();

        for(int i = 0; i < arr.length; i++)
            m_numbers.add(ConvertToInt(arr[i]));

        Collections.sort(m_numbers);
    }

    public static LotteryNumbers ParseLotteryNumberString(String lotteryNumbers)
    {
        return new LotteryNumbers(lotteryNumbers);
    }

    public static Map<Integer, Integer> crossExamineSets(LotteryNumbers set1, LotteryNumbers set2)
    {
        Map<Integer, Integer> dict = new HashMap<Integer, Integer>();

        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                if (set1.Numbers().get(i) == set2.Numbers().get(j))
                    dict.put(i, j);
            }
        }

        return dict;
    }

    public boolean hasDuplicateValues()
    {
        List<Integer> lst = new ArrayList<Integer>();

        int temp = 0;

        for(int i = 0; i < m_numbers.size(); i++)
        {
            temp = m_numbers.get(i);

            if(temp != 0 && !lst.contains(temp))
                lst.add(temp);
        }

        //Compare the count of the list versus a distinct list (not including zero)
        return m_numbers.size() != lst.size();
    }

    public int sum()
    {
        //Not as efficient as the C# version
        int sum = 0;

        for(int i : m_numbers)
            sum += i;

        return sum;
    }

    @Override
    public String toString()
    {
        StringBuilder sb = new StringBuilder();
    
        for(int i = 0; i < m_numbers.size(); i++)
            sb.append(String.format("%02d", m_numbers.get(i))).append("-");

        sb.deleteCharAt(sb.length() - 1); //Remove trailing -
    
        return sb.toString();
    }

    private static int ConvertToInt(String strInt)
    {
        int intValue = 0;

        try
        {
            intValue = Integer.parseInt(strInt);
        }
        catch(Exception ex)
        {
            intValue = 0;
        }

        return intValue;
    }
}
