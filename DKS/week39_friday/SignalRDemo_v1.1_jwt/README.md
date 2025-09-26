# SignalRDemo - how to

1. Öppna en terminal, skriv: `dotnet new web -n NameOfYourProject` där **NameOfYourProject** ändras till ditt tänkta projektnamn.
2. I samma terminal, gå till mappen `cd NameOfYourProject` och skriv `dotnet add package Microsoft.AspNet.SignalR` som lägger till SignalR
3. Uppdatera din **Program.cs** samt lägg till en **ChatHub.cs** (se kod i demot för respektive klassfil).
4. Skapa en `wwwroot` mapp under root / och i terminal ändra till den `cd wwwroot`. Följ upp med att skriva `npm create vite@latest` som skapar en mapp åt dig (namnge till förslagsvis `client`).
5. Börja koda din klientdel och när du är klar så öppna en terminal som står i den mappen (alltså ../wwwroot/client) och där skriver du `npm run build`. Den kommer skapa en `dist` mapp åt dig - det innehållet vill du kopiera över till `wwwroot`.
   
  - Din `wwwroot` ska alltså innehålla en `assets`, `client` och `index.html` (assets & client är mappar)
  - **Notis**: Demot innehåller alltså ingen färdig klient eller `dist` mapp (du behöver göra det själv).
  
7. För att köra din applikation, backa till projektmappens root (`C:\Users\yourUser\Repos\SignalRDemo` kanske?) och skriv `dotnet run` - gå därefter till `http://localhost:port` där port är något fyrsiffrigt exempelvis 5173.

