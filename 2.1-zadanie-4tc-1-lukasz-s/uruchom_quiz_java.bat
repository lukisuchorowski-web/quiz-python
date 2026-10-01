@echo off
chcp 65001 > nul
title Szkolny Quiz Wiedzy - Java Swing
echo Uruchamianie aplikacji Szkolny Quiz Wiedzy w Javie...

where java >nul 2>nul
if %ERRORLEVEL% neq 0 (
    echo.
    echo [BLAD] Nie znaleziono zainstalowanego srodowiska Java (JDK/JRE)!
    echo Mozesz uruchomic wersje C#: uruchom_quiz_cs.bat
    echo lub wersje przegladarkowa: quiz_standalone.html
    echo.
    pause
    exit /b
)

where javac >nul 2>nul
if %ERRORLEVEL% equ 0 (
    echo Kompilacja QuizApp.java...
    javac -encoding UTF-8 QuizApp.java
    if %ERRORLEVEL% equ 0 (
        java QuizApp
        exit /b
    )
)

echo Uruchamianie za pomoca java QuizApp.java...
java QuizApp.java
if %ERRORLEVEL% neq 0 (
    pause
)
