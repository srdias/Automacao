
#include <Arduino.h>

#include "dht11.h"

#define PINO_A0 0
#define PINO_A1 1
#define PINO_A2 2
#define PINO_A3 3
#define PINO_A4 4
#define PINO_A5 5

#define PINO_D0 0
#define PINO_D1 1
#define PINO_D2 2
#define PINO_D3 3
#define PINO_D4 4
#define PINO_D5 5
#define PINO_D6 6
#define PINO_D7 7
#define PINO_D8 8
#define PINO_D9 9
#define PINO_D10 10
#define PINO_D11 11
#define PINO_D12 12
#define PINO_D13 13

#define TIPO_MODULO_BANCADA 1
#define TIPO_MODULO_CLIMA   2
#define TIPO_MODULO_TEMPO   3
#define TIPO_MODULO_MATERNIDADE 4

#define VARIAVEL_QTDE_MEDIA  30
#define DELAY_LOOP_ARDUINO 1000

#define adSerial(valor) Serial.print(valor);
#define adSerialLong(valor) Serial.print(valor);
/*
class Variavel{
	private:
		short iTipoModulo;
		short iRegistro;
		short iSequencial;
		short valor;

	public:
		Variavel();
		short setIndetificacao(short aTipoModulo, short aRegistro, short aSequencial);
		short setValor(short aValor);
		short getValor();
};
*/

class Modulo{
	private:
		short * iVariaveis;
		short iVarQtde;
	
	public:

		short iRegistro;
		short iTipoModulo;

		short setRegistro(short aRegistro);
		short setTipoModulo(short aTipoModulo);

		//short publicarValor(short numVar, short valor);
		
		void publicar();
		void publicarDebug();
		void publicarString(char * asString);
		virtual short acao();
		short live();
		
		short varAlocar(short qtde);
		short varSetValor(short varNum, short varValor);
		short varGetValor(short varNum);
		short varReset();
		short varNextVarPublicao(short varNum, char *texto);
		
		virtual short triggerAlterarVariavel(short varNum, short varNovoValor, short varAnteriorValor);
		
};


class Pino {
	short iPino;
	public:
		short setPino(short aPino);
		short getPino();
		short anaRead();
		short digRead();
		short mode(short aMode);
		short digWrite(short aValor);
};

#define BANCADA_NIVEL_AGUA 0
#define BANCADA_ESTADO_REPOSICAO_AGUA 1
#define BANCADA_ESTADO_FLUXO_AGUA 2
#define BANCADA_NIVEL_AGUA_LIMITE_INFERIOR 3
#define BANCADA_NIVEL_AGUA_LIMITE_SUPERIOR 4
#define BANCADA_NIVEL_AGUA_LIMITE_ACIONAMENTO 5
#define BANCADA_NIVEL_AGUA_LIMITE_DESACIONAMENTO 6
#define BANCADA_QTDE_VARIAVEIS 7
	
class Bancadas :public Modulo{
	Pino iPinoNivelAgua;
	Pino iPinoFluxoAgua;
	Pino iPinoReposicaoAgua;
	
	public:
		Bancadas(short aRegistro);
		short setPinos(short aPinoNivelAgua, short aPinoFluxoAgua, short aPinoReposicaoAgua);
		short acao();
		short triggerAlterarVariavel(short varNum, short varNovoValor, short varAnteriorValor);

};

#define CLIMA_UMIDADE 0
#define CLIMA_TEMPERATURA 1
#define CLIMA_LUMINOSIDADE 2
#define CLIMA_QTDE_VARIAVEIS 3
	
class Clima :public Modulo{
	Pino iPinoDht11;
	Pino iPinoLdr;
	dht11 iDht11;
	
	public:
		Clima(short aRegistro);
		void setPinosAnalogicoLdr(short aPinoLdr);
		void setPinosDigitalDht11(short aPinoDht11);
		short acao();
		short triggerAlterarVariavel(short varNum, short varNovoValor, short varAnteriorValor);

};


#define TEMPO_HOR 0
#define TEMPO_MIN 1
#define TEMPO_SEG 2
#define TEMPO_HOR_BASE 3
#define TEMPO_MIN_BASE 4
#define TEMPO_SEG_BASE 5
#define TEMPO_QTDE_VARIAVEIS 6

class Tempo :public Modulo{
	public:
		unsigned long millisBase;
		Tempo(short aRegistro);
		short acao();
		short triggerAlterarVariavel(short varNum, short varNovoValor, short varAnteriorValor);
};
/*
class AdVariavelContainer{

	Variavel *iVariaveis;
	public:
		short addVariavel(Variavel aVariavel);
};
*/
#define MODULO_CONTAINER_QTDE 10


class AdModulosContainer{
	public: 
		Modulo * iModulos[MODULO_CONTAINER_QTDE];
		short qtdeItens;
		AdModulosContainer();
		void add(Modulo *moduloRef);
		void liveAll();
		void processarComandos();
		void parseChangeVar(char * comando);
		
};

#define MATERNIDADE_LAMPADA_STATUS 0
#define MATERNIDADE_LUMINOSIDADE_MINIMA 1
#define MATERNIDADE_LUMINOSIDADE_MAXIMA 2
#define MATERNIDADE_QTDE_VARIAVEIS 2

class BancadaMaternidade :public Modulo{
	Pino iPinoReleLampada;
	Clima * iClima;
	public:
		BancadaMaternidade(short aRegistro);
		void setPino(short aPinoReleLampada);
		void setClima(Clima * aClima);
		short acao();
		short triggerAlterarVariavel(short varNum, short varNovoValor, short varAnteriorValor);
};


