## Task PUXDesign - DiffChecker .NET aplikace
Autor: Tadeáš Kozub

## Popis
Aplikace slouzi k vyhodnocovani snapshotu zadaneho adresare. Nabizi Diffchecker.Library obsahujici
kod pro samotne vytvareni a porovnavani snapshotu. Dale nabizi REST API implementovanou
s pomoci MinimalAPI, diky ktere neni potreba vytvaret API controllery.
FE cast je implementovana jako jednoducha page v ramci Blazor.
Implementace zabarala cca 6 hodin.

## Pouziti
FE cast je dostupna po spusteni DiffChecker.BlazorApp. Pro spusteni MinimalAPI je potreba spustit projekt DiffChecker.WebAPI.

## Omezení
- FE je pouze zakladni, bez pouziti CSS, nebo uzivatelksy privetivejsiho zobrazovani 
(radeji bych v tomto pripade pouzil Typescript, to je vsak mimo scope zadani).

## Mozna vylepseni a dalsi rozvoj
- Sada unit testu pro DiffChecker.Library
- Poradne otestovani prace s Paths na UNIX systemech
- Vymena porovnavani snapshotu za vytvoreni hashe misto porovnani posledniho zapisu do souboru (pomalejsi porovnani, ale zamezi se napriklad pripadu, kdy uzivatel zapsal do souboru a zase obsah smazal)
- Rozsireni FE o moznost zobrazovani detailu snapshotu