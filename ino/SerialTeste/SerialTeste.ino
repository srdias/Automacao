
#include <SoftwareSerial.h>

SoftwareSerial mySerial(10, 11); // RX, TX

int teste=0;

void setup() {
  // put your setup code here, to run once:
    Serial.begin(9600);
    Serial.setTimeout(10);
    Serial.println("Hello Computer");
    
    mySerial.begin(9600);
    mySerial.println("Hello, world?");

}

void loop() {
  
  teste++;
  
  Serial.print("UNIDADE2: ");
  Serial.println(teste);

  mySerial.print("UNIDADE2: ");
  mySerial.println(teste);
  
  int c=0;
  while(mySerial.available() && (c++)<20){
    if(c==0) Serial.write("mySerial:");
    Serial.write(mySerial.read());
  }
  
  delay(400);
}

void serialEvent(){
  if (Serial.available() > 0) {
    char buffer[25];
    int qtde=Serial.readBytes(buffer, 24);
    buffer[qtde]=0;
    
    // say what you got:
    Serial.print("I received: ");
    Serial.println(buffer);
  }
}
