using SharpDX.DirectInput;
using System.Text;
using WpfApp.GameController;

namespace WpfApp
{
    internal class GameControllerManager
    {

        private DirectInput directInput;
        private Joystick joystick;
        private bool disposedValue;
        private SharpDXDirectInputElecomGamePadState gamePad = new SharpDXDirectInputElecomGamePadState();

        public GameControllerManager()
        {
            directInput = new DirectInput();
            var devices = directInput.GetDevices(DeviceType.Gamepad, DeviceEnumerationFlags.AllDevices);

            foreach (var device in devices)
            {
                joystick = new Joystick(directInput, device.InstanceGuid);
                break;
            }
            if (joystick == null)
            {
                throw new Exception("ゲームパッドが見つかりません。");
            }

            // バッファサイズを指定
            joystick.Properties.BufferSize = 128;

            // ジョイスティックの軸の最小値と最大値を -1000～+1000に設定
            //foreach (DeviceObjectInstance deviceObject in joystick.GetObjects())
            //{
            //    switch (deviceObject.ObjectId.Flags)
            //    {
            //        // 絶対軸または相対軸
            //        case DeviceObjectTypeFlags.Axis:
            //        case DeviceObjectTypeFlags.AbsoluteAxis:
            //        case DeviceObjectTypeFlags.RelativeAxis:
            //            var ir = joystick.GetObjectPropertiesById(deviceObject.ObjectId);
            //            if (ir != null)
            //            {
            //                try
            //                {
            //                    ir.Range = new InputRange(-1000, 1000);
            //                }
            //                catch (Exception) { }
            //            }
            //            break;
            //    }
            //}

            joystick.Acquire();
        }

        public GamePad HandleInput()
        {
            joystick.Poll();
            var state = joystick.GetCurrentState();
            gamePad.SetState(state);
            return gamePad;
            //StringBuilder inputTextBuilder = new StringBuilder();

            //// ゲームパッドの各ボタンの状態を表示
            //for (int i = 0; i < state.Buttons.Length; i++)
            //{
            //    Console.WriteLine($"Button {i}: {state.Buttons[i]}");
            //    if (state.Buttons[i])
            //    {
            //        inputTextBuilder.AppendLine($"Button {i} Pressed");
            //    }
            //}

            //// スティックの状態を表示
            //Console.WriteLine($"X: {state.X}, Y: {state.Y}");
            //inputTextBuilder.AppendLine($"X: {state.X}, Y: {state.Y}");
            //return inputTextBuilder.ToString();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: マネージド状態を破棄します (マネージド オブジェクト)
                    joystick?.Dispose();
                    directInput?.Dispose();
                }

                // TODO: アンマネージド リソース (アンマネージド オブジェクト) を解放し、ファイナライザーをオーバーライドします
                // TODO: 大きなフィールドを null に設定します
                disposedValue = true;
            }
        }

        // // TODO: 'Dispose(bool disposing)' にアンマネージド リソースを解放するコードが含まれる場合にのみ、ファイナライザーをオーバーライドします
        // ~GameController()
        // {
        //     // このコードを変更しないでください。クリーンアップ コードを 'Dispose(bool disposing)' メソッドに記述します
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // このコードを変更しないでください。クリーンアップ コードを 'Dispose(bool disposing)' メソッドに記述します
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
