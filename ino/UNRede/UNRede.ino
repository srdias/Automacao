// A simple web server that always just says "Hello World"

#include "EtherShield.h"
#include "ETHER_28J60.h"

static uint8_t mac[6] = {0x54, 0x55, 0x58, 0x10, 0x00, 0x24};   // this just needs to be unique for your network, 
                                                                // so unless you have more than one of these boards
                                                                // connected, you should be fine with this value.
                                                           
static uint8_t ip[4] = {192, 168, 0, 152};                       // the IP address for your board. Check your home hub
                                                                // to find an IP address not in use and pick that
                                                                // this or 10.0.0.15 are likely formats for an address
                                                                // that will work.

static uint16_t port = 80;                                      // Use port 80 - the standard for HTTP
int teste=0;
ETHER_28J60 e;

#define MAX_BUFFER_SERIAL 50

char bufferSerial[MAX_BUFFER_SERIAL];

int freeRam () {
  extern int __heap_start, *__brkval; 
  int v; 
  return (int) &v - (__brkval == 0 ? (int) &__heap_start : (int) __brkval); 
}

void setup()
{ 
	Serial.begin(9600);   
	Serial.println("Iniciando arduino...");
	
	e.setup(mac, ip, port);
}

void loop()
{
  char* params;
	
  if (params = e.serviceRequest()){
    e.print("<html>");
    e.print("<title>Pagina de teste do arduino</title>");
    e.print("<body>");
	
    e.print("<li> Memoria livre: ");
    e.print(freeRam ());
    e.print("<li> Contador: ");
    e.print(teste);
	
    e.print("<H1>Analog Values</H1><br/><table>");
    e.print("<tr><th>Input</th><th>Value</th></tr>");
    for (int i = 0; i < 6; i++)
    {
      e.print("<tr><td>"); e.print(i); e.print("</td><td>"); e.print(analogRead(i)); e.print("</td></tr>");
    }
    e.print("</table>");
    e.print("</body>");
    e.print("</html>");
    e.respond();
  }
  teste++;
  delay(100);
}

