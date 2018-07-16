using System;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using GHIElectronics.NETMF.FEZ;

public class LedTests
{
    public static void FlashingLed()
    {
        OutputPort MyLED = new OutputPort((Cpu.Pin)FEZ_Pin.Digital.LED, true);

        while (true)
        {
            MyLED.Write(false);
            
            Thread.Sleep(300);
            
            MyLED.Write(true);
            
            Thread.Sleep(300);
        }
    }

    public static void TurnOffLedWithButton()
    { 
        OutputPort MyLED = new OutputPort((Cpu.Pin)FEZ_Pin.Digital.LED, true);

        InputPort MyButton = new InputPort((Cpu.Pin)FEZ_Pin.Digital.LDR, false, Port.ResistorMode.PullUp);

        while (true)
        {
            MyLED.Write(MyButton.Read());

            Thread.Sleep(10);
        }
    }
}