using System.Text;

namespace WpfApp.GameController
{
    internal class ControllerState
    {
        public bool ButtonL1 { get; set; } = false;
        public bool ButtonL2 { get; set; } = false;
        public bool ButtonR1 { get; set; } = false;
        public bool ButtonR2 { get; set; } = false;
        /// <summary>
        /// XYAB buttons.Left
        /// </summary>
        public bool ButtonX { get; set; } = false;
        /// <summary>
        /// XYAB buttons.Up
        /// </summary>
        public bool ButtonY { get; set; } = false;
        /// <summary>
        /// XYAB buttons.Down
        /// </summary>
        public bool ButtonA { get; set; } = false;
        /// <summary>
        /// XYAB buttons.Right
        /// </summary>
        public bool ButtonB { get; set; } = false;

        public bool ButtonUp { get; set; } = false;
        public bool ButtonDown { get; set; } = false;
        public bool ButtonLeft { get; set; } = false;
        public bool ButtonRight { get; set; } = false;

        public int LeftStickX { get; set; } = 0;
        public int LeftStickY { get; set; } = 0;
        public int RightStickX { get; set; } = 0;
        public int RightStickY { get; set; } = 0;

        public override string ToString()
        {
            var text = new StringBuilder();
            if (ButtonL1)
            {
                text.AppendLine("Button L1 Pressed.");
            }
            if (ButtonR1)
            {
                text.AppendLine("Button R1 Pressed.");
            }
            if (ButtonL2)
            {
                text.AppendLine("Button L2 Pressed.");
            }
            if (ButtonR2)
            {
                text.AppendLine("Button R2 Pressed.");
            }
            if (ButtonX)
            {
                text.AppendLine("Button X Pressed.");
            }
            if (ButtonY)
            {
                text.AppendLine("Button Y Pressed.");
            }
            if (ButtonA)
            {
                text.AppendLine("Button A Pressed.");
            }
            if (ButtonB)
            {
                text.AppendLine("Button B Pressed.");
            }
            if (ButtonLeft)
            {
                text.AppendLine("Button Left Pressed.");
            }
            if (ButtonRight)
            {
                text.AppendLine("Button Right Pressed.");
            }
            if (ButtonUp)
            {
                text.AppendLine("Button Up Pressed.");
            }
            if (ButtonDown)
            {
                text.AppendLine("Button Down Pressed.");
            }
            text.AppendLine($"Left X: {LeftStickX}, Y: {LeftStickY}");
            text.AppendLine($"Right X: {RightStickX}, Y: {RightStickY}");

            return text.ToString();
        }
    }
}
