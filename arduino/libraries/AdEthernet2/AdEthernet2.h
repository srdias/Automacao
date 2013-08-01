
#include <EtherShield.h>
#include <ETHER_28J60.h>

static uint8_t mac[6] = {0x54, 0x55, 0x58, 0x10, 0x00, 0x24};
static uint8_t ip[4] = {192, 168, 0, 152};                   
static uint16_t port = 80;                                   

/*
#define SERIAL_MODO_NORMAL 1
#define SERIAL_MODO_DEBUG 2
class AdSerial{

	int modo;

	public:
		char bufferSerial[MAX_BUFFER_SERIAL];
		void setup(int aModo);
		void readBufferSerial();
		void printDebug();
		void printDebug(char * texto);
		void print(char * texto);
};
*/

class AdEthernet2{
	public:
		ETHER_28J60 e;
//		AdSerial * iAdSerial;
		void setup(/*AdSerial * aAdSerial*/);
		void atenderRequisicoes();
};


