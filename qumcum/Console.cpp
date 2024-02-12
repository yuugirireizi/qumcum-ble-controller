#include "Console.h"

#include <Arduino.h>

void Console::setup() {
  Serial.begin(9600);
}

void Console::info(const char *message) {
  Serial.println(message);
}

void Console::debug(const char *message) {
  Serial.println(message);
}
