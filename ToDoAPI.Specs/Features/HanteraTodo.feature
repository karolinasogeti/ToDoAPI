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