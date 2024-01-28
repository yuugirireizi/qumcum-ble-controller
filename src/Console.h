/**
 * シリアルモニタに文字列を出力する
 * 動作確認用
 * シリアルモニタのボーレート（baud）は9600
*/
#ifndef CONSOLE_H
#define CONSOLE_H

class Console {
  public:
  void setup();
  void info(const char* message);
  void debug(const char* message);
};

#endif
