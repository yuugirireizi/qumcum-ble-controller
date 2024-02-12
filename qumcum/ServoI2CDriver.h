// ServoI2CDriver.h ヘッダーファイル

#ifndef SERVO_I2C_DRIVER_H
#define SERVO_I2C_DRIVER_H

#include <Arduino.h>
#include "Console.h"

#define CMD_SIZE 10

class ServoI2CDriver {
  private:
  uint8_t cmd[CMD_SIZE];  // 毎回生成するとメモリ不足になるので、メンバに一つだけ持つ
  Console console;

  void clearCmd();

  void sendToServo(const uint8_t* tx, int size);

  /**
   * {tx} [out]: 受信したデータはここに入れる
   * {size}: I2Cに要求する受信サイズ
   * Return: 実際に受信したサイズ
  */
  uint16_t recvFromServo(uint8_t* rx, int size);
  
  public:
  void setup();

  /**
   * モーターの回転を指示する。ただし、実際にモーターが動くのはexecute関数実行時であることに注意
   * {motorChannel}: 動かすモーターのチャンネル
   * {angle}: 角度。単位は度数法で1/10度。例：angle = 1 -> 0.1度、angle = 900 -> 90度。
   * {duration}: 動作時間。指定した角度を何秒で回すか（msec）
  */
  void rotate(uint8_t motorChannel, uint16_t angle, uint16_t duration);

  /**
   * それまでにrotateで指示したモーターの回転を一斉にスタートする
   * execute後、rotateで指定したdurationだけ待機することを推奨
   */
  void execute();
};

#endif
