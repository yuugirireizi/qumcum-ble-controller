using System.Windows;
using System.Windows.Media;
using WpfApp.DeviceUnit;
using static System.Net.Mime.MediaTypeNames;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BluetoothLEManager bleManager = new BluetoothLEManager();
        private GameControllerManager? controller;
        private Device device = new Device();

        /// <summary>
        /// デバイスに定期的にメッセージを送信する。
        /// Windowクラスと同期をとるため、あえてメインスレッドでやる。
        /// </summary>
        private System.Timers.Timer sendMessageTimer = new System.Timers.Timer();

        public MainWindow()
        {
            InitializeComponent();

            try
            {
                controller = new GameControllerManager();
                CompositionTarget.Rendering += (sender, e) =>
                {
                    if (controller == null)
                    {
                        return;
                    }
                    var gamePad = controller.HandleInput();
                    device.SetValue(gamePad);
                    ControllerCaptureTextBlock.Text = gamePad.Current.ToString();
                    SendMessageTextBlock.Text = device.ToMessage();
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Close();
            }

            // SendMessageTimerの初期化
            sendMessageTimer.Interval = 1000;
            sendMessageTimer.Elapsed += async (sender, e) => {
                var message = device.ToMessage();
                var result = await bleManager.SendAsync(message);
                if (result == 100)
                {
                    // NOP
                    return;
                }
            };
        }

        private async void ConnectButtonClick(object sender, RoutedEventArgs e)
        {
            var result = await bleManager.Connect();
            if (result == 100)
            {
                // 実行中なので無視
                // NOP
                return;
            }
            MessageBox.Show("Connect result: " + result.ToString());
        }

        private void SendStartClick(object sender, RoutedEventArgs e)
        {
            sendMessageTimer.Start();
        }

        private void SendStopClick(object sender, RoutedEventArgs e)
        {
            sendMessageTimer.Stop();
        }

        private void DisconnectButtonClick(object sender, RoutedEventArgs e)
        {
            bleManager.Disconnect();
        }
    }
}