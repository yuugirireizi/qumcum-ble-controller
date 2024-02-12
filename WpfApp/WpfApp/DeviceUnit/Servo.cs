namespace WpfApp.DeviceUnit
{
    internal class Servo
    {
        /// <summary>
        /// モータのチャンネル
        /// </summary>
        public int Channel { get; set; } = 0;

        /// <summary>
        /// 角度
        /// 1/10度単位
        /// </summary>
        public int Angle { get; set; } = 0;

        public Servo(int ch, int angle) {
            Channel = ch;
            Angle = angle;
        }

        public string ToMessage()
        {
            return "xs" + 
                /*chを1byte16進数文字列*/ Channel.ToString("X2") +
                /*angleを2byte16進数文字列*/ Angle.ToString("X4");
        }
    }
}
