## README – TransporterDemo.puml

### 📌 Beskrivning  
Detta projekt innehåller ett **klassdiagram** för ett transportsystem skapat med **PlantUML**. Diagrammet illustrerar hur olika transportmedel (`Car`, `Bike`, `Bus`) implementerar `ITransport`-gränssnittet, samt hur `TransportFactory` används för att skapa instanser av dessa klasser.

---

### 🛠️ Installation & Konfiguration av PlantUML i VSCode  

1. **Installera PlantUML-tillägget**  
   - Öppna **VSCode**  
   - Gå till **Extensions** (Ctrl+Shift+X)  
   - Sök efter *PlantUML* och installera det  

2. **Installera GraphViz** *(krävs för lokal rendering)*  
   - Ladda ner och installera **GraphViz** från:  
     👉 [https://graphviz.gitlab.io/download/](https://graphviz.gitlab.io/download/)  

3. **Konfigurera PlantUML-server i VSCode**  
   - Öppna **Inställningar** (Ctrl+Shift+P → `Preferences: Open Settings (JSON)`)  
   - Lägg till följande rader i `settings.json`:  
     ```json
     {
       "plantuml.server": "http://www.plantuml.com/plantuml",
       "plantuml.render": "PlantUMLServer"
     }
     ```
   - Spara filen och starta om VSCode  

---

### 🚀 Hur man skapar och förhandsgranskar PlantUML-diagram  

#### **1. Skapa en `.puml`-fil**  
- Skapa en ny fil i VSCode, t.ex. **TransporterDemo.puml**  
- Lägg till följande PlantUML-kod:  

  ```plantuml
  @startuml TransporterDemo
  interface ITransport {
    +GetTravelTime()
  }

  class Bike {
    +GetTravelTime()
  }

  class Bus {
    +GetTravelTime()
  }

  class Car {
    +GetTravelTime()
  }

  class TransportFactory {
    +CreateTransport(string transportType): ITransport
  }

  ITransport <|.. Bike
  ITransport <|.. Bus
  ITransport <|.. Car
  TransportFactory ..> ITransport : "Creates"
  @enduml
  ```

#### **2. Förhandsgranska diagrammet**  
- Öppna `.puml`-filen  
- Tryck **Alt + D** *(snabbförhandsgranskning i PlantUML)*  
- Eller högerklicka och välj **"Preview Current Diagram"**  

#### **3. Exportera diagram**  
För att spara diagrammet som en bild, kör:  
```sh
plantuml -tpng TransporterDemo.puml
```
Detta genererar en bild `TransporterDemo.png`.

---

### 📌 Vad diagrammet visar  

- **`ITransport`** är ett **gränssnitt** med metoden `GetTravelTime()`  
- **`Bike`**, **`Bus`** och **`Car`** **implementerar** `ITransport`  
- **`TransportFactory`** har en metod `CreateTransport()` som **skapar** objekt av typen `ITransport`  

---

### ❓ Vanliga problem & lösningar  

**Problem:** Förhandsgranskning i VSCode visar bara en svart ruta.  
✅ **Lösning:** Se till att `"plantuml.server"` är korrekt inställd i `settings.json`.  

**Problem:** Ingen PlantUML-server hittad.  
✅ **Lösning:** Installera GraphViz och starta om VSCode.  

**Problem:** Diagrammet genereras inte.  
✅ **Lösning:** Kontrollera att `.puml`-filen är korrekt skriven och att PlantUML-tillägget är installerat.