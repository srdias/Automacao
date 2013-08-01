
#include <AdModulos.h>	

AdModulosContainer iAdModulosContainer;
Bancadas iBancada(1);
Clima iClima(2);
Tempo iTempo(3);


int freeRam () {
  extern int __heap_start, *__brkval; 
  int v; 
  return (int) &v - (__brkval == 0 ? (int) &__heap_start : (int) __brkval); 
}


void setup() {

  Serial.begin(9600);   

  Serial.println("Iniciando arduino...");

  iBancada.setPinos(PINO_A1,PINO_A2,PINO_D3);
  iClima.setPinos(PINO_D9,PINO_A0);
  
  iAdModulosContainer.add(&iBancada);
  iAdModulosContainer.add(&iClima);
  iAdModulosContainer.add(&iTempo);

  Serial.print("freeRam=");
  Serial.println(freeRam ());

}

void loop() {
	iAdModulosContainer.liveAll();
	iAdModulosContainer.processarComandos();
	delay(DELAY_LOOP_ARDUINO);
}
