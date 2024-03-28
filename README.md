# Projekt inżynierski

## Autorzy

- Marcin Larek
- Konrad Furmaniak
- Jakub Kuczko

- Promotor: dr Paweł Płaczek

## Temat projektu

Projekt turowej gry action rpg

## Opis projektu

Efektem projektu jest w pełni sprawna gra RPG stworzona na silniku Unity w wersji 2021.3.20f1. Głównym aspektem naszego projektu jest proceduralne generowanie najważniejszych elementów gry takich jak mapa eksploracji terenu misji, rekruci/członkowie załogi, ekwipunek, zdarzenia losowe, misje. Rozgrywka polega na podejmowaniu się wcześniej zrekrutowanymi członkami drużyny różnego rodzaju misji mających na celu przemierzanie losowo generowanego poziomu w celu odnalezienia finałowego pomieszczenia i pokonaniu znajdującego się tam “bossa”. Za każdą wykonaną misję dostajemy punkty doświadczenia oraz będące walutą w grze kredyty. Obie te rzeczy możemy użyć do rozwijania zarówno naszych członków załogi jak i naszego statku co wiąże się z otrzymaniem różnego rodzaju bonusów.

### Menu główne
![alt text](https://github.com/MarcinLarek/Praca-Inzynierska/blob/master/Screens/image6.png "Screen1")
![alt text](https://github.com/MarcinLarek/Praca-Inzynierska/blob/master/Screens/image12.png "Screen2")
Scena menu głównego pozwala nam na wybranie jednej z trzech opcji. Przycisk Play odpowiada za uruchomienie właściwej rozgrywki. Przycisk Quit kończy działanie naszej aplikacji, natomiast pod przyciskiem Options kryją się  ustawienia gry, gdzie możemy zmienić rozdzielczość oraz głośność dźwięków aplikacji oraz przełączyć tryb pełnoekranowy.

### Main Hub
![alt text](https://github.com/MarcinLarek/Praca-Inzynierska/blob/master/Screens/image11.png "Screen3")
Po uruchomieniu rozgrywki wita nas ekran głównego hub-a rozgrywki. Mamy tutaj dostęp do głównego interfejsu gry umożliwiającego nam takie rzeczy jak:
- Swobodne zapisywanie/wczytywanie stanu rozgrywki.
- Dane przechowywane są na urządzeniu użytkownika w postaci pliku json.
- Powrót do menu głównego oraz sceny głównego hub-a
- Lewy panel: Podgląd informacji o ilości naszych kredytów, ilości zrekrutowanych członków załogi oraz ilości członków załogi przydzielonych do następnej wykonywanej przez nas misji.
- Środkowy panel: Wyświetlanie szczegółowych informacji (takich jak np. informacji o ekwipunku, atrybutach postaci) wykorzystywany głównie w pozostałych scenach.
- Prawy panel: System ekwipunku wyświetlający zdobyte przez nas w trakcie gry przedmioty.
Nad interfejsem głównym możemy zobaczyć przyciski pozwalające nam przenieść się do pozostałych obszarów gry.

### Ulepszenia statku
![alt text](https://github.com/MarcinLarek/Praca-Inzynierska/blob/master/Screens/image1.png "Screen4")
Przycisk Ship Upgrade przenosi nas do sceny umożliwiającej nam wykupienie ulepszeń statku dających nam pasywne globalne bonusy takie jak zwiększona ilość dostępnych rekrutów/załogi, możliwość zabrania na misję większej ilości bohaterów czy też generowanie nowych rekrutów z ulepszonymi statystykami. Wszystkie ulepszenia należy wykupywać z kolejności porządkowej (Nie możemy zakupić ulepszenia poziomu trzeciego jeśli poprzednie dwa nie zostały już zakupione).

### Wybór misji
![alt text](https://github.com/MarcinLarek/Praca-Inzynierska/blob/master/Screens/image10.png "Screen5")
Przycisk select mission przenosi nas do sceny generującej nam od 1 do 5 losowych, możliwych do wyboru misji. Różnią się one potencjalnym wynagrodzeniem oraz sposobem generowania się mapy. Aby rozpocząć misję musimy mieć jednak przypisaną co najmniej jedną postać do aktywnej drużyny.

### Ulepszanie załogi
![alt text](https://github.com/MarcinLarek/Praca-Inzynierska/blob/master/Screens/image2.png "Screen6")
Przycisk upgrade crew zaprowadzi nas do sceny umożliwiającej nam zarządzanie naszą zrekrutowaną załogą. Po lewej w przewijanej liście wyświetlają się nam wszyscy nasi zrekrutowani członkowie drużyny. Kliknięcie na któryś z portretów wyświetla nam dane wybranej postaci, co pozwala na jej rozwijanie i edytowanie. Po lewej stronie od portretu postaci widzimy jej wyposażenie. Elementy ekwipunku możemy zdejmować zakładać przeciągając je pomiędzy wyposażeniem postaci a ekwipunkiem gracza w prawym dolnym rogu.  W prawym górnym panelu widzimy statystyki naszej postaci oraz zdobyte doświadczenie które możemy wykorzystać na rozwijanie jej cech.
Warto zauważyć że w środkowym panelu głównego interfejsu wyświetlają się dodatkowe informacje zawierające opis klasy i zdolności danej postaci. Dodatkowo wyświetlane są tam dodatkowe informacje o statystykach po najechaniu kursorem myszki na odpowiednią statystykę w prawym górnym panelu. Po rozwinięciu postaci i zedytowaniu jej ekwipunku zatwierdzamy wszelkie zmiany przyciskiem Accpet Changes.

### System Handlu
![alt text](https://github.com/MarcinLarek/Praca-Inzynierska/blob/master/Screens/image7.png "Screen7")
Przycisk Trade przenosi nas do sceny handlu. Jego działanie wzorowane jest na systemie barteru znanego z takich gier jak “Baldur’s Gate 3” lub “Divinity: Original Sin”.  Do środkowego panelu ekwipunku przeciągamy przedmioty które chcemy kupić znajdujące się w ekwipunku sprzedawcy znajdującego się po prawej stronie. Analogicznie robimy tak samo z przedmiotami z naszego ekwipunku po lewej stronie które chcemy sprzedać. Gra następnie wylicza wartość przedmiotów znajdujących się w koszyku. Po naciśnięciu przycisku Accept Transaction sprzedawane przedmioty przechodzą do ekwipunku sprzedawcy, a zakupione do naszego. Następnie suma transakcji jest odejmowana lub dodawana do naszego konta w zależności od jej wartości.
Ilość i rodzaj przedmiotów dostępnych u handlarza jest losowo generowana i zmienia się pomiędzy misjami.

### Zarządzanie drużyną
![alt text](https://github.com/MarcinLarek/Praca-Inzynierska/blob/master/Screens/image3.png "Screen8")
Przycisk Crew Management przenosi nas do panelu rekrutacji i zarządzania drużyną. Każdorazowo pomiędzy misjami generuje się lista dostępnych do rekrutacji bohaterów. Ich ilość jak i losowo generowane statystyki zależą od wykupionych przez nas ulepszeń statku. Gwarantując że mamy dostępne miejsce na kolejnego członka załogi i możemy zapłacić cenę rekrutacji, obliczaną na podstawie jego statystyk możemy zrekrutować danego bohatera do naszej załogi. Po rekrutacji możemy przypisać naszych członków załogi do drużyny wybierającej się na następną misję. Po przypisaniu, kolor ich portretów zmieni się na zielony.

### Mapa eksploracji
![alt text](https://github.com/MarcinLarek/Praca-Inzynierska/blob/master/Screens/image5.png "Screen9")
![alt text](https://github.com/MarcinLarek/Praca-Inzynierska/blob/master/Screens/image9.png "Screen10")
![alt text](https://github.com/MarcinLarek/Praca-Inzynierska/blob/master/Screens/image8.png "Screen11")
Do mapy eksploracji przenosimy się  po wybraniu interesującej nas misji. Jest ona każdorazowo generowana losowo na podstawie przypisanych do generatora prefabrykatów pomieszczeń. Po wygenerowaniu mapy wybierane jest ostatnie stworzone pomieszczenie i zamieniane jest ono na pomieszczenie zawierające cel misji - walkę z bossem. Następnie w pozostałych pomieszczeniach generowana jest ich zawartość. Każde pomieszczenie może okazać się puste lub zawierać zdarzenie losowe takie jak znalezienie możliwego do sprzedania sprzętu czy też zawierać losową ilość przeciwników którzy zainicjują walkę z drużyną gracza.
Obecnie, w zależności od rodzaju misji dostępne są trzy metody generowania mapy eksploracji:
- Mine Tunnels: Generator skupia się w tym przypadku na generowaniu prostych krzyżujących się korytarzy. Powstała w ten sposób mapa jest zazwyczaj średniego rozmiaru i stosunkowo łatwa w nawigacji.  
- Shipwreck: Maga generuje się głównie przy użyciu zakręcanych pomieszczeń. Nie ujrzymy tutaj długich prostych korytarzy, a sama mapa jest zazwyczaj niewielka zamykając się w okolicach 10 pomieszczeń.
- Scavenger Base: Ten tryb generacji mapy łączy powyższe tryby w jeden. Odwodnienia długich korytarzy mogą tworzyć duże, kręte i skomplikowane układy pomieszczeń. Wybierając misję z tym trybem generowania mapy możemy liczyć na największą nieprzewidywalność oraz na możliwie największe rozmiary mapy.

### Scena walki
![alt text](https://github.com/MarcinLarek/Praca-Inzynierska/blob/master/Screens/image4.png "Screen12")
Wchodząc do zdarzenia zawierającego walkę, lub do finalnego pomieszczenia z bossem przenosimy się do sceny walki. Ilość wygenerowanych przeciwników waha się od 1 do 5. Walka działa w trybie turowym a każdą turę zaczynają postacie gracza. Podczas swojej tury każda z postaci ma ograniczoną ilość punktów akcji (AP) a każda czynność opłacona jest kosztem punktów akcji. Aktywna obecnie postać zaznaczona jest zielonym znacznikiem. Klikając lewym przyciskiem myszy na dowolną inną postać wybieramy cel naszej akcji, który podświetla się niebieskim znacznikiem. Gracz w swojej turze może użyć następujących akcji:
- Atak podstawowy, zadający obrażenia wybranemu przeciwnikowi. Obrażenia zależne są od posiadanej broni oraz pancerza przeciwnika.
- Użycie przedmiotu leczącego, regenerującego zdrowie naszej postaci. Warunkiem wykorzystania tej akcji jest wcześniejsze wyposażenie postaci w przedmiot leczący
- Transfer punktów akcji pozwalający nam przenieść jeden z naszych punktów do innej postaci. Koszt przeniesienia jednego punktu wynosi dwa punkty akcji. Przydatne jeżeli jedna z naszych postaci posiada lepsze statystyki, uzbrojenie lub zdolności.
- Wykorzystanie jednej z trzech specjalnych zdolności danej klasy. Zdolności specjalne są diametralnie zróżnicowane a ich pole działania sięga od zadawania specjalnych obrażeń poprzez leczenie do specjalnego wspierania pozostałych członków drużyny. Wysoka użyteczność zdolności specjalnych często opłacona jest dużym kosztem użycia.
Sama walka opiera się głównie na umiejętnościach  naszych postaci. Przykładowo atakując wykonujemy przeciwstawny test umiejętności z naszym celem taki wyglądający następująco:
Atakujący:
Umiejętność AGILITY + Losowa wartość 1-10 + Celność dobytej broni  
Obrońca:
Umiejętność AGILITY + Losowa wartość 1-10
Osoba z wyższym wynikiem wygrywa test i na tej podstawie decydowane jest czy cel otrzymał obrażenia. W podobny sposób  wygląda np test obrażeń krytycznych w którym testowana jest umiejętność szczęścia naszej postaci. Pozostałe atrybuty postaci wykorzystywane są np. przy obliczaniu negacji obrażeń lub przy niektórych umiejętnościach specjalnych klasy.

Efektem końcowym projektu jest w pełni funkcjonalna gra, stworzona w taki sposób aby z łatwością można było ją rozbudowywać i zapewniać dodatkowe późniejsze wsparcie. Postawienie nacisku na losowy aspekt gry zapewnia nam brak wymogu przebudowywania i zmieniania systemów jeśli chcemy wprowadzić do nich nową zawartość.
