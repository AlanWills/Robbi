if "%~1"=="" (
	set PATH="%CD%\Builds\Android\Robbi-%1%.apk"
) else (
    set /p PATH=<"%CD%\Builds\Android\BUILD_LOCATION.txt"
)

echo %PATH%

"C:\Program Files\Unity\Hub\Editor\2020.1.1f1\Editor\Data\PlaybackEngines\AndroidPlayer\SDK\platform-tools\adb.exe" install "%PATH%"