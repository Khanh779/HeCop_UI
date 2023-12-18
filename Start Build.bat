call "C:\Program Files\Microsoft Visual Studio\2022\Community\Common7\Tools\VsDevCmd.bat"
Msbuild.exe "D:\Data\Tailieu\Projects\C#\HeCop_UI_Tools\HeCop_UI.sln"
copy "D:\Data\Tailieu\Projects\C#\HeCop_UI_Tools\HeCop_UI_Tools\bin\Debug\HecopUI_Winforms.dll" "C:\HecopUI_Winforms.dll"
pause