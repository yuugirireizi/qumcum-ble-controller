/**
 * LED制御クラス
*/
#ifndef LED_DRIVER_H
#define LED_DRIVER_H

#include <Arduino.h>

// qumcumのLEDピン番号
#define LED_RED 32
#define LED_GREEN 27
#define LED_BLUE 33

class LEDDriver {
  public:
  void setup();

  /**
   * {led}: LED_RED, LED_GREEN, LED_BLUE
   * {on}: HIGH(0x1):点灯、LOW(0x0): 消灯
  */
  void turn(uint8_t led, uint8_t val);
};

#endif
