# Wulkanizacja.Desktop

## Opis
Wulkanizacja Desktop to aplikacja WPF stworzona w .NET 8, która umożliwia zarządzanie oponami w warsztacie wulkanizacyjnym. Aplikacja pozwala na dodawanie, edytowanie, usuwanie oraz wyszukiwanie opon.

## Funkcje
- Dodawanie nowych opon
- Edytowanie istniejących opon
- Usuwanie opon
- Wyszukiwanie opon według rozmiaru i typu
- Wyświetlanie informacji o oponach
- Obsługa dialogów informacyjnych, błędów i pytań
- BusyIndicator do wskazywania stanu ładowania

## Wymagania
- .NET 8 SDK
- Visual Studio 2022 lub inne

## Konfiguracja
W App.xaml.cs ustawiamy odpowiedni adres Wulkanizacja.Service

            var client = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:5884")
            };
            

## Uruchomienie
1. Sklonuj repozytorium.
2. Otwórz projekt w Visual Studio 2022 lub inne.
3. Przywróć pakiety NuGet.
4. Uruchom aplikację.

## Użycie
1. Uruchom Wulkanizacja.Service.
1. Uruchom aplikację.
2. Kliknij zaloguj (logowanie jest poglądowe narazie nic tam nie ma).
3. W kolejnym okienku masz przyciski Dodaj, edytuj, usuwaj i wyszukuj opony według potrzeb (rozmiar sie podaje w stylu 185/55 R16).

## Architektura
- **MVVM (Model-View-ViewModel)**: Aplikacja korzysta z wzorca MVVM, aby oddzielić logikę biznesową od interfejsu użytkownika.
- **Usługi**: `DialogService`, `WebServiceClient`, `TireRepository` obsługują logikę komunikacji i zarządzania danymi.
- **Widoki**: `MainWindow`, `GeneralView`, `AddTireDialog`, `EditTireDialog` itp.
- **ViewModel**: `GeneralViewModel`, `AddTireDialogViewModel`, `EditTireDialogViewModel` itp.
- **Modele**: `TireModel`, `ErrorResponse` itp.
- **Konwertery**: `BooleanToVisibilityConverter`, `TireTypeToLocalizedStringConverter`, `WeekYearToDateConverter`.

## Przykłady
### Dodawanie nowej opony
1. Kliknij przycisk "Dodaj".
2. Wypełnij formularz dodawania opony.
3. Kliknij "Dodaj", aby zapisać nową oponę.

### Wyszukiwanie opon
1. Wprowadź rozmiar opony w polu wyszukiwania.
2. Wybierz typ opony z listy rozwijanej.
3. Kliknij "Szukaj", aby wyświetlić wyniki.

## Autorzy
- [Aleksander Żak]
