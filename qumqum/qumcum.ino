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

  if (value.find("r") != -1) {
    led.turn(LED_RED, HIGH);
  }
  if (value.find("g") != -1) {
    led.turn(LED_GREEN, HIGH);
  }
  if (value.find("b") != -1) {
    led.turn(LED_BLUE, HIGH);
  }
  if (value.find("c") != -1) {
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
}

void loop() {
  // put your main code here, to run repeatedly:
  ble.loop(&receivedBLE);
}
