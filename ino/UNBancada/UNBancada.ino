
#include <AdModulos.h>	
#include <AdEthernet2.h>
#include <EtherCard.h>

AdModulosContainer iAdModulosContainer;
Bancadas iBancada(1);
Clima iClima(2);
Tempo iTempo(3);
BancadaMaternidade iMaternidade(4);

AdEthernet2 iRede;

int freeRam () {
  extern int __heap_start, *__brkval; 
  int v; 
  return (int) &v - (__brkval == 0 ? (int) &__heap_start : (int) __brkval); 
}


void setup() {

  Serial.begin(9600);   

  Serial.println("Iniciando arduino...");
  
  iRede.setup();

  iBancada.setPinos(PINO_A1,PINO_A2,PINO_D3);
  iClima.setPinos(PINO_D9,PINO_A0);
  
  iMaternidade.setPino(PINO_D4);
  iMaternidade.setClima(&iClima);
  
  iAdModulosContainer.add(&iBancada);
  iAdModulosContainer.add(&iClima);
  iAdModulosContainer.add(&iTempo);
  iAdModulosContainer.add(&iMaternidade);

  Serial.print("freeRam=");
  Serial.println(freeRam ());

}

void loop() {
	iAdModulosContainer.liveAll();
	iAdModulosContainer.processarComandos();
	iRede.atenderRequisicoes(&iAdModulosContainer);
	delay(DELAY_LOOP_ARDUINO);
};

