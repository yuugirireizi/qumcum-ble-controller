namespace WpfApp.GameController
{
    internal class GamePad
    {
        public ControllerState Current { get; protected set; } = new ControllerState();
        public ControllerState Changed { get; protected set; } = new ControllerState();
    }
}
