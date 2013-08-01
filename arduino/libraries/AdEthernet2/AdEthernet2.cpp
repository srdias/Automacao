
//-- Classe para uso do modulo Enc28j60

#include <Arduino.h>
//#include <AdModulos.h>
#include "AdEthernet2.h"

extern int freeRam ();
extern char bufferSerial[];

void AdEthernet2::setup(/*AdSerial * aAdSerial*/){
//	this->iAdSerial = aAdSerial;
	e.setup(mac, ip, port);
};

void AdEthernet2::atenderRequisicoes(){

	char* params;
	
	if (params = e.serviceRequest()){
	
		e.print("<H1>Web Remote</H1>");
		e.print("<A HREF='?cmd=off'>Turn off</A>");

		e.print("<BR>");
		e.print("<BR>");
		e.print("Dados recebidos http: ");
		e.print(params);
		Serial.print(params);

		e.print("<BR>");
		e.print("<BR>");
		e.print("Dados recebidos na serial: ");
		e.print(bufferSerial);
		
		e.print("<BR>");
		e.print("<BR>");
		e.print("freeRam=");
		e.print(freeRam ());


		e.respond();
	}

};
/*
void AdSerial::setup(int aModo){

	this->modo=aModo;
	Serial.begin(9600);   
	Serial.setTimeout(100);

	this->printDebug("Iniciando arduino...");
	this->printDebug();
	
	return;
};

void AdSerial::printDebug(){
	printDebug("");
	return;
};

void AdSerial::printDebug(char * texto){

	if(texto[0]){
		Serial.print("SERIAL_DEBUG: ");
		Serial.print(texto);
	}else{
		Serial.println();
	}
	
	return;
};

void AdSerial::print(char * texto){
	Serial.print(texto);
	return;
};

void AdSerial::readBufferSerial(){
	
	int liQtdeLidoTotal=0;
    if(Serial.available()){
		int qtdeBytesLidos=Serial.readBytes(bufferSerial, MAX_BUFFER_SERIAL -1);
		bufferSerial[qtdeBytesLidos]=0;
		this->printDebug(bufferSerial);
		this->printDebug();
    }

	return;
};

*/
