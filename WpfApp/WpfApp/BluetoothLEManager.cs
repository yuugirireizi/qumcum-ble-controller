using System.Text;
using Windows.Devices.Bluetooth;
using Windows.Devices.Bluetooth.Advertisement;
using Windows.Devices.Bluetooth.GenericAttributeProfile;


namespace WpfApp
{
    public class BluetoothLEManager
    {
        private const string DeviceName = "MyESP32";
        private const string ServiceUUID = "4fafc201-1fb5-459e-8fcc-c5c9c331914b";
        private const string CharacteristicUUID = "beb5483e-36e1-4688-b7f5-ea07361b26a8";
        
        // 接続していない時はnull
        private BluetoothLEDevice? bleDevice;
        private GattCharacteristic? characteristic;
        private bool isConnecting = false;
        private bool isSending = false;

        public async Task<int> Connect()
        {
            if (isConnecting)
            {
                // 実行中
                return 100;
            }

            isConnecting = true;

            // コールバック関数内で接続するので、接続したタイミングで完了するタスクを呼び出し元に返す
            var tcs = new TaskCompletionSource<int>();

            // Bluetooth接続を探す監視オブジェクト
            var watcher = new BluetoothLEAdvertisementWatcher();

            var count = 0;
            watcher.Received += async (sender, args) => {
                if (watcher.Status != BluetoothLEAdvertisementWatcherStatus.Started)
                {
                    return;
                }

                if (args.Advertisement.LocalName.ToString() == DeviceName)
                {
                    watcher.Stop();
                    var device = await BluetoothLEDevice.FromBluetoothAddressAsync(args.BluetoothAddress);
                    if (device == null)
                    {
                        // 接続できない
                        tcs.SetResult(-10);
                        return;
                    }

                    bleDevice = device;
                    // Console.WriteLine("デバイスに接続しました。");

                    // サービスを検索
                    var serviceResult = await bleDevice.GetGattServicesForUuidAsync(new Guid(ServiceUUID));
                    var service = serviceResult.Services.FirstOrDefault();
                    if (service == null)
                    {
                        // 指定されたサービスが見つかりません。
                        tcs.SetResult(-20);
                        return;
                    }

                    // Console.WriteLine("サービスを見つけました。");

                    // サービスからキャラクタリスティックを取得
                    var characteristicResult = await service.GetCharacteristicsForUuidAsync(new Guid(CharacteristicUUID));
                    characteristic = characteristicResult.Characteristics.FirstOrDefault();

                    if (characteristic == null)
                    {
                        // Console.WriteLine("指定されたキャラクタリスティックが見つかりません。");
                        tcs.SetResult(-30);
                        return;
                    }

                    // Console.WriteLine("キャラクタリスティックを見つけました。");

                    // 接続できたら
                    tcs.SetResult(0);
                }

                count++;
                // 接続できない(タイムアウトの代用)
                if (count >= 10)
                {
                    watcher.Stop();
                    tcs.SetResult(-100);
                }
            };
            watcher.ScanningMode = BluetoothLEScanningMode.Active;
            watcher.Start();

            var result = await tcs.Task;
            isConnecting = false;
            return result;
        }

        public async Task<int> SendAsync(string data)
        {
            if (isSending)
            {
                return -100;
            }
            isSending = true;
            var resultValue = 0;

            if (characteristic != null)
            {
                // 文字列をバイト配列に変換して書き込む
                var bytes = Encoding.UTF8.GetBytes(data);
                var writer = new Windows.Storage.Streams.DataWriter();
                writer.WriteBytes(bytes);

                // 書き込みを実行
                var result = await characteristic.WriteValueAsync(writer.DetachBuffer());
                if (result != GattCommunicationStatus.Success)
                {
                    // Console.WriteLine("書き込みエラーが発生しました。");
                    resultValue = -20;
                }
                else
                {
                    // Console.WriteLine("文字列を送信しました。");
                    resultValue = data.Length;
                }
            }
            else
            {
                // Console.WriteLine("キャラクタリスティックが初期化されていません。");
                resultValue = -10;
            }

            isSending = false;
            return resultValue;
        }

        /// <summary>
        /// BLE接続を切断する。
        /// 
        /// FIXME 切断後にConnect関数で再接続できない問題がある。
        /// 本アプリまたはデバイスの切断/ファイナライズ処理に不足があると思う。
        /// デバイス側を再起動すればいいので、今後の課題。
        /// </summary>
        public void Disconnect()
        {
            if (bleDevice != null)
            {
                // 接続を切断
                bleDevice.Dispose();
                bleDevice = null;
            }
        }
    }
}
