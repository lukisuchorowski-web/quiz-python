@echo off
chcp 65001 > nul
title Szkolny Quiz Wiedzy
echo Uruchamianie aplikacji Szkolny Quiz Wiedzy (Python)...
where py >nul 2>nul
if %ERRORLEVEL% equ 0 (
    py quiz.py
    exit /b
)
where python >nul 2>nul
if %ERRORLEVEL% equ 0 (
    python quiz.py
    exit /b
)

echo.
echo [BLAD] Nie znaleziono zainstalowanego srodowiska Python!
echo Mozesz zamiast tego uruchomic wersje C#: uruchom_quiz_cs.bat
echo lub wersje przegladarkowa: quiz_standalone.html
echo.
pause
