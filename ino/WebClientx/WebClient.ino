
#include <Ethernet.h>
#include <SPI.h>
#include <AdModulos.h>	
#include <AdEthernet.h>	

AdEthernet iEthernet;
Clima iClima(2);

void setup() {

	Serial.begin(9600);
  
	Serial.println("Ligando arduino...");
	
	//iEthernet.setup();
	iClima.setPinos(3,3);
  


}

void loop(){

	static int contador=0;
	
	iClima.live();
	/*
	iEthernet.TrocarInformacaoes(iClima.varGetValor(CLIMA_LUMINOSIDADE),
	                             iClima.varGetValor(CLIMA_TEMPERATURA),
								 iClima.varGetValor(CLIMA_UMIDADE)
								);
	*/
	adSerial("\r\nIntervalo de tempo entre a comunicacao.");

	delay(1000);
}

