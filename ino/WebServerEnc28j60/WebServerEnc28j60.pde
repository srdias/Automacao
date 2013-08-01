#include <EtherShield.h>
#include <ETHER_28J60.h>

static uint8_t mac[6] = {0x54, 0x55, 0x58, 0x10, 0x00, 0x24};
static uint8_t ip[4] = {192, 168, 0, 152};                   
static uint16_t port = 80;                                   

ETHER_28J60 e;

void setup()
{ 
	Serial.begin(9600);
	Serial.println("Iniciando o arduino...");
	
	e.setup(mac, ip, port);
}

void loop()
{
	char* params;
	Serial.println("Inicio loop...");
	if (params = e.serviceRequest()){
	
		e.print("<H1>Web Remote</H1>");
		
		if (strcmp(params, "?cmd=on") == 0){
			e.print("<A HREF='?cmd=off'>Turn off</A>");
			
		}else if (strcmp(params, "?cmd=off") == 0){
			e.print("<A HREF='?cmd=on'>Turn on</A>");
		}
		e.respond();
	}
	Serial.println("Fim loop...");
}
