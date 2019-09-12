rem svcutill /t:code /language:cs /out:generatedProxy.cs /config:app.config http://localhost:8090/MyService/SimpleCalculator
rem svcutill /language:cs /out:generatedProxy.cs /config:MyCalculatorServiceClient\app.config /mergeConfig http://localhost:8090/MyService/SimpleCalculator
rem "c:\Program Files (x86)\Microsoft SDKs\Windows\v10.0A\bin\NETFX 4.7.2 Tools\svcutil.exe" %1 %2 %3% %4 %5 %6 %7 %8 %9
"c:\Program Files (x86)\Microsoft SDKs\Windows\v10.0A\bin\NETFX 4.7.2 Tools\svcutil.exe" /language:cs /out:%1\generatedProxy.cs /config:%1\app.config /mergeConfig %2
pause