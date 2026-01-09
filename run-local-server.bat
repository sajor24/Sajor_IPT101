@echo off
echo Starting Love Letter App Server...
echo.
echo Your app will be available at:
echo http://localhost:8000
echo.
echo Press Ctrl+C to stop the server
echo.
cd publish-ready
python -m http.server 8000
pause