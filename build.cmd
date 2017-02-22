@echo off
.paket\paket.bootstrapper.exe
if errorlevel 1 (
  exit /b %errorlevel%
)
.paket\paket.exe init
.paket\paket.exe update

if not exist paket.lock (
  .paket\paket.exe install
) else (
  .paket\paket.exe restore
)
if errorlevel 1 (
  exit /b %errorlevel%
)


packages\FSharp.Compiler.Tools\tools\fsi.exe --load:app.fsx