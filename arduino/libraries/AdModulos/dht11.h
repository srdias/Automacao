
#define DHTLIB_OK				0
#define DHTLIB_ERROR_CHECKSUM	-1
#define DHTLIB_ERROR_TIMEOUT	-2

class dht11{
private:
	int pino;
	int humidity;
	int temperature;
	int codigoErro;
public:
	int iniciar(int aPino);
    int read();
	int setUmidade(int umidade);
	int getUmidade();
	int setTemperatura(int umidade);
	int getTemperatura();
	int ocorreuErro();
	String getMensagemErro();
	int print();
};