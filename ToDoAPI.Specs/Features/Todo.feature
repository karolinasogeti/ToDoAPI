#language: sv
Egenskap: Hantera todos
  
  Som användare vill jag kunna hantera mina todos så att jag kan hålla koll på vad som ska göras

Scenario: Visa tom todo-lista
	Givet att todo-listan är tom
	När jag öppnar todo-appen
	Så ska jag se att listan är tom

Scenario: Lägga till en ny todo
	Givet att todo-listan är tom
	När jag lägger till en ny todo med titeln "Handla mjölk"
	Så ska todo-listan innehålla en todo med titeln "Handla mjölk"

Scenariomall: Lägga till en ny todo med exempel
	Givet att todo-listan är tom
	När jag lägger till en ny todo med titeln <titel>
	Så ska todo-listan innehålla en todo med titeln <titel>

Exempel:
	| titel          |
	| "Handla mjölk" |
	| "Laga mat"     |
	| "Städa"        |

Scenario: Markera en todo som klar
	Givet att todo-listan innehåller en todo med titeln "Handla mjölk" som inte är klar
	När jag markerar todo:n "Handla mjölk" som klar
	Så ska todo:n "Handla mjölk" vara markerad som klar

Scenario: Ta bort en todo
	Givet att todo-listan innehåller en todo med titeln "Handla mjölk"
	När jag tar bort todo:n "Handla mjölk"
	Så ska todo-listan vara tom

Scenario: Lägga till flera todos
	Givet att todo-listan är tom
	När jag lägger till följande todos
		| titel         |
		| Handla mjölk  |
		| Laga mat      |
		| Städa         |
	Så ska todo-listan innehålla 3 todos
