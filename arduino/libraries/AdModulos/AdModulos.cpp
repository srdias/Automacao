
#include "AdModulos.h"

short Modulo::setRegistro(short aRegistro){
	this->iRegistro=aRegistro;
	this->iVarQtde=0;

	return 1;
};

short Modulo::setTipoModulo(short aTipoModulo){
	this->iTipoModulo=aTipoModulo;
	
	return 1;
};
/*
short Modulo::publicarValor(short numVar, short valor){
	
	Serial.print(iRegistro);
	Serial.print(";");
	Serial.print(iTipoModulo);
	Serial.print(";");
	Serial.print(numVar);
	Serial.print(";");
	Serial.print(valor);
	Serial.print("|");
	Serial.println();
	
	return 1;
}; 
*/

void convBase32(int valor, char * retorno){
	short fisrt=valor/32;
	short last=valor%32;
	char base32[] PROGMEM  = {"0123456789ABCDEFGHIJKLMNOPQRSTUV"};
	char basen[] PROGMEM  = {"abcdefghij"};
	
	if(valor<10){
		retorno[0]=basen[valor];
		retorno[1]=0;
	}else{
		retorno[0]=base32[fisrt];
		retorno[1]=base32[last];
	}
}

void addBase32ToString(char * a_string, int valor){

	char lsconvertido[2];
	short len=strlen(a_string);
	
	convBase32(valor, lsconvertido);
	
	a_string[len++]=lsconvertido[0];
	a_string[len++]=lsconvertido[1];
	a_string[len]=0;

}

void Modulo::publicarDebug(){
	char * lsOut = new char[this->iVarQtde*2+4+1];
	
	sprintf(lsOut,"%d;%d",iRegistro,iTipoModulo);
	for(short i=0;i<this->iVarQtde;i++){
		sprintf(&lsOut[strlen(lsOut)],";%d",this->iVariaveis[i]);
	};
	sprintf(&lsOut[strlen(lsOut)],"|");
	Serial.println(lsOut);
	delete lsOut;
};

void Modulo::publicar(){
	char * lsOut = new char[this->iVarQtde*2+4+1];
	
	lsOut[0]=0;
	addBase32ToString(lsOut,iRegistro);
	addBase32ToString(lsOut,iTipoModulo);
	
	for(short i=0;i<this->iVarQtde;i++){
		addBase32ToString(lsOut,this->iVariaveis[i]);
	};
	strcat(lsOut,"|");
	Serial.print(lsOut);
	delete lsOut;
};

short Modulo::varAlocar(short qtde){

	this->iVarQtde = qtde;
	this->iVariaveis = new short[this->iVarQtde];
	for(short i=0;i<this->iVarQtde;i++){
		this->iVariaveis[i]=0;
	};
	
	return 1;
};

short Modulo::varSetValor(short varNum, short varValor){

	if(this->iVarQtde<=varNum) return 0;
	
	short varAnteriorValor=this->iVariaveis[varNum];
	this->iVariaveis[varNum]=varValor;
	
	this->triggerAlterarVariavel(varNum, varValor, varAnteriorValor);

	return 1;
};

short Modulo::varGetValor(short varNum){
	if(this->iVarQtde<=varNum) return 0;
	return this->iVariaveis[varNum];
};

short Modulo::varNextVarPublicao(short varNum, char *texto){

	if(varNum>=iVarQtde) return 0;
	
	sprintf(texto, "e;%d;%d;%d;%d|", iRegistro, iTipoModulo, varNum, varGetValor(varNum) );
	
	return 1;
};

short Modulo::varReset(){
	if( iVarQtde > 0 ){
		delete iVariaveis;
		iVarQtde=0;
	};
	return 1;
};

short Modulo::live(){
  this->acao();
  this->publicar();
  return 1;
};


//-------------------------------------------------------------
//-- Pino
//-------------------------------------------------------------

short Pino::setPino(short aPino){
	iPino=aPino;
	return 1;
};

short Pino::getPino(){
	return iPino;
};

short Pino::anaRead(){
	return analogRead(getPino());
};
short Pino::digRead(){
	return digitalRead(getPino());
};
short Pino::mode(short aMode){
	pinMode(this->getPino(),aMode);
	return 1;
};
short Pino::digWrite(short aValor){
	digitalWrite(this->getPino(),aValor);
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
	Serial.println();
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