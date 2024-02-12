using SharpDX.DirectInput;

namespace WpfApp.GameController
{
    internal class SharpDXDirectInputElecomGamePadState: GamePad
    {
        public void SetState(JoystickState state)
        {
            Changed.ButtonX = Current.ButtonX != state.Buttons[0];
            Current.ButtonX = state.Buttons[0];
            Changed.ButtonY = Current.ButtonY != state.Buttons[1];
            Current.ButtonY = state.Buttons[1];
            Changed.ButtonA = Current.ButtonA != state.Buttons[2];
            Current.ButtonA = state.Buttons[2];
            Changed.ButtonB = Current.ButtonB != state.Buttons[3];
            Current.ButtonB = state.Buttons[3];
            Changed.ButtonL1 = Current.ButtonL1 != state.Buttons[4];
            Current.ButtonL1 = state.Buttons[4];
            Changed.ButtonR1 = Current.ButtonR1 != state.Buttons[5];
            Current.ButtonR1 = state.Buttons[5];
            Changed.ButtonL2 = Current.ButtonL2 != state.Buttons[6];
            Current.ButtonL2 = state.Buttons[6];
            Changed.ButtonR2 = Current.ButtonR2 != state.Buttons[7];
            Current.ButtonR2 = state.Buttons[7];

            // POVの場合
            Changed.ButtonUp = Current.ButtonUp != (state.PointOfViewControllers[0] == 0);
            Current.ButtonUp = state.PointOfViewControllers[0] == 0;     // 上
            Changed.ButtonRight = Current.ButtonRight != (state.PointOfViewControllers[0] == 9000);
            Current.ButtonRight = state.PointOfViewControllers[0] == 9000;  // 右
            Changed.ButtonDown = Current.ButtonDown != (state.PointOfViewControllers[0] == 18000);
            Current.ButtonDown = state.PointOfViewControllers[0] == 18000; // 下
            Changed.ButtonLeft = Current.ButtonLeft != (state.PointOfViewControllers[0] == 27000);
            Current.ButtonLeft = state.PointOfViewControllers[0] == 27000; // 左

            // 左のアナログスティックはX軸とY軸
            Changed.LeftStickX = Current.LeftStickX - state.X;
            Current.LeftStickX = state.X;
            Changed.LeftStickY = Current.LeftStickY - state.Y;
            Current.LeftStickY = state.Y;
            // 右のアナログスティックはZ軸とZ軸の回転
            Changed.RightStickX = Current.RightStickX - state.Z;
            Current.RightStickX = state.Z;
            Changed.RightStickY = Current.RightStickY - state.RotationZ;
            Current.RightStickY = state.RotationZ;
        }
    }
}
