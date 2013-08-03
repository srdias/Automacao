
#include "AdModulos.h"

Clima::Clima(short aRegistro){
	this->setRegistro(aRegistro);
	this->setTipoModulo(TIPO_MODULO_CLIMA);
	
	varAlocar(CLIMA_QTDE_VARIAVEIS);
};

short Clima::setPinos(short aPinoDht11, short aPinoLdr){

	iPinoDht11.setPino(aPinoDht11);
	iPinoLdr.setPino(aPinoLdr);
	
	iPinoDht11.mode(INPUT);
	iPinoLdr.mode(INPUT);
	
	iDht11.iniciar(iPinoDht11.getPino());
	
	varSetValor(CLIMA_UMIDADE, 0);
	varSetValor(CLIMA_TEMPERATURA, 0);
	varSetValor(CLIMA_LUMINOSIDADE, 0);
	
/*	varUsaMedia(CLIMA_UMIDADE, 1);
	varUsaMedia(CLIMA_TEMPERATURA, 1);
	varUsaMedia(CLIMA_LUMINOSIDADE, 1);*/
	
	return 1;
};

short Clima::acao(){
	iDht11.read();
	varSetValor(CLIMA_UMIDADE, iDht11.getUmidade());
	varSetValor(CLIMA_TEMPERATURA, iDht11.getTemperatura());
	varSetValor(CLIMA_LUMINOSIDADE, iPinoLdr.anaRead());
	
	return 1;
};

short Clima::triggerAlterarVariavel(short varNum, short varNovoValor, short varAnteriorValor){
	return 1;
};

#define null 0
AdModulosContainer::AdModulosContainer(){
	qtdeItens=0;
	for(short i=0; i<MODULO_CONTAINER_QTDE; i++){
		iModulos[i]=null;
	}
};


void AdModulosContainer::add(Modulo *moduloRef){

	iModulos[qtdeItens]=moduloRef;
	qtdeItens++;

	
};

void AdModulosContainer::liveAll(){
	for(short i=0; i<qtdeItens; i++){
		iModulos[i]->live();
	}
};

void AdModulosContainer::processarComandos(){

    while(Serial.available()){
		char lsTexto[25];
		short qtdeBytesLidos=Serial.readBytesUntil('|', lsTexto, 25);
		lsTexto[qtdeBytesLidos]=0;
/*		Serial.print("lsTexto:'");
		Serial.print(lsTexto);
		Serial.print("'");
		Serial.println();*/
		parseChangeVar(lsTexto);
    }
	
}

void AdModulosContainer::parseChangeVar(char * comando){

	short parte=0;
	short numero=0;
	short modulo=0;
	short sequencial=0;
	short variavel=0;
	short valor=0;
	short controle=0;
	char acao;
	for(short i=0; comando[i]; i++){
		if(comando[i]==';'){
			parte++;
			continue;
		}
		numero=comando[i]-48;
		
		if(parte==0) acao=comando[i];
		if(parte==1) modulo=(modulo*10+numero);
		if(parte==2) sequencial=sequencial*10+numero;
		if(parte==3) variavel=variavel*10+numero;
		if(parte==4) valor=valor*10+numero;
	}
/*	
	Serial.print("Resultado:");
	Serial.print(" acao:"); Serial.print(acao);
	Serial.print(" controle:"); Serial.print(controle);
	Serial.print(" modulo:"); Serial.print(modulo);
	Serial.print(" sequencial:"); Serial.print(sequencial);
	Serial.print(" variavel:"); Serial.print(variavel);
	Serial.print(" valor:"); Serial.print(valor);
	Serial.println();
*/	
	short validacao=(modulo>=0 && modulo<=1024) &&
	              (controle>=0 && controle<=1024) &&
	              (sequencial>=0 && sequencial<=1024) &&
	              (variavel>=0 && variavel<=1024) &&
	              (valor>=0 && valor<=1024) &&
	              (acao=='a') &&
				  (parte==4);
	
	if( validacao ){
		for(short i=0; i<qtdeItens; i++){
			if( (iModulos[i]->iTipoModulo == modulo) &&
				(iModulos[i]->iRegistro == sequencial) ){
				iModulos[i]->varSetValor(variavel, valor);
				break;
			}
		}
	}
}


Tempo::Tempo(short aRegistro){
	this->setRegistro(aRegistro);
	this->setTipoModulo(TIPO_MODULO_TEMPO);
	
	millisBase=millis();
	
	varAlocar(TEMPO_QTDE_VARIAVEIS);
 
	varSetValor(TEMPO_HOR, 0);
	varSetValor(TEMPO_MIN, 0);
	varSetValor(TEMPO_SEG, 0);

};

short Tempo::triggerAlterarVariavel(short varNum, short varNovoValor, short varAnteriorValor){
	if(varNum >= TEMPO_HOR_BASE && varNum <= TEMPO_SEG_BASE){
		millisBase=millis();
	}
	return 1;
};

short calcResto(unsigned long *base, unsigned long *inteiro, unsigned long divisor){

	*inteiro = (*base/divisor);
	*base = *base - ((*inteiro)*divisor);

	return 0;
}

short Tempo::acao(){

	unsigned long segundos=(millis()-millisBase) / 1000;
	unsigned long hora;
	unsigned long minutos;
	
	calcResto(&segundos, &hora, 60 * 60);
	calcResto(&segundos, &minutos, 60);

	short liTempoHor=varGetValor(TEMPO_HOR_BASE);
	short liTempoMin=varGetValor(TEMPO_MIN_BASE);
	short liTempoSeg=varGetValor(TEMPO_SEG_BASE);
	
	//-- a;3;3;3;21|a;3;3;4;51|a;3;3;5;40|
	
	liTempoSeg += segundos;
	if( liTempoSeg > 59 ){
		liTempoSeg = liTempoSeg - 60;
		liTempoMin ++;
	};

	liTempoMin += minutos;
	if( liTempoMin > 59 ){
		liTempoMin = liTempoMin - 60;
		liTempoHor ++;
	};
	
	liTempoHor += hora;
	if( liTempoHor > 23 ){
		liTempoHor = liTempoHor - 24;
	};
	
	varSetValor(TEMPO_HOR, liTempoHor);
	varSetValor(TEMPO_MIN, liTempoMin);
	varSetValor(TEMPO_SEG, liTempoSeg);
	
	return 1;
};

BancadaMaternidade::BancadaMaternidade(short aRegistro){
	this->setRegistro(aRegistro);
	this->setTipoModulo(TIPO_MODULO_MATERNIDADE);
	
	varAlocar(MATERNIDADE_QTDE_VARIAVEIS);
 
	varSetValor(MATERNIDADE_LAMPADA_STATUS, 0);
	varSetValor(MATERNIDADE_LUMINOSIDADE_MINIMA, 450);
	varSetValor(MATERNIDADE_LUMINOSIDADE_MAXIMA, 500);
};
short BancadaMaternidade::acao(){

	return 1;

};

short BancadaMaternidade::triggerAlterarVariavel(short varNum, short varNovoValor, short varAnteriorValor){
	return 1;
};

void BancadaMaternidade::setPino(short aPinoReleLampada){
	this->iPinoReleLampada.setPino(aPinoReleLampada);
};

void BancadaMaternidade::setClima(Clima * aClima){
	this->iClima = aClima;
};
