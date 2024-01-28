#include "LEDDriver.h"

void LEDDriver::setup() {
  pinMode(LED_RED, OUTPUT);
  pinMode(LED_GREEN, OUTPUT);
  pinMode(LED_BLUE, OUTPUT);
}

void LEDDriver::turn(uint8_t led, uint8_t val) {
  digitalWrite(led, val);
}
