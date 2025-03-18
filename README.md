# Wulkanizacja.Desktop

## Opis
Wulkanizacja Desktop to aplikacja WPF stworzona w .NET 8, która umożliwia zarządzanie oponami w warsztacie wulkanizacyjnym. Aplikacja pozwala na dodawanie, edytowanie, usuwanie oraz wyszukiwanie opon.

## Funkcje
- Rejestracja użytkownika
- Logowanie
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
W appsettings.json ustawiamy odpowiedni adres dla Wulkanizacja.Service oraz Wulkanizacja.Auth
```json
{
  "ServiceUrls": {
    "Wulkanizacja.Service": "http://localhost:5884",
    "Wulkanizacja.Auth": "http://localhost:5020"
  }
}
```

## Uruchomienie
1. Sklonuj repozytorium.
2. Otwórz projekt w Visual Studio 2022 lub inne.
3. Przywróć pakiety NuGet.
4. Uruchom aplikację.

## Użycie
1. Uruchom Wulkanizacja.Service
2. Uruchom Wulkanizacja.Auth
3. Uruchom aplikację
4. Zarejestruj swojego użytkownika wymaganie na hasło to 1 znak specjalny i duża litera oraz @ w emailu
5. Zaloguj się wcześniej utworzonym użytkownikiem
6. W kolejnym okienku masz przyciski Dodaj, edytuj, usuwaj i wyszukuj opony według potrzeb (rozmiar sie podaje w stylu 185/55 R16).

## Architektura
- **MVVM (Model-View-ViewModel)**: Aplikacja korzysta z wzorca MVVM, aby oddzielić logikę biznesową od interfejsu użytkownika.
- **Usługi**: `DialogService`, `WebServiceClient`, `TireRepository` obsługują logikę komunikacji i zarządzania danymi.
- **Widoki**: `GeneralView`, `LoginView`.
- **ViewModel**: `GeneralViewModel`, `AddTireDialogViewModel`, `EditTireDialogViewModel` itp.
- **Modele**: `TireModel`, `ErrorResponse` itp.
- **Konwertery**: `BooleanToVisibilityConverter`, `TireTypeToLocalizedStringConverter`, `WeekYearToDateConverter`.

## Przykłady
### Rejestracja użytkownika
1. Kliknij przycisk "Zarejestruj".
2. Wypełnij formularz rejestracji.
3. Kliknij "Zarejestruj", aby zarejestrować nowego użytkownika.

### Dodawanie nowej opony
1. Kliknij przycisk "Dodaj".
2. Wypełnij formularz dodawania opony.
3. Kliknij "Dodaj", aby zapisać nową oponę.

### Wyszukiwanie opon
1. Wprowadź rozmiar opony w polu wyszukiwania.
2. Wybierz typ opony z listy rozwijanej.
3. Kliknij "Szukaj", aby wyświetlić wyniki (rozmiar sie podaje w stylu 185/55 R16).

## Autorzy
- [Aleksander Żak]
