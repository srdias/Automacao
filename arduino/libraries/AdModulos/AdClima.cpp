
#include "AdModulos.h"

Clima::Clima(short aRegistro){
	this->setRegistro(aRegistro);
	this->setTipoModulo(TIPO_MODULO_CLIMA);
	
	varAlocar(CLIMA_QTDE_VARIAVEIS);
};

void Clima::setPinosAnalogicoLdr(short aPinoLdr){
	iPinoLdr.setPino(aPinoLdr);
	iPinoLdr.mode(INPUT);

	varSetValor(CLIMA_LUMINOSIDADE, 0);
};

void Clima::setPinosDigitalDht11(short aPinoDht11){
	iPinoDht11.setPino(aPinoDht11);
	iPinoDht11.mode(INPUT);
	iDht11.iniciar(iPinoDht11.getPino());

	varSetValor(CLIMA_UMIDADE, 0);
	varSetValor(CLIMA_TEMPERATURA, 0);
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
