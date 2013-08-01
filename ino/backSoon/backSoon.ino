 
#include <EtherCard.h>

#define STATIC 0

// ethernet mac address - must be unique on your network
static byte mymac[] = { 0x74,0x69,0x69,0x2D,0x30,0x31 };

byte Ethernet::buffer[1024]; // tcp/ip send and receive buffer
static BufferFiller bfill;  // used as cursor while filling the buffer


//char page[512];
long llteste=0;

static int freeRam () {
  extern int __heap_start, *__brkval; 
  int v; 
  return (int) &v - (__brkval == 0 ? (int) &__heap_start : (int) __brkval); 
}

void setup(){
  Serial.begin(9600);
  Serial.println("--backSoon--");
  Serial.println("Passo 1.");
  
  Serial.print("Free ram:");
  Serial.println(freeRam());
  
  if (ether.begin(sizeof Ethernet::buffer, mymac) == 0) Serial.println( "Failed to access Ethernet controller");
  
  Serial.println("Passo 2.");
  
  if (!ether.dhcpSetup()) Serial.println("DHCP failed");

  Serial.println("Passo 3.");

  ether.printIp("IP:  ", ether.myip);
  ether.printIp("GW:  ", ether.gwip);  
  ether.printIp("DNS: ", ether.dnsip);  

  Serial.println("Inicializacao concluida.");

}

void loop(){
  
  word len = ether.packetReceive();
  word pos = ether.packetLoop(len);

  if (pos) {
    bfill = ether.tcpOffset();
    char* data = (char *) Ethernet::buffer + pos;
    
    Serial.print("Data: [");
    Serial.print(data);
    Serial.println("] ");

    bfill.emit_p(PSTR("<html>"));
    bfill.emit_p(PSTR("<head>"));
    bfill.emit_p(PSTR("<title>Pagina de teste</title>"));
    bfill.emit_p(PSTR("<meta http-equiv='refresh' content='$D'/><h3>Last $D messages:</h3>"), 3, 50);
    bfill.emit_p(PSTR("</head>"));
    bfill.emit_p(PSTR("<body>"));

    static int teste=0;
    bfill.emit_p(PSTR("Meu numero: $D ; "), teste++);
    
    bfill.emit_p(PSTR("<br><br>Porta Digitais: "));
    for(int i=0;i<14;i++){
      bfill.emit_p(PSTR("<li>Porta digital: $D - Valor= $D"), i, digitalRead(i) );
    }

    bfill.emit_p(PSTR("<br><br>Porta Analogicas: "));
    for(int i=0;i<6;i++){
      bfill.emit_p(PSTR("<li>Porta analogica: $D - Valor= $D"), i, analogRead(i) );
    }
    bfill.emit_p(PSTR("<br><br>"));
      
    long t = millis() / 1000;
    word h = t / 3600;
    byte m = (t / 60) % 60;
    byte s = t % 60;
    bfill.emit_p(PSTR("Uptime is $D$D:$D$D:$D$D"), h/10, h%10, m/10, m%10, s/10, s%10);
    bfill.emit_p(PSTR(" ($D bytes free)"), freeRam());

    bfill.emit_p(PSTR("</body>"));
    bfill.emit_p(PSTR("</html>"));
    Serial.println("Tamanho: ");
    Serial.println(bfill.position());
    ether.httpServerReply(bfill.position()); // send web page data
  };
}
