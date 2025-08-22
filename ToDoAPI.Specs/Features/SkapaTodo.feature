#language: sv
Egenskap: Skapa todos

  Som användare vill jag kunna skapa mina todos så att jag kan hålla koll på vad som ska göras

Bakgrund: Givet att todo-listan är tom
# Detta är en bakgrund som körs innan varje scenario i denna egenskap

Scenario: Visa tom todo-lista
	När jag öppnar todo-appen
	Så ska jag se att listan är tom

Scenario: Lägga till en ny todo
	När jag lägger till en ny todo med titeln "Handla mjölk"
	Så ska todo-listan innehålla en todo med titeln "Handla mjölk"

Scenariomall: Lägga till en ny todo med exempel
	När jag lägger till en ny todo med titeln <titel>
	Så ska todo-listan innehålla en todo med titeln <titel>

Exempel:
	| titel          |
	| "Handla mjölk" |
	| "Laga mat"     |
	| "Städa"        |

Scenario: Lägga till flera todos
	När jag lägger till följande todos
		| titel         |
		| Handla mjölk  |
		| Laga mat      |
		| Städa         |
	Så ska todo-listan innehålla 3 todos

Scenario: Lägga till en todo med detaljer
	När jag lägger till en ny todo med titeln "Handla mjölk", beskrivningen "Köp mjölk från affären" och förfallodatumet "2025-10-01"
	Så ska todo-listan innehålla en todo med titeln "Handla mjölk", beskrivningen "Köp mjölk från affären" och förfallodatumet "2025-10-01"	

Scenario: Lägga till en todo med detaljer (enklare att läsa)
	När jag lägger till en ny todo med 
		| titel 		| beskrivning               | förfallodatum |
		| Handla mjölk	| Köp mjölk från affären 	| 2025-10-01  	|
	Så ska todo-listan innehålla en todo med 
		| titel 		| beskrivning               | förfallodatum |
		| Handla mjölk	| Köp mjölk från affären 	| 2025-10-01  	|

Scenario: Lägga till en todo med passerat datum
	När jag lägger till en ny todo med titeln "Handla mjölk", beskrivningen "Köp mjölk från affären" och förfallodatumet "2022-01-01"
	Så ska jag få felmeddelandet "Förfallodatumet kan inte vara i det förflutna"

Scenario: Lägga till en todo med felaktigt datumformat
	När jag lägger till en ny todo med titeln "Handla mjölk", beskrivningen "Köp mjölk från affären" och förfallodatumet "2023-31-12"
	Så ska jag få felmeddelandet "Datumformatet är ogiltigt"
