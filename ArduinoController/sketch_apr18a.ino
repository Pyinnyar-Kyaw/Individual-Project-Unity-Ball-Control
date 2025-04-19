#include <Wire.h>
#include <MPU6050_light.h>

MPU6050 mpu(Wire);

void setup() {
  Serial.begin(9600);
  Wire.begin();
  mpu.begin();

  // if (mpu.testConnection()) {
  //   Serial.println("MPU6050 connection successful");
  // } else {
  //   Serial.println("MPU6050 connection failed");
  // }
}

void loop() {
  mpu.update();

  float pitch = mpu.getAngleX();
  float roll = mpu.getAngleY();

  Serial.print(pitch);
  Serial.print(",");
  Serial.println(roll);

  delay(50);
}
