
@echo off
"C:\Program Files\Unity\Hub\Editor\2022.1.18f1\Editor\Data\PlaybackEngines\WebGLSupport\BuildTools\Emscripten\emscripten\emcc.bat" %* < nul
exit %ERRORLEVEL%
