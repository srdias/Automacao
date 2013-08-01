
#include "AdModulos.h"

int dht11::setUmidade(int umidade){
	humidity = umidade;
	return humidity;
};
int dht11::getUmidade(){
	return humidity;
};
int dht11::setTemperatura(int temperatura){
	temperature = temperatura;
	return temperature;
};
int dht11::getTemperatura(){
	return temperature;
};

int dht11::ocorreuErro(){
	return codigoErro;
};

int dht11::print(){
/*
	this->read();

	Serial.print( CODIGO_UMIDADE );
	Serial.print( this->getUmidade() );
	Serial.print( ";" );
	
	Serial.print( CODIGO_TEMPERATURA );
	Serial.print( this->getTemperatura() );
	Serial.print( ";" );
*/	
	return 1;
};

String dht11::getMensagemErro(){
	String lsMensagemErro=String("");
	switch (codigoErro){
		case DHTLIB_OK: 
			lsMensagemErro.concat("OK"); 
			break;
		case DHTLIB_ERROR_CHECKSUM: 
			lsMensagemErro.concat("Checksum error"); 
			break;
		case DHTLIB_ERROR_TIMEOUT: 
			lsMensagemErro.concat("Time out error"); 
			break;
		default: 
			lsMensagemErro.concat("Unknown error"); 
			break;
	}
	return lsMensagemErro;
};

int dht11::iniciar(int aPino){
	pino = aPino;
	
};

int dht11::read()
{
	// BUFFER TO RECEIVE
	uint8_t bits[5];
	uint8_t cnt = 7;
	uint8_t idx = 0;
	codigoErro=DHTLIB_OK;

	// EMPTY BUFFER
	for (int i=0; i< 5; i++) bits[i] = 0;

	// REQUEST SAMPLE
	pinMode(pino, OUTPUT);
	digitalWrite(pino, LOW);
	delay(18);
	digitalWrite(pino, HIGH);
	delayMicroseconds(40);
	pinMode(pino, INPUT);
	
	setUmidade(0); 
	setTemperatura(0); 
	
	// ACKNOWLEDGE or TIMEOUT
	unsigned int loopCnt = 10000;
	while(digitalRead(pino) == LOW)
		if (loopCnt-- == 0){
			codigoErro=DHTLIB_ERROR_TIMEOUT;
			return codigoErro;
		};

	loopCnt = 10000;
	while(digitalRead(pino) == HIGH)
		if (loopCnt-- == 0){
			codigoErro=DHTLIB_ERROR_TIMEOUT;
			return codigoErro;
		};

	// READ OUTPUT - 40 BITS => 5 BYTES or TIMEOUT
	for (int i=0; i<40; i++)
	{
		loopCnt = 10000;
		while(digitalRead(pino) == LOW)
			if (loopCnt-- == 0){
			codigoErro=DHTLIB_ERROR_TIMEOUT;
			return codigoErro;
		};

		unsigned long t = micros();

		loopCnt = 10000;
		while(digitalRead(pino) == HIGH)
			if (loopCnt-- == 0){
			codigoErro=DHTLIB_ERROR_TIMEOUT;
			return codigoErro;
		};

		if ((micros() - t) > 40) bits[idx] |= (1 << cnt);
		if (cnt == 0)   // next byte?
		{
			cnt = 7;    // restart at MSB
			idx++;      // next byte!
		}
		else cnt--;
	}

	setUmidade( bits[0] ); 
	setTemperatura( bits[2] ); 

	uint8_t sum = bits[0] + bits[2];  

	if (bits[4] != sum){
		codigoErro=DHTLIB_ERROR_CHECKSUM;
		return codigoErro;
	};
		
	return codigoErro;
}
