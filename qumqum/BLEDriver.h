#include "BLECharacteristic.h"
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

class MyCallbacks: public BLECharacteristicCallbacks {
  void onWrite(BLECharacteristic *pCharacteristic) {
    value = pCharacteristic->getValue();
    if (value.length() > 0) {
      bReceived = true;
    }
  }

  public:
  std::string value;
  bool bReceived = false;
};

class BLEDriver {
  private:
  MyCallbacks myCallbacks;
  public:
  void setup();

  void loop(BLELoopCallback callback);
};

#endif
