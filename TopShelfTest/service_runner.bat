@echo off
service install
echo Service is installed, anykey to start...
pause
service start
echo Service is started, anykey to stop and uninatall....
pause
service stop
service uninstall
echo Work complete, anykey to exit.
pause
