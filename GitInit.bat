@echo off
chcp 65001 >nul
:menu
cls
echo ==============================
echo   🚀 F15 - Git Menu v3.2 🚀
echo ==============================
echo 1) git status
echo 2) git pull --rebase origin main
echo 3) git add . + commit + push
echo 4) git log --oneline --graph --decorate --all
echo 5) git push origin main --force
echo 6) git reset --hard HEAD
echo ------------------------------
echo 7) git branch (ver ramas)
echo 8) git checkout nombre-rama (cambiar de rama)
echo 9) git checkout -b nombre-rama origin/main (crear rama desde main remota)
echo 10) git push origin --delete nombre-rama (borrar rama en GitHub)
echo ------------------------------
echo 0) Salir
echo ==============================
set /p opcion="Selecciona una opcion: "

:: Verde = 0A | Amarillo = 0E | Rojo = 0C
if "%opcion%"=="1" (
    color 0E
    git status
    pause
    goto menu
)

if "%opcion%"=="2" (
    color 0E
    git pull --rebase origin main
    if %ERRORLEVEL% EQU 0 (
        color 0A
        echo ✅ Pull con rebase completado F15
    ) else (
        color 0C
        echo ❌ Error en pull con rebase
    )
    pause
    goto menu
)

if "%opcion%"=="3" (
    color 0E
    git add .

    REM Verifica si hay cambios staged o unstaged
    git diff --cached --quiet
    set staged=%ERRORLEVEL%
    git diff --quiet
    set unstaged=%ERRORLEVEL%

    if %staged% NEQ 0 (
        set haycambios=1
    ) else if %unstaged% NEQ 0 (
        set haycambios=1
    ) else (
        set haycambios=0
    )

    if %haycambios%==1 (
        set /p msg="Escribe el mensaje del commit (o deja vacío para automático): "
        if "%msg%"=="" (
            set msg=Commit automatico desde BAT F15
        )
        git commit -m "%msg%"
        if %ERRORLEVEL% EQU 0 (
            call :dopush
        ) else (
            color 0C
            echo ❌ Error al hacer commit
            pause
            goto menu
        )
    ) else (
        color 0A
        echo ✅ Todo al día F15 😎
        pause
        goto menu
    )
)

if "%opcion%"=="4" (
    color 0E
    git log --oneline --graph --decorate --all
    pause
    goto menu
)

if "%opcion%"=="5" (
    color 0E
    git push origin main --force
    if %ERRORLEVEL% EQU 0 (
        color 0A
        echo ⚡ Push forzado completado
    ) else (
        color 0C
        echo ❌ Error en push forzado
    )
    pause
    goto menu
)

if "%opcion%"=="6" (
    color 0C
    git reset --hard HEAD
    color 0A
    echo 🔄 Reset hecho (HEAD limpio)
    pause
    goto menu
)

if "%opcion%"=="7" (
    color 0E
    git branch
    pause
    goto menu
)

if "%opcion%"=="8" (
    set /p rama="Nombre de la rama a cambiar: "
    git checkout %rama%
    if %ERRORLEVEL% EQU 0 (
        color 0A
        echo ✅ Cambiaste a la rama %rama%
    ) else (
        color 0C
        echo ❌ No se pudo cambiar de rama
    )
    pause
    goto menu
)

if "%opcion%"=="9" (
    set /p rama="Nombre de la nueva rama: "
    git checkout -b %rama% origin/main
    if %ERRORLEVEL% EQU 0 (
        color 0A
        echo ✅ Rama %rama% creada desde main
    ) else (
        color 0C
        echo ❌ No se pudo crear la rama
    )
    pause
    goto menu
)

if "%opcion%"=="10" (
    set /p rama="Nombre de la rama a borrar en GitHub: "
    git push origin --delete %rama%
    if %ERRORLEVEL% EQU 0 (
        color 0A
        echo 🗑️ Rama %rama% borrada en GitHub
    ) else (
        color 0C
        echo ❌ No se pudo borrar la rama
    )
    pause
    goto menu
)

if "%opcion%"=="0" exit

goto menu

:dopush
git push origin main
if %ERRORLEVEL% EQU 0 (
    color 0A
    echo ✅ Commit y push realizados con éxito 🚀
) else (
    color 0C
    echo ❌ Error al hacer push
)
pause
goto menu
