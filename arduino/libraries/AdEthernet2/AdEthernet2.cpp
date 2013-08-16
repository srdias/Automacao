
//-- Classe para uso do modulo Enc28j60

#include <Arduino.h>
#include <AdModulos.h>
#include <AdEthernet2.h>

extern int freeRam ();

// ethernet mac address - must be unique on your network
static byte mymac[] = { 0x74,0x69,0x69,0x2D,0x30,0x31 };

byte Ethernet::buffer[512]; // tcp/ip send and receive buffer
static BufferFiller bfill;  // used as cursor while filling the buffer

int strStartWith(char *base, char *find){
	int i;
	for(i=0; base[i] && find[i] && base[i]==find[i];i ++);
	return !(find[i]);
};

void strMid(char *base, char * dest, int start, char stop){
	int j=0;
	for(int i=start;base[i] && base[i] != stop;i++){
		dest[j++]=base[i];
	};
	dest[j]=0;
}


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
		char lsString[30];
		
//		Serial.print("Data: [");
//		Serial.print(data);
//		Serial.println("] ");
		
		if( strStartWith(data,"GET /?a;") == 1 ){
			strMid(data,lsString,6, ' ');
			Serial.print("HTTP GET=");
			Serial.println(lsString);
		}
		

		bfill.emit_p(PSTR("["));
		for(short i=0; i<aAdModulosContainer->qtdeItens; i++){
			aAdModulosContainer->iModulos[i]->publicarString(lsString);
			bfill.emit_p(PSTR("v:$S"), lsString);
		}

		bfill.emit_p(PSTR("]"));
		bfill.emit_p(PSTR("FreeRam=$D"), freeRam ());
		//bfill.emit_p(PSTR("<br>Recebido:$S"), data);

		ether.httpServerReply(bfill.position()); // send web page data
		
	};

};

