
#include "AdModulos.h"


//-------------------------------------------------------------
//-- Modulo Bancada
//-------------------------------------------------------------

Bancadas::Bancadas(short aRegistro){
	this->setRegistro(aRegistro);
	this->setTipoModulo(TIPO_MODULO_BANCADA);

	varAlocar(BANCADA_QTDE_VARIAVEIS);

};

short Bancadas::setPinos(short aPinoNivelAgua, short aPinoFluxoAgua, short aPinoReposicaoAgua){
	iPinoNivelAgua.setPino(aPinoNivelAgua);
	iPinoFluxoAgua.setPino(aPinoFluxoAgua);
	iPinoReposicaoAgua.setPino(aPinoReposicaoAgua);
	
	iPinoNivelAgua.mode(INPUT);
	iPinoNivelAgua.mode(INPUT);
	iPinoNivelAgua.mode(OUTPUT);

	varSetValor(BANCADA_NIVEL_AGUA_LIMITE_INFERIOR, 100);
	varSetValor(BANCADA_NIVEL_AGUA_LIMITE_SUPERIOR, 900);
	varSetValor(BANCADA_NIVEL_AGUA_LIMITE_ACIONAMENTO, 700);
	varSetValor(BANCADA_NIVEL_AGUA_LIMITE_DESACIONAMENTO, 800);
	
	return 1;
};

short Bancadas::acao(){
	
	varSetValor(BANCADA_NIVEL_AGUA, iPinoNivelAgua.anaRead());
	varSetValor(BANCADA_ESTADO_REPOSICAO_AGUA, iPinoFluxoAgua.anaRead());
	
	if( varGetValor(BANCADA_NIVEL_AGUA_LIMITE_ACIONAMENTO) > varGetValor(BANCADA_NIVEL_AGUA) ){
		varSetValor(BANCADA_ESTADO_FLUXO_AGUA, 1);
		iPinoReposicaoAgua.digWrite(HIGH);
	}
	
	if( varGetValor(BANCADA_NIVEL_AGUA_LIMITE_DESACIONAMENTO) < varGetValor(BANCADA_NIVEL_AGUA) ){
		varSetValor(BANCADA_ESTADO_FLUXO_AGUA, 0);
		iPinoReposicaoAgua.digWrite(LOW);
	};
	
	return 1;
};

short Bancadas::triggerAlterarVariavel(short varNum, short varNovoValor, short varAnteriorValor){
	return 1;
};

