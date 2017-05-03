cls
rem call npm i
del temp\*.* /F /Q
call fable
call webpack
call .\rust.cmd
