
//Detta interface är ett exempel på Invarians. 
//Vi använder detta interface med två, ena genom en "Get" som enbart returnerar en lista av, medans vi sedan har en void som visar items från listan
//Get-metoden är en covariant-metod medans ShowItems kan argumenteras för vara en contravariant metod. 
//Eftersom vi har en covariant och en contravariant kommer det således leda till att interfacet är invariant. 
//Interfacet används för constraints för profilklasser under som visar antingen ett inventory med dina varor eller vilka planeter du besökt och upptäckt

//I och med detta interface använder vi oss också av generic supertypes. 
//Vi använder typ-parametern <T> för att hålla interfacet "öppet" och där vi har möjligheten att "stänga" subklasserna med den typ som de implementerar.
// Detta möjliggör att vi kan använda oss av olika typer av subklasserna. Vi kan också i framtiden på ett enkelt sätt lägga till fler subklasser som innehar andra olika typer från de som redan implementerats
//Exempel på detta om vi introducerar en klass med "keys", som kan användas i olika pussel på planeten. Vi kan då väldigt enkelt implementera en klass som visar vilka nycklar som spelaren har. 

public interface IProfile<T>
{
    List<T> GetItems();
    void ShowItems() { }
}