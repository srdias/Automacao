
void setup() {                
  // initialize the digital pin as an output.
  pinMode(5, OUTPUT);     
  pinMode(6, OUTPUT);     
}


void loop() {
  digitalWrite(5, HIGH);
  digitalWrite(6, LOW); 
  delay(1000);            
  digitalWrite(5, LOW);
  digitalWrite(6, HIGH); 
  delay(1000);            
}