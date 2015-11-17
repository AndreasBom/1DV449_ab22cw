# 1DV449_ab22cw
####Webbteknik II    
     
     
###Finns det några etiska aspekter vid webbskrapning. Kan du hitta något rättsfall?   
Själva skrapningen, anser jag inte ha några problem med etiska aspekter. Det skulle vara samma sak som att fråga sig om det finns några etiska aspekter med att använda en webbrowser. Däremot kan själva användningen av det skrapade innehållet resultera i tveksamma etiska dilemman. Det mest självklara fallet skulle vara om någon skrapar data från en sida och sedan presenterar detta, som att de själva är upphovsmakarna av informationen. Att inte tillåta skrapor, eller webbots på sin sida, och hävda att innehållet lyder under coperight eller license, tycker inte jag är rätt. Essensen med WWW är just att innehållet ska vara lätt tillgängligt, fritt och för alla. Det finns flera rättsfall där webbskrapning har varit kärnpunkten. Tex, Facebook vs Power.com och Ebay vs Biddler's Edge.     

###Finns det några riktlinjer för utvecklare att tänka på om man vill vara "en god skrapare" mot serverägarna?   
University of Washington har upprättat riktlinjer för webbskrapning.     
De allmängilltiga reglerna skulle kunna sammanfattas:   
*Respektera robots.txt   
*Identifiera din skrapa, och tillhandaha en presentationssida där syftet med skrapan klargörs. 
*Stör/manipulera inte med den normala driften av värddatorn/servern.   
   
###Begränsningar i din lösning- vad är generellt och vad är inte generellt i din kod?   
Min lösning har en class (ScrapeAgent) som har flera metoder för att skrapa en angiven url. Parametrarna som metoderna tar in bestämmer vilka taggar eller atribute som skrapan ska leta efter. Lösningen använder sig av mönsterdesignen facade, vilket gör det enkelt att implementera fler "underklasser" (fler än /calendar, /dinner, /cinema) som kopplas samman.
När jag skrapar efter en specifik länk (tex /calendar) så använder jag mig av funktionen .Contains("calendar"). Detta anser jag är mer generellt, än att använda ett specificerat index i en lista (links[1]).
Varje sida som sedan skrapas på information utgörs av en egen klass. Dessa är hårt knutna till hur websidan som ska skrapas, är uppbyggd. Exempelvis så används olika taggar för att hitta rätt data.

###Vad kan robots.txt spela för roll?
I denna filen kan sidägaren lägga till hur man önskar att en skrapa ska agera. Det kan tex vara att man tillåter skrapor på hela sida, eller enbart på vissa sidor. Man kan tillåta specifika skrapor, men inte andra (tex googlebot). 
