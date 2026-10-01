# 🎓 Szkolny Quiz Wiedzy - Aplikacja Desktopowa

Nowoczesna aplikacja desktopowa zawierająca quiz z 5 pytaniami ze szkolnych przedmiotów wraz z podsumowaniem wyników.

Aplikacja jest dostępna w **3 językach programowania** do wyboru:
1. 🟦 **C# (.NET / Windows Forms)** – natywna dla systemu Windows (nie wymaga instalowania dodatkowych środowisk!).
2. ☕ **Java (Swing)** – klasyczna aplikacja okienkowa.
3. 🐍 **Python (Tkinter)** – nowoczesna wersja skryptowa.
4. 🌐 **HTML5 / JS** – lokalna wersja przeglądarkowa.

---

## 📋 Baza pytań i losowanie:

Aplikacja posiada rozbudowaną **bazę 18 pytań** ze szkolnych przedmiotów:
- 🌍 **Geografia** (np. stolica Australii, najdłuższa rzeka w Polsce, pustynia Sahara)
- ⚔️ **Historia** (np. bitwa pod Grunwaldem, chrzest Polski, pierwszy król Polski)
- 🧬 **Biologia** (np. produkcja insuliny, jednostka komórkowa, chlorofil i fotosynteza)
- ⚗️ **Chemia** (np. symbol złota, pH=7, wzór wody)
- 📐 **Matematyka** (np. kolejność działań, suma kątów w trójkącie, pierwiastek ze 144)
- ⚡ **Fizyka** (jednostka siły w układzie SI)
- 💻 **Informatyka** (liczba bitów w bajcie)
- 📚 **Język Polski** (autor „Pana Tadeusza”)

**Za każdym uruchomieniem aplikacji oraz po kliknięciu "Nowy quiz" losowane jest 5 unikalnych pytań**, dzięki czemu quiz nigdy nie jest taki sam!

---

## 🚀 Jak uruchomić (wybierz wersję):

### 1️⃣ Wersja C# (.NET) - [QuizApp.cs](file:///c:/Users/%C5%81ukasz%20Suchorowski/Documents/Nowy%20folder/QuizApp.cs):
- **Dwuklik:** Uruchom **`uruchom_quiz_cs.bat`** (automatycznie kompiluje do pliku `.exe` za pomocą systemowego kompilatora `csc.exe` i włącza aplikację!).

### 2️⃣ Wersja Java (Swing) - [QuizApp.java](file:///c:/Users/%C5%81ukasz%20Suchorowski/Documents/Nowy%20folder/QuizApp.java):
- **Dwuklik:** Uruchom **`uruchom_quiz_java.bat`**.
- **Terminal:**
  ```bash
  javac -encoding UTF-8 QuizApp.java
  java QuizApp
  ```

### 3️⃣ Wersja Python (Tkinter) - [quiz.py](file:///c:/Users/%C5%81ukasz%20Suchorowski/Documents/Nowy%20folder/quiz.py):
- **Dwuklik:** Uruchom **`uruchom_quiz.bat`**.
- **Terminal:**
  ```bash
  python quiz.py
  ```

### 4️⃣ Wersja Webowa - [quiz_standalone.html](file:///c:/Users/%C5%81ukasz%20Suchorowski/Documents/Nowy%20folder/quiz_standalone.html):
- Kliknij dwukrotnie plik `quiz_standalone.html`, aby otworzyć quiz w dowolnej przeglądarce internetowej.

---

## ✨ Funkcje aplikacji:
- **Losowanie pytań**: Za każdym podejściem quiz wybiera 5 losowych pytań z bazy 18 szkolnych zagadnień.
- **Nowoczesny interfejs graficzny**: Styl Dark Slate & Indigo z kartami i czytelną typografią.
- **Dynamiczny wskaźnik postępu**: Pokazuje aktualny numer pytania (np. 1 z 5).
- **Interaktywne odpowiedzi**: Podświetlanie po najechaniu myszką, zaznaczanie wyboru.
- **Walidacja**: Blokada przejścia do kolejnego pytania bez zaznaczenia odpowiedzi.
- **Szczegółowy ekran końcowy**:
  - Wyświetlenie dokładnej liczby punktów (np. `4 / 5`) oraz procentu (`80%`).
  - Odznaka słowna zależna od wyniku (np. *Perfekcyjnie!*, *Bardzo dobrze!*).
  - Zestawienie wszystkich 5 pytań ze wskazaniem Twojej odpowiedzi oraz poprawnej odpowiedzi.
  - Przycisk **"Nowy quiz (losuj inne pytania)"** umożliwiający natychmiastowe wylosowanie nowego zestawu pytań.
