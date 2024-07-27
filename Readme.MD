-- En el caso de que el Firewall Bloquee las entradas de datos para ser consultados desde otra maquina, se debe de crear una nuava regla:
New-NetFirewallRule -DisplayName "Allow ASP.NET Core" -Direction Inbound -Protocol TCP -LocalPort 5000 -Action Allow
