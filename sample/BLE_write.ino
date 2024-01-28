/*
    Based on Neil Kolban example for IDF: https://github.com/nkolban/esp32-snippets/blob/master/cpp_utils/tests/BLE%20Tests/SampleWrite.cpp
    Ported to Arduino ESP32 by Evandro Copercini
*/

#include <BLEDevice.h>
#include <BLEUtils.h>
#include <BLEServer.h>
#include <ESP32Servo.h>

// See the following for generating UUIDs:
// https://www.uuidgenerator.net/

#define SERVICE_UUID        "4fafc201-1fb5-459e-8fcc-c5c9c331914b"
#define CHARACTERISTIC_UUID "beb5483e-36e1-4688-b7f5-ea07361b26a8"

#define LED_RED 32
#define LED_GREEN 27
#define LED_BLUE 33
#define SERVO_POWER 34

std::string value;
bool bReceived=false;

// サーボの数
const int numServos = 3; // サーボの数を設定（4番ピンから10番ピンまでの7つ）

// サーボのピン番号
int servoPins[numServos] = {4,7,10}; // サーボのピン番号を配列で指定

// サーボオブジェクトの配列
Servo servos[numServos];

class MyCallbacks: public BLECharacteristicCallbacks {
  void onWrite(BLECharacteristic *pCharacteristic) {
    value = pCharacteristic->getValue();
    if (value.length() > 0) {
      Serial.println(value.c_str());
      bReceived=true;
    }
  }
};

void setup() {
  pinMode(SERVO_POWER,OUTPUT) ;  //電源供給ピン

  Serial.begin(115200);

  Serial.println("1- Download and install an BLE scanner app in your phone");
  Serial.println("2- Scan for BLE devices in the app");
  Serial.println("3- Connect to MyESP32");
  Serial.println("4- Go to CUSTOM CHARACTERISTIC in CUSTOM SERVICE and write something");
  Serial.println("5- See the magic =)");

  BLEDevice::init("MyESP32");
  BLEServer *pServer = BLEDevice::createServer();

  BLEService *pService = pServer->createService(SERVICE_UUID);

  BLECharacteristic *pCharacteristic = pService->createCharacteristic(
                                         CHARACTERISTIC_UUID,
                                         BLECharacteristic::PROPERTY_READ |
                                         BLECharacteristic::PROPERTY_WRITE
                                       );

  pCharacteristic->setCallbacks(new MyCallbacks());

  pCharacteristic->setValue("Hello World");
  pService->start();

  BLEAdvertising *pAdvertising = pServer->getAdvertising();
  pAdvertising->start();

  pinMode(LED_RED, OUTPUT);
  pinMode(LED_GREEN, OUTPUT);
  pinMode(LED_BLUE, OUTPUT);

  // digitalWrite(SERVO_POWER,HIGH); //電源供給ON
  // サーボの初期化
  for (int i = 0; i < numServos; i++) {
    servos[i].attach(servoPins[i], 510, 2400);
    // servos[i].write(0);  // 初期角度0度
  }
}

void loop() {
  if(bReceived){
    if(value.find("r")!=-1)
      digitalWrite(LED_RED,HIGH);
    if(value.find("g")!=-1)
      digitalWrite(LED_GREEN,HIGH);
    if(value.find("b")!=-1)
      digitalWrite(LED_BLUE,HIGH);
    if(value.find("c")!=-1){
      digitalWrite(LED_RED,LOW);
      digitalWrite(LED_GREEN,LOW);
      digitalWrite(LED_BLUE,LOW);
    }
    if(value.find("l")!=-1){
      for (int i = 0; i < numServos; i++) {
        servos[i].write(60);
      }
    }
    if(value.find("n")!=-1){
      for (int i = 0; i < numServos; i++) {
        servos[i].write(90);
      }
    }
    bReceived=false;
  }
}