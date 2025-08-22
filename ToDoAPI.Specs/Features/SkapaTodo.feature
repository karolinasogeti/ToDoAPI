#language: sv
Egenskap: Skapa todos

  Som användare vill jag kunna skapa mina todos så att jag kan hålla koll på vad som ska göras

# Detta är en bakgrund som körs innan varje scenario i denna egenskap
Bakgrund: 
	Givet att todo-listan är tom

Scenario: Visa tom todo-lista
	När jag öppnar todo-appen
	Så ska jag se att listan är tom

Scenario: Lägga till en ny todo
	När jag lägger till en ny todo med titeln "Handla mjölk"
	Så ska todo-listan innehålla en todo med titeln "Handla mjölk"

# Scenario med exempel används när man vill köra ett test flera gånger med olika data, kan också kallas Scenariomall
Scenario: Lägga till en ny todo med exempel
	När jag lägger till en ny todo med titeln <titel>
	Så ska todo-listan innehålla en todo med titeln <titel>

Exempel:
	| titel          |
	| "Handla mjölk" |
	| "Laga mat"     |
	| "Städa"        |

# Scenario med tabell används när man vill skicka in mycket data och öka läsbarheten
Scenario: Lägga till flera todos
	När jag lägger till följande todos
		| titel        |
		| Handla mjölk |
		| Laga mat     |
		| Städa        |
	Så ska todo-listan innehålla 3 todos




