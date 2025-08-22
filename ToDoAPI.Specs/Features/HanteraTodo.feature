#language: sv
Egenskap: Hantera todos

  Som användare vill jag kunna klarmarkera, ta bort och redigera mina todos så min lista på uppgifter är aktuell

Scenario: Markera en todo som klar
	Givet att todo-listan innehåller en todo med titeln "Handla mjölk" som inte är klar
	När jag markerar todo:n "Handla mjölk" som klar
	Så ska todo:n "Handla mjölk" vara markerad som klar

Scenario: Ta bort en todo
	Givet att todo-listan innehåller en todo med titeln "Handla mjölk"
	När jag tar bort todo:n "Handla mjölk"
	Så ska todo-listan vara tom

Scenario: Redigera en todo
    Givet att todo-listan innehåller en todo med titeln "Handla mjölk"
    När jag redigerar todo:n "Handla mjölk" och ändrar titeln till "Köpa mjölk"
    Så ska todo-listan innehålla en todo med titeln "Köpa mjölk"

Scenario: Försöka redigera en todo alla egenskaper
    Givet att todo-listan innehåller en todo med titeln "Handla mjölk"
    När jag försöker redigera todo:n "Handla mjölk" och ändrar alla egenskaper
        | titel          | beskrivning               | förfallodatum |  
        | "Köpa mjölk"   | "Köp mjölk från affären"  | "2025-10-01" |
    Så ska todo-listan innehålla en todo med 
        | titel          | beskrivning               | förfallodatum |  
        | "Köpa mjölk"   | "Köp mjölk från affären"  | "2025-10-01" |

Scenario: Försöka ange en tom titel vid redigering
    Givet att todo-listan innehåller en todo med titeln "Handla mjölk"
    När jag försöker redigera todo:n "Handla mjölk" och anger en tom titel
    Så ska jag få felmeddelandet "Titeln kan inte vara tom"

Scenario: Försöka ange ett felaktigt datumformat vid redigering
    Givet att todo-listan innehåller en todo med titeln "Handla mjölk"
    När jag försöker redigera todo:n "Handla mjölk" och anger ett datumformat <datum>
    Så ska jag få felmeddelandet "Datumformatet är ogiltigt"

    Exempel:
        | datum       |
        | "2023-31-12"|
        | "2023-02-30"|
        | "2023-13-01"|