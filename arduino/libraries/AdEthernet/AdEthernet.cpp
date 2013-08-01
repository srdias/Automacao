
#include "AdModulos.h"
#include "AdEthernet.h"

int porta = 11000;
byte mac[] = {  0xDE, 0xAD, 0xBE, 0xEF, 0xFE, 0xED };

IPAddress server(192,168,0,153);

EthernetClient client;	

void AdEthernet::setup() {

	if (Ethernet.begin(mac) == 0) {
		adSerial("Falha configurando Ethernet usando DHCP");
	}else{
		adSerial("Ethernet configurada usando DHCP");
	}
  
}

void AdEthernet::TrocarInformacaoes(int ldr, int temp, int umid){
    
    int ibConectado;
	static long contador=0;
    
    ibConectado=connectLan();
    
    if(ibConectado){
		sendData("Arduino",contador,ldr,temp,umid);
	};

    DadosEsperar();

    readData("\r\nLendo dados (depois)...");
    
    disconnectlan();

}

int AdEthernet::DadosEsperar(){
    int timeOut=0;
    
    adSerial("\r\nEsperando resposta do servidor");    
    while (!client.available() && timeOut<3000) {
        delay(1);
        timeOut++;
        if(timeOut%500==0) adSerial(".");
    }

    adSerial("\r\nTempo de timeout: ");    
    adSerial(timeOut);
}

int AdEthernet::readData(char parametro[]){
    
    char lsPalavra[255];
    char lsControle[255];
    char lsNomeVar[255];
    char lsValorVar[255];
    int controle=0;
    
    adSerial(parametro);
      
    while (client.available()) {
        lsPalavra[controle++]=client.read();
    }
    lsPalavra[controle]=0;
    Serial.print(lsPalavra);
    
    String lsDadosRecebidos = lsPalavra;
    controle = lsDadosRecebidos.length();

    int posInicio=0;    
    int posFim=0;    
    for(int i=0; i<controle; i++){
        if( lsDadosRecebidos.charAt(i) == ';' ){
            posFim=i;
            
            String lsParte = lsDadosRecebidos.substring(posInicio,posFim);
            Serial.print("\r\nPalavra: ");
            Serial.print(lsPalavra);
            posInicio=i+1;
        }
    }
    
    return 0;
}

void AdEthernet::sendData(char * sParametro, int contador, int ldr, int temp, int umid ){
    char lsTexto[255];
    char lsHora[255];
    
    sprintf(lsTexto,"nome=%s%d&contador=%d&ldr=%d&Temperatura=%d&Umidade=%d",sParametro,contador,contador,ldr,temp,umid);
    client.write(lsTexto);
            
    adSerial("\r\n** ");
    adSerial(lsTexto);
}

int AdEthernet::connectLan(){
    
    int ibConectado;

    adSerial("\r\nConectando...");
    if (client.connect(server, porta)) {
        adSerial("\n\rConectado.");
        ibConectado=true;
    }else {
        adSerial("\r\nA conexao falhou.");
        ibConectado=false;
    }
    
    return ibConectado;

}

int AdEthernet::disconnectlan(){

    client.flush();
    client.stop();
    adSerial("\r\nDesconectado.");

    return true;
};


