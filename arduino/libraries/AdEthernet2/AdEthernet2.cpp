
//-- Classe para uso do modulo Enc28j60

#include <Arduino.h>
#include <AdModulos.h>
#include <AdEthernet2.h>

extern int freeRam ();

// ethernet mac address - must be unique on your network
static byte mymac[] = { 0x74,0x69,0x69,0x2D,0x30,0x31 };

byte Ethernet::buffer[1024]; // tcp/ip send and receive buffer
static BufferFiller bfill;  // used as cursor while filling the buffer

void AdEthernet2::setup(){

	Serial.println("Enc28j60 Iniciando...");

	if (ether.begin(sizeof Ethernet::buffer, mymac) == 0) Serial.println( "Failed to access Ethernet controller");
	if (!ether.dhcpSetup()) Serial.println("DHCP failed");

	Serial.println("Enc28j60 Iniciado.");

	ether.printIp("IP:  ", ether.myip);
	ether.printIp("GW:  ", ether.gwip);  
	ether.printIp("DNS: ", ether.dnsip);  
	
	Serial.println("Enc28j60 ok.");
};

void AdEthernet2::atenderRequisicoes(AdModulosContainer * aAdModulosContainer){

	word len = ether.packetReceive();
	word pos = ether.packetLoop(len);

	if (pos) {
		bfill = ether.tcpOffset();
		char* data = (char *) Ethernet::buffer + pos;

		char lsString[100];
		/*
		for(short i=0; i<aAdModulosContainer->qtdeItens; i++){
			aAdModulosContainer->iModulos[i]->publicarString(lsString);
			bfill.emit_p(lsString);
			bfill.emit_p("\r");
		}
		*/
		bfill.emit_p(PSTR("Teste: $S ; "), lsString);
		bfill.emit_p("\r");
		bfill.emit_p(".");

		ether.httpServerReply(bfill.position()); // send web page data
		
	};

};
