@echo off
dotnet tool update xcodetool -g --prerelease
if errorlevel 1 (
	echo xcode 预览版更新失败，正在安装 .NET 工具...
	dotnet tool install xcodetool -g --prerelease
)

xcode %*