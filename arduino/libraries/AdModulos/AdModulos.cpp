
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
	retorno[0]=base32[fisrt];
	retorno[1]=base32[last];
}

void addBase32ToString(char * a_string, int valor){

	char lsconvertido[2];
	short len=strlen(a_string);
	
	convBase32(valor, lsconvertido);
	
	a_string[len]=lsconvertido[0];
	a_string[len+1]=lsconvertido[1];
	a_string[len+2]=0;

}

short Modulo::publicar(){
/*
	short ctrl=0;
	for(short i=0;i<this->iVarQtde;i++){
		if(ctrl==0){
			Serial.print("---");
			Serial.println();
			ctrl=1;
		}
		
		publicarValor(i, this->iVariaveis[i] );
	}
*/
	char * lsOut = new char[this->iVarQtde*2+4+1];
	char * lsOut2 = new char[this->iVarQtde*2+4+1];
	
	lsOut2[0]=0;
	addBase32ToString(lsOut2,iRegistro);
	addBase32ToString(lsOut2,iTipoModulo);
	
	sprintf(lsOut,"%d;%d",iRegistro,iTipoModulo);
	for(short i=0;i<this->iVarQtde;i++){
		sprintf(&lsOut[strlen(lsOut)],";%d",this->iVariaveis[i]);
		addBase32ToString(lsOut2,this->iVariaveis[i]);
	};
	sprintf(&lsOut[strlen(lsOut)],"|");
	Serial.println(lsOut);
	Serial.println(lsOut2);
	delete lsOut;
	delete lsOut2;
	return 1;
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


