// A simple web server that always just says "Hello World"

#include "etherShield.h"
#include "ETHER_28J60.h"

int outputPin = 13;

static uint8_t mac[6] = {0x54, 0x55, 0x58, 0x10, 0x00, 0x24};

static uint8_t ip[4] = {192,168,0,150}; 

static uint16_t port = 81; // Use port 80 - the standard for HTTP

ETHER_28J60 e;
int teste=0;
void setup()
{
e.setup(mac, ip, port);
pinMode(outputPin, OUTPUT);

}

void loop(){
	char* params;
	teste++;
	if (params = e.serviceRequest()){
		e.print("<H1>Web Remote</H1>");
		e.print("Contador:");
		e.print(teste);
		
		if (strcmp(params, "?cmd=on") == 0){

			digitalWrite(outputPin, HIGH);
			e.print("<A HREF='?cmd=off'>Turn off</A>");
		}else{
			digitalWrite(outputPin, LOW);
			e.print("<A HREF='?cmd=on'>Turn on</A>");
		}
		e.respond();
	}
}