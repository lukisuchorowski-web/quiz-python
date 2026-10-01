@echo off
chcp 65001 > nul
title Szkolny Quiz Wiedzy - C# Windows Forms

set CSC=C:\Windows\Microsoft.NET\Framework64\v4.0.30319\csc.exe
if not exist "%CSC%" (
    set CSC=C:\Windows\Microsoft.NET\Framework\v4.0.30319\csc.exe
)

if exist "QuizApp.exe" (
    echo Uruchamianie QuizApp.exe...
    start "" "QuizApp.exe"
    exit /b
)

if exist "%CSC%" (
    echo Kompilacja QuizApp.cs za pomoca wbudowanego kompilatora C# (.NET)...
    "%CSC%" /nologo /target:winexe /out:QuizApp.exe QuizApp.cs
    if %ERRORLEVEL% equ 0 (
        echo Sukces! Uruchamianie QuizApp.exe...
        start "" "QuizApp.exe"
        exit /b
    )
)

where dotnet >nul 2>nul
if %ERRORLEVEL% equ 0 (
    echo Uruchamianie przez .NET SDK...
    dotnet run
    exit /b
)

echo [BLAD] Nie znaleziono kompilatora csc.exe.
pause
