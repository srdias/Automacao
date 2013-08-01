
class AdEthernet{
	public:
		void setup();
		void TrocarInformacaoes(int ldr, int temp, int umid);
		int DadosEsperar();
		int readData(char parametro[]);
		void sendData(char * sParametro, int contador, int ldr, int temp, int umid );
		int  connectLan();
		int  disconnectlan();
};

