#include "Console.h"
#include "ServoI2CDriver.h"
#include "LEDDriver.h"

Console console;
LEDDriver led;
ServoI2CDriver servo;

void setup() {
  // put your setup code here, to run once:
  console.setup();
  led.setup();
  servo.setup();
}

void loop() {
  // put your main code here, to run repeatedly:
  console.info("test");

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
