/**
 * BLE(BlueTooth Low Enagy)制御クラス
*/
#ifndef BLE_DRIVER_H
#define BLE_DRIVER_H

#include <Arduino.h>
#include <BLEDevice.h>
#include <BLEUtils.h>
#include <BLEServer.h>

typedef void (*BLELoopCallback)(std::string);

class BLEDriver {
  public:
  void setup();

  void loop(BLELoopCallback callback);
};

#endif
