#include "ServoI2CDriver.h"
#include <Wire.h>

#define SERVO_IC_I2C_ADDR 8

void ServoI2CDriver::clearCmd() {
  memset(cmd, 0x00, sizeof(cmd));
}

void ServoI2CDriver::sendToServo(const uint8_t* tx, int size) {
  Wire.beginTransmission(SERVO_IC_I2C_ADDR);
  while(size-- > 0) {
    Wire.write(*tx++);
  }
  Wire.endTransmission();
}

/**
  * Arg {tx} [out]: 受信したデータはここに入れる
  * Arg {size}: I2Cに要求する受信サイズ
  * Return: 実際に受信したサイズ
*/
uint16_t ServoI2CDriver::recvFromServo(uint8_t* rx, int size) {
  uint16_t n=0;
  Wire.requestFrom(SERVO_IC_I2C_ADDR, size); // request len bytes from Slave ID #addr
  while (Wire.available()){
    *rx++ = Wire.read();
    n++;
  }
  return n;
}

void ServoI2CDriver::setup() {
  Wire.setClock(100000);
  Wire.begin();

  // サーバーモータ動作モード設定
  cmd[0] = 0;
  cmd[1] = 1;
  sendToServo(cmd, 2);
  clearCmd();

  // PWMパルス出力を許可
  cmd[0] = 2; // cmdno
  cmd[1] = 3; // bOut
  *(uint16_t*)(&(cmd[2])) = 1;
  sendToServo(cmd, 4);
  clearCmd();
  
  // 念のため、電文の通達を待つディレイを入れる
  delay(100);
}

void ServoI2CDriver::rotate(uint8_t motorChannel, uint16_t angle, uint16_t duration) {
  console.debug("start rotate");
  cmd[0] = 4;
  cmd[1] = motorChannel;
  // cmd[1] = 3;
  *(uint16_t*)(&(cmd[2])) = angle; // AngleHD 角度(1=1/10 度)
  *(uint16_t*)(&(cmd[4])) = duration; // mTime 動作時間(msec)
  // *(uint16_t*)(&(cmd[2])) = 0; // AngleHD 角度(1=1/10 度)
  // *(uint16_t*)(&(cmd[4])) = 1000; // mTime 動作時間(msec)
  sendToServo(cmd, 6);
  clearCmd();
}

void ServoI2CDriver::execute() {
  console.debug("start execute");
  cmd[0] = 1; // cmdno
  sendToServo(cmd, 1);
  clearCmd();
}
