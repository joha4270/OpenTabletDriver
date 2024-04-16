using OpenTabletDriver.Tablet;

namespace OpenTabletDriver.Configurations.Parsers.XP_Pen
{
    public struct XP_PenAuxReport : IAuxReport, IAbsoluteWheelReport
    {
        private const int WheelSteps = 90;
        private static int WheelPositionCounter = 0;
        public XP_PenAuxReport(byte[] report, int index = 2)
        {
            Raw = report;

            AuxButtons = new bool[]
            {
                report[index].IsBitSet(0),
                report[index].IsBitSet(1),
                report[index].IsBitSet(2),
                report[index].IsBitSet(3),
                report[index].IsBitSet(4),
                report[index].IsBitSet(5),
                report[index].IsBitSet(6),
                report[index].IsBitSet(7),
                report[index + 1].IsBitSet(0),
                report[index + 1].IsBitSet(1),
            };


            var wheelData = report[7];

                if(wheelData == 2)
                {
                    WheelPositionCounter += 1;
                }
                else if(wheelData == 1)
                {
                    WheelPositionCounter -= 1;
                }

                if(WheelPositionCounter < 0)
                {
                    WheelPositionCounter = WheelSteps - 1;
                }
                WheelPositionCounter %= WheelSteps;
                WheelPosition = (uint?)WheelPositionCounter;
            }
        }

        public bool[] AuxButtons { set; get; }
        public byte[] Raw { set; get; }
        public uint? WheelPosition { get; set; }
    }
}
