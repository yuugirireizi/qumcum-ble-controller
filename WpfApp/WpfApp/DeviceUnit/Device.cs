using WpfApp.GameController;

namespace WpfApp.DeviceUnit
{
    internal class Device
    {
        /// <summary>
        /// 右腕 ch1
        /// </summary>
        private Servo RightArm = new Servo(0, 900);

        /// <summary>
        /// 左腕 ch7
        /// </summary>
        private Servo LeftArm = new Servo(6, 900);

        /// <summary>
        /// 顔 ch3
        /// </summary>
        private Servo Face = new Servo(3, 900);

        /// <summary>
        /// 右足 ch2
        /// </summary>
        private Servo RightFoot = new Servo(1, 900);

        /// <summary>
        /// 右足首 ch3
        /// </summary>
        private Servo RightAnkle = new Servo(2, 900);

        /// <summary>
        /// 左足 ch6
        /// </summary>
        private Servo LeftFoot = new Servo(5, 900);

        /// <summary>
        /// 左足首 ch5
        /// </summary>
        private Servo LeftAnkle = new Servo(4, 900);

        /// <summary>
        /// LED
        /// </summary>
        private LED LED = new LED();

        public string ToMessage()
        {
            return LED.ToMessage() +
                RightArm.ToMessage() +
                LeftArm.ToMessage() +
                Face.ToMessage() +
                RightFoot.ToMessage() +
                RightAnkle.ToMessage() +
                LeftFoot.ToMessage() +
                LeftAnkle.ToMessage();
        }

        public void SetValue(GamePad state)
        {
            if (state.Current.ButtonB && state.Changed.ButtonB)
            {
                LED.Red = !LED.Red;
            }
        }
    }
}
