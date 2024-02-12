#include "Console.h"
#include "ServoI2CDriver.h"
#include "LEDDriver.h"
#include "BLEDriver.h"

Console console;
LEDDriver led;
ServoI2CDriver servo;
BLEDriver ble;

void setup() {
  // put your setup code here, to run once:
  console.setup();
  led.setup();
  servo.setup();
  ble.setup();
}

void receivedBLE(std::string value) {
  console.debug("received");

  if (value.find("xr") != -1) {
    led.turn(LED_RED, HIGH);
  }
  if (value.find("xg") != -1) {
    led.turn(LED_GREEN, HIGH);
  }
  if (value.find("xb") != -1) {
    led.turn(LED_BLUE, HIGH);
  }
  if (value.find("xc") != -1) {
    led.turn(LED_RED, LOW);
    led.turn(LED_GREEN, LOW);
    led.turn(LED_BLUE, LOW);
  }
  if (value.find("l") != -1) {
    servo.rotate(3, 900, 1000);
    servo.execute();
    delay(1000);

    servo.rotate(3, 600, 1000);
    servo.execute();
    delay(1000);

    servo.rotate(3, 1200, 1000);
    servo.execute();
    delay(1000);
  }

  // Servoの指示を確認する
  // xs000384 <- という命令がくる。xsの後、2文字が2桁の16進数でチャンネル、更に4文字が4桁の16進数で角度を示している。
  std::string target = "xs";
  size_t pos = value.find(target);
  bool isFind = false;
  
  while (pos != std::string::npos) { // xsが見つかった場合
    if (pos + target.length() + 6 <= value.length()) { // 2桁の16進数と4桁の16進数が含まれるか確認
      std::string hex2 = value.substr(pos + target.length(), 2); // 2桁の16進数を取得
      std::string hex4 = value.substr(pos + target.length() + 2, 4); // 4桁の16進数を取得

      // 16進数を10進数に変換
      uint8_t decimal2 = std::strtol(hex2.c_str(), nullptr, 16);
      uint16_t decimal4 = std::strtol(hex4.c_str(), nullptr, 16);

      // 結果を出力
      servo.rotate(decimal2, decimal4, 1000);
      isFind = true;
    }

    // 次のxsを探す
    pos = value.find(target, pos + target.length());
  }
  if (isFind) {
    servo.execute();
    delay(1000);
  }
}

void loop() {
  // put your main code here, to run repeatedly:
  ble.loop(&receivedBLE);
}
